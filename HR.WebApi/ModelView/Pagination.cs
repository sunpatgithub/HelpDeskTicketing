using System;

namespace HR.WebApi.ModelView
{
    public class Pagination
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public int RecordCount { get; set; } = 0;
        public int RecordLimit { get; set; } = 500;

        public string CommonSearch { get; set; } = String.Empty;
        public string OrderBy { get; set; }
        //internal Pagination()
        //{
        //    this.PageIndex = PageIndex * PageSize;
        //}
    }
}
