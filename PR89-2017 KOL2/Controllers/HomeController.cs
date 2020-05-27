using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR89_2017_KOL2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if(korisnik == null || korisnik.KorisnickoIme.Equals(""))
            {
               // return RedirectToAction("Index", "Authentication");
            }
            return View();
        }


    }
}