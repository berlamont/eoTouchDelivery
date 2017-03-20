using System.Threading.Tasks;

namespace eoTouchDelivery.Core.DataServices
{
    public interface IProfileService
    {
        Task<Models.Users.UserProfile> GetCurrentProfileAsync();

        Task UploadUserImageAsync(Models.Users.UserProfile profile, string imageAsBase64);
    }
}