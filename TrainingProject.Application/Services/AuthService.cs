using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TrainingProject.Application.Resources;
using TrainingProject.Domain.Dto;
using TrainingProject.Domain.Dto.User;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Enum;
using TrainingProject.Domain.Interfaces.Databases;
using TrainingProject.Domain.Interfaces.Repositories;
using TrainingProject.Domain.Interfaces.Services;
using TrainingProject.Domain.Result;

namespace TrainingProject.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper,
        IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService,
        IBaseRepository<Role> roleRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }

        var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
        if (user != null)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.UserAlreadyExists,
                ErrorCode = (int)ErrorCodes.UserAlreadyExists
            };
        }
        var hashUserPassword = HashPassword(dto.Password);

        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                user = new User()
                {
                    Login = dto.Login,
                    Password = hashUserPassword
                };
                await _unitOfWork.Users.CreateAsync(user);

                await _unitOfWork.SaveChangesAsync();

                var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == nameof(Roles.User));
                if (role == null)
                {
                    return new BaseResult<UserDto>()
                    {
                        ErrorMessage = ErrorMessage.RoleNotFound,
                        ErrorCode = (int)ErrorCodes.RoleNotFound
                    };
                }

                UserRole userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };
                await _unitOfWork.UserRoles.CreateAsync(userRole);

                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        return new BaseResult<UserDto>()
        {
            Data = _mapper.Map<UserDto>(user),
        };
    }
    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);
        if (user == null)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        if (!IsVerifyPassword(user.Password, dto.Password))
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.PasswordIsWrong,
                ErrorCode = (int)ErrorCodes.PasswordIsWrong
            };
        }

        var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.Id == user.Id);

        var userRoles = user.Roles;
        var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
        claims.Add(new Claim(ClaimTypes.Name, user.Login));

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        if (userToken == null)
        {
            userToken = new UserToken()
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
            };
            await _userTokenRepository.CreateAsync(userToken);
        }
        else
        {
            userToken.RefreshToken = refreshToken;
            userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            var updatedUserToken = _userTokenRepository.Update(userToken);
        }

        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }

    private bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPassword(userPassword);
        return hash == userPasswordHash;
    }

}
