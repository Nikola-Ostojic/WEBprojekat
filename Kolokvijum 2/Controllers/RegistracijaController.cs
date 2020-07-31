using Kolokvijum_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kolokvijum_2.Controllers
{
    public class RegistracijaController : Controller
    {
        public ActionResult Prijava(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {

                string ime = Request["ime"];
                string prezime = Request["prezime"];
                string email = Request["email"];
                string datum_rodjenja = Request["datum rodjenja"];
                string korisnicko_ime = Request["korisnicko ime"];
                string lozinka = Request["lozinka"];

                if (String.IsNullOrEmpty(ime))
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }
                if (String.IsNullOrEmpty(prezime))
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }
                Pol pol = Request["pol"].Equals("Muski") ? Pol.Muski : Pol.Zenski;

                if (String.IsNullOrEmpty(email))
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }
                if (String.IsNullOrEmpty(datum_rodjenja))
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }

                if (String.IsNullOrEmpty(korisnicko_ime) || korisnicko_ime.Length < 4)
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }
                if (String.IsNullOrEmpty(lozinka) || lozinka.Length < 8)
                {
                    ViewBag.greska = 2;
                    return RedirectToAction("Registracija", "Registracija");
                }

                Korisnik korisnik = new Korisnik();
                List<Korisnik> lista = korisnik.Procitaj();
                bool nadjen = false;

                foreach (Korisnik k in lista)
                {
                    if (k.KorisnickoIme == korisnicko_ime && lozinka == k.Lozinka)
                        nadjen = true;
                }

                if (nadjen == true)
                {
                    ViewBag.greska = 1;
                    return View();
                }

                Korisnik kk = new Korisnik(korisnicko_ime, lozinka, ime, prezime, pol, email, datum_rodjenja, Uloga.Kupac);
                kk.Obrisan = false;

                kk.Upisi(kk);

                kk.Ulogovan = true;
                Session["korisnik"] = kk;

                return View();
            }
            else
            {
                /* string korisnicko_ime;
                 string lozinka;

                 if ((korisnicko_ime = Request["korisnicko ime"]) == null || korisnicko_ime.Count() < 4)
                 {
                     ViewBag.greska = 2;
                     return RedirectToAction("Registracija", "Registracija");
                 }
                 if ((lozinka = Request["lozinka"]) == null || lozinka.Count() < 8)
                 {
                     ViewBag.greska = 2;
                     return RedirectToAction("Registracija", "Registracija");
                 }

                 Korisnik korisnik = new Korisnik();
                 List<Korisnik> lista = korisnik.Procitaj(false);
                 bool nadjen = false;

                 foreach (Korisnik k in lista)
                 {
                     if (k.KorisnickoIme == korisnicko_ime && lozinka == k.Lozinka)
                     {
                         nadjen = true;
                         korisnik = k;
                     }
                 }

                 if (nadjen == false)
                 {
                     ViewBag.greska = 1;
                     return View();
                 }

                 korisnik.Ulogovan = true;
                 Session["korisnik"] = korisnik;

                 return RedirectToAction("");*/

                return View();
            }           
        }

        public ActionResult Registracija()
        {
            return View();
        }

        public ActionResult Odjava()
        {
            Korisnik k = (Korisnik)Session["korisnik"];

            if (k.UlogaProperty == Uloga.Kupac)
            {
                System.IO.File.Delete(Server.MapPath("~/App_Data/Kupovine.txt"));
            }

            k.Ulogovan = false;
            Session.Abandon();

            return View("Prijava");
        }
    }
}