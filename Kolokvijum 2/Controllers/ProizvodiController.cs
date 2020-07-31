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
    [Route("Proizvodi/{action}")]
    public class ProizvodiController : Controller
    {
        // GET: Proizvodi
        public ActionResult PrikazProizvoda()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pretrazi()
        {
            string vrsta = Request["pretrazi_vrsta"];
            string trazeno = Request["pretrazi_tekst"];
            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();

            if (vrsta.Equals("Naziv"))
            {
                foreach (Proizvod proizvod in lista)
                {
                    if (proizvod.Naziv == trazeno)
                    {
                        ViewBag.naziv = trazeno;
                        return View(proizvod);
                    }
                }
            }
            else
            {
                foreach (Proizvod proizvod in lista)
                {
                    if (proizvod.Sorta == trazeno)
                    {
                        ViewBag.sorta = trazeno;
                        return View(proizvod);
                    }
                }
            }

            ViewBag.greska = 1;

            return View();
        }

        [HttpPost]
        public ActionResult PretraziPoCeni()
        {
            string vrsta = Request["pretrazi_vrsta"];
            int od = Convert.ToInt32(Request["od"]);
            int DO = Convert.ToInt32(Request["do"]);
            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();
            List<Proizvod> lista2 = new List<Proizvod>();

           foreach (Proizvod proizvod in lista)
           {
               if (od <= proizvod.CenaPoKg && proizvod.CenaPoKg <= DO)
               {
                    lista2.Add(proizvod);
               }
           }

            if (lista2 != null)
            {
                ViewBag.pretrazeni = lista2;
            }
            else
            {
                ViewBag.greska = 1;
            }

            if (od > DO)
            {
                ViewBag.greska = 2;
            }

           return View("Pretrazi");
        }

        [HttpPost]
        public ActionResult Sortiraj()
        {
            string vrsta = Request["sortiraj"];
            string vrsta_sortiranja = Request["vrsta_sortiranja"];
            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();

            if (vrsta_sortiranja.Equals("Opadajuce"))
            {
                if (vrsta.Equals("Cena"))
                {
                    lista = lista.OrderByDescending(x => x.CenaPoKg).ToList();

                     
                }
                else if (vrsta.Equals("Sorta"))
                {
                    lista = lista.OrderByDescending(x => x.Sorta).ToList();
                }
                else
                {
                    lista = lista.OrderByDescending(x => x.Naziv).ToList();
                }
            }
            else
            {
                if (vrsta.Equals("Cena"))
                {
                    lista = lista.OrderBy(x => x.CenaPoKg).ToList();
                }
                else if (vrsta.Equals("Sorta"))
                {
                    lista = lista.OrderBy(x => x.Sorta).ToList();
                }
                else
                {
                    lista = lista.OrderBy(x => x.Naziv).ToList();
                }
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


            return RedirectToAction("PrikazProizvoda");
        }

        public ActionResult Detalji(string naziv, string sorta)
        {
            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();

            foreach(Proizvod proizvod in lista)
            {
                if (proizvod.Naziv.Equals(naziv) && proizvod.Sorta.Equals(sorta))
                    return View(proizvod);
            }

            return View();
        }

        public ActionResult DodajUKorpu(string naziv, string sorta)
        {
            int kolicina = Convert.ToInt32(Request["kolicina"]);

            Proizvod p = new Proizvod();
            List<Proizvod> lista = p.Procitaj();

            foreach(Proizvod proizvod in lista)
            {
                if (proizvod.Naziv.Equals(naziv) && proizvod.Sorta.Equals(sorta))
                {
                    p = proizvod;
                    break;
                }
            }

            if (p.UkupnaKolicinaRaspolozivogProizvoda < kolicina)
            {
                ViewBag.greska3 = "1";

                return View("PrikazProizvoda");
            }

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Naziv == p.Naziv && lista[i].Poreklo == p.Poreklo && lista[i].CenaPoKg == p.CenaPoKg)
                {
                    lista[i].UkupnaKolicinaRaspolozivogProizvoda -= kolicina;

                    break;
                }
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

            Kupovina k = new Kupovina(DateTime.Now, kolicina, p.CenaPoKg);
            k.Kupac = (Korisnik)Session["korisnik"];
            k.ProizvodProperty = p;
            k.Upisi(k);
            

            return RedirectToAction("PrikazProizvoda");
        }

        public ActionResult DodajProizvod()
        {
            if (Request["naziv"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["sorta"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["narodno ime"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["boja"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["poreklo"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["opis"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["cena"].Equals(""))            //Pitanje je da li radi ova provera
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }
            else if (Request["ukupna kolicina"].Equals(""))
            {
                ViewBag.greska = "greska";
                return View("PrikazProizvoda");
            }


            string naziv = Request["naziv"];
            string sorta = Request["sorta"];
            string narodno_ime = Request["narodno ime"];
            string boja = Request["boja"];
            string poreklo = Request["poreklo"];
            string opis = Request["opis"];
            int cena_po_kg = Convert.ToInt32(Request["cena"]);
            int ukupna = Convert.ToInt32(Request["ukupna kolicina"]);

            Proizvod p = new Proizvod(naziv, sorta, narodno_ime, boja, poreklo, opis, cena_po_kg, ukupna);
            p.Upisi(p);

            return View("PrikazProizvoda");
        }
    }
}