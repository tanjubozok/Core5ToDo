using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;

        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService, IMapper mapper)
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;
            _mapper = mapper;
        }

        public IActionResult ListeGorev()
        {
            TempData["Active"] = "gorev";
            return View(_mapper.Map<List<GorevListDto>>(_gorevService.GetirAciliyetIleTamamlanmayanlari()));
        }

        public IActionResult EkleGorev()
        {
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");
            return View();
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevAddDto model)
        {
            if (ModelState.IsValid)
            {
                Gorev gorev = new()
                {
                    Ad = model.Ad,
                    Aciklama = model.Aciklama,
                    AciliyetId = model.AciliyetId
                };
                _gorevService.Kaydet(gorev);
                return RedirectToAction("ListeGorev");
            }
            return View(model);
        }

        public IActionResult GuncelleGorev(int id)
        {
            TempData["Active"] = "gorev";
            var gorev = _gorevService.GetirId(id);           
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorev.AciliyetId);
            return View(_mapper.Map<GorevUpdateDto>(gorev));
        }

        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var gorev = _gorevService.GetirId(model.Id);
                gorev.Ad = model.Ad;
                gorev.Aciklama = model.Aciklama;
                gorev.AciliyetId = model.AciliyetId;
                _gorevService.Guncelle(gorev);
                return RedirectToAction("ListeGorev");
            }
            return View(model);
        }

        public IActionResult GorevSil(int id)
        {
            var gorev = _gorevService.GetirId(id);
            if (gorev != null)
            {
                _gorevService.Sil(gorev);
                return Json(null);
            }
            return View();
        }
    }
}
