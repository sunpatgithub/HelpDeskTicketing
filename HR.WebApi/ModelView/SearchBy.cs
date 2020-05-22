namespace HR.WebApi.ModelView
{
    public class SearchBy
    {
        public string FieldName { get; set; } = string.Empty;
        public string FieldValue { get; set; } = string.Empty;
        public string Parameter { get; set; } = string.Empty;
        public string ConditionWith { get; set; } = string.Empty;

        public int RecordLimit { get; set; } = 1000;
    }
}
