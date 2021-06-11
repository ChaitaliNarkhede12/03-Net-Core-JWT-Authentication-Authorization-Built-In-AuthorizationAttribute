using ApplicationLayer.Interface;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class UserAppService: IUserAppService
    {
        public readonly IRepository<UserInfo> _userGenericRepository;

        public UserAppService(IRepository<UserInfo> genericRepository)
        {
            _userGenericRepository = genericRepository;
        }

        public async Task<IEnumerable<UserInfo>> GetUserDetailsByExpression(Expression<Func<UserInfo, bool>> predicate)
        {
            var result = await _userGenericRepository.Get(predicate);
            return result;
        }
    }
}
