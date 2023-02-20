using CreditCardValidator.Application;
using CreditCardValidator.Domain;
using CreditCardValidator.Infrastructure.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Infrastructure.Handlers
{
    public class ValidateCardNumberHandler : IRequestHandler<ValidateCardNumberQuery, CreditCardRange>

    {
        private readonly ICreditCardValidatorRepository _creditCardValidatorRepository;

        public ValidateCardNumberHandler(ICreditCardValidatorRepository creditCardValidatorRepository )
        {
            this._creditCardValidatorRepository = creditCardValidatorRepository;
        }
        public Task<CreditCardRange> Handle(ValidateCardNumberQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_creditCardValidatorRepository.ValidateCardNumber(request.creditCardNumber));
        }
    }
}
