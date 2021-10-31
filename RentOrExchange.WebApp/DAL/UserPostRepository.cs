using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
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

        public void CreateUserPost(UserPostViewModel userPost)
        {
            //_dbContext.UserPost.Add(userPost);

            try
            {
                //object[] xparams = {
                ////new SqlParameter("@ParameterWithNumvalue", DBNull.Value),
                //new SqlParameter("@title", userPost.Title),
                //new SqlParameter("@description", userPost.Description),
                //new SqlParameter("@createdBy", userPost.CreatedBy),
                //new SqlParameter("@postType", userPost.PostType),
                //new SqlParameter("@price", userPost.Price),
                //new SqlParameter("@address", userPost.Address),
                //new SqlParameter("@postalCode", userPost.PostalCode),
                //new SqlParameter("@fileName", userPost.PostFile),
                ////new SqlParameter("@Out_Parameter", SqlDbType.Int) {Direction = ParameterDirection.Output}
                //};

                var res = _dbContext.Database.ExecuteSqlRaw("CreateUserPost {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", userPost.Title, userPost.Description, userPost.CreatedBy, userPost.PostType, userPost.Price, userPost.Address, userPost.PostalCode, userPost.PostFile);

            }
            catch (Exception ex)
            {
            }
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