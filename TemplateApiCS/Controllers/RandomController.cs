using TemplateApiCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApiCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomController : BaseController
    {
        public RandomController(ILogger<RandomController> Logger) : base(Logger)
        {
        }

        [HttpGet(nameof(Dice))]
        public int Dice(int min = 1, int max = 7)
        {
            return Random.Shared.Next(min, max);
        }
    }
}