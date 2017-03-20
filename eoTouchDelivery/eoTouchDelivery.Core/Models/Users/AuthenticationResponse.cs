using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Models.Users
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public string AccessToken { get; set; }
    }
}
