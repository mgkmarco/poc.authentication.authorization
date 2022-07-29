using System;
using System.Security.Principal;

namespace CustomIdentityProviderSample.CustomProvider
{
    public class ApplicationUser : IIdentity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string NormalizedEmail { get; internal set; }
        public String PasswordHash { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; internal set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; internal set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}