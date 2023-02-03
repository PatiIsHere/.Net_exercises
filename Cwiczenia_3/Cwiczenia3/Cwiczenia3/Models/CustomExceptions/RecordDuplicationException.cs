using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia3.Models.CustomExceptions
{
    class RecordDuplicationException : Exception
    {
        public override string Message => "Student with given index number exist - object not created";
    }
}
