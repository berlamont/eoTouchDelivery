using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Models.Users;

namespace eoTouchDelivery.Core.Models.Users
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public UserProfile Profile { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
