using Microsoft.AspNetCore.Mvc;

namespace Neredekal.Hotel.Api.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
