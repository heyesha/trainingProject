using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrainingProject.Domain.Dto;
using TrainingProject.Domain.Result;

namespace TrainingProject.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}
