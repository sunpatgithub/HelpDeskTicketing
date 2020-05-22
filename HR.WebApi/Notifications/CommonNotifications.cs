using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Notifications
{
    public class CommonNotifications : INotifications
    {
        private readonly ApplicationDbContext adbContext;

        public CommonNotifications(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public void DoorAccessAlert()
        {
            throw new NotImplementedException();
        }

        public void InfraAlert()
        {
            throw new NotImplementedException();
        }

        public void LeaveAlert()
        {
            throw new NotImplementedException();
        }

        public void PayRollAlert()
        {
            throw new NotImplementedException();
        }
        /*
        public IEnumerable<Employee> ProbationAlert(int company_Id, int days,bool sendMail)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(days).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from emp in adbContext.employee
                        join emp_prob in adbContext.employee_probation on emp.Emp_Id equals emp_prob.Emp_Id
                        where emp.Company_Id == company_Id
                        && emp_prob.ProbationEndDate >= startDate && emp_prob.ProbationEndDate <= endDate                        
                        select emp).ToList();

                if (vList.Count() > 0 && sendMail == true)
                {
                    foreach (var item in vList)
                    {
                        //#region Sent link to Employee 

                        //Common.Email vEmailConfiguration = new Common.Email(adbContext);
                        //var usermodel = vEmailConfiguration.GetEmailBody(item., String.Empty, String.Empty, "Employee Probation " );
                        //vEmailConfiguration.SendEmail(usermodel, vEmailConfiguration.GetEmailConfiguration(item.Company_Id));

                        //#endregion
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public IEnumerable<Employee> NextReviewDateAlert(int company_Id, int days, bool sendMail)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(days).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from emp in adbContext.employee
                             join emp_prob in adbContext.employee_probation on emp.Emp_Id equals emp_prob.Emp_Id
                             where emp.Company_Id == company_Id
                             && emp_prob.NextReviewDate >= startDate && emp_prob.NextReviewDate <= endDate
                             select emp).ToList();

                if (vList.Count() > 0 && sendMail == true)
                {
                    foreach (var item in vList)
                    {
                        //#region Sent link to Employee 

                        //Common.Email vEmailConfiguration = new Common.Email(adbContext);
                        //var usermodel = vEmailConfiguration.GetEmailBody(item., String.Empty, String.Empty, "Employee Probation " );
                        //vEmailConfiguration.SendEmail(usermodel, vEmailConfiguration.GetEmailConfiguration(item.Company_Id));

                        //#endregion
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Employee> RafAlert(int company_Id, bool sendMail)
        {
            try
            {
                var vList = (from emp in adbContext.employee
                             join emp_ref in adbContext.employee_reference on emp.Emp_Id equals emp_ref.Emp_Id
                             where emp.Company_Id == company_Id
                             select emp).ToList();

                if (vList.Count() > 0 && sendMail == true)
                {
                    foreach (var item in vList)
                    {
                        //#region Sent link to Employee 

                        //Common.Email vEmailConfiguration = new Common.Email(adbContext);
                        //var usermodel = vEmailConfiguration.GetEmailBody(item., String.Empty, String.Empty, "Employee Probation " );
                        //vEmailConfiguration.SendEmail(usermodel, vEmailConfiguration.GetEmailConfiguration(item.Company_Id));

                        //#endregion
                    }
                }
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        public void SafetyAlert()
        {
            throw new NotImplementedException();
        }
    }
}
