using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.Controllers
{
    [Authorize(Roles = "AppUser")]
    public class AppUserController : Controller
    {
        private readonly UserManager<MyAppUser> _userManager;
        private readonly IUserPostRepository _userPostRepository;

        public AppUserController(UserManager<MyAppUser> userManager,
                                IUserPostRepository userPostRepository)
        {
            _userManager = userManager;
            _userPostRepository = userPostRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                _userPostRepository.CreateUserPost(
                    new UserPost
                    {
                        Title = userPost.Title,
                        Description = userPost.Description,
                        IsActive =  false,
                        IsApproved = false,
                        CreatedBy = user.UserName,
                        CreatedOn = DateTime.UtcNow

                    });
                
                _userPostRepository.Save();
                return RedirectToAction("Rent");
            }
            return View(userPost);
        }
    }
}
