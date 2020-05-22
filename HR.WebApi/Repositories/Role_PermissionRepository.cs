using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HR.WebApi.Exceptions;

namespace HR.WebApi.Repositories
{
    public class Role_PermissionRepository<T> : ICommonRepository<Role_Permission>
    {
        private readonly ApplicationDbContext adbContext;

        public Role_PermissionRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Role_Permission>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Role_Permission> vList;
                if (RecordLimit > 0)
                    vList = adbContext.role_permission.Take(RecordLimit).ToList();
                else
                    vList = adbContext.role_permission.ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Role_Permission>> Get(int id)
        {
            try
            {
                var vList = adbContext.role_permission.Where(w => w.Id == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Role_Permission>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Role_Permission> vList;
                if (String.IsNullOrEmpty(searchValue))
                    vList = adbContext.role_permission.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    vList = adbContext.role_permission.Where(w => new[] { Convert.ToString(w.Module_Per_Id) , Convert.ToString(w.Role_Id) }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Role_Permission entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.role_permission.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Role_Permission entity)
        {
            try
            {
                
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
                var vList = adbContext.role_permission.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.isActive = isActive;
                adbContext.role_permission.Update(vList);
                await Task.FromResult(adbContext.SaveChanges());

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
                //Delete Role_Permission
                var vList = adbContext.role_permission.Where(w => w.Id == id).ToList().SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                adbContext.role_permission.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Role_Permission entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0)
                    intCount = adbContext.role_permission.Where(w => w.Id != entity.Id && (w.Role_Id == entity.Role_Id && w.Module_Per_Id == entity.Module_Per_Id)).Count();
                else
                    intCount = adbContext.role_permission.Where(w => w.Role_Id == entity.Role_Id && w.Module_Per_Id == entity.Module_Per_Id).Count();
                return (intCount > 0 ? true : false);
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
                    //Find Role_Permission all no of rows
                    var vCount = adbContext.role_permission.Count();
                    return vCount;
                }
                else
                {
                    //Find Role_Permission no of rows with Searching
                    var vCount = adbContext.role_permission.Where(w => new[] { Convert.ToString(w.Module_Per_Id), Convert.ToString(w.Role_Id) }.Any(a => a.Contains(searchValue.ToLower()))).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
