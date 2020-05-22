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
    public class Employee_PrevSalaryActivities : IActivities<Employee_Salary>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_PrevSalaryActivities(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Employee_Salary>> GetById(int id)
        {
            try
            {
                DateTime date = DateTime.Now.AddMonths(-1);
                int month = DateTime.Now.AddMonths(-1).Month, nextMonth;
                int year = DateTime.Now.AddMonths(-1).Year, nextYear;
                if (month == 12)
                {
                    nextYear = (year + 1);
                    nextMonth = 1;
                }
                else
                {
                    nextYear = year;
                    nextMonth = month;
                }
                var startDate = Convert.ToDateTime(date.ToString("" + year + "-" + month + "-01") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(date.ToString("" + nextYear + "-" + nextMonth + "-01") + " 00:00:00.000");

                var vList = (from emp in adbContext.employee
                             join emp_salary in adbContext.employee_salary on emp.Emp_Id equals emp_salary.Emp_Id
                             where emp.Emp_Id == id
                                && emp_salary.ApprisalFrom >= startDate && emp_salary.ApprisalFrom <= endDate
                             select emp_salary).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee_Salary>> GetByCompanyId(int id)
        {
            try
            {
                DateTime date = DateTime.Now.AddMonths(-1);
                int month = DateTime.Now.AddMonths(-1).Month, nextMonth;
                int year = DateTime.Now.AddMonths(-1).Year, nextYear;
                if (month == 12)
                {
                    nextYear = (year + 1);
                    nextMonth = 1;
                }
                else
                {
                    nextYear = year;
                    nextMonth = month;
                }
                var startDate = Convert.ToDateTime(date.ToString("" + year + "-" + month + "-01") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(date.ToString("" + nextYear + "-" + nextMonth + "-01") + " 00:00:00.000");

                var vList = (from emp in adbContext.employee
                             join emp_salary in adbContext.employee_salary on emp.Emp_Id equals emp_salary.Emp_Id
                             where emp.Company_Id == id
                                && emp_salary.ApprisalFrom >= startDate && emp_salary.ApprisalFrom <= endDate
                             select emp_salary).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
