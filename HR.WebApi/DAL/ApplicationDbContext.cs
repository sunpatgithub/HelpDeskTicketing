using HR.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HR.WebApi.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public void BeginTransaction(short CommandTimeout = 180)
        {
            base.Database.SetCommandTimeout(CommandTimeout);
            base.Database.BeginTransaction();
        }

        public void CommitTransaction(short CommandTimeout = 180)
        {
            base.Database.SetCommandTimeout(CommandTimeout);
            base.Database.CommitTransaction();
        }

        public void RollBackTransaction(short CommandTimeout = 180)
        {
            base.Database.SetCommandTimeout(CommandTimeout);
            base.Database.RollbackTransaction();
        }

        public DbSet<Company> company { get; set; }
        //public DbSet<Company_Contact> company_contact { get; set; }
        //public DbSet<Contract> contract { get; set; }
        public DbSet<Email_Config> email_config { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Designation> designation { get; set; }
        //public DbSet<Ethinicity> ethinicity { get; set; }
        //public DbSet<Marital_status> marital_status { get; set; }
        public DbSet<Employee> employee { get; set; }
        //public DbSet<Employee_Address> employee_address { get; set; }
        //public DbSet<Employee_Bank> employee_bank { get; set; }
        public DbSet<Employee_BasicInfo> employee_basicinfo { get; set; }
        //public DbSet<Employee_Contact> employee_contact { get; set; }
        //public DbSet<Employee_Contract> employee_contract { get; set; }
        //public DbSet<Employee_Document> employee_document { get; set; }
        //public DbSet<Employee_Emergency> employee_emergency { get; set; }
        //public DbSet<Employee_Reference> employee_reference { get; set; }
        //public DbSet<Employee_RightToWork> employee_righttowork { get; set; }
        //public DbSet<Employee_Probation> employee_probation { get; set; }
        //public DbSet<Employee_Resignation> employee_resignation { get; set; }
        //public DbSet<Employee_Salary> employee_salary { get; set; }
        //public DbSet<Probation> probation { get; set; }
        //public DbSet<Shift> shift { get; set; }
        //public DbSet<Site> site { get; set; }
        //public DbSet<Zone> zone { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<User_Password> user_password { get; set; }
        public DbSet<User_PasswordReset> user_passwordreset { get; set; }
        public DbSet<Job_Description> job_description { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Role_Permission> role_permission { get; set; }
        public DbSet<Role_Menu_Link_Old> role_menu_link_old { get; set; }
        public DbSet<User_Role> user_role { get; set; }
        public DbSet<Document> document { get; set; }
        public DbSet<User_Token> user_token { get; set; }
        public DbSet<UserLog> userlog { get; set; }
        public DbSet<AuditLog> auditlog { get; set; }
        public DbSet<Module> module { get; set; }
        public DbSet<Module_Permission> module_permission { get; set; }
        //public DbSet<RightToWork_Category> righttowork_category { get; set; }

        //public DbSet<Raf> raf { get; set; }
        //public DbSet<Raf_Approval> raf_approval { get; set; }
        //public DbSet<Raf_Approved> raf_approved { get; set; }
        //public DbSet<Raf_Rejected> raf_rejected { get; set; }

        //public DbSet<Candidate> candidate { get; set; }

        public DbSet<Task_Activity> task_activity { get; set; }
        //public DbSet<HistoryTable> historytable { get; set; }
        //public DbSet<Vacancy> vacancy { get; set; }
        //public DbSet<Relationship> relationship { get; set; }

        //public DbSet<Leave_Type> leave_type { get; set; }
        //public DbSet<Emp_Yearly_Leave> emp_yearly_leave { get; set; }
        //public DbSet<Emp_Leave> emp_leave { get; set; }
        //public DbSet<Emp_Leave_Approve_History> emp_leave_approve_history { get; set; }

        //public DbSet<Report_Group> report_group { get; set; }
        //public DbSet<Report_Group_Link> report_group_link { get; set; }
        //public DbSet<Report_Builder> report_builder { get; set; }


        public DbSet<Category> category { get; set; }
    }
}
