using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            RefreshTokenIds = new HashSet<RefreshTokenId>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Passw { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<RefreshTokenId> RefreshTokenIds { get; set; }
    }
}
