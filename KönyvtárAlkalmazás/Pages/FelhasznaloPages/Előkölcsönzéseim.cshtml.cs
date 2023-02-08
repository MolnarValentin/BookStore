using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class ElőkölcsönzéseimModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public ElőkölcsönzéseimModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Felhasználó? Felhasználó { get; set; }

        public List<Előkölcsönzés>? Előkölcsönzés { get; set; }

        public List<Könyv>? Könyvek{ get; set; }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == "igen")
            {
                return RedirectToPage("/Index");
            }

            Felhasználó = new();

            Előkölcsönzés = new();

            Könyvek = new();

            Könyvek = _context.Könyvek.ToList();

            foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Előkölcsönzések).ToList())
            {
                if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                {
                    foreach (var elokolcson in felhasznalo.Előkölcsönzések.ToList())
                    {
                        Előkölcsönzés.Add(elokolcson);
                    }
                }
            }


            return Page();
        }
    }
}
