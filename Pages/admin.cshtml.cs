using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace authentification.Pages
{
    [Authorize(Roles = "Admin")]
    public class adminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public adminModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public authentification.Pages.AdminViewModel ViewModel { get; set; }

        public async Task OnGetAsync()
        {
            ViewModel = new authentification.Pages.AdminViewModel
            {
                Users = _userManager.Users.ToList(),
                Roles = _roleManager.Roles.ToList()
            };
        }

        public async Task<IActionResult> OnPostEditRoleAsync(string id, string newRole)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newRole))
            {
                ModelState.AddModelError(string.Empty, "ID utilisateur ou r�le invalide.");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Utilisateur introuvable.");
                return Page();
            }

            // Retirer les anciens 
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Ajouter le nouveau r�le
            var roleExists = await _roleManager.RoleExistsAsync(newRole);
            if (!roleExists)
            {
                ModelState.AddModelError(string.Empty, "Le r�le sp�cifi� n'existe pas.");
                return Page();
            }

            var result = await _userManager.AddToRoleAsync(user, newRole);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la mise � jour du r�le.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}



