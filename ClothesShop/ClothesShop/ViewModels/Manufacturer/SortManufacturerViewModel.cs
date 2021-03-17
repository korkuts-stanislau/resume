using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.Manufacturer
{
    public class SortManufacturerViewModel
    {
        public EntityServices.ManufacturerService.SortState NameSort { get; set; }
        public EntityServices.ManufacturerService.SortState Current { get; set; }
        public SortManufacturerViewModel(ClothesShop.EntityServices.ManufacturerService.SortState sortState)
        {
            NameSort = sortState == EntityServices.ManufacturerService.SortState.NameAsc ? EntityServices.ManufacturerService.SortState.NameDesc : EntityServices.ManufacturerService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
