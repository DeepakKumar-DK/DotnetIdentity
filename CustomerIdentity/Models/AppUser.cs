using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace CustomerIdentity.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }

        public string? Address { get; set; }
    }
}
