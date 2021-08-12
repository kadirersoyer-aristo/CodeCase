using BeymenCodeCase.Common;
using BeymenCodeCase.Context;
using BeymenCodeCase.Entity;
using BeymenCodeCase.Services.Abstact;
using BeymenCodeCase.Services.Helper;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly ConfigurationSetting _configurationSetting;
        private readonly IContextHelper _contextHelper;
        private readonly IConnectionMultiplexer _connectionMultiplexer = null;

        public RedisService(IConnectionMultiplexer connectionMultiplexer,
            IContextHelper contextHelper, ConfigurationSetting configurationSetting)
        {
            _configurationSetting = configurationSetting;
            _contextHelper = contextHelper;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task Delete(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task<string> GetValueCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            key = $"{_configurationSetting.applicationName}_{key}";
            return await db.StringGetAsync(key);
        }

        /// <summary>
        ///  Zamanım yoktu burada çok solid aramayalım :) 
        /// </summary>

        public async void InsertOrUpdateRedis()
        {

            // Not Disposable Content
            using (var db = _contextHelper.GetContextOptions)
            {
                var configQueryable = db.Configuration;

                var task = db.ScheduleTask.FirstOrDefault(x => x.Name == _configurationSetting.applicationName);
                // Get Last Activity Date
                var lastActivityDate = task?.LastSuccessUtc
                                        ?? DateTime.Now;
                if (task?.IsRunning ?? false)
                    return;

                if (task == null)
                {
                    task = new ScheduleTask
                    {
                        CreateDate = DateTime.Now,
                        Enabled = true,
                        Name = _configurationSetting.applicationName,
                        IsRunning = true,
                        Seconds = _configurationSetting.refreshTimerIntervalInMs,
                        Type = _configurationSetting.applicationName
                    };
                    db.ScheduleTask.Add(task);
                    db.SaveChanges();
                };
                // Update Task
                task.IsRunning = true;
                db.ScheduleTask.Update(task);
                // Inserted Datas
                var getInsertedDatas = configQueryable.Where(m => m.IsActive && m.CreateDate >= lastActivityDate
                                        && m.ApplicationName == _configurationSetting.applicationName).ToList();

                foreach (var item in getInsertedDatas)
                {
                    var key = $"{_configurationSetting.applicationName}_{item.Name}";
                    await SetCacheValueAsync(key, item.Value);
                }

                // Inserted Datas
                var getUpdatedDatas = configQueryable.Where(m => m.IsActive && m.UpdateDate >= lastActivityDate
                                        && m.ApplicationName == _configurationSetting.applicationName).ToList();

                foreach (var item in getUpdatedDatas)
                {
                    var key = $"{_configurationSetting.applicationName}_{item.Name}";
                    await SetCacheValueAsync(key, item.Value);
                }
            }
        }

        public string KeyBuilder(string applicationName, string key, bool isActive)
        {
            var keys = $"{applicationName}_{key}";
            return keys;
        }

        public async Task SetCacheValueAsync(string key1, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key1, value);
        }

        public Task SyncronizationRedis()
        {
            throw new NotImplementedException();
        }
    }
}
