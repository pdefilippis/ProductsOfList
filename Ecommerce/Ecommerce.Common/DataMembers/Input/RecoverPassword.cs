using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Input
{
    public class RecoverPassword
    {
        public string Email { get; set; }
        public string Token { get; set; }
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
    }
}
