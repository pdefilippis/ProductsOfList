using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Common.DataMembers.Input
{
    public class EmailNotification
    {
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
