using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcHastaneOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcHastaneOtomasyon.Controllers
{
    public class HastaController : Controller
    {
        // GET: Hasta
        Context c = new Context();

        public ActionResult Index(string Search_Data, string option, int Page_No = 1)
        {
            var degerler = from x in c.Hastas where x.Durum == true select x;
            if (!string.IsNullOrEmpty(Search_Data))
            {
                if (option == "HastaAd")
                {
                    degerler = degerler.Where(x => x.HastaAd.ToUpper().Contains(Search_Data.ToUpper())
                || x.HastaSoyad.ToUpper().Contains(Search_Data.ToUpper()));
                }
                else if (option == "HastaNum")
                {
                    degerler = degerler.Where(x => x.HastaNum.Contains(Search_Data));
                }

            }
            return View(degerler.ToList().ToPagedList(Page_No, 10));
        }
        [HttpGet]
        public ActionResult YeniHasta()
        {
            List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DoktorAd + " " + x.DoktorSoyad,
                                               Value = x.DoktorId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            int s1, s2, s3;
            Random rnd = new Random();
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + s2.ToString() + s3.ToString();
            ViewBag.hastanum = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniHasta(Hasta p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.DoktorAd + " " + x.DoktorSoyad,
                                                   Value = x.DoktorId.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                int s1, s2, s3;
                Random rnd = new Random();
                s1 = rnd.Next(100, 1000);
                s2 = rnd.Next(10, 99);
                s3 = rnd.Next(10, 99);
                string kod = s1.ToString() + s2.ToString() + s3.ToString();
                ViewBag.hastanum = kod;
                return View();
            }
            p.Durum = true;
            c.Hastas.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HastaSil(int id)
        {
            var deger = c.Hastas.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HastaGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DoktorAd + " " + x.DoktorSoyad,
                                               Value = x.DoktorId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var hastadeger = c.Hastas.Find(id);
            return View("HastaGetir", hastadeger);
        }
        public ActionResult HastaGuncelle(Hasta p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.DoktorAd + " " + x.DoktorSoyad,
                                                   Value = x.DoktorId.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
               
                return View("HastaGetir");
            }
            var hasta = c.Hastas.Find(p.HastaNo);
            hasta.Durum = true;
            hasta.HastaAd = p.HastaAd;
            hasta.HastaSoyad = p.HastaSoyad;
            hasta.HastaCinsiyet = p.HastaCinsiyet;
            hasta.HastaDogumtarih = p.HastaDogumtarih;
            hasta.HastaMail = p.HastaMail;
            hasta.Doktorid = p.Doktorid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HastaRandevuBilgi(int id)
        {
            var degerler = c.RandevuBilgis.Where(x => x.Hastano == id).ToList();
            var hasta = c.Hastas.Where(x => x.HastaNo == id).Select(y => y.HastaAd + " " + y.HastaSoyad).FirstOrDefault();
            ViewBag.h = hasta;
            return View(degerler);
        }

    }
}