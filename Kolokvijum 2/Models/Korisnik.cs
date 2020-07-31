using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Kolokvijum_2.Models
{

    public enum Pol { Muski, Zenski }
    public enum Uloga { Administrator, Kupac }
    public class Korisnik
    {
        #region Polja

        private string korisnicko_ime;
        private string lozinka;
        private string ime;
        private string prezime;
        private Pol pol;
        private string email;
        private string datum_rodjenja;
        private Uloga uloga;
        private bool ulogovan;
        private bool obrisan;

        #endregion

        #region Properties


        public string KorisnickoIme { get { return korisnicko_ime; } set { korisnicko_ime = value; } }

        public string Lozinka { get { return lozinka; } set { lozinka = value; } }

        public string Ime { get { return ime; } set { ime = value; } }

        public string Prezime { get { return prezime; } set { prezime = value; } }

        public Pol PolProperty { get { return pol; } set { pol = value; } }
      
        public string Email { get { return email; } set { email = value; } }
       
        public string DatumRodjenja { get { return datum_rodjenja; } set { datum_rodjenja = value; ; } }
        public Uloga UlogaProperty { get { return uloga; } set { uloga = value; } }
        public bool Obrisan { get { return obrisan; } set { obrisan = value; } }
        public bool Ulogovan { get { return ulogovan; } set { ulogovan = value; } }

        #endregion

        #region Konstruktor

        public Korisnik(string korisnicko_ime, string lozinka, string ime, string prezime, Pol pol, string email, string datum_rodjenja, Uloga uloga)
        {
            this.korisnicko_ime = korisnicko_ime;
            this.lozinka = lozinka;
            this.ime = ime;
            this.prezime = prezime;
            this.pol = pol;
            this.email = email;
            this.datum_rodjenja = datum_rodjenja;
            this.uloga = uloga;
        }

        public Korisnik()
        {

        }

        #endregion

        #region Metode

        public List<Korisnik> Procitaj()
        {
            string path;
            Uloga ul;
            Pol p = Pol.Muski;



            path = HostingEnvironment.MapPath("~/App_Data/Kupci.txt");

            string red;
            string[] delovi;
            List<Korisnik> retVal = new List<Korisnik>();

            using (TextReader reader = new StreamReader(path))
            {
                while (!String.IsNullOrEmpty(red = reader.ReadLine()))
                {

                    delovi = red.Split(';');

                    if (delovi[8].Equals("administrator"))
                    {
                        ul = Uloga.Administrator;
                    }
                    else
                    {
                        ul = Uloga.Kupac;
                    }

                    if (delovi[7].Equals("obrisan"))
                    {
                        continue;
                    }

                    if (delovi[4].Equals("Zenski"))
                        p = Pol.Zenski;

                    Korisnik k = new Korisnik(delovi[0], delovi[1], delovi[2], delovi[3], p, delovi[5], delovi[6], ul);

                    retVal.Add(k);

                }
            }

            return retVal;
        }

        public void Upisi(Korisnik k)
        {
            List<Korisnik> lista = k.Procitaj();
            lista.Add(k);
            string str = "";

            for (int i = 0; i < lista.Count; i++)
            {
                str += lista[i].ToString();
            }

            File.WriteAllText(HostingEnvironment.MapPath("~/App_Data/Kupci.txt"), str);
        }

        public void Obrisi(string korisnicko_ime)
        {
            List<Korisnik> lista = Procitaj();

                foreach(Korisnik korisnik in lista)
                {
                    if (korisnik.KorisnickoIme.Equals(korisnicko_ime))
                    {
                        korisnik.Obrisan = true;
                        break;
                    }
                }            

                string str = "";

                for (int i = 0; i < lista.Count; i++)
                {
                    str += lista[i].ToString();
                }

                File.WriteAllText(HostingEnvironment.MapPath("~/App_Data/Kupci.txt"), str);
            
        }

        public override string ToString()
        {
            string p = "Zenski";
            string obr = "nije obrisan";
            string uloga;
            if (this.UlogaProperty == Uloga.Administrator)
            {
                uloga = "administrator";
            }
            else
            {
                uloga = "kupac";
            }

            if (this.PolProperty == Pol.Muski)
                p = "Muski";

            if (this.UlogaProperty == Uloga.Kupac && this.Obrisan == true)
                obr = "obrisan";

            return this.korisnicko_ime + ";" + this.lozinka + ";" + this.ime + ";" + this.prezime + ";" + p + ";" + this.email + ";" + this.datum_rodjenja + ";" + obr + ";" + uloga + "\n";
        }

        #endregion
    }
}