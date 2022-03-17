using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTokenController : ControllerBase
    {
        private static readonly HttpClient client = new();

        public GetTokenController() : base()
        {

        }

        [HttpPost("{username}&{password}")]
        public async Task<string> GetToken(string username, string password)
        {
            Dictionary<string, string> body = new()
            {
                { "username", username },
                { "password", password },
                { "grant_type", "password" },
                { "client_id", "todolistapi" },
                { "client_secret", "secret" },
            };

            FormUrlEncodedContent content = new(body);

            HttpResponseMessage httpResponse = await client.PostAsync("http://localhost:25841/connect/token", content);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
