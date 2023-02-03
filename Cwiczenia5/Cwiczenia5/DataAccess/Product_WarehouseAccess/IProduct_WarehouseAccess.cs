using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.Product_WarehouseAccess
{
    public interface IProduct_WarehouseAccess
    {
        Task<bool> IsOrderCompleted(int IdOrder);
        Task<int> InsertNewProductWarehouse(int IdWarehouse, int IdProduct, int IdOrder, int Amount, decimal Price, DateTime CreatedAt);
    }
}
