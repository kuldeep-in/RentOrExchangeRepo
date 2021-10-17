using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private DBContext _dbContext;
        private UserManager<MyAppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(DBContext dbContext,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<MyAppUser> userManager)
        {
            this._dbContext = dbContext;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AllPostsToApprove()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> ApprovePost(string id, int btnAction)
        {
            if (btnAction == 0)
            {
                var user = await _userManager.FindByIdAsync(id);
                var logins = await _userManager.GetLoginsAsync(user);
                var rolesForUser = await _userManager.GetRolesAsync(user);

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    IdentityResult result = IdentityResult.Success;
                    foreach (var login in logins)
                    {
                        result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                        if (result != IdentityResult.Success)
                            break;
                    }
                    if (result == IdentityResult.Success)
                    {
                        foreach (var item in rolesForUser)
                        {
                            result = await _userManager.RemoveFromRoleAsync(user, item);
                            if (result != IdentityResult.Success)
                                break;
                        }
                    }
                    if (result == IdentityResult.Success)
                    {
                        result = await _userManager.DeleteAsync(user);
                        if (result == IdentityResult.Success)
                            transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                    }
                }
            }
            else
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
                user.LockoutEnd = null;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("AllUsers");
        }


        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
