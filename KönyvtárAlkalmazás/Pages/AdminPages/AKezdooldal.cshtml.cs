using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class AKezdooldalModel : PageModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [BindProperty]
        public string? Keresés { get; set; }

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment Environment;


        public AKezdooldalModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToPage("/Index");
            }





            foreach (var felhasználó in _context.Felhasználók.Include(x => x.Kölcsönzések).ThenInclude(x => x.Könyv).ToList())
            {
                if (felhasználó.Kölcsönzések != null)
                {
                    foreach (var kolcsonzes in felhasználó.Kölcsönzések.ToList())
                    {
                        if (kolcsonzes.LejáratiDátum < DateTime.Now)
                        {
                            kolcsonzes.Könyv.Kikölcsönözték = false;
                            _context.Könyvek.Update(kolcsonzes.Könyv);

                            felhasználó.Kölcsönzések.Remove(kolcsonzes);
                            _context.Felhasználók.Update(felhasználó);
                        }
                    }
                }
            }

            _context.SaveChanges();

            foreach (var felhasználó in _context.Felhasználók.Include(x => x.Előkölcsönzések).ThenInclude(x => x.Könyv).ToList())
            {
                if (felhasználó.Előkölcsönzések != null)
                {
                    foreach (var elokolcsonzes in felhasználó.Előkölcsönzések.ToList())
                    {
                        if (elokolcsonzes.LejáratiDátum < DateTime.Now)
                        {
                            elokolcsonzes.Könyv.Előkölcsönözték = false;
                            _context.Könyvek.Update(elokolcsonzes.Könyv);

                            felhasználó.Előkölcsönzések.Remove(elokolcsonzes);
                            _context.Felhasználók.Update(felhasználó);

                        }
                    }
                }
            }


            _context.SaveChanges();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/AdminPages/KeresesListazas", new { Keresés });
        }

    }
}