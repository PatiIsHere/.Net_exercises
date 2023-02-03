using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.Controllers
{
    [Route("api/warehouses2")]
    [ApiController]
    public class WarehousesController2 : ControllerBase
    {
        //TODO - dodaj do konstruktora configa z connection stringiem (na podstawie win authentication)

        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse(int IdProduct, int IdWarehouse, int Amount, DateTime CreatedAt)
        {
            using SqlConnection connection = new SqlConnection();
            
            connection.ConnectionString = "Data Source=db-mssql;Initial Catalog=s17427;Integrated Security=True";

            using SqlCommand command = new SqlCommand("AddProductToWarehouse", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            


            command.Parameters.AddWithValue("@IdProduct", IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@CreatedAt", CreatedAt);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;

            SqlDataReader dr = await command.ExecuteReaderAsync();
            int returnId = 0;
            using (dr)
            {

               while (await dr.ReadAsync())
                {
                    returnId = Decimal.ToInt32((decimal)dr["NewId"]);
                }
            }
            await tran.CommitAsync();

            return Ok(returnId);
            //TODO proper error code with info
            return StatusCode(404);
        }
    }
}
