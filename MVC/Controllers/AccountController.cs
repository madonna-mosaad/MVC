using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<Users> _userManager;
		private readonly SignInManager<Users> _signInManager;

		public AccountController(UserManager<Users> userManager,SignInManager<Users> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Users()
                {
                    FName= signUpViewModel.FName,
                    LName= signUpViewModel.LName,
                    Email= signUpViewModel.Email,
                    IsAgree= signUpViewModel.IsAgree,
                    UserName=signUpViewModel.Email.Split('@')[0]
                    
                };
                var Result =await _userManager.CreateAsync(user,signUpViewModel.Password);
                if (Result.Succeeded)
                {
					return RedirectToAction("SignIn");
				}
                foreach(var r in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty,r.Description);
                }
            }
            return View(signUpViewModel);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn( SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signInViewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);
                    if (flag)
                    {
                        var result=await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, true, false);
                        if (result.Succeeded)
                        {
							return RedirectToAction("Index", "Home");
						}
                    }
                }
                ModelState.AddModelError(string.Empty, "Logi Erorr");
            }
            return View(signInViewModel);
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
            
        }
       
    }
}
