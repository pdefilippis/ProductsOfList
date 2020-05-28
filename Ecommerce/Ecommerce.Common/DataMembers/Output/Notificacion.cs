using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    public class Notificacion
    {
        public Usuario Usuario { get; set; }
        public Articulo Articulo { get; set; }
        public bool Leido { get; set; }
        public DateTime Stamp { get; set; }
    }
}
