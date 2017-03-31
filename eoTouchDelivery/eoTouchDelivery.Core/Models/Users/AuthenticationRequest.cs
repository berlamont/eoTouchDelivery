using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Models.Users
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }

        public string Credentials { get; set; }

        public string GrantType { get; set; }
    }
}
