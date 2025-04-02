using System.Threading.Tasks;
using TrainingProject.Domain.Dto;
using TrainingProject.Domain.Dto.User;
using TrainingProject.Domain.Result;

namespace TrainingProject.Domain.Interfaces.Services;

/// <summary>
/// Сервис, предназначенный для авторизации/регистрации
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}
