using CreditCardValidator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Application
{
    public interface ICreditCardValidatorRepository
    {
        CreditCardRange ValidateCardNumber(string creditCardNumber);        

        
    }
}
