using CreditCardValidator.Application;
using CreditCardValidator.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditCardValidator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        


        private readonly ICreditCardValidatorService _creditCardValidatorService;
        //private readonly IMediator _mediator;


        //public CreditCardsController(IMediator mediator)
        //{
        //    this._mediator = mediator;
        //}

        //// POST api/<CreditCardController>
        //[HttpPost]
        //public async Task<String> Post([FromBody] string value)
        //{
        //    return await _mediator.Send(new ValidateCardNumberQuery)
        //}


        public CreditCardsController(ICreditCardValidatorService creditCardValidatorService )
        {
            this._creditCardValidatorService = creditCardValidatorService;
        }

        // POST api/<CreditCardController>
        [HttpPost]
        public ActionResult<String> Post([FromForm] string card)
        {
            var cardNumber = card.Split('=')[1].ToString();
            CreditCardRange cardDetails =_creditCardValidatorService.ValidateCardNumber(cardNumber);
            if (cardDetails.IssuerAccepted)
            {
                return Ok(cardDetails.Issuer + " :" + cardNumber + " (valid)");
            }else
            {
                return Ok(cardDetails.Issuer + " :" + cardNumber + " (invalid)");
            }
           
        }

        
    }
}
