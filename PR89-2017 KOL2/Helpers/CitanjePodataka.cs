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
        public static string pathKorisnik = $"{System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/App_Data/Korisnici.txt";
        public static string pathVozilo = $"{System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/App_Data/Vozila.txt";
        public static string pathKupovina = $"{System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/App_Data/Kupovina.txt";

        public static Dictionary<String,Korisnik> citajKorisnike()
        {
            Dictionary<String, Korisnik> korisnici = new Dictionary<string, Korisnik>();

            if (!System.IO.File.Exists(pathKorisnik))
                System.IO.File.Create(pathKorisnik);
            using (System.IO.StreamReader sr = System.IO.File.OpenText(pathKorisnik))
            {
                try
                {
                    string line = "";
                    int brojac = 0;
                    while((line = sr.ReadLine()) != null) {
                        string[] user = line.Split('|');
                        korisnici.Add(user[0], new Korisnik
                        {
                            Id = ++brojac,
                            KorisnickoIme = user[0],
                            Lozinka = user[1],
                            Ime = user[2],
                            Prezime = user[3],
                            Pol = (Sex)Enum.Parse(typeof(Sex), user[4]),
                            Email = user[5],
                            DatumRodjenja = DateTime.ParseExact(user[6], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                            Uloga = (Role)Enum.Parse(typeof(Role), user[7]),
                            Obrisan = bool.Parse(user[8])
                        });
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return korisnici;
        }

        public static bool pisiKorisnika(Korisnik korisnik)
        {

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(pathKorisnik))
            {
                try
                {
                    sw.WriteLine(korisnik.ToString());
                    return true;
                   
                }
                catch(Exception e)
                {
                    //neka greska
                    return false;
                }
            }
        }
        public static bool izmeniKorisnika(Dictionary<string,Korisnik> korisnici)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(pathKorisnik))
            {
                try
                {
                    foreach(Korisnik korisnik in korisnici.Values)
                    {
                        sw.WriteLine(korisnik.ToString());
                    }

                    return true;

                }
                catch (Exception e)
                {
                    //neka greska
                    return false;
                }
            }

        }

        public static List<Vozilo> citajVozila()
        {
            List<Vozilo> vozila = new List<Vozilo>();
            if (!System.IO.File.Exists(pathVozilo))
                System.IO.File.Create(pathVozilo);
            using (System.IO.StreamReader sr = System.IO.File.OpenText(pathVozilo))
            {
                try
                {
                    string line = "";
                    int brojac = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] vozilo = line.Split('|');
                        vozila.Add(new Vozilo
                        {
                            Id = ++brojac,
                            Marka = vozilo[0],
                            Model = vozilo[1],
                            OznakaSasije = vozilo[2],
                            Boja = vozilo[3],
                            BrojVrata = Int32.Parse(vozilo[4]),
                            Opis = vozilo[5],
                            VrstaGoriva = (Fuel)Enum.Parse(typeof(Fuel), vozilo[6]),
                            Cena = Double.Parse(vozilo[7]),
                            NaStanju = bool.Parse(vozilo[8]),
                            KupacId = Int32.Parse(vozilo[9])

                        });
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("exception se desio!!!");
                }
            }
            return vozila;
        }

        public static bool pisiVozilo(Vozilo vozilo)
        {
  
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(pathVozilo))
            {
                try
                {
                    sw.WriteLine(vozilo.ToString());
                    return true;

                }
                catch (Exception e)
                {
                    //neka greska
                    return false;
                }
            }
        }
        public static bool izmeniVozilo(List<Vozilo> vozila)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(pathVozilo))
            {
                try
                {
                    for (int i = 0; i < vozila.Count; i++)
                    {
                        sw.WriteLine(vozila[i].ToString());
                    }
                    
                    return true;

                }
                catch (Exception e)
                {
                    //neka greska
                    return false;
                }
            }
            
        }

        public static List<Kupovina> citajKupovinu()
        {
            List<Kupovina> kupovina = new List<Kupovina>();
            if (!System.IO.File.Exists(pathKupovina))
            {
                System.IO.File.Create(pathKupovina);
                return kupovina;
            }
                
            using (System.IO.StreamReader sr = System.IO.File.OpenText(pathKupovina))
            {
                try
                {
                    string line = "";
                    int brojac = 0;
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] kup = line.Split('|');
                        Kupovina kupac = new Kupovina();
     
                        kupovina.Add(new Kupovina {
                            Id = ++brojac,
                            Kupac = citajKorisnike().Values.Where(k => k.Id == Int32.Parse(kup[0])).Select(k => k).SingleOrDefault(),
                            _Vozilo = citajVozila().Where(v => v.Id == Int32.Parse(kup[1])).Select(v => v).SingleOrDefault(),
                            DatumKupovine = DateTime.ParseExact(kup[2], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                            NaplacenaCena = double.Parse(kup[3])

                        }); 

                    }
                }
                catch (Exception e)
                {
                    //neka greska
                }
            }
            return kupovina;
        }

        public static bool pisiKupovinu(Kupovina kupovina)
        {

            using (System.IO.StreamWriter sw = System.IO.File.AppendText(pathKupovina))
            {
                try
                {
                    sw.WriteLine(kupovina.ToString());
                    return true;

                }
                catch (Exception e)
                {
                    //neka greska
                    return false;
                }
            }
        }
    }
}