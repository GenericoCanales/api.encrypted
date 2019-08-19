using System;
using System.Collections.Generic;

namespace api.visanet.validate.entitys
{
    public class Security
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public List<Key> keys { get; set; }
        public int expiresIn { get; set; }
    }
}