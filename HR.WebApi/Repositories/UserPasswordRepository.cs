using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HR.WebApi.Exceptions;

namespace HR.WebApi.Repositories
{
    public class UserPasswordRepository : IUserPassword
    {
        private readonly ApplicationDbContext adbContext;

        private int PastPasswordVerify = AppSettings.PastPasswordVerify;

        public UserPasswordRepository(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        //Create new user_password & verify 5 password only which are not same
        public bool InsertPassword(int userId, string password)
        {
            try
            {
                if (!IsPasswordExist(userId, password))
                {
                    var vList = GetPasswordByUserId(userId);
                    if (vList.Count() > 0)
                    {
                        var vCount = vList.OrderByDescending(w => w.AddedOn).Skip(PastPasswordVerify);
                        adbContext.user_password.RemoveRange(vCount);
                        adbContext.SaveChanges();
                    }
                    var vUserPassword = new User_Password
                    {
                        User_Id = userId,
                        Password = EncryptedPassword(password),
                        AddedBy = userId,
                        AddedOn = DateTime.Now
                    };
                    adbContext.user_password.Add(vUserPassword);
                    adbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool VerifyPassword(string password, string cryptPass)
        {
            return CommonUtility.Crypt.Verify(password, cryptPass);
        }

        //Verify password should not match from past 5 old passwords
        public bool IsPasswordExist(int user_Id, string newPassword)
        {
            bool blnExist = false;
            var vList = GetPasswordByUserId(user_Id);
            if (vList.Count() > 0)
            {
                foreach (var item in vList)
                {
                    if (VerifyPassword(newPassword, item.Password))
                    {
                      return  blnExist = true;
                        //throw new PasswordFoundInHistoryException("Password Already Available");
                    }
                    else
                        blnExist = false;
                }
            }
            else
            {
                blnExist = false;
                //Here exception not required, because any password not exist
                //throw new Exception("Password Not Available");
            }
            return blnExist;
        }

        public IEnumerable<User_Password> GetPasswordByUserId(int id)
        {
            try
            {
                return adbContext.user_password.Where(w => w.User_Id == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptedPassword(string password)
        {
            return CommonUtility.Crypt.GetPassword(password);
        }
    }
}
