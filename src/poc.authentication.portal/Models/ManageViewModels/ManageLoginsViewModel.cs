using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CustomIdentityProviderSample.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class AuthenticationDescription
    {
        public string AuthenticationScheme { get; set; }
        public string DisplayName { get; set; }
    }
}
