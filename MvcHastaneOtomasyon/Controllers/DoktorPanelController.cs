using MvcHastaneOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;


namespace MvcHastaneOtomasyon.Controllers
{
    public class DoktorPanelController : Controller
    {
        // GET: DoktorPanel
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
           
            var mail = (string)Session["DoktorMail"];
            var mail2 = (string)Session["Gonderici"];
            var degerler = c.mesajlars.Where(x => x.Alici == mail).ToList();
            var gorsel = c.Doktors.Where(x => x.DoktorMail == mail).Select(y =>y.DoktorGorsel).FirstOrDefault();
            ViewBag.gorsel = gorsel;
            var adsoyad = c.Doktors.Where(x => x.DoktorMail == mail).Select(y => y.DoktorAd + " " + y.DoktorSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var poliklinik = c.Doktors.Where(x => x.DoktorMail == mail).Select(y => y.Poliklinik.PoliklinikAd).FirstOrDefault();
            ViewBag.poliklinik = poliklinik;
            ViewBag.m = mail;
            
            return View(degerler);
        }
        public ActionResult Hastalarim(string Search_Data, string option, int Page_No = 1)
        {
            var mail = (string)Session["DoktorMail"];
            var id = c.Doktors.Where(x => x.DoktorMail == mail.ToString()).Select(y
                => y.DoktorId).FirstOrDefault();
            var degerler = from x in c.Hastas where x.Doktorid == id select x;
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
        public ActionResult Randevu(string Search_Data, string option, int Page_No = 1)
        {
            var mail = (string)Session["DoktorMail"];
            var id = c.Doktors.Where(x => x.DoktorMail == mail.ToString()).Select(y
               => y.DoktorId).FirstOrDefault();
            var degerler = from x in c.RandevuBilgis where x.Doktorid == id select x;
            if (option == "HastaNum")
            {
                degerler = degerler.Where(x => x.Hasta.HastaNum.Contains(Search_Data));
            }
            else if (option == "HastaAd")
            {
                degerler = degerler.Where(x => x.Hasta.HastaAd.ToUpper().Contains(Search_Data.ToUpper())
            || x.Hasta.HastaSoyad.ToUpper().Contains(Search_Data.ToUpper()));
            }
            return View(degerler.ToList().ToPagedList(Page_No, 10));
        }
        public ActionResult DoktorRandevuTakip(int id)
        {
            var degerler = c.RandevuBilgis.Where(x => x.Hastano == id).ToList();
            var hasta = c.Hastas.Where(x => x.HastaNo == id).Select(y => y.HastaAd + " " + y.HastaSoyad).FirstOrDefault();
            ViewBag.h = hasta;
            return View(degerler);
        }
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["DoktorMail"];
            var mesajlar = c.mesajlars.Where(x=>x.Alici==mail).OrderByDescending(x=>x.MesajId).ToList();
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View(mesajlar);
        }
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["DoktorMail"];
            var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(z => z.MesajId).ToList();
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.mesajlars.Where(x => x.MesajId == id).ToList();
            var mail = (string)Session["DoktorMail"];
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["DoktorMail"];
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(mesajlar m)
        {
            var mail = (string)Session["DoktorMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["DoktorMail"];
            List<SelectListItem> deger1 = (from x in c.Polikliniklers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PoliklinikAd,
                                               Value = x.PoliklinikId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var id = c.Doktors.Where(x => x.DoktorMail == mail).Select(y => y.DoktorId).FirstOrDefault();
            var doktorbul = c.Doktors.Find(id);
            return PartialView("Partial1",doktorbul);
        }
        public ActionResult DoktorBilgiGuncelle(Doktor dr)
        {
            var doktor = c.Doktors.Find(dr.DoktorId);
            doktor.DoktorAd = dr.DoktorAd;
            doktor.DoktorSoyad = dr.DoktorSoyad;
            doktor.DoktorMail = dr.DoktorMail;
            doktor.Poliklinikid = dr.Poliklinikid;
            doktor.DoktorSifre = dr.DoktorSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}