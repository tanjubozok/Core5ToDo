using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.BildirimDtos;
using ToDo.Entities.Concrete;
using ToDo.WebUI.BaseControllers;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class BildirimController : BaseIdentityController
    {
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _bildirimService = bildirimService;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            TempData["Active"] = TempdataInfo.Bildirim;
            var user = await GirisYapanKullanici();
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
            var user = await GirisYapanKullanici();
            _bildirimService.GetirOkunmayanlar(user.Id);
            return RedirectToAction("List");
        }
    }
}
