using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class FKezdooldalModel : PageModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [BindProperty]
        public string? Keresés { get; set; }

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment Environment;


        public FKezdooldalModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null)
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

            XmlDocument doc = new();

            doc.Load(string.Concat(this.Environment.WebRootPath, "/Keresések.xml"));

            //Create a new node.
            XmlElement elem = doc.CreateElement("Keresés");


            elem.InnerText = HttpContext.Session.GetString("Felhasználónév") + " - " + Keresés;

            //Add the node to the document.
            if (doc.DocumentElement is not null)
            {
                doc.DocumentElement.AppendChild(elem);
            }
            doc.Save(string.Concat(this.Environment.WebRootPath, "/Keresések.xml"));
            return RedirectToPage("/FelhasznaloPages/KeresesListazas", new { Keresés } );

        }
    }
}
