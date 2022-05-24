using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class Hasta
    {
        [Key]
        [Display(Name = "Hasta Ad-Soyad")]
        public int HastaNo { get; set; }

        [Display(Name = "Hasta No")]
        [Column(TypeName = "Varchar")]
        [StringLength(7)]
        public string HastaNum { get; set; }


        [Display(Name = "Hasta Adı")]
        [Column(TypeName="Varchar")]
        [StringLength(30,ErrorMessage ="En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage ="Bu Alanı Boş Geçemezsiniz!")]
        public string HastaAd { get; set; }


        [Display(Name = "Hasta Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage = "En Fazla 30 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]

        public string HastaSoyad { get; set; }


        [Display(Name = "Cinsiyet")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string HastaCinsiyet { get; set; }


        [Display(Name = "Mail Adresi")]
        [Column(TypeName = "Varchar")]
        [StringLength(50,ErrorMessage = "En Fazla 50 Karakter Uzunluğunda Olabilir!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        [EmailAddress(ErrorMessage = "Geçersiz mail adresi,deneme@deneme.com formatına dikkat ediniz")]
        public string HastaMail { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public DateTime HastaDogumtarih { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public int Doktorid { get; set; }

        public virtual Doktor Doktor { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public bool Durum { get; set; }
        public ICollection<RandevuBilgi> RandevuBilgis { get; set; }

    }
}