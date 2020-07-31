using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kolokvijum_2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* routes.MapRoute(
                  "Proizvodi",
                  "Proizvodi/NeregistrovaniPrikaz",
                 new { controller = "Proizvodi", action = "NeregistrovaniPrikaz" }    
             );*/

            routes.MapRoute(
                  "Obrisi",
                  "Prijavljen/Obrisi/{korisnicko_ime}",
                 new { controller = "Prijavljen", action = "Obrisi", korisnicko_ime = UrlParameter.Optional }
             );

            routes.MapRoute(
                  "Detalji",
                  "Proizvodi/Detalji/{naziv}/{sorta}",
                 new { controller = "Proizvodi", action = "Detalji", naziv = UrlParameter.Optional, sorta = UrlParameter.Optional }
             );

            routes.MapRoute(
                  "Azuriraj2",
                  "Prijavljen/Azuriraj2/{naziv}/{sorta}",
                 new { controller = "Prijavljen", action = "Azuriraj2", naziv = UrlParameter.Optional, sorta = UrlParameter.Optional }
             );

            routes.MapRoute(
                  "Azuriraj",
                  "Prijavljen/Azuriraj/{naziv}/{sorta}",
                 new { controller = "Prijavljen", action = "Azuriraj", naziv = UrlParameter.Optional, sorta = UrlParameter.Optional }
             );

            routes.MapRoute(
                  "DodajUKorpu",
                  "Proizvodi/DodajUKorpu/{naziv}/{sorta}",
                 new { controller = "Proizvodi", action = "DodajUKorpu", naziv = UrlParameter.Optional, sorta = UrlParameter.Optional }
             );


            routes.MapRoute(
                  "Prijava",
                  "Registracija/Prijava/{id}",
                 new { controller = "Registracija", action = "Prijava", id = UrlParameter.Optional }    
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Proizvodi", action = "PrikazProizvoda", id = UrlParameter.Optional }
            );



        }
    }
}
