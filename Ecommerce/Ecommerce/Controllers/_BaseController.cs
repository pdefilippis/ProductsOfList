using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class _BaseController : Controller
    {
        [NonAction]
        protected long DateTimeToUserFriendlyTicks(DateTime dateTime)
        {
            return dateTime.Ticks;
        }
        protected int CurrentUserId
        {
            get
            {
                return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
    }
}
