using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PR89_2017_KOL2.Helpers
{
    public static class CitanjePodataka
    {
        public static string path = $"{System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/App_Data/Korisnici.txt";

        public static Dictionary<String,Korisnik> citajKorisnike()
        {
            Dictionary<String, Korisnik> korisnici = new Dictionary<string, Korisnik>();
            
            string[] users = System.IO.File.ReadAllLines(path);

            for(int i=0; i<users.Length; i++)
            {
                string[] user = users[i].Split(',');
                korisnici.Add(user[0], new Korisnik {
                    Id = i + 1,
                    KorisnickoIme = user[0],
                    Lozinka = user[1],
                    Ime = user[2],
                    Prezime = user[3],
                    Pol = (Sex)Enum.Parse(typeof(Sex), user[4]),
                    Email = user[5],
                    DatumRodjenja = DateTime.Parse(user[6]),
                    Uloga = (Role)Enum.Parse(typeof(Role), user[7])
                }); 
            }
            return korisnici;
        }

        public static bool pisiKorisnika(Korisnik korisnik)
        {
            using(System.IO.StreamWriter sw = System.IO.File.AppendText(path))
            {

            }
            return false;
        }
    }
}