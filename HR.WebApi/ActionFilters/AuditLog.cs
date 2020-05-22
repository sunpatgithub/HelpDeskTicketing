using HR.WebApi.DAL;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net;
using System.Reflection;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditLog : ActionFilterAttribute
    {
        private static readonly ILog Logfile = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ApplicationDbContext adbContext;

        public AuditLog(ApplicationDbContext applicationDbContext)
        {
            adbContext = applicationDbContext;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //add into event log  
            var vHeaders = filterContext.HttpContext.Request.Headers;
            string strRemoteIp = Convert.ToString(filterContext.HttpContext.Connection.LocalIpAddress);
            AddAuditLog(filterContext.RouteData, strRemoteIp, filterContext, Convert.ToInt32(vHeaders["USER_ID"]));
        }

        public void AddAuditLog(RouteData routeData, string strRemoteIp, ResultExecutedContext resultState,int id)
        {
            try
            {
                adbContext.auditlog.Add(new Model.AuditLog
                {
                    User_Id = id,
                    Description = "Process : " + routeData.Values["controller"],
                    Type = "Id : " + resultState.Result,
                    HostName = Dns.GetHostName(),
                    IpAddress = strRemoteIp,
                    Status = ActionStat(Convert.ToString(routeData.Values["action"])),
                    AddedBy = id,
                    AddedOn = DateTime.Now
                });
                adbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Write Log manually in text file
                Logfile.Error("Error on AuditLog", ex);
            }
        }

        public string ActionStat(string actionName)
        {
            switch (actionName)
            {
                case "Get":
                    return "View";
                case "Post":
                    return "Add";
                case "Put":
                    return "Edit";
                case "Delete":
                    return "Delete";
                default:
                    return actionName;
            }
        }
    }
}