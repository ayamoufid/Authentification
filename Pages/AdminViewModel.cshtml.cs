using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace authentification.Pages
{
    public class AdminViewModel
    {
        public List<IdentityUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}

