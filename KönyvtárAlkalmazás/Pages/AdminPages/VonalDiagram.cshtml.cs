using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    [BindProperties]
    public class VonalDiagramModel : PageModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [DataType(DataType.DateTime, ErrorMessage = "Nem dátum formátum")]
        public DateTime KezdetiDátum { get; set; }


        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [DataType(DataType.DateTime, ErrorMessage = "Nem dátum formátum")]
        public DateTime VégDátum { get; set; }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == null)
            {
                RedirectToPage("/Index");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/AdminPages/VonalDiagramÁbra", new { KezdetiDátum, VégDátum });
        }
    }
}
