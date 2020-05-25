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

        [Required(ErrorMessage ="Korisnicko ime je obavezno!")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
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
        
    }
}