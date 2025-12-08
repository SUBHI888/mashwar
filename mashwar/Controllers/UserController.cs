using mashwar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
 namespace mashwar.Controllers
{
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _UserManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _RoleManager;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _signInManager = signInManager;
            _RoleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Regester()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Regester(RegesterModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new IdentityUser
                {
                    UserName = model.User_Email,
                    Email = model.User_Email

                };
                var Result = await _UserManager.CreateAsync(User, model.Password);
                if (Result.Succeeded) { return RedirectToAction("index", "home"); }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityUser User = new IdentityUser
            {
                UserName = model.User_Email,
                Email = model.User_Email


            };
            var result = await _signInManager.PasswordSignInAsync(User.UserName, model.User_Password, true, true);

            if (result.Succeeded) { return RedirectToAction("index", "home"); }
            return View(model);

        }



        public IActionResult AccessDenied()
        {  return View(); }
        public async Task<IActionResult> Logout()

        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "user"); ;
        }

    }
}
       


        



