namespace HR.WebApi.Model
{
    public class UserRolesList
    {
        public int Role_Id { get; set; }
        public int Module_Id { get; set; }
        public int Module_Per_Id { get; set; }

        public string Module_Name { get; set; }
        public string Module_DisplayName { get; set; }
        public string Module_Permission { get; set; }
        public string Module_Url { get; set; }
    }
}
