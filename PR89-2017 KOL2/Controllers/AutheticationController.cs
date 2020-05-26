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
           // Korisnik k = korisnici.Where(x => x.Value.Equals(korisnik)).Select(x => x.Value).Single();
            if(korisnici.ContainsValue(korisnik) || korisnici.ContainsKey(korisnik.KorisnickoIme))
            {
                ViewBag.Message = $"Korisnik sa korisnickim imenom {korisnik.KorisnickoIme} vec postoji!";
                return View();
            }
            korisnici.Add(korisnik.KorisnickoIme, korisnik);
            Session["korisnik"] = korisnik;
            ViewBag.Message = $"Uspesno ste se registrovali!";
            return View();
        }
    }
}