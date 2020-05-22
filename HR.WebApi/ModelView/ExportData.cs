using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class ExportData
    {
        public string status { get; } = "Open";
        public string Extension { get; set; } = ".csv";
    }
}
