using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace TrainingProject.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();
}
