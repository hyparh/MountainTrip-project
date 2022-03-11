using Microsoft.AspNetCore.Mvc;

namespace MountainTrip.Areas.Admin.Controllers
{    
    public class TripsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
