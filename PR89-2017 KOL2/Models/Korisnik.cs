using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PR89_2017_KOL2.Models
{
    public class Korisnik
    {
        
        private int id;

        [Required(ErrorMessage ="Korisnicko ime je obavezno!"),MinLength(3)]
        private string korisnickoIme;
        [Required(ErrorMessage = "Lozinka je obavezna!")]
        [RegularExpression(@"[a-zA-Z0-9]{8,}",ErrorMessage ="Mora biti minimum 8 karaktera!")]
        private string lozinka;
        [Required(ErrorMessage = "Ime je obavezno!")]
        private string ime;
        [Required(ErrorMessage = "Prezime je obavezno!")]
        private string prezime;
        [Required(ErrorMessage = "Pol je obavezan!")]
        private Sex pol;
        [Required(ErrorMessage = "Email je obavezan!")]
        private string email;
        [Required(ErrorMessage = "Datum rodjenja je obavezan!")]
        private DateTime datumRodjenja;
        private Role uloga;
        
        public int Id { get => id; set => id = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Sex Pol { get => pol; set => pol = value; }
        public string Email { get => email; set => email = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public Role Uloga { get => uloga; set => uloga = value; }

        public override bool Equals(object obj)
        {
            var korisnik = obj as Korisnik;
            return korisnik != null &&
                   id == korisnik.id &&
                   korisnickoIme == korisnik.korisnickoIme &&
                   lozinka == korisnik.lozinka &&
                   ime == korisnik.ime &&
                   prezime == korisnik.prezime &&
                   pol == korisnik.pol &&
                   email == korisnik.email &&
                   datumRodjenja == korisnik.datumRodjenja &&
                   uloga == korisnik.uloga &&
                   Id == korisnik.Id &&
                   KorisnickoIme == korisnik.KorisnickoIme &&
                   Lozinka == korisnik.Lozinka &&
                   Ime == korisnik.Ime &&
                   Prezime == korisnik.Prezime &&
                   Pol == korisnik.Pol &&
                   Email == korisnik.Email &&
                   DatumRodjenja == korisnik.DatumRodjenja &&
                   Uloga == korisnik.Uloga;
        }

        public override int GetHashCode()
        {
            var hashCode = -1055405030;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(korisnickoIme);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lozinka);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ime);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(prezime);
            hashCode = hashCode * -1521134295 + pol.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + datumRodjenja.GetHashCode();
            hashCode = hashCode * -1521134295 + uloga.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KorisnickoIme);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lozinka);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Ime);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Prezime);
            hashCode = hashCode * -1521134295 + Pol.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + DatumRodjenja.GetHashCode();
            hashCode = hashCode * -1521134295 + Uloga.GetHashCode();
            return hashCode;
        }
    }
}