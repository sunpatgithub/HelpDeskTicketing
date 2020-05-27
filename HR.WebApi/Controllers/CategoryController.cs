using System;
using HR.CommonUtility;
using System.Threading.Tasks;
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
    public class CategoryController : ControllerBase
    {
        private ICommonRepository<Category> categoryRepository { get; set; }
        public CategoryController(ICommonRepository<Category> commonRepository)
        {
            this.categoryRepository = commonRepository;
        }

        [HttpGet]
        [HttpGet("{recordLimit}")]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await categoryRepository.GetAll(recordLimit);

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
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.View })]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await categoryRepository.Get(id);

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
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.View })]
        public async Task<IActionResult> FindPagination(Pagination pagination)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                ReturnBy<Category> vList = new ReturnBy<Category>();
                vList.list = await categoryRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);

                vList.RecordCount = categoryRepository.RecordCount(pagination.CommonSearch);
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

        // POST: api/Category
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
      //  [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.Add })]
        public async Task<IActionResult> Add(Category category)
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
                if (categoryRepository.Exists(category))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await categoryRepository.Insert(category);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = category;
                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                objHelper.Status = StatusCodes.Status500InternalServerError;
                objHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objHelper);
            }
        }

        // PUT: api/Category/5
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.Edit })]
        public async Task<IActionResult> Edit(Category category)
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
                if (categoryRepository.Exists(category))
                {
                    objHelper.Status = StatusCodes.Status200OK;
                    objHelper.Message = "Data already available";
                    return Ok(objHelper);
                }

                await categoryRepository.Update(category);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = "Saved Successfully";
                objHelper.Data = category;
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
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.Edit })]
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
                await categoryRepository.ToogleStatus(id, isActive);
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

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Category", EnumPermission.Delete })]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await categoryRepository.Delete(id);
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