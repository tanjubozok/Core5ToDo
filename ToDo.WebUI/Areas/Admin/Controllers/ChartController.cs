using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDo.Business.Interfaces;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class ChartController : Controller
    {
        private readonly IAppUserService _userService;

        public ChartController(IAppUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Grafik;
            return View();
        }

        public IActionResult EnCokTamamlayan()
        {
            var jsonString = JsonConvert.SerializeObject(_userService.GetirEnCokGorevTamamlamisPersoneller());
            return Json(jsonString);
        }

        public IActionResult EnCokCalisan()
        {
            var jsonString = JsonConvert.SerializeObject(_userService.GetirEnCokGorevdeCalisanPersoneller());
            return Json(jsonString);
        }
    }
}
