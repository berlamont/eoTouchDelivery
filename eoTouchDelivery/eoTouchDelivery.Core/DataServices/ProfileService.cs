﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eoTouchDelivery.Core.DataServices.Base;
using eoTouchDelivery.Core.Helpers;
using eoTouchDelivery.Models;
using eoTouchDelivery.Models.Users;

namespace eoTouchDelivery.Core.DataServices
{
    public class ProfileService : IProfileService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IAuthenticationService _authenticationService;

        public ProfileService(IRequestProvider requestProvider, IAuthenticationService authenticationService)
        {
            _requestProvider = requestProvider;
            _authenticationService = authenticationService;
        }

        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/{userId}";

            var uri = builder.ToString();

            return _requestProvider.GetAsync<UserProfile>(uri);
        }

        public Task<UserAndProfileModel> SignUp(UserAndProfileModel profile)
        {
            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/";
            var uri = builder.ToString();

            return _requestProvider.PostAsync<UserAndProfileModel>(uri, profile);
        }

        public async Task UploadUserImageAsync(UserProfile profile, string imageAsBase64)
        {
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/image/{userId}";
            var uri = builder.ToString();

            var imageModel = new ImageModel
            {
                Data = imageAsBase64
            };

            await _requestProvider.PutAsync(uri, imageModel);
            await CacheHelper.RemoveFromCache(profile.PhotoUrl);
        }
    }
}
