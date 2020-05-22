using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User_Request
    {
        [Key]
        public int? User_Id { get; set; }

        [Required]
        public string Login_Id { get; set; }

        [Required]
        public string Password { get; set; }

        public string App_Id { get; set; }
        
        public string Ip_Address { get; set; }
        
        public string Host_Name { get; set; }
    }
}
