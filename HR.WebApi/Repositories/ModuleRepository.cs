using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class ModuleRepository<T> : ICommonRepository<Module>
    {
        private readonly ApplicationDbContext adbContext;

        public ModuleRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Module>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Module> vList;
                if (RecordLimit > 0)
                    vList = adbContext.module.AsEnumerable().Take(RecordLimit).ToList();
                else
                    vList = adbContext.module.AsEnumerable().ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Module>> Get(int id)
        {
            try
            {
                var vList = adbContext.module.AsEnumerable().Where(w => w.Id == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Module>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Module> vList;
                if (String.IsNullOrEmpty(searchValue))
                    vList = adbContext.module.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    vList = adbContext.module.Where(w => new[] { w.Name.ToLower(), w.Description.ToLower(), w.DisplayName.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Module entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.module.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Module entity)
        {
            try
            {
                var vList = adbContext.module.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");

                vList.Id = entity.Id;
                vList.Name = entity.Name;
                vList.Description = entity.Description;
                vList.DisplayName = entity.DisplayName;
                vList.Url = entity.Url;
                vList.isActive = entity.isActive;

                adbContext.module.Update(vList);

                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ToogleStatus(int id, short isActive)
        {
            try
            {
                var vList = adbContext.module.Where(w => w.Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.isActive = isActive;
                adbContext.module.Update(vList);
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
                var vList = adbContext.module.Where(w => w.Id == id).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                adbContext.module.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Module entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Id > 0)
                    intCount = adbContext.module.AsEnumerable().Where(w => w.Id != entity.Id && (w.Name == entity.Name && w.DisplayName == entity.DisplayName && w.Url == entity.Url)).Count();
                else
                    intCount = adbContext.module.AsEnumerable().Where(w => w.Id != entity.Id && (w.Name == entity.Name && w.DisplayName == entity.DisplayName && w.Url == entity.Url)).Count();
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
                    //Find Module all no of rows
                    var vCount = adbContext.module.Count();
                    return vCount;
                }
                else
                {
                    //Find Module no of rows with Searching
                    var vCount = adbContext.module.Where(w => new[] { w.Name.ToLower(), w.DisplayName.ToLower(), w.Description.ToLower()}.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
