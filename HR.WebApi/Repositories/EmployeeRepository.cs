using HR.WebApi.Controllers;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HR.WebApi.Exceptions;
using HR.WebApi.ModelView;
using System.Text;
using System.Linq.Dynamic.Core;
using HR.CommonUtility;

namespace HR.WebApi.Repositories
{
    public class EmployeeRepository<T> : ICommonRepository<Employee>, IPaginated<Employee>
    {
        private readonly ApplicationDbContext adbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }
        public async Task<IEnumerable<Employee>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Employee> vList;
                if (RecordLimit > 0)
                    vList = adbContext.employee.Take(RecordLimit).ToList();
                else
                    vList = adbContext.employee.ToList();

                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Employee>> Get(int id)
        {
            try
            {
                IEnumerable<Employee> vList =  adbContext.employee.Where(w=>w.Emp_Id == id).ToList();
                
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Employee entity)
        {
            try
            {
                if (Exists(entity))
                    throw new RecordAlreadyExistException("Employee Already Available");


                adbContext.employee.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
               
                if (string.IsNullOrEmpty(entity.Emp_Code))
                    AutoGenerateEmployeeCode(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Employee entity)
        {
            try
            {
                if (Exists(entity))
                    throw new RecordAlreadyExistException("Employee Already Available");

                var lstEmp = adbContext.employee.Where(x => x.Emp_Id == entity.Emp_Id).FirstOrDefault();
                if (lstEmp == null)
                    throw new RecoredNotFoundException("Data Not Available");              

                lstEmp.Company_Id = entity.Company_Id;
                lstEmp.Site_Id = entity.Site_Id;
                lstEmp.JD_Id = entity.JD_Id;
                lstEmp.Dept_Id = entity.Dept_Id;
                lstEmp.Desig_Id = entity.Desig_Id;
                lstEmp.Zone_Id = entity.Zone_Id;
                lstEmp.Shift_Id = entity.Shift_Id;
                //lstEmp.Emp_Code = entity.Emp_Code;
                lstEmp.JoiningDate = entity.JoiningDate;
                lstEmp.Reporting_Id = entity.Reporting_Id;
                lstEmp.isSponsored = entity.isSponsored;
                lstEmp.Tupe = entity.Tupe;
                lstEmp.NiNo = entity.NiNo;
                lstEmp.NiCategory = entity.NiCategory;
                lstEmp.PreviousEmp_Id = entity.PreviousEmp_Id;
                lstEmp.isActive = entity.isActive;
                lstEmp.UpdatedBy = entity.UpdatedBy;
                lstEmp.UpdatedOn = DateTime.Now;

                adbContext.employee.Update(lstEmp);
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
                var vList = adbContext.employee.Where(w => w.Emp_Id == id && w.isActive != isActive).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.isActive = isActive;
                adbContext.employee.Update(vList);
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
                var vList = adbContext.employee.Where(w => w.Emp_Id == id).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.isActive = 0;
                adbContext.employee.Update(vList);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Employee entity)
        {
            try
            {
                int intCount = 0;
                if (entity.Emp_Id > 0)
                    intCount = adbContext.employee.Where(w => w.Emp_Id != entity.Emp_Id && w.Company_Id == entity.Company_Id && (w.Emp_Code == entity.Emp_Code)).Count();
                else
                    intCount = adbContext.employee.Where(w => w.Company_Id == entity.Company_Id && (w.Emp_Code == entity.Emp_Code)).Count();
                return (intCount > 0 ? true : false);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Employee> vList;
                if (String.IsNullOrEmpty(searchValue)) 
                    vList =  adbContext.employee.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                   
                else
                    vList =  adbContext.employee.Where(w => new[] { w.Emp_Code, Convert.ToString(w.Reporting_Id), w.NiNo, w.NiCategory }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                  

                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Data Not Available");

                return await Task.FromResult(vList);
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
                    var vCount = (from emp in adbContext.employee
                                  select emp.Emp_Id).Count();
                    return vCount;
                }
                else
                {
                    var vList = adbContext.employee.Where
                        (w => new[] { w.Emp_Code, Convert.ToString(w.Reporting_Id), w.NiNo, w.NiCategory }.Any(a => a.Contains(searchValue))).Count();

                    return vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AutoGenerateEmployeeCode(Employee entity)
        {
            entity.Emp_Code = "E-" + entity.Emp_Id;           

            adbContext.employee.Update(entity);            
            adbContext.SaveChanges();
        }

        public async Task<ReturnBy<Employee>> GetPaginated(PaginationBy search)
        {
            try
            {
                string strOrder = string.IsNullOrEmpty(search.OrderBy) ? "Emp_Id" : search.OrderBy;
                string strWhere = Common.Search.WhereString(search);

                IEnumerable<Employee> vEmployee;
                if (!String.IsNullOrEmpty(search.CommonSearch))
                    vEmployee = adbContext.employee.Where(w => new[] { w.Emp_Code, Convert.ToString(w.Emp_Id), Convert.ToString(w.Company_Id), Convert.ToString(w.Site_Id), Convert.ToString(w.JD_Id), Convert.ToString(w.Dept_Id), Convert.ToString(w.Desig_Id), Convert.ToString(w.Zone_Id), Convert.ToString(w.Shift_Id) }.Any(a => a.Contains(search.CommonSearch.ToLower()))).OrderBy(strOrder).ToList();
                 
                else
                    vEmployee = adbContext.employee.Where(strWhere).OrderBy(strOrder).ToList();

                ReturnBy<Employee> vList = new ReturnBy<Employee>()
                {
                    list = vEmployee.Skip(search.PageIndex * search.PageSize).Take(search.PageSize).ToList(),
                    RecordCount = vEmployee.Count()
                };

                if (vList.list == null || vList.RecordCount == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
