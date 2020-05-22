using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Activities
{
    public class ReferenceActivities : IActivities<Employee_Reference>
    {
        private readonly ApplicationDbContext adbContext;

        public ReferenceActivities(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }


        public async Task<IEnumerable<Employee_Reference>> GetById(int id)
        {
            try
            {
                var vList = (from emp in adbContext.employee
                             join emp_ref in adbContext.employee_reference on emp.Emp_Id equals emp_ref.Emp_Id
                             where emp.Emp_Id == id                                
                             select emp_ref).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Reference>> GetByCompanyId(int id)
        {
            try
            {
                var vList = (from emp in adbContext.employee
                             join emp_ref in adbContext.employee_reference on emp.Emp_Id equals emp_ref.Emp_Id
                             where emp.Company_Id == id
                             select emp_ref).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
