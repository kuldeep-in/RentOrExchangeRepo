using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentOrExchange.WebApp.Areas.Identity.Data;
using RentOrExchange.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public IEnumerable<UserPostViewModel> GetPostToApprove()
        {
            //var param = new SqlParameter[] {
            //            new SqlParameter() {
            //                ParameterName = "@IdStu",
            //                SqlDbType =  System.Data.SqlDbType.Int,
            //                Direction = System.Data.ParameterDirection.Input,
            //                Value = 5
            //            },
            //            new SqlParameter() {
            //                ParameterName = "@IdAdd",
            //                SqlDbType =  System.Data.SqlDbType.Int,
            //                Direction = System.Data.ParameterDirection.Input,
            //                Value = 10
            //            }};

            List<UserPostViewModel> result = new List<UserPostViewModel>();
            using (var cnn = _dbContext.Database.GetDbConnection())
            {
                var cmm = cnn.CreateCommand();
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandText = "[dbo].[GetPostToApprove]";
                //cmm.Parameters.AddRange(param);
                cmm.Connection = cnn;
                cnn.Open();
                var reader = cmm.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new UserPostViewModel
                    {
                        UserPostId = Convert.ToInt32(reader["UserPostId"]),
                        Title = Convert.ToString(reader["Title"]),
                        Description = Convert.ToString(reader["Description"]),
                        CreatedBy = Convert.ToString(reader["CreatedBy"]),
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        PostType = Convert.ToInt32(reader["PostType"]),
                        Price = Convert.ToDouble(reader["Price"]),
                        Address = Convert.ToString(reader["Address"]),
                        PostalCode = Convert.ToString(reader["PostalCode"]),
                        PostFile = Path.Combine("https://strentorexchange.blob.core.windows.net/userposts", Convert.ToString(reader["PostFile"]))
                    });
                }
                // reader.NextResult(); //move the next record set

            }

            return result;

        }

        public IEnumerable<UserPost> GetAllUserPosts(string userId)
        {
            return _dbContext.UserPost.Where(x => x.CreatedBy == userId).ToList();
        }

        public IEnumerable<UserPostViewModel> GetPostsByType(int postType)
        {
            var param = new SqlParameter() {
                            ParameterName = "@PostTypeId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = postType
                        };

            List<UserPostViewModel> result = new List<UserPostViewModel>();
            using (var cnn = _dbContext.Database.GetDbConnection())
            {
                var cmm = cnn.CreateCommand();
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandText = "[dbo].[USP_GetAllUserPosts]";
                cmm.Parameters.Add(param);
                cmm.Connection = cnn;
                cnn.Open();
                var reader = cmm.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new UserPostViewModel
                    {
                        UserPostId = Convert.ToInt32(reader["UserPostId"]),
                        Title = Convert.ToString(reader["Title"]),
                        Description = Convert.ToString(reader["Description"]),
                        CreatedBy = Convert.ToString(reader["CreatedBy"]),
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        PostType = Convert.ToInt32(reader["PostType"]),
                        Price = Convert.ToDouble(reader["Price"]),
                        Address = Convert.ToString(reader["Address"]),
                        PostalCode = Convert.ToString(reader["PostalCode"]),
                        PostFile = Path.Combine("https://strentorexchange.blob.core.windows.net/userposts", Convert.ToString(reader["PostFile"]))
                    });
                }
                // reader.NextResult(); //move the next record set

            }

            return result;
        }

        public IEnumerable<UserPost> GetAllPosts()
        {
            return _dbContext.UserPost.ToList();
        }

        public void CreateUserPost(UserPostViewModel userPost)
        {
            try
            {
                var res = _dbContext.Database.ExecuteSqlRaw("CreateUserPost {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", userPost.Title, userPost.Description, userPost.CreatedBy, userPost.PostType, userPost.Price, userPost.Address, userPost.PostalCode, userPost.PostFile);
            }
            catch (Exception ex)
            {
            }
        }

        public void ApproveUserPost(int id, int btnAction)
        {
            try
            {
                var res = _dbContext.Database.ExecuteSqlRaw("Usp_ApproveUserPost {0}, {1}", id, btnAction);
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