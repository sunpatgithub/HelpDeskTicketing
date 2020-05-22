using HR.WebApi.Common;
using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebApi.Repositories
{
    public class UserService<T> : IUserService<UserView>
    {
        private readonly ApplicationDbContext adbContext;
        private string EmailDefaultPassword = AppSettings.EmailDefaultPassword;
        private int PasswordExpiryTime = AppSettings.PasswordExpiryTime;
        private int PasswordExpiryDays = AppSettings.PasswordExpiryDays;
        private string PasswordResetLink = AppSettings.PasswordResetLink;

        private UserPasswordRepository userPasswordRepository;
        private UserPasswordResetRepository userPasswordResetRepository;
        public TokenService tokenService;

        public UserService(ApplicationDbContext applicationDbContext, TokenService tokenService, UserPasswordRepository userPasswordRepository, UserPasswordResetRepository userPasswordResetRepository)
        {
            adbContext = applicationDbContext;
            this.tokenService = tokenService;
            this.userPasswordRepository = userPasswordRepository;
            this.userPasswordResetRepository = userPasswordResetRepository;
        }

        //Get users data
        public IEnumerable<UserView> GetAll(int RecordLimit)
        {
            try
            {
                IEnumerable<UserView> vList;
                if (RecordLimit > 0)
                {
                    vList = (from user in adbContext.users
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,
                                 AddedBy = user.AddedBy,
                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).Take(RecordLimit).ToList();
                }
                else
                {
                    vList = (from user in adbContext.users
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,
                                 AddedBy = user.AddedBy,
                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).ToList();
                }
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get user by userId
        public UserView GetUserById(int id)
        {
            try
            {
              var vList = (from user in adbContext.users
                         join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                           join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                           where user.isActive == 1 && comp.isActive == 1 && user.User_Id == id
                         select new UserView
                         {
                             User_Id = user.User_Id,
                             Company_Id = user.Company_Id,
                             Emp_Id = user.Emp_Id,
                             Login_Id = user.Login_Id,
                             User_Type = user.User_Type,
                             Email = user.Email,
                             Password = user.Password,
                             PasswordExpiryDate = user.PasswordExpiryDate,
                             Attempted = user.Attempted,
                             isLocked = user.isLocked,
                             LockExpiryTime = user.LockExpiryTime,
                             isActive = user.isActive,
                             AddedBy = user.AddedBy,
                             Company_Name = comp.Company_Name,
                             FirstName = emp.FirstName,
                             MiddleName = emp.MiddleName,
                             LastName = emp.LastName
                         }).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Get Data Empty");
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get user by LoginId
        public UserView GetUserByLoginId(string login_Id)
        {
            try
            {
                var vList = (from user in adbContext.users
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             where user.isActive == 1 && comp.isActive == 1 && user.Login_Id == login_Id
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,
                                 AddedBy = user.AddedBy,
                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Get Data Empty");
                return vList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateUser(UserView entity)
        {
            adbContext.BeginTransaction();
            try
            {
                if(entity.Emp_Id == 0)
                    throw new RecoredNotFoundException("Please Enter Emp_Id");

                if (string.IsNullOrEmpty(entity.Password))
                    throw new RecoredNotFoundException("Please Enter Password");

                if (string.IsNullOrEmpty(entity.Login_Id))
                    throw new RecoredNotFoundException("Please Enter Login_Id");

                var vList = new User
                {
                    Company_Id = entity.Company_Id,
                    Emp_Id = entity.Emp_Id,
                    Login_Id = entity.Login_Id,
                    User_Type = entity.User_Type,
                    Email = entity.Email,
                    Password = userPasswordRepository.EncryptedPassword(entity.Password),
                    PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryTime),
                    Attempted = 1,
                    isLocked = 0,
                    LockExpiryTime = entity.LockExpiryTime,
                    isActive = 1,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now
                };

                adbContext.users.Add(vList);
                adbContext.SaveChanges();

                userPasswordRepository.InsertPassword(entity.User_Id, entity.Password);

                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public void UpdateUser(UserView entity)
        {
            adbContext.BeginTransaction();
            try
            {
                //if (!userPasswordRepository.InsertPassword(entity.User_Id, entity.Password))
                //    throw new PasswordFoundInHistoryException("Please Enter New Password");

                var vList = adbContext.users.Where(w => w.User_Id == entity.User_Id).FirstOrDefault();
                if (vList == null)
                    throw new RecoredNotFoundException("Data Not Available");

                vList.Company_Id = entity.Company_Id;
                //vList.Emp_Id = entity.Emp_Id;
                //vList.Login_Id = entity.Login_Id;
                vList.User_Type = entity.User_Type;
                vList.Email = entity.Email;
                //vList.Password = userPasswordRepository.EncryptedPassword(entity.Password);
                //vList.PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryTime);
                //vList.Attempted = entity.Attempted;
                vList.isLocked = entity.isLocked;
                vList.LockExpiryTime = entity.LockExpiryTime;
                vList.isActive = entity.isActive;
                vList.UpdatedBy = entity.UpdatedBy;
                vList.UpdatedOn = DateTime.Now;

                adbContext.users.Update(vList);
                adbContext.SaveChanges();

                adbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                adbContext.RollBackTransaction();
                throw ex;
            }
        }

        public bool UserExist(UserView entity)
        {
            try
            {
                int intCount = 0;
                if (entity.User_Id > 0)
                    intCount = adbContext.users.Where(w => w.User_Id != entity.User_Id && w.Login_Id == entity.Login_Id).Count();
                else
                    intCount = adbContext.users.Where(w => w.Login_Id == entity.Login_Id).Count();
                return (intCount > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserView> FindUserById(int id)
        {
            try
            {
                var vList = (from user in adbContext.users.Where(w => w.User_Id.ToString().Contains(id.ToString()))
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             where user.isActive == 1 && comp.isActive == 1
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,

                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).ToList();

                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return vList;

                //return adbContext.users.Where(w => w.User_Id.ToString().Contains(id.ToString())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserView> FindUserByLogInId(string login_Id)
        {
            try
            {
                var vList = (from user in adbContext.users.Where(w => w.Login_Id.ToString().Contains(login_Id.ToString()))
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             where user.isActive == 1 && comp.isActive == 1
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,

                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).ToList();

                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return vList;
                //return adbContext.users.Where(w => w.Login_Id.Contains(login_Id)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Set Active User
        public void ActivateUser(int id)
        {
            try
            {
                var vList = adbContext.users.Where(w => w.User_Id == id).FirstOrDefault();
                if (vList == null)
                    throw new UserNotFoundException("User Not Found");
                vList.isActive = 1;
                adbContext.users.Update(vList);
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Set Disable User
        public void InactivateUser(int id)
        {
            try
            {
                var vRolecheck = adbContext.user_role.Where(w => w.Role_Id == 1).ToList();

                if (vRolecheck.Count() > 1)
                {
                    var vList = adbContext.users.Where(w => w.User_Id == id).FirstOrDefault();
                    if (vList == null)
                        throw new UserNotFoundException("User Not Found");
                    vList.isActive = 0;
                    adbContext.users.Update(vList);
                    adbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete User
        public void Delete(int id)
        {
            try
            {
                var vList = adbContext.users.Where(w => w.User_Id == id).FirstOrDefault();

                if (vList == null)
                    throw new UserNotFoundException("User Not Found");

                adbContext.users.Remove(vList);
                adbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //User Change Password
        public void ChangePassword(int user_Id, string oldPassword, string newPassword)
        {
            try
            {
                User vList = adbContext.users.Where(w => w.User_Id == user_Id).FirstOrDefault();
                if (vList == null)
                    throw new UserNotFoundException("User Not Found");

                if (!userPasswordRepository.VerifyPassword(oldPassword, vList.Password))
                    throw new OldPasswordDoesNotMatchException("Old Password Does Not Match. Please Enter New Password.");

                if (!userPasswordRepository.InsertPassword(user_Id, newPassword))
                    throw new PasswordFoundInHistoryException("Password Already Exist. Please Enter New Password.");

                vList.Password = userPasswordRepository.EncryptedPassword(newPassword);
                vList.UpdatedOn = DateTime.Now;

                adbContext.users.Update(vList);
                adbContext.SaveChanges();

                // remove token
                tokenService.Delete(user_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Admin change password of any user
        public void AdminChangePassword(string login_Id, string password)
        {
            try
            {
                User user = adbContext.users.Where(w => w.Login_Id == login_Id).FirstOrDefault();
                user.Password = userPasswordRepository.EncryptedPassword(password);
                user.PasswordExpiryDate = DateTime.Now;

                user.UpdatedOn = DateTime.Now;

                adbContext.users.Update(user);
                adbContext.SaveChanges();
                userPasswordRepository.InsertPassword(user.User_Id, password);
                //  remove token
                tokenService.Delete(user.User_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Find Paginated 
        public IEnumerable<UserView> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            try
            {

                IEnumerable<UserView> vList;
                if (String.IsNullOrEmpty(searchValue))
                {
                    //Find User with Paging
                    vList = (from user in adbContext.users
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             where user.isActive == 1 && comp.isActive == 1
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,

                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    //Find Department with Paging & Searching
                    vList = (from user in adbContext.users.Where(w => new[] { Convert.ToString(w.User_Id), w.Login_Id, w.User_Type, w.Email }.Any(a => a.Contains(searchValue)))
                             join comp in adbContext.company on user.Company_Id equals comp.Company_Id
                             join emp in adbContext.employee_basicinfo on user.Emp_Id equals emp.Emp_Id
                             where user.isActive == 1 && comp.isActive == 1
                             select new UserView
                             {
                                 User_Id = user.User_Id,
                                 Company_Id = user.Company_Id,
                                 Emp_Id = user.Emp_Id,
                                 Login_Id = user.Login_Id,
                                 User_Type = user.User_Type,
                                 Email = user.Email,
                                 Password = user.Password,
                                 PasswordExpiryDate = user.PasswordExpiryDate,
                                 Attempted = user.Attempted,
                                 isLocked = user.isLocked,
                                 LockExpiryTime = user.LockExpiryTime,
                                 isActive = user.isActive,

                                 Company_Name = comp.Company_Name,
                                 FirstName = emp.FirstName,
                                 MiddleName = emp.MiddleName,
                                 LastName = emp.LastName
                             }).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                if (vList == null || vList.Count() == 0)
                    throw new RecoredNotFoundException("Get Data Empty");

                return vList;

                //if (!String.IsNullOrEmpty(searchValue))
                //    return adbContext.users.Where(w => new[] { Convert.ToString(w.User_Id), w.Login_Id, w.User_Type, w.Email }.Any(a => a.Contains(searchValue))).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                //else
                //    return adbContext.users.Where(W => W.Login_Id.Contains(searchValue)).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //User Forgot Password - loginId
        public void ForgotPassword(string loginId)
        {
            try
            {
                var vList = GetUserByLoginId(loginId);
                if (vList == null)
                    throw new UserNotFoundException("User Not Found");

                string strToken_No = tokenService.GenerateToken();
                string strLink = PasswordResetLink + "Login_Id=" + vList.Login_Id + "&Token_No=" + strToken_No;
                userPasswordResetRepository.Insert(vList.User_Id, strLink, strToken_No);

                #region Sent link to User 

                Common.Email vEmailConfiguration = new Common.Email(adbContext);
                var usermodel = vEmailConfiguration.GetEmailBody(vList.Email, String.Empty, String.Empty, "Reset Password Link", "Please find below link for your reset password  " + strLink);
                vEmailConfiguration.SendEmail(usermodel, vEmailConfiguration.GetEmailConfiguration(vList.Company_Id));

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Reset password
        public bool ResetPassword(string loginId, string password)
        {
            try
            {
                var vList = adbContext.users.Where(w => w.Login_Id == loginId).FirstOrDefault();
                if (vList == null)
                    throw new UserNotFoundException("User Not Found");

                //verfiy last passwords
                if (userPasswordRepository.IsPasswordExist(vList.User_Id, password))
                    throw new PasswordFoundInHistoryException("Please Enter New Password");

                vList.Password = userPasswordRepository.EncryptedPassword(password);
                vList.PasswordExpiryDate = DateTime.Now.AddDays(PasswordExpiryDays);
                vList.UpdatedBy = vList.User_Id;
                vList.UpdatedOn = DateTime.Now;

                adbContext.users.Update(vList);
                adbContext.SaveChanges();

                //remove token
                tokenService.Delete(vList.User_Id);

                //remove link
                userPasswordResetRepository.Delete(vList.User_Id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Login
        public User_Response SignIn(User_Request userRequest)
        {
            User_Response objUserResponse = new User_Response();
            try
            {
                var vList = GetUserByLoginId(userRequest.Login_Id);
                if (vList == null && vList.isActive != 1)
                    throw new UserNotFoundException("User Not Found");

                objUserResponse.isAvailable = true;

                if (!userPasswordRepository.VerifyPassword(userRequest.Password, vList.Password.ToString()))
                    throw new OldPasswordDoesNotMatchException("Old Password Does Not Match. Please Enter New Password.");

                objUserResponse.User_Id = vList.User_Id;
                objUserResponse.Login_Id = vList.Login_Id;
                objUserResponse.Company_Id = vList.Company_Id;

                if (vList.PasswordExpiryDate <= DateTime.Now)   //need one check for temporary password
                    objUserResponse.isExpired = true;
                else
                    objUserResponse.isExpired = false;

                objUserResponse.Token_No = tokenService.GenerateToken();
                tokenService.Add(new User_Token
                {
                    User_Id = vList.User_Id,
                    Token_No = objUserResponse.Token_No,
                    AddedBy = vList.User_Id
                });
                objUserResponse.isVerify = true;

                //users role list
                objUserResponse.UserRoles = GetUserActiveRolesList(vList.User_Id);
            }
            catch (Exception ex)
            {
                objUserResponse.Reset();
                throw ex;
            }
            finally
            {
                //create UserLog
                AddUserLog(userRequest.Ip_Address, userRequest.Host_Name, userRequest.User_Id, userRequest.Login_Id);
            }
            return objUserResponse;
        }

        private IEnumerable<UserRolesList> GetUserActiveRolesList(int intUserId)
        {
            dynamic dList;
            try
            {
                dList = (from ur in adbContext.user_role.AsQueryable()
                         join rp in adbContext.role_permission.AsQueryable() on ur.Role_Id equals rp.Role_Id
                         join mp in adbContext.module_permission.AsQueryable() on rp.Module_Per_Id equals mp.Id
                         join m in adbContext.module.AsQueryable() on mp.Module_Id equals m.Id
                         where ur.User_Id == intUserId && ur.isActive == 1 && rp.isActive == 1 && mp.isActive == 1 && m.isActive == 1
                         select new UserRolesList
                         {
                             Role_Id = rp.Role_Id,
                             Module_Per_Id = mp.Id,
                             Module_Id = m.Id,
                             Module_DisplayName = m.DisplayName,
                             Module_Name = m.Name,
                             Module_Permission = mp.Name,
                             Module_Url = m.Url
                         }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dList;
        }

        public void AddUserLog(string Ip_Address, string Host_Name, int? User_Id, string Login_Id)
        {
            try
            {
                adbContext.Add<UserLog>(new UserLog { AddedOn = DateTime.Now, Ip_Address = Ip_Address, Host_Name = Host_Name, User_Id = User_Id, User_Name = Login_Id });
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SignOut(int id)
        {
            try
            {
                tokenService.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Login

        #region Finalizer with Disposed

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) adbContext.Dispose();
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this);
        }
        #endregion Finalizer with Disposed
    }
}
