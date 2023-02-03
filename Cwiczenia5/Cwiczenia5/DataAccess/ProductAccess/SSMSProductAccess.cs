using Cwiczenia5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.ProductAccess
{
    public class SSMSProductAccess : IProductAccess
    {
        private readonly IConfiguration _configuration;

        public SSMSProductAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> DoesProductExist(int IdProduct)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();
           
            command.CommandText = "SELECT COUNT(*) as CountOfProduct FROM [dbo].[Product] WHERE IdProduct = @IdProduct";
            command.Parameters.AddWithValue("@IdProduct", IdProduct);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            SqlDataReader dr = await command.ExecuteReaderAsync();

            int rowCount = 0;
            while (dr.Read())
            {
                rowCount = (int)dr["CountOfProduct"];
                   
            }

            if(rowCount == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<Product> GetProduct(int IdProduct)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT [IdProduct],[Name],[Description],[Price] FROM [s17427].[dbo].[Product] where IdProduct = @IdProduct";
            command.Parameters.AddWithValue("@IdProduct", IdProduct);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            SqlDataReader dr = await command.ExecuteReaderAsync();

            if (!dr.HasRows)
            {
                return null;
            }

            var resultProduct = new Product() ;
            
            while (dr.Read())
            {
                resultProduct.IdProduct = (int)dr["IdProduct"];
                resultProduct.Name = (string)dr["Name"];
                resultProduct.Description = (string)dr["Description"];
                resultProduct.Price = (decimal)dr["Price"];

            }

            return resultProduct;           
        }
    }
}
