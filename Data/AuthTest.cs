using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace ITDocumentation.Data
{
    public class AuthTest
    {

        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthTest(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<IIdentity> GetIdentity()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity;
        }

    }
}




