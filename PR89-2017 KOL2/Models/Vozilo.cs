using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PR89_2017_KOL2.Models
{
    public class Vozilo
    {
        private int id;
        [Required(ErrorMessage = "Marka je obavezna!"), MinLength(3)]
        private string marka;
        [Required(ErrorMessage = "Model je obavezan!")]
        private string model;
        [Required(ErrorMessage = "Oznaka sasije je obavezna!")]
        private string oznakaSasije;
        [Required(ErrorMessage = "Boja je obavezna")]
        private string boja;
        [Required(ErrorMessage = "Boja je obavezna!")]
        private int brojVrata;
        [Required(ErrorMessage = "Opis je obavezan!")]
        private string opis;
        [Required(ErrorMessage = "Vrsta goriva je obavezna!")]
        private Fuel vrstaGoriva;
        [Required(ErrorMessage = "Cena je obavezna!")]
        private double cena;
        [Required(ErrorMessage = "Stanje je obavezno!")]
        private bool naStanju;

        public int Id { get => id; set => id = value; }
        public string Marka { get => marka; set => marka = value; }
        public string Model { get => model; set => model = value; }
        public string OznakaSasije { get => oznakaSasije; set => oznakaSasije = value; }
        public string Boja { get => boja; set => boja = value; }
        public int BrojVrata { get => brojVrata; set => brojVrata = value; }
        public string Opis { get => opis; set => opis = value; }
        public Fuel VrstaGoriva { get => vrstaGoriva; set => vrstaGoriva = value; }
        public double Cena { get => cena; set => cena = value; }
        public bool NaStanju { get => naStanju; set => naStanju = value; }
    }
}