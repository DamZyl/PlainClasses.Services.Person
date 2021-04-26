using Microsoft.AspNetCore.Mvc;

namespace PlainClasses.Services.Person.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController()
        {
        }

        [HttpGet]
        public string Get() => "Person Service";
    }
}