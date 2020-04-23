using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    class ArticuloUsuario
    {
        public int UserId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int ArticuloId { get; set; }
        public virtual Articulo Articulo { get; set; }
    }
}
