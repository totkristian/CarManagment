using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR89_2017_KOL2.Models
{
    public class Kupovina
    {
        private int id;
        private Korisnik _korisnik;
        private Vozilo _vozilo;
        private DateTime datumKupovine;
        private double naplacenaCena;

        public int Id { get => id; set => id = value; }
        public Korisnik Korisnik { get => _korisnik; set => _korisnik = value; }
        public Vozilo Vozilo { get => _vozilo; set => _vozilo = value; }
        public DateTime DatumKupovine { get => datumKupovine; set => datumKupovine = value; }
        public double NaplacenaCena { get => naplacenaCena; set => naplacenaCena = value; }
        
    }
}