using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.DTO.DTOs.AppUserDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICustomLogger _logger;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICustomLogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            TempData["Active"] = "home";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var identityUser = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (identityUser.Succeeded)
                    {
                        var userRole = await _userManager.GetRolesAsync(user);
                        if (userRole.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                    }
                }
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre hatalıdır");
            }
            return View(model);
        }

        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    SurName = model.SurName,
                    Email = model.Email,
                    UserName = model.UserName,
                };
                var addUserResult = await _userManager.CreateAsync(user, model.Password);
                if (addUserResult.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (addRoleResult.Succeeded)
                    {
                        return RedirectToAction("GirisYap");
                    }
                    foreach (var item in addRoleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                foreach (var item in addUserResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa bulunamadı";
            }
            return View();
        }

        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"Hatanın oluştuğu yer :{exceptionHandler.Path}\n" +
                $"Hatanın mesajı :{exceptionHandler.Error.Message}\n" +
                $"Stack Trace :{exceptionHandler.Error.StackTrace}");

            ViewBag.Path = exceptionHandler.Path;
            ViewBag.Message = exceptionHandler.Error.Message;

            return View();
        }
    }
}
