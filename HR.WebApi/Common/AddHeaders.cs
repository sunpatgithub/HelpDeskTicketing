using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace HR.WebApi.Common
{
    public class AddHeaders : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
                if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "LOGIN_ID",
                    In = ParameterLocation.Header,
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "TOKEN_NO",
                    In = ParameterLocation.Header,
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "user_Id",
                    In = ParameterLocation.Header,
                    Required = false
                });
        }
    }
}