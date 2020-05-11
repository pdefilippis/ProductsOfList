using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    public class Articulo
    {
        public Articulo()
        {
            UsuariosInteresados = new List<Usuario>();
        }

        public int Id { get; set; }
        public ArticuloTipo Tipo { get; set; }
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }
        public Lote Lote { get; set; }
        public decimal Precio { get; set; }
        public Usuario UsuarioAdjudicado { get; set; }
        public bool Activo { get; set; }
        public Marca Marca { get; set; }
        public virtual ICollection<Usuario> UsuariosInteresados { get; set; }
    }
}
