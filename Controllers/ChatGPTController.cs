using DemoChatGPT.Models.ChatGPT;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoChatGPT.Controllers
{
    public class ChatGPTController : Controller
    {
        public static string _Endpoint = "https://api.openai.com/";
        public static string _URI = "v1/chat/completions";
        public static string _ApiKey = "sk-";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string request)
        {
            try
            {
                var client = new RestClient(_Endpoint);
                var requestOpenIA = new RestRequest(_URI);

                requestOpenIA.AddHeader("Content-Type", "application/json");
                requestOpenIA.AddHeader("Authorization", $"Bearer {_ApiKey}");

                var body = new Request
                {
                    model = "gpt-3.5-turbo",
                    messages = new List<Message>() {
                    new Message
                    {
                        role = "user",
                        content = request
                    }
                }
                };

                var jsonBody = JsonSerializer.Serialize(body);
                requestOpenIA.AddJsonBody(jsonBody);

                var responseOpenIA = client.Post<Response>(requestOpenIA);
                var response = responseOpenIA.choices[0].message.content;

                ViewBag.Response = response;
            }
            catch (Exception ex)
            {
                ViewBag.Response = ex;
            }
          
            return View();
        }
    }
}
