using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class KodMegadasaModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public KodMegadasaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? ElőkölcsönzésId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? KönyvId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [Range(0,999,ErrorMessage = "A számnak 0 és 999 közöttinek kell lennie")]
        public int Azonositokod { get; set; }

        public bool SikertelenAzonositas { get; set; }

        public bool SikeresKölcsönzés { get; set; }

        public Előkölcsönzés Előkölcsönzés { get; set; }

        public Könyv Könyv { get; set; }

        public Kölcsönzés Kölcsönzés { get; set; }

        public Felhasználó Felhasználó { get; set; }



        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == "igen")
            {
                return RedirectToPage("/Index");
            }

            if (ElőkölcsönzésId == null || KönyvId == null)
            {
                return RedirectToPage("/FelhasznaloPages/Előkölcsönzéseim");
            }


            Előkölcsönzés = new();

            Azonositokod = new();

            foreach (var elokolcsonzes in _context.Előkölcsönzések.ToList())
            {
                if (elokolcsonzes.Id == ElőkölcsönzésId)
                {
                    Előkölcsönzés = elokolcsonzes;
                }
            }

            if (Előkölcsönzés == null || ElőkölcsönzésId == null || KönyvId == null || Előkölcsönzés.KezdetiDátum > DateTime.Now || Előkölcsönzés.LejáratiDátum < DateTime.Now)
            {
                return RedirectToPage("/FelhasznaloPages/Előkölcsönzéseim");
            }

            return Page();

            
        }

        public IActionResult OnPost()
        {

            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Előkölcsönzés = new();

            Kölcsönzés = new();


            foreach (var elokolcsonzes in _context.Előkölcsönzések.ToList())
            {
                if (elokolcsonzes.Id == ElőkölcsönzésId)
                {
                    Előkölcsönzés = elokolcsonzes;
                }
            }

            if (Előkölcsönzés == null || ElőkölcsönzésId == null || KönyvId == null || Előkölcsönzés.KezdetiDátum > DateTime.Now || Előkölcsönzés.LejáratiDátum < DateTime.Now)
            {
                return RedirectToPage("/FelhasznaloPages/Előkölcsönzéseim");
            }

            int azonositokod = Előkölcsönzés.AzonosítóKód;

            if (Azonositokod == azonositokod)
            {


                foreach (var felhasznalo in _context.Felhasználók.Include(x => x.Előkölcsönzések))
                {
                    if (felhasznalo.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                    {
                        felhasznalo.Előkölcsönzések.Remove(Előkölcsönzés);
                    }
                }

                _context.SaveChanges();

                Felhasználó = new();

                foreach (var item in _context.Felhasználók)
                {
                    if (item.Felhasználónév == HttpContext.Session.GetString("Felhasználónév"))
                    {
                        Felhasználó = item;
                    }
                }


                foreach (var item in _context.Könyvek.ToList())
                {
                    if (item.Id == KönyvId)
                    {
                        SikeresKölcsönzés = true;

                        item.Előkölcsönözték = false;
                        item.Kikölcsönözték = true;

                        _context.Könyvek.Update(item);


                        _context.SaveChanges();



                        Kölcsönzés.LejáratiDátum = DateTime.Now.AddMonths(1);

                        Kölcsönzés.KölcsönzőEmailCíme = Felhasználó.Email;

                        Kölcsönzés.KölcsönzőTelefonszáma = Felhasználó.Telefonszám;

                        Kölcsönzés.Könyv = item;

                        _context.Kölcsönzések.Add(Kölcsönzés);

                        _context.SaveChanges();

                        if (Felhasználó.Kölcsönzések == null)
                        {
                            Felhasználó.Kölcsönzések = new();
                        }

                        Felhasználó.Kölcsönzések.Add(Kölcsönzés);

                        _context.SaveChanges();
                    }
                }

                _context.SaveChanges();

            }
            else
            {
                SikertelenAzonositas = true;
            }

            return RedirectToPage("/FelhasznaloPages/SikeresKodAktivalas", new { SikertelenAzonositas, SikeresKölcsönzés, KönyvId });
        }
    }
}
