using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<Users> _userManager;
		private readonly SignInManager<Users> _signInManager;

		public UserController(UserManager<Users> userManager, SignInManager<Users> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public async Task<IActionResult> Index(string Email)
		{
			if (string.IsNullOrWhiteSpace(Email))
			{
				//to Map from Model to ViewModel to each User
				var users = await _userManager.Users.Select(U => new UsersViewModel //this is more than  one query (users make get all and select make select )so must put MultipleActiveResultSets=True in connection string 
				{
					Email = U.Email,
					FName = U.FName,
					lName = U.LName,
					Phone = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result,
					Id = U.Id
				}).ToListAsync();//convert from IQuaryable to any collection 
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(Email);
				if (user is not null)
				{
					var mapped = new UsersViewModel
					{
						Email = user.Email,
						FName = user.FName,
						lName = user.LName,
						Phone = user.PhoneNumber,
						Roles = _userManager.GetRolesAsync(user).Result,
						Id = user.Id
					};
					//convert to list because view is bind to IEnumrable<UsersViewMode> (@model IEnumrable<UsersViewMode>)
					return View(new List<UsersViewModel> { mapped });
				}
			}
			return View();
		}
	}
}