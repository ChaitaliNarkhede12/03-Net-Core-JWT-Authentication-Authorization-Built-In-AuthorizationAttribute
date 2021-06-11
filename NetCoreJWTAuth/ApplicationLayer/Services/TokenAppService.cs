using ApplicationLayer.Interface;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class TokenAppService : ITokenAppService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserAppService _userAppService;
        private readonly ITokenRepository _tokenRepository;
        public TokenAppService(IOptions<AppSettings> appSettings,
            ITokenRepository tokenRepository,
            IUserAppService userAppService)
        {
            _appSettings = appSettings.Value;
            _tokenRepository = tokenRepository;
            _userAppService = userAppService;
        }

        public async Task<TokenModel> Login(string userName, string password)
        {
            var userList = await _userAppService.GetUserDetailsByExpression(x => x.UserName == userName);
            var user = userList.FirstOrDefault();

            if (user == null)
            {
                throw new Exception("error");
            }

            var token = GetTokenModel(user.UserId);
            DateTime refreshTokenExpirytime = DateTime.UtcNow.AddHours(2);

            var result = await _tokenRepository.CreateRefreshTokenOnLogin(token.refreshToken, refreshTokenExpirytime, user.UserId).ConfigureAwait(false);

            if (result > 0)
            {
                return token;
            }
            return null;
        }

        public async Task<TokenModel> CreateTokenOnRefreshToken(string refreshToken)
        {
            var oldRefreshToken = refreshToken;
            var user = await _tokenRepository.GetUserFromRefreshToken(oldRefreshToken).ConfigureAwait(false);

            if (user != null)
            {
                var token = GetTokenModel(user.UserId);
                DateTime refreshTokenExpiryTime = DateTime.UtcNow.AddHours(2);

                //Update refresh token
                var result = await _tokenRepository.UpdateRefreshTokenOnRefresh(oldRefreshToken, token.refreshToken, refreshTokenExpiryTime).ConfigureAwait(false);

                if (result > 0)
                {
                    return token;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<int> RemoveRefreshToken(string refreshToken)
        {
            var result = await _tokenRepository.RemoveRefreshToken(refreshToken);

            if(result > 0)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        private TokenModel GetTokenModel(int userId)
        {
            string refreshtoken = Guid.NewGuid().ToString();
            DateTime accessTokenExpirtTime = DateTime.UtcNow.AddDays(1);

            var token = new TokenModel
            {
                accessToken = GenerateJwtToken(userId),
                refreshToken = refreshtoken,
                accessTokenExpiryTime= accessTokenExpirtTime
            };
            return token;
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Name, userId.ToString())
                    new Claim("id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var finaltoken = tokenHandler.WriteToken(token);
            return finaltoken;
        }
    }
}
