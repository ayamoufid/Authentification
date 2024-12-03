using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace authentification.Pages
{
    [Authorize(Roles = "Commercial")]
    public class CommercialModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
