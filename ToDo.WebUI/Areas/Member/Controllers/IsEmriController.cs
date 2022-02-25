using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.DTO.DTOs.RaporDtos;
using ToDo.Entities.Concrete;
using ToDo.WebUI.BaseControllers;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class IsEmriController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public IsEmriController(IGorevService gorevService, UserManager<AppUser> userManager, IRaporService raporService, IBildirimService bildirimService, IMapper mapper) : base(userManager)
        {
            _gorevService = gorevService;
            _raporService = raporService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            var user = await GirisYapanKullanici();
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla(I => I.AppUserId == user.Id && !I.Durum)));
        }

        public IActionResult RaporYaz(int id)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            var gorev = _gorevService.GetirAciliyetIleId(id);
            RaporAddDto model = new()
            {
                GorevId = id,
                Gorev = gorev
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RaporYaz(RaporAddDto model)
        {
            if (ModelState.IsValid)
            {
                Rapor rapor = new()
                {
                    Detay = model.Detay,
                    Tanim = model.Tanim,
                    GorevId = model.GorevId
                };
                _raporService.Kaydet(rapor);

                var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
                var aktifKullanici = await GirisYapanKullanici();

                foreach (var admin in adminUserList)
                {
                    _bildirimService.Kaydet(new Bildirim
                    {
                        Aciklama = $"{aktifKullanici.Name} {aktifKullanici.SurName} yeni bir rapor yazdı",
                        AppUserId = admin.Id
                    });
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult RaporDuzenle(int id)
        {
            var rapor = _raporService.GetirGorevIleId(id);
            RaporUpdateDto model = new()
            {
                Id = id,
                Tanim = rapor.Tanim,
                Detay = rapor.Detay,
                Gorev = rapor.Gorev
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RaporDuzenle(RaporUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var rapor = _raporService.GetirGorevIleId(model.Id);
                rapor.Tanim = model.Tanim;
                rapor.Detay = model.Detay;

                _raporService.Guncelle(rapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> TamamlaGorev(int gorevId)
        {
            var gorev = _gorevService.GetirId(gorevId);
            gorev.Durum = true;
            _gorevService.Guncelle(gorev);
            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var aktifKullanici = await GirisYapanKullanici();

            foreach (var admin in adminUserList)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{aktifKullanici.Name} {aktifKullanici.SurName} vermiş olduğunuz bir görevi tamamladı",
                    AppUserId = admin.Id
                });
            }
            return Json(null);
        }
    }
}
