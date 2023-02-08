using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class KeresesListazasModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Keresés { get; set; }

        private readonly ApplicationDbContext _context;

        public KeresesListazasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Könyv>? KönyvekLista { get; set; }

        public Felhasználó? Felhasználó { get; set; }

        public List<Kölcsönzés>? Kölcsönzések { get; set; }

        public bool ElőrendelElrejt { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || Keresés == null || HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToPage("/Index");
            }


            KönyvekLista = new();

            Kölcsönzések = new();

            Felhasználó = new();

            Kölcsönzések = _context.Kölcsönzések.ToList();

            foreach (var felhasznalo in _context.Felhasználók.ToList())
            {
                if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                {
                    Felhasználó = felhasznalo;
                }
            }

            if (Keresés != null)
            {


                if (_context.Könyvek != null)
                {

                    foreach (var konyv in _context.Könyvek.ToList())
                    {
                        long temp;

                        if (konyv.Cím.ToLower() == Keresés.ToLower() || konyv.Író.ToLower() == Keresés.ToLower() || (long.TryParse(Keresés, out temp) && temp == konyv.ISBN) || (konyv.Kiadó != null && konyv.Kiadó.ToLower() == Keresés.ToLower()))
                        {
                            KönyvekLista.Add(konyv);
                        }
                    }

                }



            }

            return Page();
        }
    }
}
