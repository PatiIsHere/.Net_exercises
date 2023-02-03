using Cwiczenia5.DataAccess.OrderAccess;
using Cwiczenia5.DataAccess.Product_WarehouseAccess;
using Cwiczenia5.DataAccess.ProductAccess;
using Cwiczenia5.DataAccess.WarehouseAccess;
using Cwiczenia5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IProductAccess _productAccess;
        private readonly IWarehouseAccess _warehouseAccess;
        private readonly IOrderDataAccess _orderDataAccess;
        private readonly IProduct_WarehouseAccess _product_WarehouseAccess;

        public WarehousesController(IProductAccess productAccess, IWarehouseAccess warehouseAccess
                                    ,IOrderDataAccess orderDataAccess, IProduct_WarehouseAccess product_WarehouseAccess)
        {
            _productAccess = productAccess;
            _warehouseAccess = warehouseAccess;
            _orderDataAccess = orderDataAccess;
            _product_WarehouseAccess = product_WarehouseAccess;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse(int IdProduct, int IdWarehouse, int Amount, DateTime CreatedAt)
        {
         
            if(Amount !< 0)
            {
                return StatusCode(404, "Invalid parameter: Provided Amount is incorrect");
            }

            var product = _productAccess.GetProduct(IdProduct);
            var order = _orderDataAccess.GetOrder(IdProduct, Amount);

            var tempProduct = (Product)await product;
            if (tempProduct == null)
            {
                return StatusCode(404, "Invalid parameter: Provided IdProduct does not exist");
            }

            if (!await _warehouseAccess.DoesWarehouseExist(IdWarehouse))
            {
                return StatusCode(404, "Invalid parameter: Provided IdWarehouse does not exist");
            }

            var tempOrder = (Order)await order;
            if (tempOrder == null)
            {
                return StatusCode(404, "Invalid parameter: There is no order to fullfill");
            }

            if (tempOrder.CreatedAt >= CreatedAt)
            {
                return StatusCode(404, "Invalid parameter: Provided date CreatedAt has too early value");
            }

            if (await _product_WarehouseAccess.IsOrderCompleted(tempOrder.IdOrder))
            {
                return StatusCode(404, "Invalid parameter: Order for this item was already completed");
            }

            
            _orderDataAccess.UpdateFullFiledValue(tempOrder.IdOrder, CreatedAt);
            //Insert do Product_Warehouse i return pod jakim ID zostało wrzucone


            return Ok(await _product_WarehouseAccess.InsertNewProductWarehouse(IdWarehouse,tempProduct.IdProduct, tempOrder.IdOrder, tempOrder.Amount, tempProduct.Price, CreatedAt));
            //TODO proper error code with info
            return StatusCode(404);
        }

    }
}
