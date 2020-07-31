using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Kolokvijum_2.Models
{
    public class Proizvod
    {

        #region Polja

        private string naziv;
        private string sorta;
        private string narodno_ime;
        private string boja;
        private string poreklo;
        private string opis;
        private int cena_po_kg;
        private int ukupna_kolicina_raspolozivog_proizvoda;     //po kg

        #endregion

        #region Properties

        public string Naziv { get { return naziv; } set { naziv = value; } }
        public string Sorta { get { return sorta; } set { sorta = value; } }
        public string NarodnoIme { get { return narodno_ime; } set { narodno_ime = value; } }
        public string Boja { get { return boja; } set { boja = value; } }
        public string Poreklo { get { return poreklo; } set { poreklo = value; } }
        public string Opis { get { return opis; } set { opis = value; } }
        public int CenaPoKg { get { return cena_po_kg; } set { cena_po_kg = value; } }
        public int UkupnaKolicinaRaspolozivogProizvoda { get { return ukupna_kolicina_raspolozivog_proizvoda; } set { ukupna_kolicina_raspolozivog_proizvoda = value; } }

        #endregion

        #region Konstruktor

        public Proizvod(string naziv, string sorta, string narodno_ime, string boja, string poreklo, string opis, int cena_po_kg, int ukupna_kolicina_raspolozivog_proizvoda)
        {
            this.naziv = naziv;
            this.sorta = sorta;
            this.narodno_ime = narodno_ime;
            this.boja = boja;
            this.poreklo = poreklo;
            this.opis = opis;
            this.cena_po_kg = cena_po_kg;
            this.ukupna_kolicina_raspolozivog_proizvoda = ukupna_kolicina_raspolozivog_proizvoda;
        }

        public Proizvod()
        {

        }

        #endregion

        #region Metode

        public List<Proizvod> Procitaj()
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Proizvodi.txt");

            
            List<Proizvod> retVal = new List<Proizvod>();
            string red;

            using (TextReader reader = new StreamReader(path))
            {
                while ((red = reader.ReadLine()) != "")
                {
                    string[] delovi = red.Split(';');

                    retVal.Add(new Proizvod(delovi[0], delovi[1], delovi[2], delovi[3], delovi[4], delovi[5], Convert.ToInt32(delovi[6]), Convert.ToInt32(delovi[7])));
                }
            }

            return retVal;
        }

        public void Upisi(Proizvod k)
        {
            List<Proizvod> lista = k.Procitaj();
            lista.Add(k);

            string str = "";

            for (int i = 0; i < lista.Count; i++)
            {
                str += lista[i].ToString();
            }

            using (TextWriter writer = new StreamWriter(HostingEnvironment.MapPath("~/App_Data/Proizvodi.txt")))
            {
                writer.WriteLine(str);
            }
        }

        public override string ToString()
        {
            return naziv + ";" + sorta + ";" + narodno_ime + ";" + boja + ";" + poreklo + ";" + opis + ";" + cena_po_kg.ToString() + ";" + ukupna_kolicina_raspolozivog_proizvoda.ToString() + "\n";
        }

        #endregion

    }
}