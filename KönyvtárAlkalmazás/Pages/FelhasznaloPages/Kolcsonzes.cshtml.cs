using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class KolcsonzesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? KönyvId { get; set; }

        private readonly ApplicationDbContext _context;

        public KolcsonzesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Könyv? Könyv { get; set; }

        public Kölcsönzés? Kolcsonzes { get; set; }

        public Felhasználó? Felhasznalotemp { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == "igen")
            {
                return RedirectToPage("/Index");
            }
            if (KönyvId == null)
            {
                return RedirectToPage("/FelhasznaloPages/Fkezdooldal");
            }
            Kolcsonzes = new();

            Felhasznalotemp = new();

            Könyv = new();

            foreach (var könyv in _context.Könyvek.ToList())
            {
                if (könyv.Id == KönyvId)
                {
                    if (könyv.Kikölcsönözték == true || könyv.Előkölcsönözték == true)
                    {
                        return RedirectToPage("/FelhasznaloPages/Fkezdooldal");
                    }

                    könyv.Kikölcsönözték = true;
                    Könyv = könyv;
                    _context.Könyvek.Update(könyv);
                }
            }

            foreach (var felhasznalo in _context.Felhasználók.ToList())
            {
                if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                {
                    Felhasznalotemp = felhasznalo;
                }
            }

            Kolcsonzes.LejáratiDátum = DateTime.Now.AddMonths(1);
            Kolcsonzes.KölcsönzőEmailCíme = Felhasznalotemp.Email;
            Kolcsonzes.KölcsönzőTelefonszáma = Felhasznalotemp.Telefonszám;
            Kolcsonzes.Könyv = Könyv;

            _context.Kölcsönzések.Add(Kolcsonzes);

            if (Felhasznalotemp.Kölcsönzések == null)
            {
                Felhasznalotemp.Kölcsönzések = new();
            }

            Felhasznalotemp.Kölcsönzések.Add(Kolcsonzes);
            

            _context.Felhasználók.Update(Felhasznalotemp);

            _context.SaveChanges(); 


            return Page();


        }
    }
}
