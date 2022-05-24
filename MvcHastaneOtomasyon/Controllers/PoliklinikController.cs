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
    public class PoliklinikController : Controller
    {
        // GET: Poliklinik
        Context c = new Context();
        public ActionResult Index(string Search_Data, int Page_No=1)
        {
            
            var degerler = from x in c.Polikliniklers where x.PoliklinikDurum==true select x;
            if (!string.IsNullOrEmpty(Search_Data))
            {
                degerler=degerler.Where(x => x.PoliklinikAd.ToUpper().Contains(Search_Data.ToUpper()));
            }
            return View(degerler.ToList().ToPagedList(Page_No, 10));
        }

        [Authorize(Roles="A")]
        //view yüklendiği zaman burası çalışıcak-boş sayfa gelicek
        [HttpGet]
        public ActionResult PoliklinikEkle()
        {
            return View();
        }
        //butona tıkladığımızda bu kısım çalışıcak
        [HttpPost]
        public ActionResult PoliklinikEkle(Poliklinikler p)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            p.PoliklinikDurum = true;
            c.Polikliniklers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PoliklinikSil(int id)
        {
            var plk = c.Polikliniklers.Find(id);
            plk.PoliklinikDurum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PoliklinikGetir(int id)
        {
            var poliklinik = c.Polikliniklers.Find(id);
            return View("PoliklinikGetir", poliklinik);
        }
        public ActionResult PoliklinikGuncelle(Poliklinikler p)
        {
            if (!ModelState.IsValid)
            {
                return View("PoliklinikGetir");
            }
            var plkn = c.Polikliniklers.Find(p.PoliklinikId);
            plkn.PoliklinikDurum = true;
            plkn.PoliklinikAd = p.PoliklinikAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DoktorListe(int id)
        {
            var degerler = c.Doktors.Where(x => x.Poliklinikid == id && x.DoktorDurum==true).ToList();
            var dr = c.Polikliniklers.Where(x => x.PoliklinikId == id).Select(y => y.PoliklinikAd).FirstOrDefault();
            ViewBag.d = dr;
            return View(degerler);
        }
       
    }
}