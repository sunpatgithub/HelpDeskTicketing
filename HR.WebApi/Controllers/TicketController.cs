using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HR.CommonUtility;
using HR.WebApi.Common;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using Microsoft.AspNetCore.Http;
using HR.WebApi.Repositories;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    public class TicketController : ControllerBase
    {
        private ITicket<Ticket> ticketRepository { get; set; }
        private ITicketLog<TicketLog> ticketlogRepository { get; set; }

        //public TicketController(ICommonRepository<Ticket> commonRepository, ITicketLog<TicketLog> ticketlog_Repository)
        //{
        //    this.ticketRepository = commonRepository;
        //    this.ticketlogRepository = ticketlog_Repository;
        //}

        public TicketController(ITicket<Ticket> commonRepository, ITicketLog<TicketLog> ticketlog_Repository)
        {
            this.ticketRepository = commonRepository;
            this.ticketlogRepository = ticketlog_Repository;
        }

        [HttpGet]
        [HttpGet("{recordLimit}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await ticketRepository.GetAll(recordLimit);

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Get Successfully";
                objHelper.Data = vList;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await ticketRepository.Get(id);

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Get Successfully";
                objHelper.Data = vList;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // GET Category wise pagination with search or without search
        // GET: api/Category/FindPagination - body data { PageIndex:0 , PageSize:10, CommonSearch: "test" }
        //[HttpGet] old
        [HttpPost]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Ticket> vList = new ReturnBy<Ticket>();
                vList.list = await ticketRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);

                vList.RecordCount = ticketRepository.RecordCount(pagination.CommonSearch);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Get Successfully";
                objHelper.Data = vList;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // POST: api/Ticket
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //  [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.Add })]
        public async Task<IActionResult> Add(Ticket ticket)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = ModelException.Errors(ModelState);
                return BadRequest(objHelper);
            }

            try
            {
                //if (ticketRepository.Exists(ticket))
                //{
                //    objHelper.Status = StatusCodes.Status200OK;
                //    objHelper.Message = "Data already available";
                //    return Ok(objHelper);
                //}

                var newticket = await ticketRepository.Insert(ticket);
                await ticketlogRepository.Insert(GetTicketLogFromTicket(newticket, "Inserted", ""));

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = ticket;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        private TicketLog GetTicketLogFromTicket(Ticket ticket, string action, string comments)
        {
            TicketLog ticketlog = new TicketLog();
            ticketlog.TicketId = ticket.TicketId;
            ticketlog.Action = action;
            ticketlog.Comments = comments;
            ticketlog.AddedBy = ticket.AddedBy;
            ticketlog.AddedOn = ticket.AddedOn;
            ticketlog.Status = ticket.Status;
            return ticketlog;
        }

        // PUT: api/Ticket/5
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.Edit })]
        public async Task<IActionResult> Edit(Ticket ticket)
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = ModelException.Errors(ModelState);
                return BadRequest(objHelper);
            }

            try
            {
                //if (ticketRepository.Exists(ticket))
                //{
                //    objHelper.Status = StatusCodes.Status200OK;
                //    objHelper.Message = "Data already available";
                //    return Ok(objHelper);
                //}

                var updatedticket = await ticketRepository.Update(ticket);
                await ticketlogRepository.Insert(GetTicketLogFromTicket(updatedticket, "Updated", ""));

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = ticket;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.Edit })]
        public async Task<IActionResult> UpdateStatus(int id, string status, string comment) // string comment
        {
            ResponseHelper objHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objHelper.Status = StatusCodes.Status424FailedDependency;
                objHelper.Message = "Invalid Model State";
                return BadRequest(objHelper);
            }
            try
            {
                var ticket = await ticketRepository.ToogleStatus(id, status);
                await ticketlogRepository.Insert(GetTicketLogFromTicket(ticket, "Changed Status", comment));

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Ticket", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await ticketRepository.Delete(id, "Delete","");

                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }
    }
}