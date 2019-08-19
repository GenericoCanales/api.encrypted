using System;

namespace api.visanet.validate.entitys
{
    public class Order
    {
        public string tokenId { get; set; }
        public string purchaseNumber { get; set; }
        public double amount { get; set; }
        public double authorizedAmount { get; set; }
        public string currency { get; set; }
        public string externalTransactionId { get; set; }
        public int installment { get; set; }
        public string transactionId { get; set; }
    }
}