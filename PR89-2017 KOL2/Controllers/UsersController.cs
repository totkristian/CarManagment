﻿using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
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
            if(korisnik == null)
            {
                ViewBag.Message = "Nemate pravo pristupa ovoj stranici!";
                return RedirectToAction("Index", "Authentication");
            }
            else if(!korisnik.Uloga.Equals(Role.ADMINISTRATOR))
            {
                //nije admin
                ViewBag.Message = "Nemate pravo pristupa ovoj stranici!";
                return RedirectToAction("Index", "Home");
            }
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.Korisnici = korisnici.Values;


            return View();
        }
    }
}