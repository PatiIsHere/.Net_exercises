using Cwiczenia4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia4.DataAccess
{
    public class SSMSAnimalsDataAccess : IAnimalsDataAccess
    {
        private readonly IConfiguration _configuration;

        public SSMSAnimalsDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Animal>> GetAnimalsAsync(string? OrderBy)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();
            if (OrderBy != null && Animal.listOfAvaibleColumn.Contains(OrderBy.ToUpper()))
            {
                _ = OrderBy.ToUpper() switch
                {
                    "IDANIMAL" => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By IDANIMAL ASC",
                    "NAME" => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By NAME ASC",
                    "DESCRIPTION" => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By DESCRIPTION ASC",
                    "CATEGORY" => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By CATEGORY ASC",
                    "AREA" => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By AREA ASC",
                    _ => command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By Name ASC",
                };
            }
            else
            {
                command.CommandText = "select IdAnimal, Name, Description, Category, Area from Animals Order By Name ASC";
            }
            command.Connection = connection;

            await connection.OpenAsync();

            SqlDataReader dr = await command.ExecuteReaderAsync();


            var list = new List<Animal>();
            while (dr.Read())
            {
               
                    var a = new Animal
                    {
                        IdAnimal = (int)dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = (dr["Description"] == DBNull.Value? null : dr["Description"].ToString()),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    };
                    list.Add(a);
            }
            return list;
        }

        public async Task DeleteAnimal(int IdAnimal)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "Delete from Animals where IdAnimal = @IdAnimal";
            command.Parameters.AddWithValue("@IdAnimal", IdAnimal);
            command.Connection = connection;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

        public async Task InsertAnimal(Animal animal)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO [dbo].[Animals] ([Name],[Description],[Category],[Area])VALUES (@Name,@Description,@Category,@Area)";
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", (animal.Description == null? DBNull.Value: animal.Description));
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);
            command.Connection = connection;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAnimal(int IdAnimal, Animal animal)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();

            command.CommandText = "Update Animals set Name = @Name, Description = @Description, Category = @Category, Area = @Area Where IdAnimal = @IdAnimal";
            command.Parameters.AddWithValue("@IdAnimal", IdAnimal);
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", (animal.Description == null ? DBNull.Value : animal.Description));
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);
            command.Connection = connection;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();


        }

        public async Task<bool> DoesAnimalExist(int IdAnimal)
        {
            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _configuration.GetConnectionString("DefaultCon");

            SqlCommand command = new SqlCommand();
            command.CommandText = "select count(*) from Animals Where IdAnimal = @IdAnimal";
            command.Parameters.AddWithValue("@IdAnimal", IdAnimal);
                
            command.Connection = connection;

            await connection.OpenAsync();

            SqlDataReader dr = await command.ExecuteReaderAsync();

            if (dr.HasRows)
            {
                return true;
            }
            return false;
        }
    }
}
