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


namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]
    //[ServiceFilter(typeof(ActionFilters.TokenVerify))]
    public class Employee_BasicInfoController : ControllerBase
    {
        public IEmployee_BasicInfo<Employee_BasicInfo> employee_BasicInfoRepository { get; set; }
        public Employee_BasicInfoController(IEmployee_BasicInfo<Employee_BasicInfo> commonRepository)
        {
            employee_BasicInfoRepository = commonRepository;
        }

        // GET ALL Data with Record Limit or without Record Limit
        // GET: api/Employee_BasicInfo/GetAll or api/Employee_BasicInfo/GetAll/100
        [HttpGet()]
        [HttpGet("{recordLimit}")]
        //[TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "Employee_BasicInfo", EnumPermission.ViewAll })]
        public async Task<IActionResult> GetAll(int recordLimit)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                var vList = await employee_BasicInfoRepository.GetAll(recordLimit);

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


    }
}