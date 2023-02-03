using Cwiczenia5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.OrderAccess
{
    public class SSMSOrderDataAccess : IOrderDataAccess
    {
        private readonly IConfiguration _configuration;

        public SSMSOrderDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Order> GetOrder(int IdProduct, int Amount)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT [IdOrder],[IdProduct],[Amount],[CreatedAt],[FulfilledAt] " +
                                  "FROM [s17427].[dbo].[Order] " +
                                  "WHERE [IdProduct] = @IdProduct AND [Amount] = @Amount";
            command.Parameters.AddWithValue("@IdProduct", IdProduct);
            command.Parameters.AddWithValue("@Amount", Amount);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            SqlDataReader dr = await command.ExecuteReaderAsync();

            if (!dr.HasRows)
            {
                return null;
            }
            var resultOrder = new Order();

            while (dr.Read())
            {
                resultOrder.IdOrder = (int)dr["IdOrder"];
                resultOrder.IdProduct = (int)dr["IdProduct"];
                resultOrder.Amount = (int)dr["Amount"];
                resultOrder.CreatedAt = (DateTime)dr["CreatedAt"];
                if (dr["FulfilledAt"] == DBNull.Value)
                {
                    resultOrder.FulfilledAt = null;
                }
                else
                {
                    resultOrder.FulfilledAt = (DateTime)dr["FulfilledAt"];
                }

            }

            return resultOrder;
                
        }

        public async void UpdateFullFiledValue(int IdOrder, DateTime CreatedAt)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE[dbo].[Order] " +
                                  "SET[FulfilledAt] = @CreatedAt " +
                                  "WHERE[IdOrder] = @IdOrder";

            command.Parameters.AddWithValue("@IdOrder", IdOrder);
            command.Parameters.AddWithValue("@CreatedAt", CreatedAt);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            await command.ExecuteNonQueryAsync();

            tran.Commit();
        }
    }
}
