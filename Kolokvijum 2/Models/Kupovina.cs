using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Kolokvijum_2.Models
{
    public class Kupovina
    {
        #region Polja

        private Korisnik kupac;
        private Proizvod proizvod;
        private DateTime datum_kupovine;
        private int narucena_kolicina;
        private int naplacena_cena;

        #endregion

        #region Properties

        public Korisnik Kupac { get { return kupac; } set { kupac = value; } }
        public Proizvod ProizvodProperty { get { return proizvod; } set { proizvod = value; } }
        public DateTime DatumKupovine { get { return datum_kupovine; } set { this.datum_kupovine = value; } }
        public int NarucenaKolicina { get { return narucena_kolicina; } set { narucena_kolicina = value; } }
        public int NaplacenaCena { get { return naplacena_cena; } set { naplacena_cena = value; } }

        #endregion

        #region Konstruktor

        public Kupovina()
        {
        }

        public Kupovina(DateTime datum_kupovine, int narucena_kolicina, int naplacena_cena)
        {
            this.datum_kupovine = datum_kupovine;
            this.narucena_kolicina = narucena_kolicina;
            this.naplacena_cena = naplacena_cena;
        }

        #endregion

        #region Metode

        public List<Kupovina> Procitaj()
        {
            string path;

            path = HostingEnvironment.MapPath("~/App_Data/Kupovine.txt");

            string red;
            List<Kupovina> retVal = new List<Kupovina>();


            using (TextReader reader = new StreamReader(path))
            {
                while (!String.IsNullOrEmpty(red = reader.ReadLine()))
                {

                    string[] delovi = red.Split(';');

                    Kupovina k = new Kupovina(Convert.ToDateTime(delovi[2]), Convert.ToInt32(delovi[3]), Convert.ToInt32(delovi[4]));

                    Korisnik korisnik = new Korisnik();
                    Proizvod proizvod = new Proizvod();
                    List<Korisnik> lista1 = korisnik.Procitaj();
                    List<Proizvod> lista2 = proizvod.Procitaj();

                    foreach(Korisnik kk in lista1)
                    {
                        if (kk.KorisnickoIme.Equals(delovi[0]))
                        {
                            k.kupac = kk;
                            break;
                        }
                    }

                    foreach (Proizvod pp in lista2)
                    {
                        if (pp.Naziv.Equals(delovi[1]))
                        {
                            k.proizvod = pp;
                            break;
                        }
                    }

                    retVal.Add(k);

                }
            }

            return retVal;
        }

        public void Upisi(Kupovina k)
        {
            string str = "";
            if (File.Exists(HostingEnvironment.MapPath("~/App_Data/Kupovine.txt")))
            {

                List<Kupovina> lista = k.Procitaj();
                lista.Add(k);
                 str = "";

                for (int i = 0; i < lista.Count; i++)
                {
                    str += lista[i].ToString();
                }
            }
            else
            {
                str += k.ToString();
            }

            using (TextWriter writer = new StreamWriter(HostingEnvironment.MapPath("~/App_Data/Kupovine.txt")))
            {
                writer.WriteLine(str);
            }
        }

        public override string ToString()
        {
            return this.kupac.KorisnickoIme + ";" + this.proizvod.Naziv + ";" + this.DatumKupovine + ";" + this.NarucenaKolicina + ";" + this.NaplacenaCena  + "\n";
        }

        #endregion
    }
}