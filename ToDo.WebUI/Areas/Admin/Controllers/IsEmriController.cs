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
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            TempData["Active"] = "isemri";
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla()));
        }

        public IActionResult EklePersonelGorev(int id, string s, int sayfa = 1)
{
            TempData["Active"] = "isemri";

            ViewBag.AktifSayfa = sayfa;
            ViewBag.Aranan = s;

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
            TempData["Active"] = "isemri";

            PersonelGorevlendirListDto personelGorevlendirModel = new()
            {
                AppUser = _mapper.Map<AppUserListDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId)),
                Gorev = _mapper.Map<GorevListDto>(_gorevService.GetirAciliyetIleId(model.GorevId))
            };
            return View(personelGorevlendirModel);
        }

        public IActionResult GorevPersonelDetay(int id)
        {
            TempData["Active"] = "isemri";
            return View(_mapper.Map<GorevListAllDto>(_gorevService.GetirRaporlarIleId(id)));
        }

        public IActionResult GetirExcel(int id)
        {
            var list = _gorevService.GetirRaporlarIleId(id).Raporlar;
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileDownloadName = Guid.NewGuid() + ".xlsx";

            return File(_dosyaService.AktarExcel(list), contentType, fileDownloadName);
        }

        public IActionResult GetirPdf(int id)
        {
            var list = _gorevService.GetirRaporlarIleId(id).Raporlar;
            const string contentType = "application/pdf";
            string fileDownloadName = Guid.NewGuid() + ".pdf";

            return File(_dosyaService.AktarPdf(list), contentType, fileDownloadName);
        }
    }
}
