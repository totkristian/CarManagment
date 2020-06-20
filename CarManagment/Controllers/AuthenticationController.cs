using CarManagment.Helpers;
using CarManagment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CarManagment.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authetication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Korisnik nije uspesno registrovan!";
                setErrorMessages(korisnik);
                return View();
            }
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Korisnik k = korisnici.Where(x => x.Value.Equals(korisnik)).Select(x => x.Value).SingleOrDefault();
            if(k != null)
            {
                ViewBag.Message = $"Korisnik sa korisnickim imenom {korisnik.KorisnickoIme} vec postoji!";
                return View();
            }
            korisnik.Uloga = Role.KUPAC;
            korisnici.Add(korisnik.KorisnickoIme, korisnik);
            korisnik.Id = korisnici.Count == 0 ? 1 : korisnici.Select(x => x.Value.Id).Max() + 1;

            if (!CitanjePodataka.pisiKorisnika(korisnik))
                //desila se greska

            ViewBag.Message = $"Uspesno ste se registrovali!";
            return View("Index");
        }

        [HttpPost]
        public ActionResult Login(string korisnickoIme, string lozinka)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = korisnici.Where(x => x.Value.KorisnickoIme.Equals(korisnickoIme) && x.Value.Lozinka.Equals(lozinka)).Select(x => x.Value).SingleOrDefault();
            

            if(korisnik == null || korisnik.Obrisan == true)
            {
                ViewBag.Message = "Pogresna lozinka ili korisnicko ime!";
                return View("Index");
            }
            Session["korisnik"] = korisnik;
            return RedirectToAction("Cars", "Cars");
        }

        public ActionResult Logout()
        {
            Session["korisnik"] = null;
            return View("Index");
        }

        private void setErrorMessages(Korisnik k) {
            if(String.IsNullOrWhiteSpace(k.KorisnickoIme))
            {
                ViewBag.KorMsg = "Korisnicko ime je obavezno!";
            }
            else if(k.KorisnickoIme.Length < 3)
            {
                ViewBag.KorMsg = "Korisnicko ime je prekratko!";
            }
            
            if(k.Lozinka == null)
            {
                ViewBag.LozMsg = "Lozinka je obavezna!";
            }
            else
            {
                Match m = Regex.Match(k.Lozinka, @"[a-zA-Z0-9]{8,}");
                if(!m.Success) {
                    ViewBag.LozMsg = "Nije validna lozinka!";
                }
            }

            if (String.IsNullOrWhiteSpace(k.Ime))
            {
                ViewBag.ImMsg = "Ime je obavezno!";
            }

            if (String.IsNullOrWhiteSpace(k.Prezime))
            {
                ViewBag.PrzMsg = "Prezime je obavezno!";
            }

            if(String.IsNullOrWhiteSpace(k.Pol.ToString()))
            {
                ViewBag.PoMsg = "Pol je obavezan!";
            }

            if (String.IsNullOrWhiteSpace(k.Email))
            {
                ViewBag.EmMsg = "Email je obavezan!";
            }
            else
            {
                if (!Regex.IsMatch(k.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    ViewBag.EmMsg = "Pogresan format maila!";
                }
            }
            Debug.WriteLine(k.DatumRodjenja.ToString("dd/mm/yyyy"));
            if (k.DatumRodjenja.ToString("dd/mm/yyyy").Equals("01/00/0001"))
            {
                ViewBag.DatMsg = "Nije validan datum!";
            } 

            

        }
    }
}