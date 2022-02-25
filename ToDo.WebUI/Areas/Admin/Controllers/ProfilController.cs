using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using ToDo.DTO.DTOs.AppUserDtos;
using ToDo.Entities.Concrete;
using ToDo.WebUI.BaseControllers;
using ToDo.WebUI.StringInfo;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class ProfilController : BaseIdentityController
    {
        private readonly IMapper _mapper;

        public ProfilController(UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Profil;
            return View(_mapper.Map<AppUserListDto>(await GirisYapanKullanici()));
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListDto model, IFormFile Resim)
        {
            if (ModelState.IsValid)
            {
                var user = await GirisYapanKullanici();
                if (Resim != null)
                {
                    string uzanti = Path.GetExtension(Resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + resimAd);
                    using var stream = new FileStream(path, FileMode.Create);
                    await Resim.CopyToAsync(stream);
                    user.Picture = resimAd;
                }
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarılı bir şekilde tamamlandı";
                    return RedirectToAction("Index");
                }
                HataEkle(result.Errors);
            }
            return View(model);
        }
    }
}
