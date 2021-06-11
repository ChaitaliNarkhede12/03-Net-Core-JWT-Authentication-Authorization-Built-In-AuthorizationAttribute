using DataAccess.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interface
{
    public interface ITokenAppService
    {
        Task<TokenModel> Login(string userName, string password);
        Task<TokenModel> CreateTokenOnRefreshToken(string refreshToken);
        Task<int> RemoveRefreshToken(string refreshToken);
    }
}
