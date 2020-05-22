using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class User_RoleRepository<T> : ICommonRepository<User_RoleView>, ICommonQuery<User_RoleView>
    {
        private readonly ApplicationDbContext adbContext;

        public User_RoleRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User_RoleView>> GetAll(int RecordLimit)
        {
            try
            {
                if (RecordLimit > 0)
                {
                    var vList = (from ur in adbContext.user_role
                                 join u in adbContext.users on ur.User_Id equals u.User_Id
                                 join r in adbContext.roles on ur.Role_Id equals r.Id
                                 select new User_RoleView
                                 {
                                     Id = ur.Id,

                                     User_Id = ur.User_Id,
                                     Login_Id = u.Login_Id,

                                     Role_Id = ur.Role_Id,
                                     Name = r.Name,

                                     AddedBy = ur.AddedBy,
                                     AddedOn = ur.AddedOn
                                 }
                                ).Take(RecordLimit).ToList();

                    return await Task.FromResult(vList);
                }
                else
                {
                    var vList = (from ur in adbContext.user_role
                                 join u in adbContext.users on ur.User_Id equals u.User_Id
                                 join r in adbContext.roles on ur.Role_Id equals r.Id
                                 select new User_RoleView
                                 {
                                     Id = ur.Id,
                                     User_Id = ur.User_Id,
                                     Login_Id = u.Login_Id,

                                     Role_Id = ur.Role_Id,
                                     Name = r.Name,

                                     AddedBy = ur.AddedBy,
                                     AddedOn = ur.AddedOn
                                 }
                                ).ToList();

                    return await Task.FromResult(vList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User_RoleView>> Get(int id)
        {
            try
            {
                var vList = (from ur in adbContext.user_role
                             join u in adbContext.users on ur.User_Id equals u.User_Id
                             join r in adbContext.roles on ur.Role_Id equals r.Id
                             where ur.Id == id && ur.isActive == 1 && u.isActive == 1
                             select new User_RoleView
                             {
                                 Id = ur.Id,

                                 User_Id = ur.User_Id,
                                 Login_Id = u.Login_Id,

                                 Role_Id = ur.Role_Id,
                                 Name = r.Name,

                                 AddedBy = ur.AddedBy,
                                 AddedOn = ur.AddedOn
                             }).ToList();

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User_RoleView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from ur in adbContext.user_role.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join u in adbContext.users on ur.User_Id equals u.User_Id
                             join r in adbContext.roles on ur.Role_Id equals r.Id
                             where ur.isActive == 1 && u.isActive == 1
                             select new User_RoleView
                             {
                                 Id = ur.Id,
                                 User_Id = ur.User_Id,
                                 Login_Id = u.Login_Id,

                                 Role_Id = ur.Role_Id,
                                 Name = r.Name

                             }
                            ).Take(searchBy.RecordLimit).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User_RoleView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find User_Role with Paging
                    var vList = (from ur in adbContext.user_role
                                 join u in adbContext.users on ur.User_Id equals u.User_Id
                                 join r in adbContext.roles on ur.Role_Id equals r.Id
                                 where ur.isActive == 1 && u.isActive == 1
                                 select new User_RoleView
                                 {
                                     Id = ur.Id,

                                     User_Id = ur.User_Id,
                                     Login_Id = u.Login_Id,

                                     Role_Id = ur.Role_Id,
                                     Name = r.Name,

                                     AddedBy = ur.AddedBy,
                                     AddedOn = ur.AddedOn
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
                else
                {
                    //Find User_Role with Paging & Searching
                    var vList = (from ur in adbContext.user_role.Where(w => new[] { Convert.ToString(w.Id), Convert.ToString(w.User_Id), Convert.ToString(w.Role_Id) }.Any(a => a.Contains(searchValue)))
                                 join u in adbContext.users on ur.User_Id equals u.User_Id
                                 join r in adbContext.roles on ur.Role_Id equals r.Id
                                 select new User_RoleView
                                 {
                                     Id = ur.Id,

                                     User_Id = ur.User_Id,
                                     Login_Id = u.Login_Id,

                                     Role_Id = ur.Role_Id,
                                     Name = r.Name,

                                     AddedBy = ur.AddedBy,
                                     AddedOn = ur.AddedOn
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    if (vList != null)
                        return await Task.FromResult(vList);
                    else
                        throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(User_RoleView entity)
        {
            try
            {
                //Insert New Zone
                var vList = new User_Role
                {
                    User_Id = entity.User_Id,
                    Role_Id = entity.Role_Id,
                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };
                adbContext.user_role.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(User_RoleView entity)
        {
            try
            {
                var vList = adbContext.user_role.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (vList != null)
                {
                    vList.User_Id = entity.User_Id;
                    vList.Role_Id = entity.Role_Id;
                    vList.isActive = entity.isActive;

                    adbContext.user_role.Update(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                }
                else
                {
                    throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ToogleStatus(int id, Int16 isActive)
        {
            try
            {
                //update flag isActive=0
                var vList = adbContext.user_role.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList != null)
                {
                    vList.isActive = isActive;
                    adbContext.user_role.Update(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                }
                else
                {
                    throw new Exception("Data Not Available");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                //Delete User_Role
                var vList = adbContext.user_role.Where(w => w.Id == id).SingleOrDefault();
                if (vList != null)
                {
                    adbContext.user_role.Remove(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RecordCount(string searchValue)
        {
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find User_Role all no of rows
                    var vCount = (from ur in adbContext.user_role
                                  join u in adbContext.users on ur.User_Id equals u.User_Id
                                  join r in adbContext.roles on ur.Role_Id equals r.Id
                                  select ur.Id
                                ).Count();
                    return vCount;
                }
                else
                {
                    //Find User_Role no of rows with Searching
                    var vCount = (from ur in adbContext.user_role.Where(w => new[] { Convert.ToString(w.Id), Convert.ToString(w.User_Id), Convert.ToString(w.Role_Id) }.Any(a => a.Contains(searchValue)))
                                  join u in adbContext.users on ur.User_Id equals u.User_Id
                                  join r in adbContext.roles on ur.Role_Id equals r.Id
                                  select ur.Id
                                ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(User_RoleView entity)
        {
            int intCount = 0;
            if (entity.Id > 0)
                intCount = adbContext.user_role.Where(w => w.Id != entity.Id && (w.User_Id == entity.User_Id && w.Role_Id == entity.Role_Id)).Count();
            else
                intCount = adbContext.user_role.Where(w => w.User_Id == entity.User_Id && w.Role_Id == entity.Role_Id).Count();
            return (intCount > 0 ? true : false);
        }
    }
}
