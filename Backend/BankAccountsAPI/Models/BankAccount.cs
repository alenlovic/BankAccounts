using System.ComponentModel.DataAnnotations;

namespace BankAccountsAPI.Models
{
    public class BankAccount
    {
        [Key]
        public Guid ID { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }
    }
}
