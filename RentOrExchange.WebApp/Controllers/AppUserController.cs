using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.Controllers
{
    [Authorize(Roles = "AppUser")]
    public class AppUserController : Controller
    {
        private readonly UserManager<MyAppUser> _userManager;
        private readonly IUserPostRepository _userPostRepository;
        private BlobServiceClient _blobClient;

        public AppUserController(UserManager<MyAppUser> userManager,
                                IUserPostRepository userPostRepository, BlobServiceClient blobClient)
        {
            _userManager = userManager;
            _userPostRepository = userPostRepository;
            _blobClient = blobClient;
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

                if (userPost.PostImages != null)
                {
                    await UploadFiles(user.Id, userPost.PostImages);
                }

                _userPostRepository.CreateUserPost(
                    new UserPost
                    {
                        Title = userPost.Title,
                        Description = userPost.Description,
                        Price = userPost.Price,
                        PostType = userPost.PostType,
                        IsActive = false,
                        IsApproved = false,
                        CreatedBy = user.UserName,
                        CreatedOn = DateTime.UtcNow,
                        Address = userPost.Address,
                        PostalCode = userPost.PostalCode

                    });

                _userPostRepository.Save();
                return RedirectToAction("Rent");
            }
            return View(userPost);
        }

        public async Task<string> UploadFiles(string userId, IFormFile postImages)
        {
            BlobContainerClient containerClient = _blobClient.GetBlobContainerClient("userposts");

            string localPath = Path.GetTempPath();
            var extension = Path.GetExtension(postImages.FileName);
            string fileName = "file_" + Guid.NewGuid().ToString() + extension;
            string localFilePath = Path.Combine(localPath, fileName);

            using (var stream = System.IO.File.Create(localFilePath))
            {
                await postImages.CopyToAsync(stream);
            }

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            // Upload data from the local file
            await blobClient.UploadAsync(localFilePath, true);

            return fileName;
        }
    }
}
