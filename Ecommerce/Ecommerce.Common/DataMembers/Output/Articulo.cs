using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    public class Articulo
    {
        public int Id { get; set; }
        public ArticuloTipo Tipo { get; set; }
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }
        public Lote Lote { get; set; }
        public decimal Precio { get; set; }
        public string UsuarioAdjudicado { get; set; }
        public bool Activo { get; set; }
    }
}
