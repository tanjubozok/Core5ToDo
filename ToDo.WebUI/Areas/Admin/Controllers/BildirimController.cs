using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.BildirimDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _bildirimService = bildirimService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            TempData["Active"] = "bildirim";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bildirim = _bildirimService.GetirOkunmayanlar(user.Id);
            return View(_mapper.Map<List<BildirimListDto>>(bildirim));
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
            _bildirimService.GetirOkunmayanlar(user.Id);
            return RedirectToAction("List");
        }
    }
}
