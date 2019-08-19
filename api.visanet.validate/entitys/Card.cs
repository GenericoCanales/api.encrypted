using System;

namespace api.visanet.validate.entitys
{
    public class Card
    {
        public string cardNumber { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }
        public string cvv2 { get; set; }
    }
}