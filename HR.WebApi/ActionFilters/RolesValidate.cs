using HR.WebApi.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RolesValidate : ActionFilterAttribute
    {
        private readonly ApplicationDbContext adbContext;
        public string Module { get; set; } //Company, Dept etc...
        public string Permission { get; set; } //View, Add, Edit, Delete, Approval

        public RolesValidate(ApplicationDbContext applicationDbContext, string Module, string Permission)
        {
            adbContext = applicationDbContext;
            this.Module = Module;
            this.Permission = Permission;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var vHeaders = filterContext.HttpContext.Request.Headers;

            if (String.IsNullOrEmpty(vHeaders["USER_ID"]) && String.IsNullOrEmpty(Convert.ToString(vHeaders["TOKEN_NO"])))
            {
                filterContext.Result = new UnauthorizedResult(); return;
            }
            else
            {
                if (!RoleCheck(Convert.ToInt32(vHeaders["USER_ID"]), Module, Permission))
                    filterContext.Result = new UnauthorizedResult(); return;
            }
        }

        private bool RoleCheck(int intUserId, string strModule, string strPermission)
        {
            // Admin User = Role_Id = 1
            var vCount = (from ur in adbContext.user_role.AsQueryable()
                          join rp in adbContext.role_permission.AsQueryable() on ur.Role_Id equals rp.Role_Id
                          join mp in adbContext.module_permission.AsQueryable() on rp.Module_Per_Id equals mp.Id
                          join m in adbContext.module.AsQueryable() on mp.Module_Id equals m.Id
                          where ur.User_Id == intUserId && ((m.Name == strModule && mp.Name == strPermission) || ur.Role_Id == 1)
                          select new { ur.User_Id }).Count();
            return (vCount > 0 ? true : false);
        }
    }
}