using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Input
{
    public class Lote
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string NombreImagen { get; set; }
        public byte[] Imagen { get; set; }
    }
}
