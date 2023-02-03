using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.Models
{
    interface IStudentDataConverter
    {
        public List<string[]> ConvertDataSetForStudentExtensionRecreation(string FilePath);
    }
}
