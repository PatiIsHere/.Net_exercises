using Cwiczenia4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia4.DataAccess
{
    public interface IAnimalsDataAccess
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync(string OrderBy);
        Task UpdateAnimal(int IdAnimal, Animal animal);
        Task DeleteAnimal(int IdAnimal);
        Task InsertAnimal(Animal animal);
        Task<bool> DoesAnimalExist(int IdAnimal);
    }
}
