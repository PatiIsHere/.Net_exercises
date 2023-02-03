using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.WarehouseAccess
{
    public interface IWarehouseAccess
    {
        Task<bool> DoesWarehouseExist(int IdWarehouse);
    }
}
