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
        IEnumerable<UserPostViewModel> GetPostsByType(int postType);
        IEnumerable<UserPost> GetAllPosts();
        void CreateUserPost(UserPostViewModel userPost);
        IEnumerable<UserPostViewModel> GetPostToApprove();
        void UpdateUserPost(UserPost userPost);
        void ApproveUserPost(int id, int btnAction);
        void Save();
    }
}