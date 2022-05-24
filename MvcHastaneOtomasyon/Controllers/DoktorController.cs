using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcHastaneOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcHastaneOtomasyon.Controllers
{
    public class DoktorController : Controller
    {
        // GET: Doktor
        Context c = new Context();

        public ActionResult Index(string Search_Data, string option, int Page_No = 1)
        {
            var degerler = from x in c.Doktors where x.DoktorDurum == true select x;
            if (!string.IsNullOrEmpty(Search_Data))
            {
                if (option == "DoktorAd")
                {
                    degerler = degerler.Where(x => x.DoktorAd.ToUpper().Contains(Search_Data.ToUpper())
               || x.DoktorSoyad.ToUpper().Contains(Search_Data.ToUpper()));
                }
                else if (option == "Poliklinikid")
                {
                    degerler = degerler.Where(x => x.Poliklinik.PoliklinikAd.ToUpper().Contains(Search_Data.ToUpper()));
                }
            }
            return View(degerler.ToList().ToPagedList(Page_No, 10));
        }

        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult YeniDoktor()
        {
            List<SelectListItem> deger1 = (from x in c.Polikliniklers.Where(x => x.PoliklinikDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PoliklinikAd,
                                               Value = x.PoliklinikId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniDoktor(Doktor p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Polikliniklers.Where(x => x.PoliklinikDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.PoliklinikAd,
                                                   Value = x.PoliklinikId.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                return View();
            }
            //yaptığım işlemde dosya tutuyorsam
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                //string uzanti = Path.GetExtension(Request.Files[0].FileName)
                string yol = "~/Image/" + dosyaadi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.DoktorGorsel = "/Image/" + dosyaadi;
            }
            p.DoktorDurum = true;
            c.Doktors.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DoktorSil(int id)
        {
            var deger = c.Doktors.Find(id);
            deger.DoktorDurum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DoktorGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Polikliniklers.Where(x => x.PoliklinikDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PoliklinikAd,
                                               Value = x.PoliklinikId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var doktordeger = c.Doktors.Find(id);
            return View("DoktorGetir", doktordeger);
        }
        [HttpPost]
        public ActionResult DoktorGuncelle(Doktor p, HttpPostedFileBase DoktorGorsel)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deger1 = (from x in c.Polikliniklers.Where(x => x.PoliklinikDurum == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.PoliklinikAd,
                                                   Value = x.PoliklinikId.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                return View("DoktorGetir");
            }

            var dr = c.Doktors.Find(p.DoktorId);
            dr.DoktorDurum = true;
            dr.Poliklinikid = p.Poliklinikid;
            dr.DoktorAd = p.DoktorAd;
            dr.DoktorSoyad = p.DoktorSoyad;
            dr.DoktorMail = p.DoktorMail;
            dr.DoktorSifre = p.DoktorSifre;

            if (ModelState.IsValid)
            {
                if (DoktorGorsel != null)
                {
                    string dosyaadi = Path.GetFileName(DoktorGorsel.FileName);

                    string yol = "~/Image/" + dosyaadi;

                    DoktorGorsel.SaveAs(Server.MapPath(yol));

                    dr.DoktorGorsel = "/Image/" + dosyaadi;
                }
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HastaListe(int id)
        {
            var degerler = c.Hastas.Where(x => x.Doktorid == id && x.Durum==true).ToList();
            var dr = c.Doktors.Where(x => x.DoktorId == id).Select(y => y.DoktorAd + " " + y.DoktorSoyad).FirstOrDefault();
            ViewBag.d = dr;
            return View(degerler);
        }
        public ActionResult DoktorHastaRandevuBilgi(int id)
        {
            var degerler = c.RandevuBilgis.Where(x => x.Hastano == id).ToList();
            var hasta = c.Hastas.Where(x => x.HastaNo == id).Select(y => y.HastaAd + " " + y.HastaSoyad).FirstOrDefault();
            ViewBag.h = hasta;
            return View(degerler);
        }
        public ActionResult DoktorListe()
        {
            var doktorlar = c.Doktors.Where(x => x.DoktorDurum == true).ToList();
            return View(doktorlar);
        }

    }
}