using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface INotifications
    {
        void PayRollAlert();
        void LeaveAlert();
        //IEnumerable<Employee> ProbationAlert(int company_Id, int days, bool sendMail);
        //IEnumerable<Employee> NextReviewDateAlert(int company_Id, int days, bool sendMail);
        //IEnumerable<Employee> RafAlert(int company_Id, bool sendMail);

        void DoorAccessAlert();
        void InfraAlert(); //network + hardware

        void SafetyAlert();
    }
}
