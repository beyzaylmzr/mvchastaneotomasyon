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
    public class RandevubilgiController : Controller
    {
        // GET: Randevubilgi
        Context c = new Context();

        
        public ActionResult Index(string Search_Data,string option, int Page_No=1)
        {
            var degerler = from x in c.RandevuBilgis select x;
            if (!string.IsNullOrEmpty(Search_Data))
            {
                if (option == "HastaNum")
                {
                    degerler = degerler.Where(x => x.Hasta.HastaNum.Contains(Search_Data));
                }else if (option == "HastaAd")
                {
                    degerler = degerler.Where(x => x.Hasta.HastaAd.ToUpper().Contains(Search_Data.ToUpper())
                || x.Hasta.HastaSoyad.ToUpper().Contains(Search_Data.ToUpper()));
                }
                else if(option == "Poliklinikid")
                {
                    degerler = degerler.Where(x => x.Doktor.Poliklinik.PoliklinikAd.ToUpper().Contains(Search_Data.ToUpper()));
                }else if (option == "DoktorAd")
                {
                    degerler = degerler.Where(x => x.Doktor.DoktorAd.ToUpper().Contains(Search_Data.ToUpper())
                    ||x.Doktor.DoktorSoyad.ToUpper().Contains(Search_Data.ToUpper()));
                }
                
            }

            return View(degerler.OrderByDescending(x=>x.RandevuDate).ThenBy(y=>y.RandevuSaat).ToList().ToPagedList(Page_No, 10));
        }
      
        [HttpGet]
        public ActionResult YeniRandevu()
        {
            
            List<SelectListItem> deger1 = (from x in c.Doktors.Where(x=>x.DoktorDurum==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DoktorAd + " " + x.DoktorSoyad,
                                               Value = x.DoktorId.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in c.Hastas.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.HastaNum +"-"+x.HastaAd +" "+x.HastaSoyad,
                                               Value = x.HastaNo.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            return View();
        }
        [HttpPost]
        public ActionResult YeniRandevu(RandevuBilgi p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.DoktorAd + " " + x.DoktorSoyad,
                                                   Value = x.DoktorId.ToString()
                                               }).ToList();
                List<SelectListItem> deger2 = (from x in c.Hastas.Where(x => x.Durum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.HastaNum + "-" + x.HastaAd + " " + x.HastaSoyad,
                                                   Value = x.HastaNo.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                ViewBag.dgr2 = deger2;
                return View();
            }
            c.RandevuBilgis.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RandevuSil(int id)
        {
            var deger = c.RandevuBilgis.Find(id);
            c.RandevuBilgis.Remove(deger);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RandevuGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DoktorAd + " " + x.DoktorSoyad,
                                               Value = x.DoktorId.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in c.Hastas.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.HastaNum + "-" + x.HastaAd + " " + x.HastaSoyad,
                                               Value = x.HastaNo.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            var randevudeger = c.RandevuBilgis.Find(id);
            return View("RandevuGetir", randevudeger);
        }
        public ActionResult RandevuGuncelle(RandevuBilgi p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Doktors.Where(x => x.DoktorDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.DoktorAd + " " + x.DoktorSoyad,
                                                   Value = x.DoktorId.ToString()
                                               }).ToList();
                List<SelectListItem> deger2 = (from x in c.Hastas.Where(x => x.Durum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.HastaNum + "-" + x.HastaAd + " " + x.HastaSoyad,
                                                   Value = x.HastaNo.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                ViewBag.dgr2 = deger2;
                return View("RandevuGetir");
            }
            var randevu = c.RandevuBilgis.Find(p.RandevuId);
            randevu.Hastano = p.Hastano;
            randevu.Doktorid = p.Doktorid;
            randevu.RandevuDate = p.RandevuDate;
            randevu.RandevuSaat = p.RandevuSaat;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}