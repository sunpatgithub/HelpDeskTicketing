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
    public class CategoryRepository<T> : ICommonRepository<Category>
    {
        private readonly ApplicationDbContext adbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Category>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Category> vList;
                if (RecordLimit > 0)
                    vList = adbContext.category.Take(RecordLimit).ToList();
                else
                    vList = adbContext.category.ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> Get(int id)
        {
            try
            {
                var vList = adbContext.category.Where(w => w.CatId == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Category> vList;
                if (String.IsNullOrEmpty(searchValue))
                    vList = adbContext.category.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    vList = adbContext.category.Where(w => new[] { w.CatName.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Category entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.category.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Category entity)
        {
            try
            {
                try
                {
                    var lstCategory = adbContext.category.Where(x => x.CatId == entity.CatId).FirstOrDefault();
                    if (lstCategory == null)
                        throw new RecoredNotFoundException("Data Not Available");
                    lstCategory.CatName = entity.CatName;

                    lstCategory.isActive = entity.isActive;
                    lstCategory.UpdatedBy = entity.UpdatedBy;
                    lstCategory.UpdatedOn = DateTime.Now;

                    adbContext.category.Update(lstCategory);
                    await Task.FromResult(adbContext.SaveChanges());

                }
                catch (Exception ex)
                {
                    throw ex;
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
                var vList = adbContext.category.Where(w => w.CatId == id && w.isActive != isActive).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.isActive = isActive;
                adbContext.category.Update(vList);
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
                //Delete Category
                var vList = adbContext.category.Where(w => w.CatId == id).ToList().SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                adbContext.category.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Category entity)
        {
            try
            {
                int intCount = 0;
                if (entity.CatId > 0)
                    intCount = adbContext.category.Where(w => w.CatId != entity.CatId && (w.CatName == entity.CatName)).Count();
                else
                    intCount = adbContext.category.Where(w => w.CatName == entity.CatName).Count();
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
                    //Find Category all no of rows
                    var vCount = adbContext.category.Count();
                    return vCount;
                }
                else
                {
                    //Find Category no of rows with Searching
                    var vCount = adbContext.category.Where(w => new[] { w.CatName.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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

