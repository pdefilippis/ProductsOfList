using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Input
{
    public class Usuario
    {
        public string UserName { get; set; }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value != null)
                    password = Ecommerce.Common.Password.EncryptPassword(value);
            }
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class ChangePassword
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
