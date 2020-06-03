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
    public class FullTicketRepository<T> : IFullTicket<FullTicket>
    {
        private readonly ApplicationDbContext adbContext;

        public FullTicketRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<FullTicket>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<FullTicket> vList;
                if (RecordLimit > 0)
                {
                    
                    vList = (from t in adbContext.ticket
                             join d in adbContext.department on t.DeptId equals d.Dept_Id
                             join c in adbContext.category on t.CatId equals c.CatId
                             join u in adbContext.users on t.RequesterId equals u.User_Id
                             join a in adbContext.users on t.AssigneeId equals a.User_Id
                            // join b in adbContext.users on t.AddedBy equals b.User_Id
                             join er in adbContext.employee_basicinfo on u.Emp_Id equals er.Emp_Id
                             join ea in adbContext.employee_basicinfo on a.Emp_Id equals ea.Emp_Id
                            // join ec in adbContext.employee_basicinfo on b.Emp_Id equals ec.Emp_Id
                             //join p in adbContext.users on t.UpdatedBy equals p.User_Id
                             //join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
                             select new FullTicket
                             {
                                 TicketId = t.TicketId,
                                 RequesterId = t.RequesterId,
                                 Requester = er.FirstName + " " + er.LastName,
                                 Subject = t.Subject,
                                 Description = t.Description,
                                 CatId = t.CatId,
                                 CatName = c.CatName,
                                 Status = t.Status,
                                 Priority = t.Priority,
                                 DeptId = t.DeptId,
                                 DepartmentName = d.Dept_Name,
                                 AssigneeId = t.AssigneeId,
                                 AssigneeName = ea.FirstName + " " + ea.LastName,
                                 IsActive = t.IsActive,
                                 AddedOn = t.AddedOn,
                                 AddedBy = t.AddedBy,
                                 AddedByName = ea.FirstName + " " + ea.LastName,
                                 UpdatedBy = t.UpdatedBy,
                                 //UpdatedByName = b.Login_Id,
                                 UpdatedOn = t.UpdatedOn
                             }).Take(RecordLimit).ToList();
                }
                else
                {
                    vList = fullticketlist();
                    
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

        private IEnumerable<FullTicket> fullticketlist()
        {
            return (from t in adbContext.ticket
             join d in adbContext.department on t.DeptId equals d.Dept_Id
             join c in adbContext.category on t.CatId equals c.CatId
             join u in adbContext.users on t.RequesterId equals u.User_Id
             join a in adbContext.users on t.AssigneeId equals a.User_Id
             join b in adbContext.users on t.AddedBy equals b.User_Id
             //join p in adbContext.users on t.UpdatedBy equals p.User_Id
             //join zone in adbContext.zone on desig.Zone_Id equals zone.Zone_Id
             select new FullTicket
             {
                 TicketId = t.TicketId,
                 RequesterId = t.RequesterId,
                 Requester = u.Login_Id,
                 Subject = t.Subject,
                 Description = t.Description,
                 CatId = t.CatId,
                 CatName = c.CatName,
                 Status = t.Status,
                 Priority = t.Priority,
                 DeptId = t.DeptId,
                 DepartmentName = d.Dept_Name,
                 AssigneeId = t.AssigneeId,
                 AssigneeName = a.Login_Id,
                 IsActive = t.IsActive,
                 AddedOn = t.AddedOn,
                 AddedBy = t.AddedBy,
                 AddedByName = b.Login_Id,
                 UpdatedBy = t.UpdatedBy,
                 //UpdatedByName = b.Login_Id,
                 UpdatedOn = t.UpdatedOn
             }).ToList();
        }

        public async Task<IEnumerable<FullTicket>> Get(int id)
        {
            try
            {
                var vList = fullticketlist().Where(w => w.TicketId == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<FullTicket>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<FullTicket> vList;
                if (String.IsNullOrEmpty(searchValue))
                    vList = fullticketlist().Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    vList = fullticketlist().Where(w => new[] { w.Subject.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(FullTicket entity)
        {
            try
            {
                int intCount = 0;
                if (entity.TicketId > 0)
                    intCount = fullticketlist().Where(w => w.TicketId != entity.TicketId && (w.Subject == entity.Subject)).Count();
                else
                    intCount = fullticketlist().Where(w => w.Subject == entity.Subject).Count();
                return (intCount > 0 ? true : false);
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
                    //Find Category all no of rows
                    var vCount = fullticketlist().Count();
                    return vCount;
                }
                else
                {
                    //Find Category no of rows with Searching
                    var vCount = fullticketlist().Where(w => new[] { w.Subject.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
                    return vCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
