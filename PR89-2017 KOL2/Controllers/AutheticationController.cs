using PR89_2017_KOL2.Helpers;
using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR89_2017_KOL2.Controllers
{
    public class AutheticationController : Controller
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
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Korisnik k = korisnici.Where(x => x.Value.Equals(korisnik)).Select(x => x.Value).Single();
            if(k != null)
            {
                ViewBag.Message = $"Korisnik sa korisnickim imenom {korisnik.KorisnickoIme} vec postoji!";
                return View();
            }
            korisnik.Uloga = Role.KUPAC;
            korisnici.Add(korisnik.KorisnickoIme, korisnik);

            if (!CitanjePodataka.pisiKorisnika(korisnik))
                //desila se greska

            Session["korisnik"] = korisnik;
            ViewBag.Message = $"Uspesno ste se registrovali!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(string korisnickoIme, string lozinka)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = korisnici.Where(x => x.Value.KorisnickoIme.Equals(korisnickoIme) && x.Value.Lozinka.Equals(lozinka)).Select(x => x.Value).Single();

            if(korisnik == null)
            {
                ViewBag.Message = "Korisnik ne postoji!";
                return View("Index");
            }
            Session["korisnik"] = korisnik;
            return RedirectToAction("Index", "Home");
        }
    }
}