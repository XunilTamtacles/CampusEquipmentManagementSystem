using Microsoft.AspNetCore.Mvc;

namespace CampusEquipment.Web.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
