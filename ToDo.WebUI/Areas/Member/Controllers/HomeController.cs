using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["Active"] = "home";

            return View();
        }
    }
}
