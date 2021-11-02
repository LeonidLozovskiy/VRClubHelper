using System;
using System.Windows;
using CorgiVR.Common;
using CorgiVR.Repository;
using CorgiVR.Repository.Entities;
using CorgiVR.ViewModelEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorgiVR
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var serviceCollection = new ServiceCollection();
            
            Configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json")
                           .Build();
            
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; }

        public IConfiguration Configuration { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            ServicesCoufiguration.ConfigureServices(services, Configuration);
            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton(typeof(MainWindowViewModel));
        }

        protected void OnStartup(object sender, StartupEventArgs startupEventArgs)
        {
            var builder = new ConfigurationBuilder();

            Configuration = builder.Build();

            ServiceProviderFactory.SetContainer(ServiceProvider);
            
            CustomMigrations.Migrate(ServiceProvider);
            
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}