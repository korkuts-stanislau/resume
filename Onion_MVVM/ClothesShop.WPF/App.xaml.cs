using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClothesShop.BLL.DI;
using Ninject.Modules;
using Microsoft.Extensions.Configuration;
using System.IO;
using ClothesShop.WPF.DI;
using ClothesShop.WPF.Views;
using ClothesShop.WPF.DI.ViewModels;

namespace ClothesShop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            //BLL dependencies
            NinjectModule serviceModule = new ServiceModule(GetConnectionString());
            NinjectModule manufacturerModule = new ManufacturerServiceModule();
            NinjectModule clothingItemTypeModule = new ClothingItemTypeServiceModule();
            NinjectModule clothingItemModule = new ClothingItemServiceModule();

            //WPF dependencies
            NinjectModule manufacturerViewModelModule = new ManufacturerViewModelModule();
            NinjectModule clothingItemTypeViewModelModule = new ClothingItemTypeViewModelModule();
            NinjectModule clothingItemViewModelModule = new ClothingItemViewModelModule();
            NinjectModule mainViewModelModule = new MainViewModelModule();

            _container = new StandardKernel(serviceModule, manufacturerModule, clothingItemModule, clothingItemTypeModule,
                manufacturerViewModelModule, clothingItemTypeViewModelModule, clothingItemViewModelModule, mainViewModelModule);
        }

        private string GetConnectionString()
        {
            var curDir = Directory.GetCurrentDirectory();
            var baseDir = Directory.GetParent(curDir).Parent.Parent;

            var config = new ConfigurationBuilder()
                .AddJsonFile(@$"{baseDir}\config.json")
                .Build();

            return config["ConnectionStrings:DbConnection"];
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.Title = "ClothesShop";
        }
    }
}
