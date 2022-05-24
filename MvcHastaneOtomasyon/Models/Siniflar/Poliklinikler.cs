using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class Poliklinikler
    {
        [Key]
        [Display(Name = "Poliklinik Adı")]
        public int PoliklinikId { get; set; }



        [Display(Name = "Poliklinik Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string PoliklinikAd { get; set; }
        public ICollection<Doktor> Doktors { get; set; }


        [Display(Name = "Poliklinik Aktiflik Durumu")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public bool PoliklinikDurum { get; set; }
    }
}