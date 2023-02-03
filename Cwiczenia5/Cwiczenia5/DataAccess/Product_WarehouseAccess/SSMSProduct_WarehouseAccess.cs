using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.Product_WarehouseAccess
{
    public class SSMSProduct_WarehouseAccess : IProduct_WarehouseAccess
    {
        private readonly IConfiguration _configuration;

        public SSMSProduct_WarehouseAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> IsOrderCompleted(int IdOrder)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT COUNT(*) as CountOfCompletedOrders FROM [dbo].[Product_Warehouse] WHERE IdOrder = @IdOrder";
            command.Parameters.AddWithValue("@IdOrder", IdOrder);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            SqlDataReader dr = await command.ExecuteReaderAsync();

            int rowCount = 0;
            while (dr.Read())
            {
                rowCount = (int)dr["CountOfCompletedOrders"];

            }

            if (rowCount == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<int> InsertNewProductWarehouse(int IdWarehouse, int IdProduct, int IdOrder, int Amount, decimal Price, DateTime CreatedAt)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText =  "INSERT INTO  dbo.Product_Warehouse" +
                                                "(IdWarehouse" +
                                                ", IdProduct" +
                                                ", IdOrder" +
                                                ", Amount" +
                                                ", Price" +
                                                ", CreatedAt)" +
                                                "VALUES" +
                                                "( @IdWarehouse" +
                                                ", @IdProduct" +
                                                ", @IdOrder" +
                                                ", @Amount" +
                                                ", @Price" +
                                                ", @CreatedAt);" +
                                    "SELECT CAST(scope_identity() AS int) as NewID";
            command.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
            command.Parameters.AddWithValue("@IdProduct", IdProduct);
            command.Parameters.AddWithValue("@IdOrder", IdOrder);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@Price", Amount * Price);
            command.Parameters.AddWithValue("@CreatedAt", CreatedAt);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;

            int newId = 0;
            using (SqlDataReader dr = await command.ExecuteReaderAsync())
            {

                if (!dr.HasRows)
                {
                    return 0;
                }
                while (dr.Read())
                {
                    newId = (int)dr["NewID"];
                }
            }

            tran.Commit();

            return newId;
        }
    }
}
