using Cwiczenia5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.OrderAccess
{
    public interface IOrderDataAccess
    {
        Task<Order> GetOrder(int IdProduct, int Amount);
        void UpdateFullFiledValue(int idOrder, DateTime CreatedAt);
    }
}
