using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.BaseControllers;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class HomeController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        private readonly IRaporService _raporService;

        public HomeController(IGorevService gorevService, IBildirimService bildirimService, IRaporService raporService, UserManager<AppUser> userManager) : base(userManager)
        {
            _gorevService = gorevService;
            _bildirimService = bildirimService;
            _raporService = raporService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Anasayfa;
            var user = await GirisYapanKullanici();
            ViewBag.AtanmayiBekleyenGorevSayisi = _gorevService.GetirGorevSayisiAtanmayiBekleyen();
            ViewBag.TamamlanmisGorevSayisi = _gorevService.GetirGorevTamamlanmis();
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanSayisiileAppUserId(user.Id);
            ViewBag.ToplamRaporSayisi = _raporService.GetirRaporSayisi();
            return View();
        }
    }
}
