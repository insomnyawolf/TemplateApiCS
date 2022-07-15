using TemplateApiCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApiCS.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        public BaseController(ILogger Logger)
        {
            this.Logger = Logger;
        }
    }
}