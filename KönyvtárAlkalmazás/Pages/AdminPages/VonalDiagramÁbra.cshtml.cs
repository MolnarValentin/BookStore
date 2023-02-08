using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Pages.AdminPages
{
    public class VonalDiagramÁbraModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VonalDiagramÁbraModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime KezdetiDátum { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime VégDátum { get; set; }

        public List<string> Nevek { get; set; }
        public List<int> Db { get; set; }

        public void OnGet()
        {

            if (HttpContext.Session.GetString("Bejelentkezve") != "igen" || HttpContext.Session.GetString("Felhasználónév") == null || HttpContext.Session.GetString("Admin") == null)
            {
                RedirectToPage("/Index");
            }

            Dictionary<string, int> dict = new();

            DateTime KezdetiDatumtemp = KezdetiDátum;
            DateTime VegDatumtemp = VégDátum;

            int temp = 1;

            while (KezdetiDatumtemp < VegDatumtemp && temp != 12)
            {
                if (KezdetiDatumtemp.ToString("MMMM") == "január")
                {
                    dict.Add("January", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "február")
                {
                    dict.Add("February", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "március")
                {
                    dict.Add("March", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "április")
                {
                    dict.Add("April", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "május")
                {
                    dict.Add("May", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "június")
                {
                    dict.Add("June", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "július")
                {
                    dict.Add("July", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "augusztus")
                {
                    dict.Add("August", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "szeptember")
                {  
                    dict.Add("September", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "október")
                {
                    dict.Add("October", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "november")
                {
                    dict.Add("November", 0);
                }
                else if (KezdetiDatumtemp.ToString("MMMM") == "december")
                {
                    dict.Add("December", 0);
                }
                KezdetiDatumtemp = KezdetiDatumtemp.AddMonths(1);
                ++temp;
            }

            var kolcsonzesek = _context.Kölcsönzések.ToList();

            List<Kölcsönzés> Kölcsönzések = new();

            foreach (var kolcson in kolcsonzesek)
            {
                if (kolcson.LejáratiDátum.AddMonths(-1).Date >= KezdetiDátum.Date && kolcson.LejáratiDátum.AddMonths(-1).Date <= VégDátum.Date)
                {
                    Kölcsönzések.Add(kolcson);
                }
            }

            int jan = 0;
            int feb = 0;
            int mar = 0;
            int apr = 0;
            int maj = 0;
            int jun = 0;
            int jul = 0;
            int aug = 0;
            int sze = 0;
            int okt = 0;
            int nov = 0;
            int dec = 0;

            foreach (var kolcson in Kölcsönzések)
            {
                if (kolcson.LejáratiDátum.ToString("MMMM") == "január")
                {
                    dict["January"] = ++jan;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "február")
                {
                    dict["February"] = ++feb;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "március")
                {
                    dict["March"] = ++mar;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "április")
                {
                    dict["April"] = ++apr;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "május")
                {
                    dict["May"] = ++maj;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "június")
                {
                    dict["June"] = ++jun;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "július")
                {
                    dict["July"] = ++jul;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "augusztus")
                {
                    dict["August"] = ++aug;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "szeptember")
                {
                    dict["September"] = ++sze;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "október")
                {
                    dict["October"] = ++okt;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "november")
                {
                    dict["November"] = ++nov;
                }
                else if (kolcson.LejáratiDátum.ToString("MMMM") == "december")
                {
                    dict["December"] = ++dec;
                }
            }

            Nevek = new(dict.Keys);
            Db = new(dict.Values);
        }
    }
}
