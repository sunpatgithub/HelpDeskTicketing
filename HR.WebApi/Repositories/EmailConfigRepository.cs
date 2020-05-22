using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public class EmailConfigRepository<T> : ICommonRepository<Email_ConfigView>,ICommonQuery<Email_ConfigView>
    {
        private readonly ApplicationDbContext adbContext;

        public EmailConfigRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Email_ConfigView>> Get(int id)
        {
            try
            {
                var vList = (from con in adbContext.email_config
                             join c in adbContext.company on con.Company_Id equals c.Company_Id
                             where con.Email_Config_Id == id
                             select new Email_ConfigView
                             {
                                 Email_Config_Id = con.Email_Config_Id,
                                 Company_Id = con.Company_Id,
                                 Company_Name = c.Company_Name,
                                 Email_Host = con.Email_Host,
                                 Email_Port = con.Email_Port,
                                 Email_UserName = con.Email_UserName,
                                 Email_Password = con.Email_Password,
                                 EnableSSL = con.EnableSSL,
                                 TLSEnable = con.TLSEnable,
                                 isActive = con.isActive
                             }
                            ).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Email_ConfigView>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Email_ConfigView> vList;
                if (RecordLimit > 0)
                {
                    vList = (from con in adbContext.email_config
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new Email_ConfigView
                                 {
                                     Email_Config_Id = con.Email_Config_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Email_Host = con.Email_Host,
                                     Email_Port = con.Email_Port,
                                     Email_UserName = con.Email_UserName,
                                     Email_Password = con.Email_Password,
                                     EnableSSL = con.EnableSSL,
                                     TLSEnable = con.TLSEnable,
                                     isActive = con.isActive
                                 }
                                ).Take(RecordLimit).ToList();
                }
                else
                {
                    vList = (from con in adbContext.email_config
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new Email_ConfigView
                                 {

                                     Email_Config_Id = con.Email_Config_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Email_Host = con.Email_Host,
                                     Email_Port = con.Email_Port,
                                     Email_UserName = con.Email_UserName,
                                     Email_Password = con.Email_Password,
                                     EnableSSL = con.EnableSSL,
                                     TLSEnable = con.TLSEnable,
                                     isActive = con.isActive
                                 }
                                ).ToList();
                    return await Task.FromResult(vList);
                }
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Email_ConfigView>> GetBy(SearchBy searchBy)
        {
            try
            {
                var vList = (from con in adbContext.email_config.Where(String.Format("{0}=={1}", searchBy.FieldName, searchBy.FieldValue))
                             join c in adbContext.company on con.Company_Id equals c.Company_Id
                             select new Email_ConfigView
                             {
                                 Email_Config_Id = con.Email_Config_Id,

                                 Company_Id = con.Company_Id,
                                 Company_Name = c.Company_Name,

                                 Email_Host = con.Email_Host,
                                 Email_Port = con.Email_Port,
                                 Email_UserName = con.Email_UserName,
                                 Email_Password = con.Email_Password,
                                 EnableSSL = con.EnableSSL,
                                 TLSEnable = con.TLSEnable
                             }
                            ).Take(searchBy.RecordLimit).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Email_ConfigView entity)
        {
            try
            {
                //Insert New Email Config
                var vList = new Email_Config
                {
                    Company_Id = entity.Company_Id,
                    Email_Host = entity.Email_Host,
                    Email_Port = entity.Email_Port,
                    Email_UserName = entity.Email_UserName,
                    EnableSSL = entity.EnableSSL,
                    isActive = entity.isActive,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };
                adbContext.email_config.Add(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Email_ConfigView entity)
        {
            try
            {
                var vList = adbContext.email_config.Where(x => x.Email_Config_Id == entity.Email_Config_Id).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");

                vList.Company_Id = entity.Company_Id;
                    vList.Email_Host = entity.Email_Host;
                    vList.Email_Port = entity.Email_Port;
                    vList.Email_UserName = entity.Email_UserName;
                    vList.EnableSSL = entity.EnableSSL;
                    vList.isActive = entity.isActive;
                    vList.UpdatedBy = entity.UpdatedBy;
                    vList.UpdatedOn = DateTime.Now;

                    adbContext.email_config.Update(vList);
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
                //update flag isActive
                var vList = adbContext.email_config.Where(w => w.Email_Config_Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");

                vList.isActive = isActive;
                    adbContext.email_config.Update(vList);
                    await Task.FromResult(adbContext.SaveChanges());                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Email_ConfigView>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Email_ConfigView> vList;
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find Email Config with Paging
                    vList = (from con in adbContext.email_config
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new Email_ConfigView
                                 {
                                     Email_Config_Id = con.Email_Config_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Email_Host = con.Email_Host,
                                     Email_Port = con.Email_Port,
                                     Email_UserName = con.Email_UserName,
                                     Email_Password = con.Email_Password,
                                     EnableSSL = con.EnableSSL,
                                     TLSEnable = con.TLSEnable,
                                     isActive = con.isActive
                                 }
                                ).Skip(pageIndex * pageSize).Take(pageSize).ToList();                    
                }
                else
                {
                    //Find Department with Paging & Searching
                    vList = (from con in adbContext.email_config.Where(w => new[] { w.Email_UserName.ToLower()}.Any(a => a.Contains(searchValue.ToLower())))
                                 join c in adbContext.company on con.Company_Id equals c.Company_Id
                                 select new Email_ConfigView
                                 {
                                     Email_Config_Id = con.Email_Config_Id,
                                     Company_Id = con.Company_Id,
                                     Company_Name = c.Company_Name,
                                     Email_Host = con.Email_Host,
                                     Email_Port = con.Email_Port,
                                     Email_UserName = con.Email_UserName,
                                     Email_Password = con.Email_Password,
                                     EnableSSL = con.EnableSSL,
                                     TLSEnable = con.TLSEnable,
                                     isActive = con.isActive
                                 }
                                 ).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
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
                var vList = adbContext.email_config.Where(w => w.Email_Config_Id == id).ToList().SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");

                    vList.isActive = 0;
                    adbContext.email_config.Update(vList);
                    await Task.FromResult(adbContext.SaveChanges());
                
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
                    //Find Site all no of rows
                    var vCount = (from con in adbContext.email_config
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Email_Config_Id
                                 ).Count();
                    return vCount;
                }
                else
                {
                    //Find Site no of rows with Searching
                    var vCount = (from con in adbContext.email_config.Where(w => new[] { w.Email_UserName.ToLower() }.Any(a => a.Contains(searchValue.ToLower())))
                                  join c in adbContext.company on con.Company_Id equals c.Company_Id
                                  select con.Email_Config_Id
                                 ).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Email_ConfigView entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Email_Config_Id > 0) //Update Validation
                    intCount = adbContext.email_config.Where(w => w.Company_Id == entity.Company_Id && w.Email_Config_Id != entity.Email_Config_Id).Count();
                else //Insert Validation
                    intCount = adbContext.email_config.Where(w => w.Company_Id == entity.Company_Id).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
