using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Models.Users;

namespace eoTouchDelivery.Core.DataServices.Fake
{
    public class FakeProfileService : IProfileService
    {
        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var userProfile = new UserProfile
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserId = 1
            };

            return Task.FromResult(userProfile);
        }

        public Task<UserAndProfileModel> SignUp(UserAndProfileModel profile)
        {
            var userProfile = new UserAndProfileModel
            {
                UserName = "johndoe",
                Password = "12345",
                Email = "johndoe@mail.com",
                FirstName = "John",
                LastName = "Doe"
            };

            return Task.FromResult(userProfile);
        }

        public Task UploadUserImageAsync(UserProfile profile, string imageAsBase64)
        {
            return Task.FromResult(true);
        }
    }
}