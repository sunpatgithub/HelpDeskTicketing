using HR.CommonUtility;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HR.WebApi.Common
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext adbContext;

        public TokenService(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public void Add(User_Token entity)
        {
            try
            {
                string strToken = (entity.Token_No == String.Empty ? GenerateToken() : entity.Token_No);

                var vToken = adbContext.user_token.Where(w => w.User_Id == entity.User_Id).FirstOrDefault();
                if (vToken != null)
                {
                    vToken.Token_No = strToken;
                    vToken.Token_ExpiryDate = DateTime.Now.AddMinutes(30);
                    vToken.isActive = 1;
                    vToken.UpdatedOn = DateTime.Now;
                    vToken.UpdatedBy = entity.User_Id;
                    adbContext.user_token.Update(vToken);
                }
                else
                {
                    adbContext.Add<User_Token>(new User_Token
                    {
                        Token_ExpiryDate = DateTime.Now.AddMinutes(30),
                        isActive = 1,
                        User_Id = entity.User_Id,
                        Token_No = strToken,
                        AddedBy = entity.User_Id,
                        AddedOn = DateTime.Now
                    });
                }
                adbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(User_Token entity)
        {
            try
            {
                var vToken = adbContext.user_token.Where(w => w.User_Id == entity.User_Id).FirstOrDefault();
                if (vToken != null)
                {
                    vToken.Token_No = String.Empty;
                    vToken.Token_ExpiryDate = DateTime.Now.AddMinutes(-30);
                    vToken.isActive = 1;
                    vToken.UpdatedOn = DateTime.Now;
                    vToken.UpdatedBy = entity.User_Id;
                    adbContext.user_token.Update(vToken);
                    adbContext.SaveChanges();
                }
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
                var vToken = adbContext.user_token.Where(w => w.User_Id == userId).FirstOrDefault();
                if (vToken != null)
                {
                    vToken.Token_No = String.Empty;
                    vToken.Token_ExpiryDate = DateTime.Now.AddMinutes(-30);
                    vToken.isActive = 1;
                    vToken.UpdatedOn = DateTime.Now;
                    vToken.UpdatedBy = userId;
                    adbContext.user_token.Update(vToken);
                    adbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateToken()
        {
            string strToken = string.Empty;
            try
            {
                strToken = TokenManager.GenerateToken();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strToken;
        }

        public bool Verify(int userId = 0, string strTokenNo = "Default")
        {
            int intCount = 0;
            try
            {
                intCount = adbContext.user_token.Where(w => w.User_Id == userId && w.Token_No == strTokenNo && w.Token_ExpiryDate < DateTime.Now).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (intCount > 0 ? true : false);
        }

        public bool ValidateByUser(int userId)
        {
            int intCount = 0;
            try
            {
                intCount = adbContext.user_token.Where(w => w.User_Id == userId && w.Token_ExpiryDate < DateTime.Now).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (intCount > 0 ? true : false);
        }

        public string GetTokenByUserId(int userId)
        {
            string strTokenNo = String.Empty;
            try
            {
                strTokenNo = adbContext.user_token.Where(w => w.User_Id == userId).Select(s => s.Token_No).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strTokenNo;
        }


        public bool VerifyTokenByLoginId(string loginId, string tokenNo)
        {
            int intCount = 0;
            try
            {
                var vUserToken = (from t in adbContext.user_token
                                  join u in adbContext.users on t.User_Id equals u.User_Id
                                  where u.Login_Id == loginId && u.isActive == 1 && t.isActive == 1 && t.Token_No == tokenNo && t.Token_ExpiryDate > DateTime.Now
                                  select new User_Token
                                  {
                                      UserToken_Id = t.UserToken_Id,
                                      User_Id = t.User_Id,
                                      Token_No = t.Token_No,
                                      isActive = t.isActive,
                                      UpdatedBy = t.User_Id,
                                      UpdatedOn = DateTime.Now,
                                      Token_ExpiryDate = DateTime.Now.AddMinutes(30)
                                  }).FirstOrDefault();
                if (vUserToken != null)
                {
                    adbContext.Update(vUserToken);
                    //adbContext.Entry(vUserToken).State = EntityState.Modified;
                    adbContext.SaveChanges();
                    intCount = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (intCount > 0 ? true : false);
        }
    }
}
