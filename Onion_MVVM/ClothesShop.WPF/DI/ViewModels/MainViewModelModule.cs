using ClothesShop.WPF.Interfaces.ViewModels;
using ClothesShop.WPF.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.WPF.DI.ViewModels
{
    public class MainViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainViewModel>().To<MainViewModel>();
        }
    }
}
