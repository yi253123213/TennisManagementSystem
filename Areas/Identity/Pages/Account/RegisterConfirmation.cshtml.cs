using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisFinalGrp339.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        public string Email { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string ConfirmationLink { get; set; } = string.Empty;

        public void OnGet(string email, string userRole, string link)
        {
            Email = email ?? string.Empty;
            UserRole = userRole ?? string.Empty;
            ConfirmationLink = link ?? string.Empty;
        }
    }
}
