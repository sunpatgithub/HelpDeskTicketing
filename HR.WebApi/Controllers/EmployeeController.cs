using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.CommonUtility;
using HR.WebApi.Common;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    //[ServiceFilter(typeof(ActionFilters.TokenVerify))]
    public class EmployeeController : ControllerBase
    {
        public ICommonRepository<Employee> employeeRepository { get; set; }
        public IPaginated<Employee> paginatedQueryRepo { get; set; }
        public EmployeeController(ICommonRepository<Employee> commonRepository, IPaginated<Employee> paginatedQueryRepository)
        {
            employeeRepository = commonRepository;
            paginatedQueryRepo = paginatedQueryRepository;
        }

        [HttpGet()]
        [HttpGet("{recordLimit}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employeeRepository.GetAll(recordLimit);

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

        [HttpGet("{id}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employeeRepository.Get(id);

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

        //[HttpGet] old
        [HttpPost]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.View })]
        public async Task<IActionResult> GetBy(PaginationBy searchBy)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await paginatedQueryRepo.GetPaginated(searchBy);

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

        // GET: api/Employee/5
        //[HttpGet] old
        [HttpPost]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Employee> vList = new ReturnBy<Employee>();
                vList.list = await employeeRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
                if (vList.list.Count() == 0)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    vList.RecordCount = employeeRepository.RecordCount(pagination.CommonSearch);
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Successfully";
                    objHelper.Data = vList;
                }
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }
        // POST: api/Employee
        //[ActionFilters.AuditLog]
        //[HttpPost]
        //public async Task<IActionResult> Add(Employee employee)
        //{
        //    ResponseHelper objHelper = new ResponseHelper();
        //    if (!ModelState.IsValid)
        //    {
        //        objHelper.Status = StatusCodes.Status424FailedDependency;
        //        objHelper.Message = "Invalid Model State";
        //        return BadRequest(objHelper);
        //    }

        //    try
        //    {
        //        if (employeeRepository.Exists(employee))
        //        {
        //            objHelper.Status = StatusCodes.Status200OK;
        //            objHelper.Message = "Data already available";
        //            return Ok(objHelper);
        //        }
        //        await employeeRepository.Insert(employee);
        //        objHelper.Status = StatusCodes.Status200OK;
        //        objHelper.Message = "Saved Successfully";
        //        objHelper.Data = employee;
        //        return Ok(objHelper);
        //    }
        //    catch
        //    {
        //        objHelper.Status = StatusCodes.Status500InternalServerError;
        //        objHelper.Message = "Get Unsuccessful";
        //        return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
        //    }
        //}

        // PUT: api/Employee/5
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.Edit })]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [HttpPut]
        public async Task<IActionResult> Edit(Employee employee)
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
                if (employeeRepository.Exists(employee))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await employeeRepository.Update(employee);
                objHelper.Data = employee;
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

        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.Edit })]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [HttpPut("{id},{isActive}")]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await employeeRepository.ToogleStatus(id, isActive);
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

        //DELETE: api/Employee/5
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee", EnumPermission.Delete })]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await employeeRepository.Delete(id);
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
