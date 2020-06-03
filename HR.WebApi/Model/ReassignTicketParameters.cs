using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    sealed public class ReassignTicketParameters
    {
        public int ticketId { get; set; }
        public int deptId { get; set; }
        public string assignToId { get; set; }
        public string comment { get; set; } = "";
    }
}
