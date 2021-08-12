using BeymenCodeCase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Models
{
    public class LogsModel: BaseModel
    {
        public LogType LogType { get; set; }
        public string Message { get; set; }
        public string ShortMessage { get; set; }
    }
}
