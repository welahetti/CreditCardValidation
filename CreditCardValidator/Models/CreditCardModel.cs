using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator.Models
{
    public class CreditCardModel
    {
        [BindProperty]
        public string cardNumber { get; set; }
    }
}
