using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class ÚjKönyvModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public ÚjKönyvModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public string Cím { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public string Író { get; set; }

        [BindProperty]
        public string? Kiadó { get; set; }

        [BindProperty]
        [Range(1000000000000, 9999999999999, ErrorMessage = "A számnak 13 számjegyűnek kell lennie")]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public long ISBN { get; set; }

        [BindProperty]
        public bool NotUniqueISBN { get; set; }


        public void OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null)
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

            foreach (var konyv in _context.Könyvek.ToList())
            {
                if (konyv.ISBN == ISBN)
                {
                    NotUniqueISBN = true;
                    return Page();
                }
            }

            Könyv könyv = new();

            könyv.Cím = Cím;

            könyv.Író = Író;

            könyv.Kiadó = Kiadó;

            könyv.ISBN = ISBN;

            könyv.Kikölcsönözték = false;

            könyv.Előkölcsönözték = false;

            _context.Könyvek.Add(könyv);

            _context.SaveChanges();

            return RedirectToPage("/AdminPages/SikeresKönyvHozzáadás");
        }
    }
}
