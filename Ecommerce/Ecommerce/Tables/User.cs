using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Tables
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }

        public DateTime? LastLoginTimestamp { get; set; }
        public DateTime? InactivityReminderTimestamp { get; set; }
        public int FailedLoginAttempts { get; set; }

        public UserState State { get; set; }
        public DateTime? LastEnabledTimestamp { get; set; }

        public DateTime CreationTimestamp { get; set; } = DateTime.Now;

        public string PreferredCulture { get; set; }

        public virtual ICollection<UserArticle> UserArticles { get; set; }


        public enum UserState
        {
            [Display(Name = "USER_STATE_NOT_ENABLED")]
            NOT_ENABLED = 0,
            [Display(Name = "USER_STATE_ENABLED")]
            ENABLED = 10,
            [Display(Name = "USER_STATE_DISABLED_BY_INACTIVITY")]
            DISABLED_BY_INACTIVITY = 30,
            [Display(Name = "USER_STATE_DISABLED_BY_ACTION")]
            DISABLED_BY_ACTION = 40,
        }

    }
}