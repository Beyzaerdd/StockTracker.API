using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockTracker.Business.Abstract;
using StockTracker.Business.Configuration;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs;
using StockTracker.Shared.DTOs.EmployeeDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class AuthService :IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfig _jwtConfig;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IOptions<JwtConfig> options, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfig = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDTO<TokenDTO>> LoginAsync(LoginEmployeeDTO loginEmployeeDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginEmployeeDTO.Email);
            if (user == null)
            {
                return ResponseDTO<TokenDTO>.Fail("Böyle bir kullanıcı yok", StatusCodes.Status400BadRequest);
            }
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginEmployeeDTO.Password);
            if (!isValidPassword)
            {
                return ResponseDTO<TokenDTO>.Fail("Hatalı şifre", StatusCodes.Status400BadRequest);
            }
            var tokenDTO = await GenerateJwtToken(user);
            return ResponseDTO<TokenDTO>.Success(tokenDTO, StatusCodes.Status200OK);
        }

        private async Task<TokenDTO> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credential
                );
            var tokenDTO = new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expiry,
            };
            return tokenDTO;
        }
    }
}
