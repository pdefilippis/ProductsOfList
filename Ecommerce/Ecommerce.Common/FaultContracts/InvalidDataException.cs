using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.FaultContracts
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException()
        {
            Errores = new List<string>();
        }

        public InvalidDataException(List<string> errores) : base()
        {
            Errores.AddRange(errores);
        }

        public List<string> Errores { get; set; }
    }
}
