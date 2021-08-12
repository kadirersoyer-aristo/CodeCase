using BeymenCodeCase.Entity;
using BeymenCodeCase.Models;
using BeymenCodeCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ApplicationConfiguration> configurationRepository;

        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.configurationRepository = this.unitOfWork.GetRepository<ApplicationConfiguration>();
        }

        /// <summary>
        /// I dont want to use like AutoMapper etc...
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public IList<ApplicationConfigurationModel> GetConfigurationsByApplicationName(string applicationName)
        {
            var list = new List<ApplicationConfigurationModel>();
            configurationRepository.GetList().ToList().ForEach(m =>
            {
                list.Add(new ApplicationConfigurationModel
                {
                    ApplicationName = m.ApplicationName,
                    Id = m.Id,
                    IsActive = m.IsActive,
                    Name = m.Name,
                    Value = m.Value
                });
            });

            return list;
        }

        /// <summary>
        ///  Save Configuration
        /// </summary>
        /// <param name="applicationConfigurationModel"></param>
        /// <returns></returns>

        public ApplicationConfigurationModel SaveConfiguration(ApplicationConfigurationModel applicationConfigurationModel)
        {
            configurationRepository.Add(new ApplicationConfiguration
            {
                ApplicationName = applicationConfigurationModel.ApplicationName,
                Id = applicationConfigurationModel.Id,
                IsActive = applicationConfigurationModel.IsActive,
                Name = applicationConfigurationModel.Name,
                Value = applicationConfigurationModel.Value
            });

            unitOfWork.SaveChanges();

            return applicationConfigurationModel;
        }
    }
}
