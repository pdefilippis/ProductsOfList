using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Users
{
    public class DisableUserConfirmationViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
