﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    public class Lote
    {
        public Lote()
        {
            Articulos = new List<Articulo>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public string NombreImagen { get; set; }
        public List<Articulo> Articulos { get; set; }
        public bool Activo { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Actualizacion { get; set; }
        public Estado Estado { get; set; }
    }
}
