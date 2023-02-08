using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class KörDiagramÁbraModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public KörDiagramÁbraModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public DateTime KezdetiDátum { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime VégDátum { get; set; }

        public int[] dbkonyv { get; set; }

        public string[] irokegyesevel { get; set; }

        public List<int> dbkonyvtemp { get; set; }

        public List<string> irokegyeseveltemp { get; set; }



        public void OnGet()
        {
            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == null)
            {
                RedirectToPage("/Index");
            }

            var kolcsonzesek = _context.Kölcsönzések.Include(kolcson => kolcson.Könyv).ToList();

            List<Könyv> könyvek = new();


            foreach (var item in kolcsonzesek)
            {
                if (item.LejáratiDátum.AddMonths(-1).Date > KezdetiDátum.Date && item.LejáratiDátum.AddMonths(-1).Date <= VégDátum.Date)
                {
                    könyvek.Add(item.Könyv);
                }
            }

            string[] irok = new string[könyvek.Count];

            List<string> iroktemp = new();



            foreach (var item in _context.Könyvek.ToList())
            {
                foreach (var item2 in könyvek)
                {
                    if (item.Id == item2.Id)
                    {
                        iroktemp.Add(item.Író);
                    }
                }
            }

            for (int i = 0; i < könyvek.Count; i++)
            {
                irok[i] = iroktemp[i];
            }

            Array.Sort(irok);


            var groups = irok.GroupBy(v => v);

            irokegyeseveltemp = new();

            dbkonyvtemp = new();


            foreach (var group in groups)
            {
                irokegyeseveltemp.Add(group.Key);
                dbkonyvtemp.Add(group.Count());
            }
        }
    }
}
