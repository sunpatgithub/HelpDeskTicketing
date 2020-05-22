using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Activities
{
    public class Raf_ApprovedActivities : IActivities<Raf_ApprovedView>
    {
        private readonly ApplicationDbContext adbContext;

        public Raf_ApprovedActivities(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Raf_ApprovedView>> GetById(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from ra in adbContext.raf_approved
                             join r_a in adbContext.raf_approval on ra.Raf_Approval_Id equals r_a.Id
                             join r in adbContext.raf on ra.Raf_Id equals r.Id
                             join comp in adbContext.company on ra.Company_Id equals comp.Company_Id
                             join dept in adbContext.department on ra.Dept_Id equals dept.Dept_Id
                             join desig in adbContext.designation on ra.Desig_Id equals desig.Desig_Id
                             where ra.Raf_Id == id
                             && ra.ApprovedOn >= startDate && ra.ApprovedOn <= endDate
                             select new Raf_ApprovedView
                             {
                                 Id = ra.Id,

                                 Raf_Id = ra.Raf_Id,
                                 Raf_Name = r.Name,

                                 Raf_Approval_Id = ra.Raf_Approval_Id,
                                 ApprovalSequence = r_a.ApprovalSequence,
                                 _Description = r_a.Description,
                                 DueDate = r_a.DueDate,

                                 Company_Id = ra.Company_Id,
                                 Company_Name = comp.Company_Name,

                                 Dept_Id = ra.Dept_Id,
                                 Dept_Name = dept.Dept_Name,

                                 Desig_Id = ra.Desig_Id,
                                 Desig_Name = desig.Desig_Name,

                                 ApprovedBy = r_a.AddedBy,
                                 ApprovedOn = r_a.AddedOn,

                                 Description = ra.Description,
                                 isActive = ra.isActive
                             }).ToList();
                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Raf_ApprovedView>> GetByCompanyId(int id)
        {
            try
            {
                var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                var endDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + " 23:59:59.000");

                var vList = (from ra in adbContext.raf_approved
                             join r_a in adbContext.raf_approval on ra.Raf_Approval_Id equals r_a.Id
                             join r in adbContext.raf on ra.Raf_Id equals r.Id
                             join comp in adbContext.company on ra.Company_Id equals comp.Company_Id
                             join dept in adbContext.department on ra.Dept_Id equals dept.Dept_Id
                             join desig in adbContext.designation on ra.Desig_Id equals desig.Desig_Id
                             where ra.Company_Id == id
                             && ra.ApprovedOn >= startDate && ra.ApprovedOn <= endDate
                             select new Raf_ApprovedView
                             {
                                 Id = ra.Id,

                                 Raf_Id = ra.Raf_Id,
                                 Raf_Name = r.Name,

                                 Raf_Approval_Id = ra.Raf_Approval_Id,
                                 ApprovalSequence = r_a.ApprovalSequence,
                                 _Description = r_a.Description,
                                 DueDate = r_a.DueDate,

                                 Company_Id = ra.Company_Id,
                                 Company_Name = comp.Company_Name,

                                 Dept_Id = ra.Dept_Id,
                                 Dept_Name = dept.Dept_Name,

                                 Desig_Id = ra.Desig_Id,
                                 Desig_Name = desig.Desig_Name,

                                 ApprovedBy = r_a.AddedBy,
                                 ApprovedOn = r_a.AddedOn,

                                 Description = ra.Description,
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
