namespace KönyvtárAlkalmazás.Models
{
    public class Felhasználó
    {
        public int Id { get; set; }

        public string Felhasználónév { get; set; }

        public string Vezetéknév { get; set; }

        public string Keresztnév { get; set; }

        public string Email { get; set; }

        public string Jelszó { get; set; }

        public string Telefonszám { get; set; }

        public List<Kölcsönzés> Kölcsönzések { get; set; }

        public List<Előkölcsönzés> Előkölcsönzések { get; set; }
    }
}
