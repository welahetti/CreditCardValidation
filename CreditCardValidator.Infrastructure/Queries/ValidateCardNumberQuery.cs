using CreditCardValidator.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Infrastructure.Queries
{
    public record  ValidateCardNumberQuery(string creditCardNumber) :IRequest<CreditCardRange>;
    
    
}
