using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User_Response
    {
        [Key]
        public int? User_Id { get; set; }
        public string Login_Id { get; set; }
        public string Token_No { get; set; }
        public string EmpName { get; set; }
        public int? Company_Id { get; set; }

        public bool isExpired { get; set; } = false; //Password Verified But User date Expired
        public bool isTemporary { get; set; } = false; //Temporary User
        //public bool isActive { get; set; } = false; //User available in datbase but disabled.
        public bool isAvailable { get; set; } = false; //User available in database ?
        public bool isVerify { get; set; } = false; //User password verified ?

        public IEnumerable<UserRolesList> UserRoles { get; set; }

        protected internal void Reset()
        {
            this.User_Id = this.Company_Id = null;
            this.Login_Id = this.Token_No = string.Empty;
            this.isExpired = this.isTemporary = this.isAvailable = this.isVerify = false;
            this.UserRoles = null;
        }
    }
}