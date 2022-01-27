using Microsoft.AspNetCore.Mvc;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GorevController : Controller
    {
        public GorevController()
        {
        }

        public IActionResult ListeGorev()
        {
            TempData["Active"] = "gorev";

            return View();
        }
    }
}
