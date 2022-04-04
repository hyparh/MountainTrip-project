using Microsoft.AspNetCore.Mvc;

namespace MountainTrip.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
