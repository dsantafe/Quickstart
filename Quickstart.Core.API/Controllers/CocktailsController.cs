using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Threading.Tasks;

namespace Quickstart.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocktailsController : ControllerBase
    {
        [HttpGet]
        [Route("GetCocktail/{name}")]
        public async Task<IActionResult> GetCocktail(string name)
        {
            //HttpClient client = new HttpClient
            //{
            //    BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/")
            //};
            //HttpResponseMessage response = await client.GetAsync($"search.php?s={name}");
            //string responseText = await response.Content.ReadAsStringAsync();

            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"search.php?s={name}", Method.GET);
            var response = client.Execute(request);
            string responseText = response.Content;

            return Ok(responseText);
        }
    }
}
