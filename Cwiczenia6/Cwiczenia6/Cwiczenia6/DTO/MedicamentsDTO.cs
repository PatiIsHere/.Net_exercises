using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.DTO
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentsDTO : ControllerBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
       
        public string Type { get; set; }
    }
}
