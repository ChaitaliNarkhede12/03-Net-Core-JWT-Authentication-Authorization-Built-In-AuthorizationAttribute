using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TokenModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime accessTokenExpiryTime { get; set; }

    }
}
