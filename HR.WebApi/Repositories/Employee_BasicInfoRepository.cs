
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;

namespace HR.WebApi.Repositories
{
    public class Employee_BasicInfoRepository<T> : IEmployee_BasicInfo<Employee_BasicInfo>
    {
        private readonly ApplicationDbContext adbContext;

        public Employee_BasicInfoRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Employee_BasicInfo>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Employee_BasicInfo> vList;
                if (RecordLimit > 0)
                    vList = adbContext.employee_basicinfo.Take(RecordLimit).ToList();
                else
                    vList = adbContext.employee_basicinfo.ToList();

                if (vList == null || vList.Count() == 0)
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


