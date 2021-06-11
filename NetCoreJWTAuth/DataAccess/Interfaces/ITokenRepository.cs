using DataAccess.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ITokenRepository
    {
        Task<int> CreateRefreshTokenOnLogin(string refreshToken, DateTime expiryTime, int userId);
        Task<int> UpdateRefreshTokenOnRefresh(string oldtoken, string newToken, DateTime expiryTime);
        Task<int> RemoveRefreshToken(string refreshToken);
        Task<UserInfo> GetUserFromRefreshToken(string refreshToken);
    }
}
