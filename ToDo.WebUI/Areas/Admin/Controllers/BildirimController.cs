using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;

        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager)
        {
            _bildirimService = bildirimService;
            _userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            TempData["Active"] = "bildirim";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bildirim = _bildirimService.GetirOkunmayanlar(user.Id);

            List<BildirimListViewModel> listModel = new();
            foreach (var item in bildirim)
            {
                BildirimListViewModel model = new()
                {
                    Id = item.Id,
                    Aciklama = item.Aciklama,
                };
                listModel.Add(model);
            }
            return View(listModel);
        }

        [HttpPost]
        public IActionResult List(int id)
        {
            var bildirim = _bildirimService.GetirId(id);
            bildirim.Durum = true;
            _bildirimService.Guncelle(bildirim);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> HepsiniTamamla()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bildirimList = _bildirimService.GetirOkunmayanlar(user.Id);

            foreach (var item in bildirimList)
            {
                item.Durum = true;
            }

            return RedirectToAction("List");
        }
    }
}
