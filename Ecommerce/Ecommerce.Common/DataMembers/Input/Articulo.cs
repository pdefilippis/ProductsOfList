using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Input
{
    public class Articulo
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int IdLote { get; set; }
        public int IdTipo { get; set; }
        public string NroSerie { get; set; }
        public Usuario UserAdjudicado { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }
    }
}
