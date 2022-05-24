using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class mesajlar
    {
        [Key]
        public int MesajId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string Gonderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string Alici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string icerik { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public DateTime Tarih { get; set; }
    }
}