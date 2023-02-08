using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace KönyvtárAlkalmazás.Models
{
    public class Könyv
    {
        public int Id { get; set; } 

        public string Cím { get; set; }

        public string Író { get; set; }

        public string? Kiadó { get; set; }

        public long ISBN { get; set; }

        public bool Kikölcsönözték { get; set; }

        public bool Előkölcsönözték { get; set; }



    }
}
