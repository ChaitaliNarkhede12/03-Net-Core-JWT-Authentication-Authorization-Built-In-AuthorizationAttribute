using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interface
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserInfo>> GetUserDetailsByExpression(Expression<Func<UserInfo, bool>> predicate);

    }
}
