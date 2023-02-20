using CreditCardValidator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Application
{
    public class CreditCardValidatorService : ICreditCardValidatorService
    {
        private readonly ICreditCardValidatorRepository _creditCardValidatorRepository;

        public CreditCardValidatorService(ICreditCardValidatorRepository creditCardValidatorRepository)
        {
            this._creditCardValidatorRepository = creditCardValidatorRepository;
        }
        public CreditCardRange ValidateCardNumber(string creditCardNumber)
        {
            var creditCardType = _creditCardValidatorRepository.ValidateCardNumber(creditCardNumber);
            return creditCardType;

        }

       
    }
}
