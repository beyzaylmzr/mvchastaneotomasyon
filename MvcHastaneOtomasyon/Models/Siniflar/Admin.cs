using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string KullaniciAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string Sifre { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string Yetki { get; set; }

    }
}