using CreditCardValidator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CreditCardValidator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int cardNumber { get; set; }

       
      
        public void OnGet()
        {

        }

        

        public string BaseUrl()
        {
            var request = this.HttpContext.Request.Scheme+"://"+ this.HttpContext.Request.Host +"/";
            return request;
        }

        public async Task<IActionResult> OnPost()
        {
            CreditCardModel card = new CreditCardModel();
            if (Request.Form["cardNumber"].ToString()==string.Empty)
            {
                card.cardNumber = "Card number is not valid";
                return new OkObjectResult(card);
            }
            string trimmed = String.Concat(Request.Form["cardNumber"].ToString().Where(c => !Char.IsWhiteSpace(c)));
            string Baseurl = "https://localhost:7106/";
            //string Baseurl=BaseUrl();
            
            var result = string.Empty;
            

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new MultipartFormDataContent();
                    
                    var dataContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("cardNumber", trimmed),
                    });
                    content.Add(dataContent);
                    var request = new HttpRequestMessage();
                    request.Method = HttpMethod.Post;
                    //request.RequestUri = new Uri("api/CreditCards/");

                    request.Content = content;
                    var header = new ContentDispositionHeaderValue("form-data");
                    request.Content.Headers.ContentDisposition = header;

                    //var response = await client.PostAsync("api/CreditCards/", request.Content);
                    //string result = response.Content.ReadAsStringAsync().Result;
                    // HTTP POST
                    HttpResponseMessage response = await client.PostAsync("api/CreditCards/", request.Content);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<string>(data);
                        
                        card.cardNumber = result;
                        return new OkObjectResult(card);

                    }
                    else
                    {
                        return NotFound();
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }



    }
}