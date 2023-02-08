using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KönyvtárAlkalmazás.Pages.FelhasznaloPages
{
    public class SikeresKodAktivalasModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public SikeresKodAktivalasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? KönyvId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SikertelenAzonositas { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool SikeresKölcsönzés { get; set; }

        public Könyv Könyv { get; set; }

        public Kölcsönzés Kölcsönzés { get; set; }

        public DateTime Lejárat { get; set; }

        public void OnGet()
        {


            foreach (var item in _context.Könyvek)
            {
                if (item.Id == KönyvId)
                {
                    Könyv = item;
                }
            }

            foreach (var item in _context.Kölcsönzések)
            {
                if (item.Könyv.Id == KönyvId)
                {
                    Kölcsönzés = item;
                    Lejárat = Kölcsönzés.LejáratiDátum;
                }
            }


        }
    }
}
