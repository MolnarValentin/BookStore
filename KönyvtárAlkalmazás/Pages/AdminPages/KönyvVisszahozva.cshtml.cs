using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class KönyvVisszahozvaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public KönyvVisszahozvaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public string Felahsználónév { get; set; }

        [BindProperty]
        [Range(1000000000000, 9999999999999, ErrorMessage = "A számnak 13 számjegyűnek kell lennie")]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public long ISBN { get; set; }

        [BindProperty]
        public bool ValidISBN { get; set; } = true;

        [BindProperty]
        public bool ValidFelhasználónév { get; set; } = true;

        [BindProperty]
        public bool MégNemKölcsönöztékKi { get; set; }

        [BindProperty]
        public bool FelhasznaloNemKolcsonozte { get; set; }


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

            ValidISBN = false;

            foreach (var konyv in _context.Könyvek.ToList())
            {
                if (konyv.ISBN == ISBN)
                {
                    ValidISBN = true;
                }
            }

            if (ValidISBN == false)
            {
                return Page();
            }

            ValidFelhasználónév = false;

            foreach (var felhasznalo in _context.Felhasználók.ToList())
            {
                if (felhasznalo.Felhasználónév == Felahsználónév)
                {
                    ValidFelhasználónév = true;
                }
            }

            if (ValidFelhasználónév == false)
            {
                return Page();
            }


            foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Kölcsönzések).ThenInclude(x => x.Könyv).ToList())
            {

                if (felhasznalo.Felhasználónév == Felahsználónév)
                {
                    if (felhasznalo.Kölcsönzések != null)
                    {
                        FelhasznaloNemKolcsonozte = true;

                        foreach (var kolcsonzes in felhasznalo.Kölcsönzések.ToList())
                        {
                            if (kolcsonzes.Könyv.ISBN == ISBN)
                            {
                                FelhasznaloNemKolcsonozte = false;

                                felhasznalo.Kölcsönzések.Remove(kolcsonzes);
                                _context.Felhasználók.Update(felhasznalo);
                            }
                        }

                        if (FelhasznaloNemKolcsonozte == true)
                        {
                            return Page();
                        }
                    }
                    else
                    {
                        FelhasznaloNemKolcsonozte = true;
                        return Page();
                    }
                    
                }
            }

            _context.SaveChanges();

            Könyv könyv = new();


            foreach (var konyv in _context.Könyvek.ToList())
            {
                if (konyv.ISBN == ISBN)
                {
                    if (konyv.Kikölcsönözték == false)
                    {
                        MégNemKölcsönöztékKi = true;
                        return Page();
                    }
                    konyv.Kikölcsönözték = false;
                    könyv = konyv;

                    _context.Könyvek.Update(konyv);
                }
            }

            if (könyv.Előkölcsönözték)
            {
                foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Előkölcsönzések).ThenInclude(y => y.Könyv).ToList())
                {
                    foreach (var elokolcson in felhasznalo.Előkölcsönzések.ToList())
                    {
                        if (elokolcson.Könyv == könyv)
                        {
                            elokolcson.KezdetiDátum = DateTime.Now;
                            elokolcson.LejáratiDátum = DateTime.Now.AddDays(7);
                        }
                    }
                }
            }

            _context.SaveChanges();


            return RedirectToPage("/AdminPages/KönyvVisszahozvaVisszaigazolás");
        }
    }
}
