using Kolokvijum_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Kolokvijum_2.Controllers
{
    public class PrijavljenController : Controller
    {
        // GET: Prijavljen
        public ActionResult Prijavljen()
        {
                string korisnicko_ime = Request["korisnicko ime"];
                string lozinka = Request["lozinka"];

                if (korisnicko_ime == "")
                {
                    ViewBag.greska = "2";
                    return RedirectToAction("Prijava", "Registracija");
                }

                if (lozinka == "")
                {
                    ViewBag.greska = "2";
                    return RedirectToAction("Prijava", "Registracija");
                }

                Korisnik korisnik = new Korisnik();
                List<Korisnik> lista = korisnik.Procitaj();
                bool nadjen = false;

                foreach(Korisnik k in lista)
                {
                    if (k.KorisnickoIme.Equals(korisnicko_ime) && lozinka.Equals(k.Lozinka))
                    {
                        nadjen = true;
                        korisnik = k;
                     break;
                    }
                }

                if (nadjen == false)
                {
                    ViewBag.greska = "1";
                    return Redirect("/Registracija/Registracija");
                }

                Session["korisnik"] = korisnik;

            if (korisnik.UlogaProperty == Uloga.Kupac)
            {
                return View("Kupac");
            }
            else
            {
                return View("Administrator");
            }
        }

        public ActionResult Administrator()
        {
            return View();
        }

        public ActionResult Kupac()
        {
            return View();
        }

        public ActionResult Obrisi(string korisnicko_ime)
        {
            Korisnik k = new Korisnik();
            List<Korisnik> lista = k.Procitaj();


            foreach (Korisnik kkk in lista)
            {
                if (kkk.KorisnickoIme.Equals(korisnicko_ime))
                {
                    kkk.Obrisi(kkk.KorisnickoIme);
                    break;
                }
            }

                ViewBag.obavestenje = "obrisan";

                return View("Administrator");     
        }

        

        public ActionResult Azuriraj(string naziv, string sorta)
        {
            Proizvod p = new Proizvod();
            List<Proizvod> lista = new List<Proizvod>();

            foreach(Proizvod proizvod in lista)
            {
                if (proizvod.Naziv.Equals(naziv) && proizvod.Sorta.Equals(sorta))
                {
                    p = proizvod;
                }
            }

            return View(p);
        }

        public ActionResult Azuriraj2(string naziv, string sorta)
        {
            int index = 0;

            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Naziv.Equals(naziv) && lista[i].Sorta.Equals(sorta))
                {
                    index = i;
                    break;
                }
            }

            if (!String.IsNullOrEmpty(Request["naziv"]))
            {
                lista[index].Naziv = Request["naziv"];
            }
            else if (!String.IsNullOrEmpty(Request["sorta"]))
            {
                lista[index].Sorta = Request["sorta"];
            }
            else if (!String.IsNullOrEmpty(Request["narodno ime"]))
            {
                lista[index].NarodnoIme = Request["narodno ime"];
            }
            else if (!String.IsNullOrEmpty(Request["boja"]))
            {
                lista[index].Boja = Request["boja"];
            }
            else if (!String.IsNullOrEmpty(Request["poreklo"]))
            {
                lista[index].Poreklo = Request["poreklo"];
            }
            else if (!String.IsNullOrEmpty(Request["opis"]))
            {
                lista[index].Opis = Request["opis"];
            }
            else if (!String.IsNullOrEmpty(Request["cena"]))
            {
                lista[index].CenaPoKg = Convert.ToInt32(Request["cena"]);
            }
            else if (!String.IsNullOrEmpty(Request["ukupno"]))
            {
                lista[index].UkupnaKolicinaRaspolozivogProizvoda = Convert.ToInt32(Request["ukupno"]);
            }

            string str = "";

            for (int i = 0; i < lista.Count; i++)
            {
                str += lista[i].ToString();
            }

            using (TextWriter writer = new StreamWriter(HostingEnvironment.MapPath("~/App_Data/Proizvodi.txt")))
            {
                writer.WriteLine(str);
            }

            return RedirectToAction("PrikazProizvoda", "Proizvodi");
        }
    }
}