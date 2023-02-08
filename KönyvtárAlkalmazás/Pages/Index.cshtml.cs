using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace KönyvtárAlkalmazás.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet=true)]
        public string? Kijelentkezett { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(20, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        [BindProperty]
        public string? Felhasználónév { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező")]
        [StringLength(15, ErrorMessage = "Név hossza nem lehet több,mint {0}.")]
        [BindProperty]
        public string? Jelszó { get; set; }


        private readonly IWebHostEnvironment Environment;

        private readonly ApplicationDbContext _context;


        public IndexModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.Environment = environment;  
        }


        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var felhasznalo in _context.Felhasználók)
            {

                if (felhasznalo.Felhasználónév == Felhasználónév && felhasznalo.Jelszó == Jelszó)
                {
                    HttpContext.Session.SetString("Bejelentkezve", "igen");
                    HttpContext.Session.SetString("Felhasználónév", felhasznalo.Felhasználónév);
                    return RedirectToPage("/FelhasznaloPages/Fkezdooldal");
                }
            }

            List<Admin> adminok = new();

            //Load the XML file in XmlDocument.
            XmlDocument doc = new();
            doc.Load(string.Concat(this.Environment.WebRootPath,"/Adminok.xml"));

            XmlNodeList? nodes = doc.SelectNodes("/Rendszergazdak/Rendszergazda");

            if (nodes != null)
            {
                //Loop through the selected Nodes.
                foreach (XmlNode node in nodes)
                {
                    //Fetch the Node values and assign it to Model
                            adminok.Add(new Admin
                            {
                                Id = int.Parse(node["Id"].InnerText),
                                Felhasználónév = node["Felhasználónév"].InnerText,
                                Vezetéknév = node["Vezetéknév"].InnerText,
                                Keresztnév = node["Keresztnév"].InnerText,
                                Jelszó = node["Jelszó"].InnerText
                            });

                        

                }
            }


            foreach (var admin in adminok)
            {
                if (admin.Felhasználónév == Felhasználónév && admin.Jelszó == Jelszó)
                {
                    HttpContext.Session.SetString("Bejelentkezve", "igen");
                    HttpContext.Session.SetString("Admin", "igen");
                    HttpContext.Session.SetString("Felhasználónév", admin.Felhasználónév);
                    return RedirectToPage("/AdminPages/AKezdooldal");
                }
            }

            HttpContext.Session.SetString("Hibas", "igen");


            return RedirectToPage("/Index");

        }
    }
}