using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class RefreshTokenId
    {
        public int RefreshTokenId1 { get; set; }
        public int? UserId { get; set; }
        public string RefreshTokenValue { get; set; }
        public DateTime? ExpiryTime { get; set; }

        public virtual UserInfo User { get; set; }
    }
}
