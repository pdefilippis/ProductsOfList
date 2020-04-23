using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class Policies
    {
        //Permisos de Lotes
        public const string CanViewLots = "CanViewLots";
        public const string CanCreateLots = "CanCreateLots";
        public const string CanEditLots = "CanEditLots";

        //Permisos de Usuario
        public const string CanViewUsers = "CanViewUsers";
        public const string CanCreateUsers = "CanCreateUsers";
        public const string CanEditUsers = "CanEditUsers";
        public const string CanEnableUsers = "CanEnableUsers";
        public const string CanDisableUsers = "CanDisableUsers";
    }
}
