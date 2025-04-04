using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.Models;

namespace eoTouchDelivery.Core.Services
{
    public interface ILocationProvider
    {
        Task<ILocationResponse> GetPositionAsync();
    }
}
