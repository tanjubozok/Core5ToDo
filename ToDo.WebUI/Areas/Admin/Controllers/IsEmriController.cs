using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.AppUserDtos;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.DTO.DTOs.RaporDtos;
using ToDo.Entities.Concrete;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public IsEmriController(IAppUserService userService, IGorevService gorevService, UserManager<AppUser> userManager, IDosyaService dosyaService, IBildirimService bildirimService, IMapper mapper)
        {
            _userService = userService;
            _gorevService = gorevService;
            _userManager = userManager;
            _dosyaService = dosyaService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }

        public IActionResult ListeIsEmri()
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla()));
        }

        public IActionResult EklePersonelGorev(int id, string s, int sayfa = 1)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            ViewBag.AktifSayfa = sayfa;
            ViewBag.Aranan = s;
            var personeller = _mapper.Map<List<AppUserListDto>>(_userService.GetirAdminOlmayanlar(out int toplamSayfa, s, sayfa));
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.Personeller = personeller;
            return View(_mapper.Map<GorevListDto>(_gorevService.GetirAciliyetIleId(id)));
        }

        [HttpPost]
        public IActionResult EklePersonelGorev(PersonelGorevlendirDto model)
        {
            var gorev = _gorevService.GetirId(model.GorevId);
            gorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(gorev);

            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId = model.PersonelId,
                Aciklama = $"{gorev.Ad} adlı iş için görevlendirildiniz."
            });
            return RedirectToAction("ListeIsEmri");
        }

        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            PersonelGorevlendirListDto personelGorevlendirModel = new()
            {
                AppUser = _mapper.Map<AppUserListDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId)),
                Gorev = _mapper.Map<GorevListDto>(_gorevService.GetirAciliyetIleId(model.GorevId))
            };
            return View(personelGorevlendirModel);
        }

        public IActionResult GorevPersonelDetay(int id)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            return View(_mapper.Map<GorevListAllDto>(_gorevService.GetirRaporlarIleId(id)));
        }

        public IActionResult GetirExcel(int id)
        {
            return File(_dosyaService.AktarExcel(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarIleId(id).Raporlar)), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }

        public IActionResult GetirPdf(int id)
        {
            var path = _dosyaService.AktarPdf(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarIleId(id).Raporlar));
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");
        }
    }
}
