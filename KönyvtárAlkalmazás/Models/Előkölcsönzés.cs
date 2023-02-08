namespace KönyvtárAlkalmazás.Models
{
    public class Előkölcsönzés
    {
        public int Id { get; set; }

        public DateTime KezdetiDátum { get; set; }

        public DateTime LejáratiDátum { get; set; }

        public string KölcsönzőEmailCíme { get; set; }

        public string KölcsönzőTelefonszáma { get; set; }

        public Könyv Könyv { get; set; }

        public int AzonosítóKód { get; set; }

    }
}
