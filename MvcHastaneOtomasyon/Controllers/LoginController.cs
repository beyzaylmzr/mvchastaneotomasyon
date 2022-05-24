using MvcHastaneOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcHastaneOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoktorLogin1()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult DoktorLogin1(Doktor d)
        {
            var bilgiler = c.Doktors.FirstOrDefault(x => x.DoktorMail == d.DoktorMail && x.DoktorSifre == d.DoktorSifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.DoktorMail, false);
                Session["DoktorMail"] = bilgiler.DoktorMail.ToString();
                return RedirectToAction("Index","DoktorPanel");
            }
            else
            {
                
                return RedirectToAction("Index","Login");
            }
            
        }

        public ActionResult AdminLogin1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin1(Admin d)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAd == d.KullaniciAd && x.Sifre == d.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Poliklinik");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}