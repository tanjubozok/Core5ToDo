using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.AciliyetDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;

        public AciliyetController(IAciliyetService aciliyetService, IMapper mapper)
        {
            _aciliyetService = aciliyetService;
            _mapper = mapper;
        }

        public IActionResult ListeAciliyet()
        {
            TempData["Active"] = "aciliyet";
            return View(_mapper.Map<List<AciliyetListDto>>(_aciliyetService.GetirHepsi()));
        }

        public IActionResult EkleAciliyet()
        {
            TempData["Active"] = "aciliyet";
            return View();
        }

        [HttpPost]
        public IActionResult EkleAciliyet(AciliyetAddDto model)
        {
            if (ModelState.IsValid)
            {
                Aciliyet aciliyet = new()
                {
                    Tanim = model.Tanim
                };
                _aciliyetService.Kaydet(aciliyet);
                return RedirectToAction("ListeAciliyet");
            }
            return View(model);
        }

        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = "aciliyet";
            return View(_mapper.Map<AciliyetUpdateDto>(_aciliyetService.GetirId(id)));
        }

        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var aciliyet = _aciliyetService.GetirId(model.Id);
                aciliyet.Tanim = model.Tanim;
                _aciliyetService.Guncelle(aciliyet);
                return RedirectToAction("ListeAciliyet");
            }
            return View(model);
        }
    }
}