using BeymenCodeCase.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeymenCodeCaseProd.Test
{
    public class ConfigurationTests
    {

        ConfigurationReaderService configurationReaderService = null;
        public ConfigurationTests()
        {
            // Initialize Reader Service
            var connectionString = "localhost";
            var siteName = "SERVICE-A";
            var refreshTimerIntervalInMs = 10;

            configurationReaderService = new ConfigurationReaderService(siteName, connectionString, refreshTimerIntervalInMs);
            configurationReaderService.InitializeSettings();
        }

        [Fact]
        public async void Application_Configuration_Key_Value_Test()
        {

            // Arrange
            var excepted = "Boyner.com.tr";
            // Act
            var value = await configurationReaderService.GetValue<string>("SiteName");
            // Assert
            Assert.Equal(excepted, value);
        }
    }
}
