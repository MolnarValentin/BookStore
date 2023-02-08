using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    [BindProperties]
    public class ÚjKölcsönzésModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ÚjKölcsönzésModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public string Felahsználónév { get; set; }

        [Range(1000000000000, 9999999999999, ErrorMessage = "A számnak 13 számjegyűnek kell lennie")]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        public long ISBN { get; set; }

        [BindProperty]
        public bool ValidISBN { get; set; } = true;

        [BindProperty]
        public bool ValidFelhasználónév { get; set; } = true;

        [BindProperty]
        public bool MárKikölcsönözték { get; set; }

        [BindProperty]
        public DateTime Lejarat { get; set; }

        [BindProperty]
        public bool Előkölcsönözték { get; set; }

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

            Könyv könyv = new();


            foreach (var konyv in _context.Könyvek.ToList())
            {
                if (konyv.ISBN == ISBN)
                {
                    if (konyv.Kikölcsönözték == true)
                    {
                        MárKikölcsönözték = true;
                        return Page();
                    }
                    if (konyv.Előkölcsönözték == true)
                    {
                        Előkölcsönözték = true;
                        return Page();
                    }
                    konyv.Kikölcsönözték = true;
                    könyv = konyv;

                    _context.Könyvek.Update(konyv);
                }
            }

            _context.SaveChanges();


            Kölcsönzés kölcsönzés = new();

            string email = "";

            string telefon = "";

            foreach (var felhasznalo in _context.Felhasználók.ToList())
            {
                if (felhasznalo.Felhasználónév == Felahsználónév)
                {
                    email = felhasznalo.Email;
                    telefon = felhasznalo.Telefonszám;
                }
            }


            kölcsönzés.LejáratiDátum = DateTime.Now.AddMonths(1);
            kölcsönzés.KölcsönzőEmailCíme = email;
            kölcsönzés.KölcsönzőTelefonszáma = telefon;
            kölcsönzés.Könyv = könyv;

            Lejarat = new();

            Lejarat = kölcsönzés.LejáratiDátum;

            foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Kölcsönzések).ToList())
            {
                if (felhasznalo.Felhasználónév == Felahsználónév)
                {
                    _context.Kölcsönzések.Add(kölcsönzés);
                    if (felhasznalo.Kölcsönzések == null)
                    {
                        felhasznalo.Kölcsönzések = new();
                        felhasznalo.Kölcsönzések.Add(kölcsönzés);
                    }
                    else
                    {
                        felhasznalo.Kölcsönzések.Add(kölcsönzés);
                    }       
                    
                    _context.Felhasználók.Update(felhasznalo);
                }
            }

            _context.SaveChanges();



            return RedirectToPage("/AdminPages/ÚjKölcsönzésVisszaigazolás", new { Lejarat });
        }


    }
}
