using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.DAL;
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
        private readonly IUserPostRepository _userPostRepository;

        public AdminController(DBContext dbContext,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<MyAppUser> userManager,
                                IUserPostRepository userPostRepository)
        {
            this._dbContext = dbContext;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._userPostRepository = userPostRepository;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AllUsers()
        {
            List<MyAppUser> userlist = (from user in _dbContext.Users
                                        join userRoles in _dbContext.UserRoles on user.Id equals userRoles.UserId
                                        where userRoles.RoleId == "d154e673-0e10-4d3d-acf7-3aed33af2fca"
                                        //join role in _dbContext.Roles on userRoles.RoleId equals role.Id
                                        select new MyAppUser
                                                {
                                                    Id = user.Id,
                                                    Email = user.Email,
                                                    FirstName = user.FirstName,
                                                    LastName = user.LastName,
                                                    UserName = user.UserName
                                                }).ToList();

            return View(userlist.OrderBy(x => x.Email));

        }

        public IActionResult AllPostsToApprove()
        {
            var result = _userPostRepository.GetPostToApprove();
            return View(result);
        }

        public IActionResult ApprovePost(int id, int btnAction)
        {
            _userPostRepository.ApproveUserPost(id, btnAction);

            _dbContext.SaveChanges();
            return RedirectToAction("AllPostsToApprove");
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
