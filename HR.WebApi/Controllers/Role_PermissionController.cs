using System;
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
    [ServiceFilter(typeof(ActionFilters.TokenVerify))]
    public class Role_PermissionController : ControllerBase
    {
        private ICommonRepository<Role_Permission> role_permissionRepository { get; set; }
        public Role_PermissionController(ICommonRepository<Role_Permission> commonRepository)
        {
            role_permissionRepository = commonRepository;
        }

        // GET ALL Data with Record Limit or without Record Limit
        // GET: api/Role_Permission/GetAll or api/Role_Permission/GetAll/100
        [HttpGet]
        [HttpGet("{recordLimit}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await role_permissionRepository.GetAll(recordLimit);

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

        // GET: api/Role_Permission/Get/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await role_permissionRepository.Get(id);

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

        // GET Role_Permission wise pagination with search or without search
        // GET: api/Role_Permission/FindPagination - body data { PageIndex:0 , PageSize:10, CommonSearch: "test" }
        //[HttpGet] old
        [HttpPost]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Role_Permission> vList = new ReturnBy<Role_Permission>();
                vList.list = await role_permissionRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
                if (vList.list.Count() == 0)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    vList.RecordCount = role_permissionRepository.RecordCount(pagination.CommonSearch);
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

        //Add Data
        // POST: api/Role_Permission/Add - body data Role_Permission
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.Add })]
        public async Task<IActionResult> Add(Role_Permission Role_Permission)
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
                if (role_permissionRepository.Exists(Role_Permission))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await role_permissionRepository.Insert(Role_Permission);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = Role_Permission;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        //Update Status
        // PUT: api/Role_Permission/UpdateStatus/id,isActive
        [HttpPut("{id},{isActive}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.Edit })]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await role_permissionRepository.ToogleStatus(id, isActive);
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

        //Delete Data
        // DELETE: api/Role_Permission/Delete/1
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Role_Permission", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await role_permissionRepository.Delete(id);
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