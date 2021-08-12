using BeymenCodeCase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Helper
{
    public class ContextHelper: IContextHelper
    {
        private readonly IConfiguration configuration;

        public ContextHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///  Create Not Disposed Context
        /// </summary>
        public StorageContext GetContextOptions
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<StorageContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("StorageContextConnection"));
                var options = optionsBuilder.Options;
                return new StorageContext(options);
            }
        }
    }

    public interface IContextHelper
    {
        public StorageContext GetContextOptions { get; }
    }
}
