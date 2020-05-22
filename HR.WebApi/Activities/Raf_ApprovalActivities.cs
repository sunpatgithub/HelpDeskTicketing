using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Activities
{
    public class Raf_ApprovalActivities : IActivities<Raf_ApprovalView>
    {
        private readonly ApplicationDbContext adbContext;

        public Raf_ApprovalActivities(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }


        public async Task<IEnumerable<Raf_ApprovalView>> GetById(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from ra in adbContext.raf_approval
                             join r in adbContext.raf on ra.Raf_Id equals r.Id
                             join comp in adbContext.company on ra.Company_Id equals comp.Company_Id
                             join dept in adbContext.department on ra.Dept_Id equals dept.Dept_Id
                             join desig in adbContext.designation on ra.Desig_Id equals desig.Desig_Id
                             where ra.Raf_Id == id
                             && ra.DueDate >= startDate && ra.DueDate <= endDate
                             select new Raf_ApprovalView
                             {
                                 Id = ra.Id,

                                 Raf_Id = ra.Raf_Id,
                                 Raf_Name = r.Name,

                                 Company_Id = ra.Company_Id,
                                 Company_Name = comp.Company_Name,

                                 Dept_Id = ra.Dept_Id,
                                 Dept_Name = dept.Dept_Name,

                                 Desig_Id = ra.Desig_Id,
                                 Desig_Name = desig.Desig_Name,

                                 ApprovalSequence = ra.ApprovalSequence,
                                 Description = ra.Description,
                                 DueDate = ra.DueDate,
                                 isActive = ra.isActive
                             }).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Raf_ApprovalView>> GetByCompanyId(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from ra in adbContext.raf_approval
                             join r in adbContext.raf on ra.Raf_Id equals r.Id
                             join comp in adbContext.company on ra.Company_Id equals comp.Company_Id
                             join dept in adbContext.department on ra.Dept_Id equals dept.Dept_Id
                             join desig in adbContext.designation on ra.Desig_Id equals desig.Desig_Id
                             where ra.Company_Id == id
                             && ra.DueDate >= startDate && ra.DueDate <= endDate
                             select new Raf_ApprovalView
                             {
                                 Id = ra.Id,

                                 Raf_Id = ra.Raf_Id,
                                 Raf_Name = r.Name,

                                 Company_Id = ra.Company_Id,
                                 Company_Name = comp.Company_Name,

                                 Dept_Id = ra.Dept_Id,
                                 Dept_Name = dept.Dept_Name,

                                 Desig_Id = ra.Desig_Id,
                                 Desig_Name = desig.Desig_Name,

                                 ApprovalSequence = ra.ApprovalSequence,
                                 Description = ra.Description,
                                 DueDate = ra.DueDate,
                                 isActive = ra.isActive
                             }).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
