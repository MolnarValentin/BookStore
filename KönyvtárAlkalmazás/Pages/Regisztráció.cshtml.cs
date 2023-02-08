using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace KönyvtárAlkalmazás.Pages
{
    [BindProperties]
    public class RegisztrációModel : PageModel
    {
        

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(20, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        public string Vezetéknév { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(20, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        public string Keresztnév { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [EmailAddress(ErrorMessage = "Nem megfelelő e-mail formátum")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [RegularExpression(@"^((?:\+?3|0)6)(?:-|\()?(\d{1,2})(?:-|\))?(\d{3})-?(\d{3,4})$", ErrorMessage = "Nem megfelelő telefon formátum(HU)"), StringLength(30),]
        public string Telefonszám { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(15, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        public string Felhasználónév { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(15, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        public string Jelszó { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(15, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        [Compare("Jelszó",ErrorMessage ="A megadott jelszók nem egyeznek")]
        public string Jelszó2 { get; set; }

        private readonly ApplicationDbContext _context;

        private IWebHostEnvironment Environment;

        public RegisztrációModel(ApplicationDbContext context, IWebHostEnvironment environment)  
        {
            _context = context;
            Environment = environment;
        }
        public void OnGet()
        {
        }

       public IActionResult OnPost()
       {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            List<Admin> adminok = new List<Admin>();

            //Load the XML file in XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(this.Environment.WebRootPath,"/Adminok.xml"));

            //Loop through the selected Nodes.
            foreach (XmlNode node in doc.SelectNodes("/Rendszergazdak/Rendszergazda"))
            {
                //Fetch the Node values and assign it to Model.
                adminok.Add(new Admin
                {
                    Id = int.Parse(node["Id"].InnerText),
                    Felhasználónév = node["Felhasználónév"].InnerText,
                    Vezetéknév = node["Vezetéknév"].InnerText,
                    Keresztnév = node["Keresztnév"].InnerText,
                    Jelszó = node["Jelszó"].InnerText
                });
            }

            foreach (var admin in adminok)
            {
                if (admin.Felhasználónév == Felhasználónév)
                {
                    HttpContext.Session.SetString("FelhasznalonevFoglalt", "igen");
                    return Page();
                }
            }

            foreach (var item in _context.Felhasználók)
            {
                if (item.Felhasználónév == Felhasználónév)
                {
                    HttpContext.Session.SetString("FelhasznalonevFoglalt", "igen");
                    return Page();
                }

                if (item.Email == Email)
                {
                    HttpContext.Session.SetString("EmailFoglalt", "igen");
                    return Page();
                }
            }

            Felhasználó felhasználó = new Felhasználó();

            felhasználó.Vezetéknév = Vezetéknév;
            felhasználó.Keresztnév = Keresztnév;
            felhasználó.Felhasználónév = Felhasználónév;
            felhasználó.Telefonszám = Telefonszám;
            felhasználó.Email = Email;
            felhasználó.Jelszó = Jelszó;


            _context.Felhasználók.Add(felhasználó);

            _context.SaveChanges();

            HttpContext.Session.SetString("Bejelentkezve", "igen");
            HttpContext.Session.SetString("Felhasználónév", felhasználó.Felhasználónév);

            return RedirectToPage("/FelhasznaloPages/FKezdooldal");

        }
    }
}
