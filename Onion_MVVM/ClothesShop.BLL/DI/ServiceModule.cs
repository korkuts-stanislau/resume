using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.BLL.Services;
using ClothesShop.DAL.Interfaces;
using ClothesShop.DAL.UnitsOfWork;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.BLL.DI
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;
        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>()
                .To<ClothesShopDbUnitOfWork>()
                .WithConstructorArgument(_connectionString);
        }
    }
}
