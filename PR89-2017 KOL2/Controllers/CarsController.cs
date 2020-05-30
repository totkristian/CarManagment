using PR89_2017_KOL2.Helpers;
using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR89_2017_KOL2.Controllers
{
    public class CarsController : Controller
    {
        // GET: Car
        public ActionResult Cars()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
            if (korisnik == null)  
            {
                //nije ulogovan korisnik
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).ToList<Vozilo>();
            }
            else if (korisnik.Uloga.Equals(Role.ADMINISTRATOR))
            {
                //administrator
                vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            }
            else
            {
                //ulogovan kupac
                vozila = (List<Vozilo>)HttpContext.Application["vozila"];
                
            }
            
            ucitajOpcije();
            ViewBag.Vozila = vozila;
            return View();
        }

        public ActionResult SortirajPoMarki(string marka, string trazi)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List <Vozilo> vozila = null;
            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                if (trazi.Contains("Trazi"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true && x.Marka.Contains(marka)).ToList<Vozilo>();
                }
                else
                {

                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).OrderBy(x => !x.Marka.Equals(marka)).ToList<Vozilo>();
                }
            }
            else
            {
                if (trazi.Contains("Trazi"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Marka.Contains(marka)).ToList<Vozilo>();
                }
                else
                {

                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderBy(x => !x.Marka.Equals(marka)).ToList<Vozilo>();
                }
            }
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }

        public ActionResult SortirajPoModelu(string model, string trazi)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                if (trazi.Contains("Trazi"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true && x.Model.Contains(model)).ToList<Vozilo>();
                }
                else
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x=> x.NaStanju == true).OrderBy(x => !x.Model.Equals(model)).ToList<Vozilo>();
                }
            }
            else
            {
                if (trazi.Contains("Trazi"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Model.Contains(model)).ToList<Vozilo>();
                }
                else
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderBy(x => !x.Model.Equals(model)).ToList<Vozilo>();
                }
            }
            
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }
        public ActionResult SortirajPoCeni(string cena)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                if (cena.Equals("Rastuce"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x=>x.NaStanju==true).OrderBy(x => x.Cena).ToList<Vozilo>();
                }
                else if (cena.Equals("Opadajuce"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).OrderByDescending(x => x.Cena).ToList<Vozilo>();
                }
                else
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).ToList<Vozilo>();
                }
            }
            else
            {
                if (cena.Equals("Rastuce"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderBy(x => x.Cena).ToList<Vozilo>();
                }
                else if (cena.Equals("Opadajuce"))
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderByDescending(x => x.Cena).ToList<Vozilo>();
                }
                else
                {
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).ToList<Vozilo>();
                }
            }
            
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }

        public ActionResult NadjiPoCeni(double cenaOd, double cenaDo)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
  
            if(cenaDo == 0)
            {
                cenaDo = double.MaxValue;
            }

            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x=> x.NaStanju == true).Where(x => x.Cena >= cenaOd && x.Cena <= cenaDo).ToList<Vozilo>();
            }
            else
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Cena >= cenaOd && x.Cena <= cenaDo).ToList<Vozilo>();
            }  
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }

        private void ucitajOpcije()
        {
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            List<string> marke = vozila.Select(x => x.Marka).Distinct().ToList<string>();
            List<string> modeli = vozila.Select(x => x.Model).Distinct().ToList<string>();
            ViewBag.Korisnici = ((Dictionary<string, Korisnik>)HttpContext.Application["korisnici"]).Values.ToList<Korisnik>();

            ViewBag.Marke = marke;
            
            ViewBag.Modeli = modeli;
        }

        public ActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(Vozilo vozilo)
        {
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            if (vozila.Count != 0)
            {
                vozilo.Id = vozila.Select(x => x.Id).Max() + 1;
                
            }
            else
            {
                vozilo.Id = 1;
            }
            vozilo.KupacId = -1;
            vozilo.NaStanju = true;
            try
            {
                
                if (!CitanjePodataka.pisiVozilo(vozilo))
                {
                    //neuspesno pisanje u fajl
                    throw new Exception();
                }
                vozila.Add(vozilo);
                ucitajOpcije();
                HttpContext.Application["vozila"] = vozila;
                ViewBag.Vozila = vozila;
                return View("Cars");
            }
            catch {
                ViewBag.Message = "Desila se greska pri dodavanju vozila, pokusajte ponovo!";
                return View();
            }
            
        }

        public ActionResult BuyEditCar(int id, string button)
        {
            if (button.Contains("Izmeni"))
            {
                Vozilo vozilo = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Id == id).Select(x => x).SingleOrDefault();
                ViewBag.Vozilo = vozilo;
                return View("EditCar");
            }
            else
            {
                ViewBag.Vozila = (List<Vozilo>)HttpContext.Application["vozila"];
                ucitajOpcije();
                return View("Cars");
            }
        }

        [HttpPost]
        public ActionResult EditCarMethod(Vozilo vozilo)
        {
           
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            try
            {
                vozilo.NaStanju = true;
                vozilo.KupacId = -1;
                int index = vozila.FindIndex(x => x.Id == vozilo.Id);
                vozila[index] = vozilo;
                if (!CitanjePodataka.izmeniVozilo(vozila))
                    throw new Exception();
                HttpContext.Application["vozila"] = vozila;
            }
            catch
            {
                ViewBag.Message = "Desila se greska pri pisanju u fajl pokusajte ponovo!";
            }
            ucitajOpcije();
            ViewBag.Vozila = vozila;
            return View("Cars");
        }
    }
}