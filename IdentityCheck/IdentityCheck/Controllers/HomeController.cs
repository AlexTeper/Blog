using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityCheck.Models;
using IdentityCheck.Services.User;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authorization;
using IdentityCheck.Models.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace IdentityCheck.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public HomeController(UserManager<ApplicationUser> userManager, IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.signInManager = signInManager;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Redirect()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [Authorize]
        [HttpGet("/home")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var posts = await userService.GetPostsAsync(currentUser);
            var model = PagingList.Create(posts, 2, page);

            return View(new IndexViewModel
            {
                AppUser = currentUser,
                Posts = posts,
                PagingList = model
            });
        }
    }
}
