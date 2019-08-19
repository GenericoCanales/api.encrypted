using System;

namespace api.visanet.validate.entitys
{
    public class Key
    {
        public string token { get; set; }
        public string baseKey { get; set; }
        public string iv { get; set; }
    }
}