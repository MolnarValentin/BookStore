using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;

namespace KönyvtárAlkalmazás.Models
{
    public class Kölcsönzés
    {
        public int Id { get; set; }

        public DateTime LejáratiDátum { get; set; }

        public string KölcsönzőEmailCíme { get; set; }

        public string KölcsönzőTelefonszáma { get; set; }

        public Könyv Könyv { get; set; }
    }
}
