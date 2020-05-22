namespace HR.WebApi.Common
{
    using HR.WebApi.ModelView;
    using System.Text;

    public class Search
    {
        public static string WhereString(PaginationBy paginationBy)
        {
            StringBuilder sbWhr = new StringBuilder();
            foreach (var vAry in paginationBy.searchBy)
                sbWhr.AppendFormat("{0} {1} {2} {3} ", vAry.FieldName, vAry.Parameter, vAry.FieldValue, vAry.ConditionWith);
            return sbWhr.ToString();
        }
    }
}
