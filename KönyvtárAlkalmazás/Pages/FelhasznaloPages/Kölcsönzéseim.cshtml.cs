using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class KölcsönzéseimModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public KölcsönzéseimModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Kölcsönzés>? Kölcsönzések { get; set; }


        public string? Email { get; set; }

        public List<Könyv> Könyvek { get; set; }

        public void OnGet()
        {
            Kölcsönzések = new();

            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == "igen" )
            {
                RedirectToPage("/Index");
            }


            foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Kölcsönzések).ThenInclude(x => x.Könyv).ToList())
            {
                if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                {
                    foreach (var kolcsonzes in felhasznalo.Kölcsönzések.ToList())
                    {
                        Kölcsönzések.Add(kolcsonzes);

                    }
                }
            }
            
            Könyvek = _context.Könyvek.ToList();
        }
    }
}
