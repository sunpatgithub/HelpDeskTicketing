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
    //[ServiceFilter(typeof(ActionFilters.TokenVerify))]
    public class User_RoleController : ControllerBase
    {
        private ICommonRepository<User_RoleView> user_RoleRepository { get; set; }
        private ICommonQuery<User_RoleView> commonQueryRepo { get; set; }
        public User_RoleController(ICommonRepository<User_RoleView> commonRepository, ICommonQuery<User_RoleView> commonQueryRepo)
        {
            user_RoleRepository = commonRepository;
            this.commonQueryRepo = commonQueryRepo;
        }

        [HttpGet]
        [HttpGet("{recordLimit}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await user_RoleRepository.GetAll(RecordLimit);

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

        // GET: api/User_Role/5
        [HttpGet("{id}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await user_RoleRepository.Get(id);

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
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.View })]
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

        // GET: api/User_Role/5
        //[HttpGet] old
        [HttpPost]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<User_RoleView> vList = new ReturnBy<User_RoleView>();
                vList.list = await user_RoleRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
                if (vList.list.Count() == 0)
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Get Empty Data";
                }
                else
                {
                    vList.RecordCount = user_RoleRepository.RecordCount(pagination.CommonSearch);
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

        // POST: api/User_Role
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.Add })]
        public async Task<IActionResult> Add(User_RoleView user_Role)
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
                if (user_RoleRepository.Exists(user_Role))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await user_RoleRepository.Insert(user_Role);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = user_Role;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        //Edit Data
        // PUT: api/User_Role/Edit - body data User_RoleView model
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.Edit })]
        public async Task<IActionResult> Edit(User_RoleView user_Role)
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
                if (user_RoleRepository.Exists(user_Role))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await user_RoleRepository.Update(user_Role);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = user_Role;
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
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.Edit })]
        public async Task<IActionResult> UpdateStatus(int id, short isActive)
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
                await user_RoleRepository.ToogleStatus(id, isActive);
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

        // DELETE: api/User_Role/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User_Role", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await user_RoleRepository.Delete(id);
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