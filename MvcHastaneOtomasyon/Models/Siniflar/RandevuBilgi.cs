using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class RandevuBilgi
    {
        [Key]
        public int RandevuId { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public int Hastano { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public int Doktorid { get; set; }
        public virtual Hasta Hasta { get; set; }
        public virtual Doktor Doktor { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Randevu Tarihi")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public DateTime RandevuDate { get; set; }


        [Display(Name = "Randevu Saati")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public TimeSpan RandevuSaat { get; set; }

    }
}