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
            else if (korisnik.Uloga.Equals(Role.KUPAC))
            {
                //ulogovan kupac
                vozila = (List<Vozilo>)HttpContext.Application["vozila"];
            }
            else
            {
                //administrator
                vozila = (List<Vozilo>)HttpContext.Application["vozila"];
                
            }
           // List<Korisnik> korisnici = ;
            
            ucitajOpcije();
            ViewBag.Vozila = vozila;
            return View();
        }

        public ActionResult SortirajPoMarki(string marka, string trazi)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List <Vozilo> vozila = null; 
            if (trazi.Contains("Trazi"))
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Marka.Contains(marka)).ToList<Vozilo>();
            }
            else
            {
                
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderBy(x => !x.Marka.Equals(marka)).ToList<Vozilo>();
            }
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }

        public ActionResult SortirajPoModelu(string model, string trazi)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
            if (trazi.Contains("Trazi"))
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.Model.Contains(model)).ToList<Vozilo>();
            }
            else
            {
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).OrderBy(x => !x.Model.Equals(model)).ToList<Vozilo>();
            }
            
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }
        public ActionResult SortirajPoCeni(string cena)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Vozilo> vozila = null;
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
                vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x => x.NaStanju == true).ToList<Vozilo>();
            }
            
            ViewBag.Vozila = vozila;
            ucitajOpcije();
            return View("Cars");
        }

        public ActionResult NadjiPoCeni(string cenaOd, string cenaDo)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            double from, to;
            try
            {
                from = double.Parse(cenaOd);
            }
            catch
            {
                from = 0;
            }
            try
            {
                to = double.Parse(cenaDo);
            }
            catch
            {
                to = double.MaxValue;
            }
            if(to == 0)
            {
                to = double.MaxValue;
            }


            List<Vozilo> vozila = ((List<Vozilo>)HttpContext.Application["vozila"]).Where(x=> x.Cena >= from && x.Cena <= to ).ToList<Vozilo>();
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
    }
}