using HR.CommonUtility;
using HR.WebApi.Common;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]

    public class UserController : ControllerBase
    {
        private IUserService<UserView> userRepository { get; set; }
        private IUserPasswordReset userPasswordResetRepo { get; set; }

        public UserController(IUserService<UserView> commonRepository, IUserPasswordReset userPasswordResetRepo)
        {
            userRepository = commonRepository;
            this.userPasswordResetRepo = userPasswordResetRepo;
        }

        //Get All User Data
        [HttpGet]
        [HttpGet("{recordLimit}")]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.ViewAll })]
        public IActionResult GetAll(int RecordLimit)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                var vList = userRepository.GetAll(RecordLimit);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Get Successfully";
                objResHelper.Data = vList;

                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.View })]
        public IActionResult Get(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                var vList = userRepository.GetUserById(id);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Get Successfully";
                objResHelper.Data = vList;

                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        // GET: api/User/
        //[HttpGet] old
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.View })]
        public IActionResult FindPagination(Pagination pagination)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                ReturnBy<UserView> vList = new ReturnBy<UserView>();
                vList.list = userRepository.FindPaginated(pagination.PageIndex, pagination.PageSize, pagination.CommonSearch);
                if (vList.list.Count() == 0)
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Get Empty Data";
                }
                else
                {
                    //vList.RecordCount = userRepository.RecordCount(pagination.CommonSearch);
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Get Successfully";
                    objResHelper.Data = vList;
                }
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //New User Creation
        // POST: api/User
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.Add })]
        public IActionResult Add(UserView user)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency;
                objResHelper.Message = ModelException.Errors(ModelState);
                return BadRequest(objResHelper);
            }

            try
            {
                if (userRepository.UserExist(user))
                {
                    objResHelper.Status = StatusCodes.Status422UnprocessableEntity;
                    objResHelper.Message = "Data already available";
                    return UnprocessableEntity(objResHelper);
                }
                else
                {
                    userRepository.CreateUser(user);
                    objResHelper.Data = user;
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Saved Successfully";
                    return Ok(objResHelper);
                }
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Edit User
        // PUT: api/User/5
        [HttpPut]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.Edit })]
        public IActionResult Edit(UserView user)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency; ;
                objResHelper.Message = ModelException.Errors(ModelState);
                return BadRequest(objResHelper);
            }

            try
            {
                if (userRepository.UserExist(user))
                {
                    objResHelper.Status = StatusCodes.Status208AlreadyReported;
                    objResHelper.Message = "Data already available";
                    return Ok(objResHelper);
                }

                userRepository.UpdateUser(user);
                objResHelper.Data = user;
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";

                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //User StatusChange - active/inactive
        [HttpPut("{id},{isActive}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.Edit })]
        public IActionResult StatusChange(int id, short isActive)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                if (isActive == 1)
                    userRepository.ActivateUser(id);
                else
                    userRepository.InactivateUser(id);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Inactivate User
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        [TypeFilter(typeof(ActionFilters.RolesValidate), Arguments = new object[] { "User", EnumPermission.Delete })]
        public IActionResult Delete(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                int currentUser_Id = Convert.ToInt32(Request.Headers["USER_ID"]);

                if (currentUser_Id == id)
                    throw new Exception("User Have Not Permission To Delete Itself.");

                userRepository.InactivateUser(id);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //User Password change
        [HttpPut("{user_Id},{oldPassword},{newPassword}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        public IActionResult ChangePassword(int user_Id, string oldPassword, string newPassword)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.ChangePassword(user_Id, oldPassword, newPassword);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";

                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Admin forcefully reset password with custom password
        [HttpPost("{login_Id},{password}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        public IActionResult AdminChangePassword(string login_Id, string password)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.AdminChangePassword(login_Id, password);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Forgot Password
        [AllowAnonymous]
        [HttpPost("{login_Id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        public IActionResult ForgotPassword(string login_Id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.ForgotPassword(login_Id);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Verify Link
        [AllowAnonymous]
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        public IActionResult VerifyLink(VerifyLink verifyLink)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                bool blnCheck = userPasswordResetRepo.VerifyLink(verifyLink.login_Id, verifyLink.token_No);
                if (blnCheck)
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Verified Successfully";
                    return Ok(objResHelper);
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status400BadRequest;
                    objResHelper.Message = "Failed Verify Link";
                    return BadRequest(objResHelper);
                }
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Reset Password
        [AllowAnonymous]
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        public IActionResult ResetPassword(VerifyLink verifyLink)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                if (userPasswordResetRepo.VerifyLink(verifyLink.login_Id, verifyLink.token_No))
                {
                    userRepository.ResetPassword(verifyLink.login_Id, verifyLink.password);
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Saved Successfully";
                    return Ok(objResHelper);
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status400BadRequest;
                    objResHelper.Message = "Failed Verify Link";
                    return BadRequest(objResHelper);
                }
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        public IActionResult SignIn(User_Request user_Request)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency;
                objResHelper.Message = "User Name / Password Not Available";
                return BadRequest(objResHelper);
            }

            try
            {
                var vList = userRepository.SignIn(user_Request);
                if (vList != null && vList.User_Id > 0)
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Request Completed Successfully";
                    objResHelper.Data = vList;
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status204NoContent;
                    objResHelper.Message = "Data Not Available";
                }
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        [HttpPost("{id}")]
        [ServiceFilter(typeof(ActionFilters.AuditLog))]
        [ServiceFilter(typeof(ActionFilters.TokenVerify))]
        public IActionResult SignOut(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.SignOut(id);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Sign Out";
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }
    }
}
