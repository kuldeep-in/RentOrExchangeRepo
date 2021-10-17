using RentOrExchange.WebApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.DAL
{
    public interface IUserPostRepository : IDisposable
    {
        UserPost GetUserPostById(int id);
        IEnumerable<UserPost> GetAllUserPosts(string userId);
        IEnumerable<UserPost> GetUserPostsByType(int postType);
        IEnumerable<UserPost> GetAllPosts();
        void CreateUserPost(UserPost userPost);
        void UpdateUserPost(UserPost userPost);
        void Save();
    }
}