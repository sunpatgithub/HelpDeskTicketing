using HR.WebApi.ActionFilters;
using HR.WebApi.Common;
using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using HR.WebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Xml;

namespace HR.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            IConfigurationRoot objConfig = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            #region Connection
            string strDbType = objConfig.GetValue<string>("DataBase:ConnectionType");

            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySQL(objConfig.GetConnectionString(strDbType));
                optionsBuilder.UseApplicationServiceProvider(serviceProvider);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            #endregion Connection

            #region All Repository List

            //services.AddTransient(typeof(IPaginated<Raf_ApprovalView>), typeof(Raf_ApprovalRepository<Raf_ApprovalView>));
            //services.AddTransient(typeof(IPaginated<Raf_ApprovedView>), typeof(Raf_ApprovedRepository<Raf_ApprovedView>));
            //services.AddTransient(typeof(IPaginated<Raf_RejectedView>), typeof(Raf_RejectedRepository<Raf_RejectedView>));
            //services.AddTransient(typeof(ICommonQuery<SiteView>), typeof(SiteRepository<SiteView>));
            //services.AddTransient(typeof(ICommonQuery<ZoneView>), typeof(ZoneRepository<ZoneView>));
            services.AddTransient(typeof(ICommonQuery<DepartmentView>), typeof(DepartmentRepository<DepartmentView>));
            services.AddTransient(typeof(ICommonQuery<DesignationView>), typeof(DesignationRepository<DesignationView>));
            //services.AddTransient(typeof(ICommonQuery<Job_DiscriptionView>), typeof(Job_DescriptionRepository<Job_DiscriptionView>));
            //services.AddTransient(typeof(ICommonQuery<ProbationView>), typeof(ProbationRepository<ProbationView>));
            services.AddTransient(typeof(ICommonQuery<Module_PermissionView>), typeof(Module_PermissionRepository<Module_PermissionView>));
            //services.AddTransient(typeof(ICommonQuery<ShiftView>), typeof(ShiftRepository<ShiftView>));
            services.AddTransient(typeof(ICommonQuery<Email_ConfigView>), typeof(EmailConfigRepository<Email_ConfigView>));
            services.AddTransient(typeof(ICommonQuery<User_RoleView>), typeof(User_RoleRepository<User_RoleView>));
            //services.AddTransient(typeof(ICommonQuery<ContractView>), typeof(ContractRepository<ContractView>));
            //services.AddTransient(typeof(ICommonQuery<VacancyView>), typeof(VacancyRepository<VacancyView>));

            services.AddTransient(typeof(ICommonRepository<DepartmentView>), typeof(DepartmentRepository<DepartmentView>));
            //services.AddTransient(typeof(ICommonRepository<ShiftView>), typeof(ShiftRepository<ShiftView>));
            //services.AddTransient(typeof(ICommonRepository<SiteView>), typeof(SiteRepository<SiteView>));
            //services.AddTransient(typeof(ICommonRepository<ZoneView>), typeof(ZoneRepository<ZoneView>));
            //services.AddTransient(typeof(ICommonRepository<ContractView>), typeof(ContractRepository<ContractView>));
            services.AddTransient(typeof(ICommonRepository<DesignationView>), typeof(DesignationRepository<DesignationView>));
            //services.AddTransient(typeof(ICommonRepository<ProbationView>), typeof(ProbationRepository<ProbationView>));
            //services.AddTransient(typeof(ICommonRepository<Ethinicity>), typeof(EthinicityRepository<Ethinicity>));
            //services.AddTransient(typeof(ICommonRepository<Marital_status>), typeof(Marital_StatusRepository<Marital_status>));
            //services.AddTransient(typeof(Job_DescriptionRepository<Job_DiscriptionView>));
            //services.AddTransient(typeof(ICommonRepository<Job_DiscriptionView>), typeof(Job_DescriptionRepository<Job_DiscriptionView>));
            services.AddTransient(typeof(ICommonRepository<Email_ConfigView>), typeof(EmailConfigRepository<Email_ConfigView>));
            //services.AddTransient(typeof(ICommonRepository<Document>), typeof(DocumentRepository<Document>));
            services.AddTransient(typeof(ICommonRepository<Module_PermissionView>), typeof(Module_PermissionRepository<Module_PermissionView>));
            //services.AddTransient(typeof(ICommonRepository<VacancyView>), typeof(VacancyRepository<VacancyView>));

            //services.AddTransient(typeof(ICommonRepository<RightToWork_Category>), typeof(RightToWork_CategoryRepository<RightToWork_Category>));
            //services.AddTransient(typeof(ICommonRepository<Relationship>), typeof(RelationshipRepository<Relationship>));

            //services.AddTransient(typeof(ICommonRepository<Raf>), typeof(RafRepository<Raf>));
            //services.AddTransient(typeof(ICommonRepository<Raf_ApprovalView>), typeof(Raf_ApprovalRepository<Raf_ApprovalView>));
            //services.AddTransient(typeof(ICommonRepository<Raf_ApprovedView>), typeof(Raf_ApprovedRepository<Raf_ApprovedView>));
            //services.AddTransient(typeof(ICommonRepository<Raf_RejectedView>), typeof(Raf_RejectedRepository<Raf_RejectedView>));

            services.AddTransient(typeof(TokenService));
            //services.AddTransient(typeof(IDocuments), typeof(Documents));
            //services.AddTransient(typeof(DocumentRepository<Document>));
            //services.AddTransient(typeof(ICommonRepository<Candidate>), typeof(CandidateRepository<Candidate>));
            //services.AddTransient(typeof(ICommonRepository<Company>), typeof(CompanyRepository<Company>));
            //services.AddTransient(typeof(ICommonRepository<Company_Contact>), typeof(Company_ContactRepository<Company_Contact>));
            //services.AddTransient(typeof(Company_ContactRepository<Company_Contact>));


            services.AddTransient(typeof(IUserPassword), typeof(UserPasswordRepository));
            services.AddTransient(typeof(IUserPasswordReset), typeof(UserPasswordResetRepository));
            services.AddTransient(typeof(IUserService<UserView>), typeof(UserService<UserView>));
            services.AddTransient(typeof(UserPasswordRepository));
            services.AddTransient(typeof(UserPasswordResetRepository));
            //services.AddTransient(typeof(CompanyRepository<Company>));

            //services.AddTransient(typeof(ICommonRepository<HistoryTable>), typeof(HistoryTableRepository<HistoryTable>));
            //services.AddTransient(typeof(IPaginated<HistoryTable>), typeof(HistoryTableRepository<HistoryTable>));

            //services.AddTransient(typeof(IReportBuilder<Report_Group>), typeof(Report_GroupRepository<Report_Group>));
            //services.AddTransient(typeof(IReportBuilder<Report_Group_LinkView>), typeof(Report_Group_LinkRepository<Report_Group_LinkView>));
            //services.AddTransient(typeof(IReportBuilder<Report_Builder>), typeof(Report_BuilderRepository<Report_Builder>));
            //services.AddTransient(typeof(IReportBuilder<Model.AuditLog>), typeof(AuditLogRepository<Model.AuditLog>));


            #region Employee Repository List

            //services.AddTransient(typeof(EmployeeDetailService));
            services.AddTransient(typeof(EmployeeRepository<Employee>));
            //services.AddTransient(typeof(Employee_AddressRepository<Employee_Address>));
            //services.AddTransient(typeof(Employee_BankRepository<Employee_Bank>));
            //services.AddTransient(typeof(Employee_BasicInfoRepository<Employee_BasicInfo>));
            //services.AddTransient(typeof(Employee_ContactRepository<Employee_Contact>));
            //services.AddTransient(typeof(Employee_ContractRepository<Employee_Contract>));
            //services.AddTransient(typeof(Employee_DocumentRepository<Employee_Document>));
            //services.AddTransient(typeof(Employee_EmergencyRepository<Employee_Emergency>));
            //services.AddTransient(typeof(Employee_ProbationRepository<Employee_Probation>));
            //services.AddTransient(typeof(Employee_ReferenceRepository<Employee_Reference>));
            //services.AddTransient(typeof(Employee_ResignationRepository<Employee_Resignation>));
            //services.AddTransient(typeof(Employee_RightToWorkRepository<Employee_RightToWork>));
            //services.AddTransient(typeof(Employee_SalaryRepository<Employee_Salary>));

            services.AddTransient(typeof(ICommonRepository<Employee>), typeof(EmployeeRepository<Employee>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Address>), typeof(Employee_AddressRepository<Employee_Address>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Bank>), typeof(Employee_BankRepository<Employee_Bank>));
            //services.AddTransient(typeof(ICommonRepository<Employee_BasicInfo>), typeof(Employee_BasicInfoRepository<Employee_BasicInfo>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Contact>), typeof(Employee_ContactRepository<Employee_Contact>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Contract>), typeof(Employee_ContractRepository<Employee_Contract>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Document>), typeof(Employee_DocumentRepository<Employee_Document>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Emergency>), typeof(Employee_EmergencyRepository<Employee_Emergency>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Probation>), typeof(Employee_ProbationRepository<Employee_Probation>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Reference>), typeof(Employee_ReferenceRepository<Employee_Reference>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Resignation>), typeof(Employee_ResignationRepository<Employee_Resignation>));
            //services.AddTransient(typeof(ICommonRepository<Employee_RightToWork>), typeof(Employee_RightToWorkRepository<Employee_RightToWork>));
            //services.AddTransient(typeof(ICommonRepository<Employee_Salary>), typeof(Employee_SalaryRepository<Employee_Salary>));


            //services.AddTransient(typeof(ICommonRepository<Employee_DocumentView>), typeof(Employee_DocumentRepository<Employee_DocumentView>));

            //services.AddTransient(typeof(IPaginated<Employee_AddressView>), typeof(Employee_AddressRepository<Employee_AddressView>));
            //services.AddTransient(typeof(IPaginated<Employee_BankView>), typeof(Employee_BankRepository<Employee_BankView>));
            //services.AddTransient(typeof(IPaginated<Employee_BasicInfoView>), typeof(Employee_BasicInfoRepository<Employee_BasicInfoView>));
            //services.AddTransient(typeof(IPaginated<Employee_ContactView>), typeof(Employee_ContactRepository<Employee_ContactView>));
            //services.AddTransient(typeof(IPaginated<Employee_ContractView>), typeof(Employee_ContractRepository<Employee_ContractView>));
            //services.AddTransient(typeof(IPaginated<Employee_DocumentView>), typeof(Employee_DocumentRepository<Employee_DocumentView>));
            //services.AddTransient(typeof(IPaginated<Employee_EmergencyView>), typeof(Employee_EmergencyRepository<Employee_EmergencyView>));
            //services.AddTransient(typeof(IPaginated<Employee_ProbationView>), typeof(Employee_ProbationRepository<Employee_ProbationView>));
            //services.AddTransient(typeof(IPaginated<Employee_ReferenceView>), typeof(Employee_ReferenceRepository<Employee_ReferenceView>));
            //services.AddTransient(typeof(IPaginated<Employee_ResignationView>), typeof(Employee_ResignationRepository<Employee_ResignationView>));
            //services.AddTransient(typeof(IPaginated<Employee_RightToWorkView>), typeof(Employee_RightToWorkRepository<Employee_RightToWorkView>));
            //services.AddTransient(typeof(IPaginated<Employee_Salary>), typeof(Employee_SalaryRepository<Employee_Salary>));
            services.AddTransient(typeof(IPaginated<Employee>), typeof(EmployeeRepository<Employee>));
            //services.AddTransient(typeof(IPaginated<EmployeeDetailView>), typeof(EmployeeDetailService));
            #endregion Employee Repository List

            services.AddTransient(typeof(ICommonRepository<User_RoleView>), typeof(User_RoleRepository<User_RoleView>));
            services.AddTransient(typeof(ICommonRepository<Roles>), typeof(RolesRepository<Roles>));
            services.AddTransient(typeof(ICommonRepository<Role_Permission>), typeof(Role_PermissionRepository<Role_Permission>));
            services.AddTransient(typeof(ICommonRepository<Model.Module>), typeof(ModuleRepository<Model.Module>));

            #region Help Desk Repository List
            services.AddTransient(typeof(ICommonRepository<Category>), typeof(CategoryRepository<Category>));
            services.AddTransient(typeof(ICommonRepository<Ticket>), typeof(TicketRepository<Ticket>));
            //services.AddTransient(typeof(ITicketLog<TicketLog>), typeof(TicketLogRepository<TicketLog>));
            services.AddTransient(typeof(ITicket<Ticket>), typeof(TicketRepository<Ticket>));
            services.AddTransient(typeof(ITicketLog<TicketLog>), typeof(TicketLogRepository<TicketLog>));
            services.AddTransient(typeof(IFullTicket<FullTicket>), typeof(FullTicketRepository<FullTicket>));
            services.AddTransient(typeof(IDocuments), typeof(Documents));
            services.AddTransient(typeof(IEmployee_BasicInfo<Employee_BasicInfo>), typeof(Employee_BasicInfoRepository<Employee_BasicInfo>));
            
            #endregion


            //services.AddTransient(typeof(IActivity<EmployeeView>), typeof(Activities.PayrollActivity));
            //services.AddTransient(typeof(Activities.PayrollService));

            //services.AddTransient(typeof(ICommonRepository<Leave_Type>), typeof(Leave_TypeRepository<Leave_Type>));
            //services.AddTransient(typeof(ICommonRepository<Emp_Yearly_Leave>), typeof(Emp_Yearly_LeaveRepository<Emp_Yearly_Leave>));
            //services.AddTransient(typeof(IEmp_Yearly_Leave<Emp_Yearly_Leave>), typeof(Emp_Yearly_LeaveRepository<Emp_Yearly_Leave>));

            //services.AddTransient(typeof(ICommonRepository<Emp_Leave>), typeof(Emp_LeaveRepository<Emp_Leave>));
            //services.AddTransient(typeof(ICommonRepository<Emp_Leave_Approve_History>), typeof(Emp_Leave_Approve_HistoryRepository<Emp_Leave_Approve_History>));

            services.AddScoped<ActionFilters.TokenVerify>();
            services.AddScoped<ActionFilters.AuditLog>();
            services.AddScoped<ActionFilters.RolesValidate>();
            #endregion All Repository List

            #region Jwt Auth
            //Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //JWT Auth
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = objConfig.GetValue<string>("JwtAuth:IssuedBy"),

                    ValidateAudience = true,
                    ValidAudience = objConfig.GetValue<string>("JwtAuth:Audience"),

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(objConfig.GetValue<string>("JwtAuth:Secret"))),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Create token from HR.CommonUtility class
            //Implement below config from json to the CommonUtility class & Startup.cs here
            //JwtAuth:IssuedBy, JwtAuth:Secret, JwtAuth:Secret, JwtAuth:Audience, JwtAuth:ExpiryTime

            #endregion Jwt Auth

            #region Logger
            services.AddScoped<Log>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Log));
            });
            #endregion Logger

            #region Swagger

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AddHeaders>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HR_UK API", Version = "v1" });

                //c.DescribeAllEnumsAsStrings();
            });
            // Register the Swagger services
            //services.AddSwaggerDocument(config =>
            //{
            //    config.PostProcess = document =>
            //    {
            //        document.Info.Version = "v1";
            //        document.Info.Title = "HR_UK API";
            //    };
            //});

            #endregion Swagger

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://example.com", "http://www.contoso.com", "http://165.22.123.1" , "http://209.97.130.115:8080"); //Pending = site access,issuedby,authorised domain include in this
                    builder.WithHeaders("USER_ID", "LOGIN_ID", "COMPANY_ID", "TOKEN_NO", "Content-Type");
                    builder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                });
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logger
            XmlDocument xdDoc = new XmlDocument();
            xdDoc.Load(File.OpenRead("log4net.config"));
            loggerFactory.AddLog4Net(Convert.ToString(xdDoc["log4net"]), true);
            #endregion Logger

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
            });
            // Register the Swagger generator and the Swagger UI middlewares
            //app.UseOpenApi();
            //app.UseSwaggerUi3();
            #endregion Swagger

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
