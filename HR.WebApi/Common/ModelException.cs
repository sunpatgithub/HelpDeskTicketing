namespace HR.WebApi.Common
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;
    public class ModelException
    {
        public static string Errors(ModelStateDictionary modelState)
        {
            return string.Join(" | ", modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }
    }
}
