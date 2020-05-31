using System;
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
    public class RolesController : ControllerBase
    {
        private ICommonRepository<Roles> rolesRepository { get; set; }
        public RolesController(ICommonRepository<Roles> commonRepository)
        {
            this.rolesRepository = commonRepository;
        }

        [HttpGet]
        [HttpGet("{recordLimit}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await rolesRepository.GetAll(recordLimit);

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

        // GET: api/Roles/5
        [HttpGet("{id}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await rolesRepository.Get(id);

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

        // GET Roles wise pagination with search or without search
        // GET: api/Roles/FindPagination - body data { PageIndex:0 , PageSize:10, CommonSearch: "test" }
        //[HttpGet] old
        [HttpPost]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Roles> vList = new ReturnBy<Roles>();
                vList.list = await rolesRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);

                vList.RecordCount = rolesRepository.RecordCount(pagination.CommonSearch);
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

        // POST: api/Roles
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.Add })]
        public async Task<IActionResult> Add(Roles roles)
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
                if (rolesRepository.Exists(roles))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await rolesRepository.Insert(roles);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = roles;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // PUT: api/Roles/5
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.Edit })]
        public async Task<IActionResult> Edit(Roles roles)
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
                if (rolesRepository.Exists(roles))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await rolesRepository.Update(roles);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = roles;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        [HttpPut("{id},{isActive}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.Edit })]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
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
                await rolesRepository.ToogleStatus(id, isActive);
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

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Roles", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await rolesRepository.Delete(id);
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