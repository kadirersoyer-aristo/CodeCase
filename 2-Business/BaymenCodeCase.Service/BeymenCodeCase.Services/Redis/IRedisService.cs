using BeymenCodeCase.Services.Abstact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Redis
{
    public interface IRedisService : INoSqlService
    {
       
        /// <summary>
        ///  Get Value From Cache By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        Task<string> GetValueCacheValueAsync(string key);


        /// <summary>
        ///  Set Value With Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetCacheValueAsync(string key, string value);

        /// <summary>
        ///  Delete From Cache
        /// </summary>
        /// <param name="key"></param>
        Task Delete(string key);

        /// <summary>
        ///  Key Builder For Redis Caching
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string KeyBuilder(string applicationName, string key, bool isActive);

        /// <summary>
        ///  Syncronization To Redis
        /// </summary>
        /// <returns></returns>
        Task SyncronizationRedis();

        /// <summary>
        ///  Redis Insert Or Update Cache From RDMS
        /// </summary>
        void InsertOrUpdateRedis();
    }
}
