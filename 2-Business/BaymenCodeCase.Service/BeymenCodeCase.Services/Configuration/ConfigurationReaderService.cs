using Autofac;
using BeymenCodeCase.Common;
using BeymenCodeCase.Services.Helper;
using BeymenCodeCase.Services.Redis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Configuration
{
    public class ConfigurationReaderService
    {

        private readonly string applicationName;
        private readonly string connectionString;
        private readonly int refreshTimerIntervalInMs;
        public ConfigurationReaderService(string applicationName,
                string connectionString, int refreshTimerIntervalInMs)
        {
            this.applicationName = applicationName;
            this.connectionString = connectionString;
            this.refreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }

        /// <summary>
        ///  Get Value Async Version
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetValue<T>(string key)
        {
            var redisService = ServiceHelper.GetRedisService();
          
            var data = await redisService.GetValueCacheValueAsync(key);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public ConfigurationSetting InitializeSettings()
        {
            return new ConfigurationSetting
            {
                applicationName = applicationName,
                connectionString = connectionString,
                refreshTimerIntervalInMs = refreshTimerIntervalInMs
            };
        }
    }
}
