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
    public class NextReviewDateActivities : IActivities<Employee_Probation>
    {
        private readonly ApplicationDbContext adbContext;

        public NextReviewDateActivities(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Probation>> GetById(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from emp in adbContext.employee
                             join emp_prob in adbContext.employee_probation on emp.Emp_Id equals emp_prob.Emp_Id
                             where emp.Emp_Id == id
                                && emp_prob.NextReviewDate >= startDate && emp_prob.NextReviewDate <= endDate
                             select emp_prob).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Probation>> GetByCompanyId(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from emp in adbContext.employee
                             join emp_prob in adbContext.employee_probation on emp.Emp_Id equals emp_prob.Emp_Id
                             where emp.Company_Id == id
                                && emp_prob.NextReviewDate >= startDate && emp_prob.NextReviewDate <= endDate
                             select emp_prob).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
