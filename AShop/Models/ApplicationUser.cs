using System;
using Microsoft.AspNetCore.Identity;

namespace AShop.Models
{
    public class ApplicationUser: IdentityUser
    {   
        public string FullName { get; set; }
    }
}
