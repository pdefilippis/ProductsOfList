﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Output
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string[] Acciones { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime UltimoIngreso { get; set; }
        public string Email { get; set; }
        public bool EsAdministrador { get; set; }
        public bool Activo { get; set; }
    }
}
