using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia4.Models
{
    public class Animal
    {
        public static readonly HashSet<string>  listOfAvaibleColumn = new HashSet<string>()
        {
            "IDANIMAL", "NAME", "DESCRIPTION", "CATEGORY", "AREA"
        };

        public int IdAnimal { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Area is required")]
        public string Area { get; set; }

    }
}
