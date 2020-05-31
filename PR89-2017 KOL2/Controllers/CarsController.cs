using PR89_2017_KOL2.Helpers;
using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                //nije ulogovan korisnik ili korisnik
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).ToList<Vozilo>();
            }
            else if (korisnik.Uloga.Equals(Role.ADMINISTRATOR))
            {
                //administrator
                vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            }


            ucitajOpcije();
            ViewBag.Vozila = vozila;
            return View();
        }

        public ActionResult SortirajPoMarki(string marka, string trazi)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
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
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).OrderBy(x => !x.Model.Equals(model)).ToList<Vozilo>();
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
                    vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).OrderBy(x => x.Cena).ToList<Vozilo>();
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

            if (cenaDo == 0)
            {
                cenaDo = double.MaxValue;
            }

            if (korisnik == null || korisnik.Uloga.Equals(Role.KUPAC))
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).Where(x => x.Cena >= cenaOd && x.Cena <= cenaDo).ToList<Vozilo>();
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
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            if (k == null || k.Uloga.Equals(Role.KUPAC))
            {
                ViewBag.Vozila = vozila;
                ViewBag.Message = "Nemate prava pristupa ovoj stranici!";
                ucitajOpcije();
                return View("Cars");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(Vozilo vozilo)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            if (k == null || k.Uloga.Equals(Role.KUPAC))
            {
                ViewBag.Vozila = vozila;
                ViewBag.Message = "Nemate prava pristupa ovoj stranici!";
                ucitajOpcije();
                return View("Cars");
            }

            if (!ModelState.IsValid)
            {
                setErrorMessages(vozilo);
                ViewBag.Message = "Vozilo nije uspesno dodato!";
                return View();
            }        
            vozilo.Id = vozila.Count == 0 ? 1 : vozila.Select(x => x.Id).Max() + 1;
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
            catch
            {
                ViewBag.Message = "Desila se greska pri dodavanju vozila, pokusajte ponovo!";
                return View();
            }

        }

        private void setErrorMessages(Vozilo vozilo)
        {
            if (String.IsNullOrEmpty(vozilo.Marka))
            {
                ViewBag.MarMsg = "Marka je obavezna!";
            }
            else if(vozilo.Marka.Length<3)
            {
                ViewBag.MarMsg = "Marka nije validna!";
            }

            if (String.IsNullOrWhiteSpace(vozilo.Model))
            {
                ViewBag.ModMsg = "Model je obavezan!";

            }
            if(String.IsNullOrWhiteSpace(vozilo.Opis))
            {
                ViewBag.OpMsg = "Opis je obavezan!";
            }
            if(String.IsNullOrWhiteSpace(vozilo.OznakaSasije))
            {
                ViewBag.OzMsg = "Oznaka sasije je obavezna!";
            }
            if (String.IsNullOrWhiteSpace(vozilo.Boja))
            {
                ViewBag.BoMsg = "Boja je obavezna!";
            }

            if(vozilo.BrojVrata <= 0 || vozilo.BrojVrata > 5)
            {
                ViewBag.BrMsg = "Nije validan broj vrata";
            }

            if(vozilo.Cena <= 0)
            {
                ViewBag.CeMsg = "Nije validna cena vozila";
            }
            if(!vozilo.VrstaGoriva.Equals(Fuel.BENZIN) && 
                !vozilo.VrstaGoriva.Equals(Fuel.BENZIN_GAS) && 
                !vozilo.VrstaGoriva.Equals(Fuel.DIZEL) && 
                !vozilo.VrstaGoriva.Equals(Fuel.ELEKTRICNI_POGON) &&
                !vozilo.VrstaGoriva.Equals(Fuel.HIBRIDNI_POGON) &&
                !vozilo.VrstaGoriva.Equals(Fuel.METAN))
            {
                ViewBag.VrMsg = "Nije validna vrsta goriva!";
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
                ViewBag.Vozilo = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Id == id).Select(x => x).SingleOrDefault();
                return View("EditCar");
            }
        }

        [HttpPost]
        public ActionResult EditCarMethod(Vozilo vozilo, string submit)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            if (k == null || k.Uloga.Equals(Role.KUPAC))
            {
                ViewBag.Vozila = vozila;
                ViewBag.Message = "Nemate prava pristupa ovoj stranici!";
                ucitajOpcije();
                return View("Cars");
            }

           
            if (submit.Equals("Otkazi"))
            {
                ViewBag.Vozila = vozila;
                ucitajOpcije();
                return View("Cars");
            }
            else {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        setErrorMessages(vozilo);
                        ViewBag.Vozilo = vozilo;
                        ViewBag.Message = "Izmena vozila neuspesna!";
                        return View("EditCar");
                    }
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
                    ViewBag.Message = "Desila se greska pri izmeni vozila!";
                }
            }
            ucitajOpcije();
            ViewBag.Vozila = vozila;
            return View("Cars");
        }
        [HttpPost]
        public ActionResult BuyCar(Vozilo vozilo, string submit)
        {
            List<Vozilo> vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            Korisnik kor = (Korisnik)Session["korisnik"];
            if(kor == null || kor.Uloga.Equals(Role.ADMINISTRATOR))
            {
                ViewBag.Vozila = vozila;
                ViewBag.Message = "Nemate prava pristupa ovoj stranici!";
                ucitajOpcije();
                return View("Cars");
            }
            Kupovina kupovina = new Kupovina();
            
            if (submit.Equals("Otkazi"))
            {
                ViewBag.Vozila = vozila;
                ucitajOpcije();
                return View("Cars");
            }
            else
            {
                try
                {
                    int index = vozila.FindIndex(x => x.Id == vozilo.Id);
                    vozilo.NaStanju = false;
                    vozila[index] = vozilo;

                    if (!CitanjePodataka.izmeniVozilo(vozila))
                        throw new Exception();
                    HttpContext.Application["vozila"] = vozila;

                    List<Kupovina> k = CitanjePodataka.citajKupovinu();

                    kupovina.Id = k.Count == 0 ? 1 : k.Select(x=>x.Id).Max() + 1;
                    kupovina.Kupac = (Korisnik)Session["korisnik"];
                    kupovina.DatumKupovine = DateTime.Now.Date;
                    kupovina._Vozilo = vozila[index];
                    kupovina.NaplacenaCena = vozila[index].Cena;

                    if (!CitanjePodataka.pisiKupovinu(kupovina))
                        throw new Exception();


                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    ViewBag.Message = "Desila se greska pri kupovini vozila vozila!";
                }
            }
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return RedirectToAction("Cars");

        }

        public ActionResult MyCars()
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            if (k != null && k.Uloga.Equals(Role.KUPAC))
            {
                try
                {
                    List<Kupovina> kupovina = CitanjePodataka.citajKupovinu();
                    if (kupovina.Count < 1)
                    {
                        ViewBag.Kupovine = kupovina;
                    }
                    else
                    {
                        ViewBag.Kupovine = CitanjePodataka.citajKupovinu().Where(x => x.Kupac.Id == k.Id).Select(x => x);
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Neuspesan prikaz svih kupovina!";
                }
            }
            List<Vozilo> vozila = ((List<Vozilo>)HttpContext.Application["vozila"]);
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }
    }
}

   