using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.BLL.DI
{
    public class ClothingItemServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClothingItemService>()
                .To<ClothingItemService>();
        }
    }
}
