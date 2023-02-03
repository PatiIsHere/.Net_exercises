using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.DataAccess.WarehouseAccess
{
    public class SSMSWarehouseAccess : IWarehouseAccess
    {
        private readonly IConfiguration _configuration;

        public SSMSWarehouseAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> DoesWarehouseExist(int IdWarehouse)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT COUNT(*) as CountOfWarehouse FROM [dbo].[Warehouse] WHERE IdWarehouse = @IdWarehouse";
            command.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);


            command.Connection = connection;

            await connection.OpenAsync();
            var tran = connection.BeginTransaction();
            command.Transaction = tran;
            SqlDataReader dr = await command.ExecuteReaderAsync();

            int rowCount = 0;
            while (dr.Read())
            {
                rowCount = (int)dr["CountOfWarehouse"];

            }

            if (rowCount == 1)
            {
                return true;
            }
            return false;
        }
    }
}
