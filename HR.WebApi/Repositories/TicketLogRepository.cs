using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace HR.WebApi.Repositories
{
    public class TicketLogRepository<T> 
    {
        private readonly ApplicationDbContext adbContext;

        public TicketLogRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TicketLog>> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<TicketLog> vList;
                if (RecordLimit > 0)
                    vList = adbContext.TicketLog.Take(RecordLimit).ToList();
                else
                    vList = adbContext.TicketLog.ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TicketLog>> Get(int id)
        {
            try
            {
                var vList = adbContext.TicketLog.Where(w => w.LogId == id).ToList();
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return await Task.FromResult(vList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IEnumerable<TicketLog>> FindPaginated(int pageIndex, int pageSize, string searchValue)
        //{
        //    try
        //    {
        //        IEnumerable<TicketLog> vList;
        //        if (String.IsNullOrEmpty(searchValue))
        //            vList = adbContext.ticketlog.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //        else
        //            vList = adbContext.ticketlog.Where(w => new[] { w.Action.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //        if (vList == null || vList.Count() == 0)
        //            throw new RecoredNotFoundException("Get Data Empty");

        //        return await Task.FromResult(vList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task Insert(TicketLog entity)
        {
            try
            {
                entity.AddedOn = DateTime.Now;
                adbContext.TicketLog.Add(entity);
                await Task.FromResult(adbContext.SaveChanges());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task Update(TicketLog entity)
        //{
        //    try
        //    {
        //        try
        //        {
        //            var lstTicketLog = adbContext.ticketlog.Where(x => x.LogId == entity.LogId).FirstOrDefault();
        //            if (lstTicketLog == null)
        //                throw new RecoredNotFoundException("Data Not Available");
        //            lstTicketLog.TicketId = entity.TicketId;
        //            lstTicketLog.Action = entity.Action;
        //            lstTicketLog.Comments = entity.Comments;
        //            //lstTicketLog.isActive = entity.isActive;
        //            lstTicketLog.AddedBy = entity.AddedBy;
        //            lstTicketLog.AddedOn = entity.AddedOn;
        //            lstTicketLog.UpdatedBy = entity.Up
        //            lstTicketLog.UpdatedOn = DateTime.Now;

        //            adbContext.category.Update(lstCategory);
        //            await Task.FromResult(adbContext.SaveChanges());

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task ToogleStatus(int id, Int16 isActive)
        //{
        //    try
        //    {
        //        //update flag isActive=0
        //        var vList = adbContext.category.Where(w => w.CatId == id && w.isActive != isActive).SingleOrDefault();
        //        if (vList == null)
        //            throw new RecoredNotFoundException("Data Not Available");
        //        vList.isActive = isActive;
        //        adbContext.category.Update(vList);
        //        await Task.FromResult(adbContext.SaveChanges());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task Delete(int id)
        //{
        //    try
        //    {
        //        //Delete Category
        //        var vList = adbContext.category.Where(w => w.CatId == id).ToList().SingleOrDefault();
        //        if (vList == null)
        //            throw new RecoredNotFoundException("Data Not Available");
        //        adbContext.category.Remove(vList);
        //        await Task.FromResult(adbContext.SaveChanges());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Exists(TicketLog entity)
        //{
        //    try
        //    {
        //        int intCount = 0;
        //        if (entity.CatId > 0)
        //            intCount = adbContext.category.Where(w => w.CatId != entity.CatId && (w.CatName == entity.CatName)).Count();
        //        else
        //            intCount = adbContext.category.Where(w => w.CatName == entity.CatName).Count();
        //        return (intCount > 0 ? true : false);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int RecordCount(string searchValue)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(searchValue))
        //        {
        //            //Find Category all no of rows
        //            var vCount = adbContext.category.Count();
        //            return vCount;
        //        }
        //        else
        //        {
        //            //Find Category no of rows with Searching
        //            var vCount = adbContext.category.Where(w => new[] { w.CatName.ToLower() }.Any(a => a.Contains(searchValue.ToLower()))).Count();
        //            return vCount;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
