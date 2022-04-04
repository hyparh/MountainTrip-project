using Microsoft.AspNetCore.Mvc;

namespace MountainTrip.Controllers
{
    public class EquipmentController : Controller
    {       
        public IActionResult Equipment()
        {
            return View();
        }
    }
}
