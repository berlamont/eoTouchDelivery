using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Models.Users
{
    public class UserAndProfileModel
    {
        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int TenantId { get; set; }
    }
}
