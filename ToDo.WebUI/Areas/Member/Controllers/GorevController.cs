using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.Entities.Concrete;
using ToDo.WebUI.BaseControllers;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class GorevController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IMapper _mapper;

        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _gorevService = gorevService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int aktifSayfa = 1)
        {
            TempData["Active"] = TempdataInfo.Gorev;
            var user = await GirisYapanKullanici();
            var gorevler = _mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, user.Id, aktifSayfa));
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;
            return View(gorevler);
        }
    }
}
