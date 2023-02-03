using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia3.Models.CustomExceptions
{
    class NotEnoughDataException : Exception
    {
        public override string Message => "Not enough data - object not created";
    }
}
