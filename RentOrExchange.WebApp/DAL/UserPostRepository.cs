using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.DAL
{
    public class UserPostRepository : IUserPostRepository, IDisposable
    {
        private readonly DBContext _dbContext;

        public UserPostRepository(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserPost GetUserPostById(int id)
        {
            return _dbContext.UserPost.Find(id);
        }

        //public IEnumerable<WorkItem> GetWorkItemsByState(WIStatus workItemStatus)
        //{
        //    if (workItemStatus == WIStatus.AllStatus)
        //    {
        //        return _dbContext.WorkItems.ToList();
        //    }
        //    else
        //    {
        //        return _dbContext.WorkItems.Where(x => x.StateId == (int)workItemStatus).ToList();
        //    }
        //}

        public IEnumerable<UserPost> GetAllUserPosts(string userId)
        {
            return _dbContext.UserPost.Where(x => x.CreatedBy == userId).ToList();
        }

        public IEnumerable<UserPost> GetUserPostsByType(int postType)
        {
            return _dbContext.UserPost.Where(x => x.PostType == postType).ToList();
        }

        public IEnumerable<UserPost> GetAllPosts()
        {
            return _dbContext.UserPost.ToList();
        }

        public void CreateUserPost(UserPost userPost)
        {
            _dbContext.UserPost.Add(userPost);
        }

        public void UpdateUserPost(UserPost userPost)
        {
            _dbContext.UserPost.Update(userPost);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}