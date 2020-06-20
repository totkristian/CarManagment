using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarManagment.Models
{
    public class Vozilo
    {
        private int id;
        
        private string marka;
        
        private string model;
        
        private string oznakaSasije;
        
        private string boja;
        
        private int brojVrata;
        
        private string opis;
        
        private Fuel vrstaGoriva;
        
        private double cena;
        private bool naStanju;

        private int kupacId;

        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "Marka je obavezna!"), MinLength(3)]
        public string Marka { get => marka; set => marka = value; }
        [Required(ErrorMessage = "Model je obavezan!")]
        public string Model { get => model; set => model = value; }
        [Required(ErrorMessage = "Oznaka sasije je obavezna!")]
        public string OznakaSasije { get => oznakaSasije; set => oznakaSasije = value; }
        [Required(ErrorMessage = "Boja je obavezna")]
        public string Boja { get => boja; set => boja = value; }
        [Required(ErrorMessage = "Broj vrata je obavezna!")]
        [Range(1, 5, ErrorMessage = "Nije dobar broj vrata!")]
        public int BrojVrata { get => brojVrata; set => brojVrata = value; }
        [Required(ErrorMessage = "Opis je obavezan!")]
        public string Opis { get => opis; set => opis = value; }
        [Required(ErrorMessage = "Vrsta goriva je obavezna!")]
        public Fuel VrstaGoriva { get => vrstaGoriva; set => vrstaGoriva = value; }
        [Required(ErrorMessage = "Cena je obavezna!")]
        [Range(1, double.MaxValue, ErrorMessage = "Nije dobar broj vrata!")]
        public double Cena { get => cena; set => cena = value; }
        public bool NaStanju { get => naStanju; set => naStanju = value; }
        public int KupacId { get => kupacId; set => kupacId = value; }

        public override string ToString()
        {
            return $"{Marka}|{Model}|{OznakaSasije}|{Boja}|{BrojVrata.ToString()}|{Opis}|{VrstaGoriva.ToString()}|{Cena}|{NaStanju.ToString()}|{KupacId}";
        }
    }
}