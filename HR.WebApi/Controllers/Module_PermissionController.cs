using System;
using System.Linq;
using System.Threading.Tasks;
using HR.CommonUtility;
using HR.WebApi.Common;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    [ServiceFilter(typeof(ActionFilters.TokenVerify))]
    public class Module_PermissionController : ControllerBase
    {
        private ICommonRepository<Module_PermissionView> Module_PermissionRepo { get; set; }
        private ICommonQuery<Module_PermissionView> commonQueryRepo { get; set; }

        public Module_PermissionController(ICommonRepository<Module_PermissionView> commonRepository, ICommonQuery<Module_PermissionView> commonQueryRepo)
        {
            Module_PermissionRepo = commonRepository;
            this.commonQueryRepo = commonQueryRepo;
        }

        // GET: api/Module_Permission/GetAll/1000
        // GET: api/Module_Permission/GetAll
        [HttpGet]
        [HttpGet("{RecordLimit}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await Module_PermissionRepo.GetAll(RecordLimit);

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

        // GET: api/Module_Permission/GetAll
        [HttpGet("{id}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await Module_PermissionRepo.Get(id);

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

        // GET Moduule_permission search by
        // GET: api/Moduule_permission/GetBy/
        //[HttpGet] old
        [HttpPost]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.View })]
        public async Task<IActionResult> GetBy(SearchBy searchBy)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await commonQueryRepo.GetBy(searchBy);

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

        // GET: api/Module_Permission/FindPagination
        //[HttpGet] old
        [HttpPost]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Module_PermissionView> vList = new ReturnBy<Module_PermissionView>();
                vList.list = await Module_PermissionRepo.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
                if (vList.list.Count() == 0)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    vList.RecordCount = Module_PermissionRepo.RecordCount(pagination.CommonSearch);
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

        // POST: api/Module_Permission/Add
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.Add })]
        public async Task<IActionResult> Add(Module_PermissionView module_permission)
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
                if (Module_PermissionRepo.Exists(module_permission))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await Module_PermissionRepo.Insert(module_permission);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = module_permission;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // PUT: api/Module_Permission/Edit
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.Edit })]
        public async Task<IActionResult> Edit(Module_PermissionView module_permission)
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
                if (Module_PermissionRepo.Exists(module_permission))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }
                await Module_PermissionRepo.Update(module_permission);
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

        // PUT: api/Module_Permission/UpdateStatus/34,1
        // PUT: api/Module_Permission/UpdateStatus/34,0
        [HttpPut("{id},{isActive}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.Edit })]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await Module_PermissionRepo.ToogleStatus(id, isActive);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = await Module_PermissionRepo.Get(id);
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // DELETE: api/Module_Permission/Delete/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Module_Permission", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await Module_PermissionRepo.Delete(id);
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