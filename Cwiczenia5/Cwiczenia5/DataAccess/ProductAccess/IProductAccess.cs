using Cwiczenia5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.ProductAccess
{
    public interface IProductAccess
    {
        Task<bool> DoesProductExist(int IdProduct);

        Task<Product> GetProduct(int IdProduct);
    }
}
