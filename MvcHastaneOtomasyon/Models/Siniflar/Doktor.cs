using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class Doktor
    {
        [Key]
        [Display(Name = "Doktor Ad-Soyad")]
        public int DoktorId { get; set; }


        [Display(Name = "Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string DoktorGorsel { get; set; }
        
        [Display(Name ="Doktor Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string DoktorAd { get; set; }

        [Display(Name = "Doktor Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string DoktorSoyad { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public int Poliklinikid { get; set; }
        public virtual Poliklinikler Poliklinik { get; set; }


        [Display(Name = "Mail Adresi")]
        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        [EmailAddress(ErrorMessage = "Geçersiz mail adresi,deneme@deneme.com formatına dikkat ediniz")]
        public string DoktorMail { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "Varchar")]
        [StringLength(20, ErrorMessage = "En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string DoktorSifre { get; set; }


        [Display(Name = "Doktor Çalışma Durumu")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public bool DoktorDurum { get; set; }
        public ICollection<Hasta> Hastas { get; set; }
        public ICollection <RandevuBilgi> RandevuBilgis { get; set; }

    }
}