using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Reposiotry
{
    public class TokenRepository : Repository<RefreshTokenId>, ITokenRepository
    {
        public TokenRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork.Context != null)
            {
                dbSet = unitOfWork.Context.Set<RefreshTokenId>();
            }
        }

        public async Task<int> CreateRefreshTokenOnLogin(string refreshToken, DateTime expiryTime, int userId)
        {
            var tokenData = new RefreshTokenId
            {
                RefreshTokenValue = refreshToken,
                ExpiryTime = expiryTime,
                UserId = userId
            };

            dbContext.Set<RefreshTokenId>().Add(tokenData);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<UserInfo> GetUserFromRefreshToken(string refreshToken)
        {
            var result = await (from r in dbContext.Set<RefreshTokenId>().AsNoTracking()
                                join u in dbContext.Set<UserInfo>().AsNoTracking()
                                on r.UserId equals u.UserId
                                where r.RefreshTokenValue == refreshToken
                                select new UserInfo
                                {
                                    UserId = u.UserId,
                                    UserName = u.UserName
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> RemoveRefreshToken(string refreshToken)
        {
            var refreshTokenObj = await dbContext.Set<RefreshTokenId>().Where(x => x.RefreshTokenValue == refreshToken).FirstOrDefaultAsync();
            if (refreshTokenObj != null)
            {
                dbContext.Remove(refreshTokenObj);
                return await dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> UpdateRefreshTokenOnRefresh(string oldtoken, string newToken, DateTime expiryTime)
        {
            var refreshTokenObj = await dbContext.Set<RefreshTokenId>().Where(x => x.RefreshTokenValue == oldtoken).FirstOrDefaultAsync();
            if (refreshTokenObj != null)
            {
                refreshTokenObj.RefreshTokenValue = newToken;
                refreshTokenObj.ExpiryTime = expiryTime;

                dbContext.Set<RefreshTokenId>().Update(refreshTokenObj);
                return await dbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}
