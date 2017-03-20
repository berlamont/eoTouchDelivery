using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Models.Users;

namespace eoTouchDelivery.Core.DataServices
{
    public interface IProfileService
    {
        Task<UserProfile> GetCurrentProfileAsync();

        Task<UserAndProfileModel> SignUp(UserAndProfileModel profile);

        Task UploadUserImageAsync(UserProfile profile, string imageAsBase64);
    }
}