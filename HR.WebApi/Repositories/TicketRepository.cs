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
    public class TicketRepository<T> : ITicket<Ticket>
    {
        private readonly ApplicationDbContext adbContext;

        public TicketRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Ticket>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<Ticket> vList;
                if (RecordLimit > 0)
                    vList = adbContext.ticket.Take(RecordLimit).ToList();
                else
                    vList = adbContext.ticket.ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ticket>> Get(int id)
        {
            try
            {
                var vList = adbContext.ticket.Where(w => w.TicketId == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ticket>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {
                IEnumerable<Ticket> vList;
                if (String.IsNullOrEmpty(searchValue))
                    vList = adbContext.ticket.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                else
                    vList = adbContext.ticket.Where(w => new[] { w.Subject.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Ticket> Insert(Ticket entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.ticket.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Ticket> Update(Ticket entity)
        {
            try
            {
                try
                {
                    var lstTicket = adbContext.ticket.Where(x => x.TicketId == entity.TicketId).FirstOrDefault();
                    if (lstTicket == null)
                        throw new RecoredNotFoundException("Data Not Available");
                    lstTicket.RequesterId = entity.RequesterId;
                    lstTicket.Subject = entity.Subject;
                    lstTicket.Description = entity.Description;
                    lstTicket.CatId = entity.CatId;
                    lstTicket.Status = entity.Status;
                    lstTicket.Priority = entity.Priority;
                    lstTicket.DeptId = entity.DeptId;
                    lstTicket.AssigneeId = entity.AssigneeId;
                    lstTicket.IsActive = entity.IsActive;
                    lstTicket.AddedBy = entity.AddedBy;
                    lstTicket.AddedOn = entity.AddedOn;
                    lstTicket.UpdatedBy = entity.UpdatedBy;
                    lstTicket.UpdatedOn = DateTime.Now;
                    

                    adbContext.ticket.Update(lstTicket);
                    await Task.FromResult(adbContext.SaveChanges());

                    return lstTicket;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Ticket> ToogleStatus(int id, string status)
        {
            try
            {
                //update flag isActive=0
                var vList = adbContext.ticket.Where(w => w.TicketId == id).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.Status = status;

                adbContext.ticket.Update(vList);
                await Task.FromResult(adbContext.SaveChanges());

                return vList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Ticket> ReassignTicketMethod(int id, int deptId, int assigneeId)
        {
            try
            {
                //update flag isActive=0
                var vList = adbContext.ticket.Where(w => w.TicketId == id).SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                vList.DeptId = deptId;
                vList.AssigneeId = assigneeId;

                adbContext.ticket.Update(vList);
                await Task.FromResult(adbContext.SaveChanges());

                return vList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id, string action, string comment)
        {
            try
            {
                //Delete Category
                var vList = adbContext.ticket.Where(w => w.TicketId == id).ToList().SingleOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");
                adbContext.ticket.Remove(vList);
                await Task.FromResult(adbContext.SaveChanges());

                //InsertTicketLog(vList, action, comment);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Ticket entity)
        {
            try
            {
                int intCount = 0;
                if (entity.TicketId > 0)
                    intCount = adbContext.ticket.Where(w => w.TicketId != entity.TicketId && (w.Subject == entity.Subject)).Count();
                else
                    intCount = adbContext.ticket.Where(w => w.Subject == entity.Subject).Count();
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
                    var vCount = adbContext.ticket.Count();
                    return vCount;
                }
                else
                {
                    //Find Category no of rows with Searching
                    var vCount = adbContext.ticket.Where(w => new[] { w.Subject.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
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
