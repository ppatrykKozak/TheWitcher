using System.ComponentModel.DataAnnotations;

namespace TheWitcher.Data.Models
{
    
        public class Ekwipunek
        {

           public int Id { get; set; }


           [Required(ErrorMessage = "Nazwa ekwipunku jest obowiązkowa.")]
           [StringLength(100, ErrorMessage = "Nazwa ekwipunku nie może przekraczać 100 znaków.")]
           public string Nazwa { get; set; }


           [Required(ErrorMessage = "Typ ekwipunku jest obowiązkowy.")]
            [StringLength(50, ErrorMessage = "Typ ekwipunku nie może przekraczać 50 znaków.")]
           public string Typ { get; set; } 

           public int PostacId { get; set; } // Powiązanie z postacią
        }
    
}
