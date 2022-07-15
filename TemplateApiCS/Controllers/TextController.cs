using TemplateApiCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : BaseController
    {
        public TextController(ILogger<TextController> Logger) : base(Logger)
        {
        }

        [HttpGet(nameof(Echo))]
        public string Echo(string input)
        {
            return input;
        }

        [HttpGet(nameof(Log))]
        public void Log(string input)
        {
            Logger.LogWarning(input);
        }
    }
}