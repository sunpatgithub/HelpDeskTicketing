using HR.WebApi.Common;
using HR.WebApi.DAL;
using HR.WebApi.Exceptions;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Linq;

namespace HR.WebApi.Repositories
{
    public class UserPasswordResetRepository : IUserPasswordReset
    {
        private string EmailDefaultPassword = AppSettings.EmailDefaultPassword;
        private int PasswordExpiryTime = AppSettings.PasswordExpiryTime;
        private int PasswordExpiryDays = AppSettings.PasswordExpiryDays;
        private string PasswordResetLink = AppSettings.PasswordResetLink;

        public TokenService objTokenService;
        private UserPasswordRepository objUser_PasswordRepo;

        private readonly ApplicationDbContext adbContext;
        public UserPasswordResetRepository(ApplicationDbContext applicationDbContext, TokenService tokenService, UserPasswordRepository user_PasswordRepository)
        {
            adbContext = applicationDbContext;
            objTokenService = tokenService;
            objUser_PasswordRepo = user_PasswordRepository;
        }

        public void Insert(int userId, string Link,string token_No)
        {
            try
            {
                Delete(userId);

                 var vList = new User_PasswordReset
                {
                    User_Id = userId,
                    PasswordReset_ExpiryDate = DateTime.Now.AddMinutes(30),
                    PasswordReset_Link = Link,
                    PasswordReset_Status = "Active",
                    Token_No = token_No,
                    AddedBy = userId,
                    AddedOn = DateTime.Now
                };
                adbContext.user_passwordreset.Add(vList);
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        public void Delete(int userId)
        {
            try
            {
                var vList = adbContext.user_passwordreset.Where(w => w.User_Id == userId).ToList();
                if(vList.Count() > 0)
                {
                    adbContext.user_passwordreset.RemoveRange(vList);
                    adbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            { 

                throw ex;
            }            
        }

        //public string EncryptLink(string link)
        //{
           // return CommonUtility.Crypt.EncryptString(link);
       // }

       public string DecryptLink(string link)
       {
           return CommonUtility.Crypt.DecryptString(link);
       }

        public string[] SplitLink(string link) // verify to from controller
        {
            string strSplitLink = DecryptLink(link.Replace(PasswordResetLink, String.Empty).Trim());
            string[] strLoginId = strSplitLink.Split("||");
            return strLoginId;
        }

        public bool VerifyLink(string login_Id, string token_No)
        {
            try
            {
                var vUserPwdReset = (from pr in adbContext.user_passwordreset
                                     join u in adbContext.users on pr.User_Id equals u.User_Id
                                     where u.Login_Id == login_Id && u.isActive == 1 && pr.Token_No == token_No
                                     select new { u.Login_Id, pr.PasswordReset_ExpiryDate }).FirstOrDefault();
                if (vUserPwdReset == null)
                    throw new UserNotFoundException("User Not Found");
                    
                    if (vUserPwdReset.PasswordReset_ExpiryDate > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        throw new PasswordExpiredException("Password reset date expired");
                        //return false;
                    }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
