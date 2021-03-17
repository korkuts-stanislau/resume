using ClothesShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.BLL.Interfaces.EntityServices
{
    public interface IManufacturerService : IEntityService<ManufacturerDTO>
    {
        IEnumerable<ManufacturerDTO> FilterByNameContainedText(string text);
    }
}
