using System;

namespace api.visanet.validate.entitys
{
    public class ValidateRQ
    {
        public string channel { get; set; }
        public string captureType { get; set; }
        public bool tokenize { get; set; }
        public Order order { get; set; }
        public Card card { get; set; }
        public CardHolder cardHolder { get; set; }
    }
}