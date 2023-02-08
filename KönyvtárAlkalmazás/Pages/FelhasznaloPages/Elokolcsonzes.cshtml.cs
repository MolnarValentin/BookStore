using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class ElokolcsonzesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int? KönyvId { get; set; }

        private readonly ApplicationDbContext _context;

        public ElokolcsonzesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Könyv? Könyv { get; set; }

        public Előkölcsönzés? Előkölcsönzés { get; set; }

        public Felhasználó? Felhasznalotemp { get; set; }

        public DateTime KezdetiDatum { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == "igen")
            {
                return RedirectToPage("/Index");
            }
            if (KönyvId == null)
            {
                return RedirectToPage("/FelhansznaloPages/Fkezdooldal");
            }

            Előkölcsönzés = new();

            Felhasznalotemp = new();

            Könyv = new();

            foreach (var felhasznalo in _context.Felhasználók.ToList())
            {
                if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                {
                    Felhasznalotemp = felhasznalo;
                }
            }

            foreach (var konyv in _context.Könyvek.ToList())
            {
                if (konyv.Id == KönyvId)
                {
                    if (konyv.Kikölcsönözték != true || konyv.Előkölcsönözték == true)
                    {
                        return RedirectToPage("/FelhasznaloPages/Fkezdooldal");
                    }
                    konyv.Előkölcsönözték = true;
                    Könyv = konyv;


                    foreach (var kolcson in _context.Kölcsönzések.ToList())
                    {
                        if (kolcson.Könyv == Könyv && kolcson.KölcsönzőEmailCíme == Felhasznalotemp.Email)
                        {
                            return RedirectToPage("/FelhasznaloPages/Fkezdooldal");
                        }
                    }

                    _context.Könyvek.Update(konyv);
                }
            }

            foreach (var kolcson in _context.Kölcsönzések.ToList())
            {
                if (kolcson.Könyv.Id == KönyvId)
                {
                    KezdetiDatum = kolcson.LejáratiDátum;
                }
            }

            Random rnd = new();

            Előkölcsönzés.KezdetiDátum = KezdetiDatum;
            Előkölcsönzés.LejáratiDátum = KezdetiDatum.AddDays(7);
            Előkölcsönzés.KölcsönzőEmailCíme = Felhasznalotemp.Email;
            Előkölcsönzés.KölcsönzőTelefonszáma = Felhasznalotemp.Telefonszám;
            Előkölcsönzés.Könyv = this.Könyv;
            Előkölcsönzés.AzonosítóKód = rnd.Next(999);

            _context.Előkölcsönzések.Add(Előkölcsönzés);

            if (Felhasznalotemp.Előkölcsönzések == null)
            {
                Felhasznalotemp.Előkölcsönzések = new();

            }
            Felhasznalotemp.Előkölcsönzések.Add(Előkölcsönzés);

            _context.Felhasználók.Update(Felhasznalotemp);

            _context.SaveChanges();

            return Page();
        }
    }
}
