using KönyvtárAlkalmazás.Data;
using KönyvtárAlkalmazás.Models;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Seed
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Könyvek.Any())
                {
                    return;
                }

                if (context.Felhasználók.Any())
                {
                    return;
                }

                if (context.Előkölcsönzések.Any())
                {
                    return;
                }

                if (context.Kölcsönzések.Any())
                {
                    return;
                }

                context.Könyvek.AddRange(
                    new Könyv { Cím = "Tiny C# Projects", Író = "AXD 2029" , Kiadó = "", ISBN = 9786156647271, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Tiny C# Projects", Író = "AXD 2029" , Kiadó = "", ISBN = 9787776147272, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Tiny C# Projects", Író = "AXD 2029" , Kiadó = "", ISBN = 9786785147273, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Tiny C# Projects", Író = "AXD 2029" , Kiadó = "", ISBN = 9786156999974, Kikölcsönözték = false, Előkölcsönözték  = false},

                    new Könyv { Cím = "Atomic Habits", Író = "James Clear", Kiadó = "Penguin Random House UK", ISBN = 9781847941831, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Atomic Habits", Író = "James Clear", Kiadó = "Penguin Random House UK", ISBN = 9781847941878, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Atomic Habits", Író = "James Clear", Kiadó = "Penguin Random House UK", ISBN = 9781847941836, Kikölcsönözték = false, Előkölcsönözték = false},


                    new Könyv { Cím = "DARK NET", Író = "Stefan Mey", Kiadó = "Magistra", ISBN = 9786156147271, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "DARK NET", Író = "Stefan Mey", Kiadó = "Magistra", ISBN = 9785556147272, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "DARK NET", Író = "Stefan Mey", Kiadó = "Magistra", ISBN = 9786156147645, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "DARK NET", Író = "Stefan Mey", Kiadó = "Magistra", ISBN = 9786156147661, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "DARK NET", Író = "Stefan Mey", Kiadó = "Magistra", ISBN = 9786776188271, Kikölcsönözték = false, Előkölcsönözték = false},


                    new Könyv { Cím = "Harry Potter és a bölcsek köve", Író = "J. K. Rowling", Kiadó = "animus", ISBN = 9786156187771, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Harry Potter és a bölcsek köve", Író = "J. K. Rowling", Kiadó = "animus", ISBN = 9786156887779, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Harry Potter és a bölcsek köve", Író = "J. K. Rowling", Kiadó = "animus", ISBN = 9788856187778, Kikölcsönözték = false, Előkölcsönözték = false},
                    new Könyv { Cím = "Harry Potter és a bölcsek köve", Író = "J. K. Rowling", Kiadó = "animus", ISBN = 9786676187777, Kikölcsönözték = false, Előkölcsönözték = false}

                    );
                context.Felhasználók.AddRange(
                    new Felhasználó { Felhasználónév = "Random", Keresztnév = "Random", Vezetéknév = "Random", Email = "random@random.hu", Telefonszám = "06301234567", Jelszó ="random"},
                    new Felhasználó { Felhasználónév = "b", Keresztnév ="b", Vezetéknév = "b", Email ="b@b.hu", Telefonszám = "06301111111", Jelszó ="b"},
                    new Felhasználó  {Felhasználónév = "Jozsika01", Keresztnév = "József", Vezetéknév = "Kovács", Email = "kovacs@gmail.com", Telefonszám = "06301111111", Jelszó = "asd123"},
                    new Felhasználó { Felhasználónév = "Bazsa55", Keresztnév ="Balázs", Vezetéknév = "Nagy", Email ="bigbalazs@citromail.hu", Telefonszám = "06701234567", Jelszó ="321dsa"},
                    new Felhasználó { Felhasználónév = "Dani78", Keresztnév ="Dániel", Vezetéknév = "Tóth", Email ="daniel@gmail.com", Telefonszám = "06302222222", Jelszó ="erosjelszo99"}
                    );

                context.SaveChanges();
            }
        }
    }
}
