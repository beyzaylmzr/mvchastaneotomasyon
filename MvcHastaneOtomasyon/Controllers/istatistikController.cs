using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcHastaneOtomasyon.Models.Siniflar;

namespace MvcHastaneOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Polikliniklers.Where(x=>x.PoliklinikDurum==true).Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = c.Doktors.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = c.Doktors.Where(x => x.DoktorDurum == true).Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = c.RandevuBilgis.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = c.Hastas.Count().ToString();
            ViewBag.d5 = deger5;
            var deger6 = c.Hastas.Where(x => x.Durum == true).Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = c.Hastas.Where(x => x.HastaCinsiyet == "Kadın" & x.Durum == true).Count().ToString();
            ViewBag.d7 = deger7;
            var deger8 = c.Hastas.Where(x => x.HastaCinsiyet== "Erkek" &  x.Durum == true).Count().ToString();
            ViewBag.d8 = deger8;
            DateTime bugun = DateTime.Today;
            var deger9 = c.Hastas.Where(x => x.HastaDogumtarih.Year <= bugun.Year & x.HastaDogumtarih.Year >(bugun.Year-17) & x.Durum == true).Count().ToString();
            ViewBag.d9 = deger9;
            var deger10 = c.Hastas.Where(x => x.HastaDogumtarih.Year <= (bugun.Year-18) & x.HastaDogumtarih.Year > (bugun.Year - 45) & x.Durum == true).Count().ToString();
            ViewBag.d10 = deger10;
            var deger11 = c.Hastas.Where(x => x.HastaDogumtarih.Year <= (bugun.Year - 46) & x.HastaDogumtarih.Year > (bugun.Year - 65) & x.Durum == true).Count().ToString();
            ViewBag.d11 = deger11;
            var deger12 = c.Hastas.Where(x => x.HastaDogumtarih.Year <= (bugun.Year - 66) & x.HastaDogumtarih.Year > (bugun.Year - 105) & x.Durum == true).Count().ToString();
            ViewBag.d12 = deger12;
            return View();
        }
      
    }
}