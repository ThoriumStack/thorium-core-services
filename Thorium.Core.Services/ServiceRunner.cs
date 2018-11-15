using System;
using Microsoft.Extensions.PlatformAbstractions;
using SimpleInjector;
using Thorium.Core.Services.Abstractions;

namespace Thorium.Core.Services
{
    public class ServiceRunner<TStartup> where TStartup : class, IServiceConfiguration
    {
        private Container _bootstrapContainer;
        private CliOptions _cliOptions;
        private ApplicationEnvironment _appInfo;
        private IServiceConfiguration _serviceConfiguration;

        public ServiceRunner()
        {
            LoadAppInfo();
            _bootstrapContainer = new Container();
            _bootstrapContainer.Options.AllowOverridingRegistrations = true;
            _bootstrapContainer.Register<IServiceConfiguration, TStartup>();
            _cliOptions = new CliOptions(_appInfo.ApplicationName);
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            _bootstrapContainer.Register<IPlatformHandler, DefaultPlatformHandler>();
            _bootstrapContainer.Register<IEndpointConfigurationFactory, DefaultEndpointConfigurationFactory>();
        }

        private void LoadAppInfo()
        {
            _appInfo = PlatformServices.Default.Application;
        }

        private string GetServiceName()
        {
            return _appInfo.ApplicationName;
        }

        public ServiceRunner<TStartup> AddCli(Action<CliOptions> setup)
        {
            setup(_cliOptions);
            return this;
        }

        /// <summary>
        /// Use a custom platform handler.
        /// </summary>
        /// <typeparam name="TPlatformHandler"></typeparam>
        /// <returns></returns>
        public ServiceRunner<TStartup> UsePlatformHandler<TPlatformHandler>()
            where TPlatformHandler : class, IPlatformHandler
        {
            _bootstrapContainer.Register<IPlatformHandler, TPlatformHandler>();
            return this;
        }

        public void Run()
        {
            var platformHandler = _bootstrapContainer.GetInstance<IPlatformHandler>();
            _serviceConfiguration = _bootstrapContainer.GetInstance<IServiceConfiguration>();
            var containerFactory =  _bootstrapContainer.GetInstance<IEndpointConfigurationFactory>();
            //containerFactory.
          //  _serviceStartup.Container = new Container();
           // _serviceStartup.StartService();
            
            platformHandler.Execute(StopAllEndpoints);
        }

        private void StopAllEndpoints()
        {
            
        }
    }
}