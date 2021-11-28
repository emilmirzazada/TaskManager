using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskManager.WebUI.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Response.StatusCode = 403;
        }
    }
}
