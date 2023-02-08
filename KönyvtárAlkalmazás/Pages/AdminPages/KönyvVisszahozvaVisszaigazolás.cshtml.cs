using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class KönyvVisszahozvaVisszaigazolásModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == null)
            {
                RedirectToPage("/Index");
            }
        }
    }
}
