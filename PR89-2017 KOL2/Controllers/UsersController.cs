using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR89_2017_KOL2.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Users()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null)
            {
                ViewBag.Message = "Nemate pravo pristupa ovoj stranici!";
                return RedirectToAction("Index", "Authentication");
            }
            else if (!korisnik.Uloga.Equals(Role.ADMINISTRATOR))
            {
                //nije admin
                ViewBag.Message = "Nemate pravo pristupa ovoj stranici!";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Korisnici = ((Dictionary<string, Korisnik>)HttpContext.Application["korisnici"]).Values;


            return View();
        }

        public ActionResult deleteUser(string korIme)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            try
            {
                korisnici.Remove(korIme);
                HttpContext.Application["korisnici"] = korisnici;
            }
            catch
            {
                ViewBag.Message("Taj korisnik ne postoji!");
            }
            ViewBag.Korisnici = korisnici;
            return View("Users");
        }
    }
}