using BeymenCodeCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        ///  Get Configuration By Application Name
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        IList<ApplicationConfigurationModel> GetConfigurationsByApplicationName(string applicationName);

        /// <summary>
        ///  Save Configuration
        /// </summary>
        /// <returns></returns>
        ApplicationConfigurationModel SaveConfiguration(ApplicationConfigurationModel applicationConfigurationModel);
    }
}
