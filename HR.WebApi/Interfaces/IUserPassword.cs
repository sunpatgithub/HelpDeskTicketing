using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IUserPassword
    {        
        bool IsPasswordExist(int id, string password);

        bool VerifyPassword(string password, string cryptPass);

        string EncryptedPassword(string password); //Generate Encrypted Password

        IEnumerable<User_Password> GetPasswordByUserId(int id);

        bool InsertPassword(int id, string password);

    }
}
