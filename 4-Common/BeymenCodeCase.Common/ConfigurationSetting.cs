using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Common
{
    /// <summary>
    ///  Inject all constructor
    /// </summary>
    public class ConfigurationSetting
    {
        public string applicationName { get; set; }
        public string connectionString { get; set; }
        public int refreshTimerIntervalInMs { get; set; }
    }
}
