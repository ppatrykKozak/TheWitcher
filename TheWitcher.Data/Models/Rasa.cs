using System.ComponentModel.DataAnnotations;

namespace TheWitcher.Data.Models
{
    public class Rasa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa Rasy jest obowiązkowa")]
        [StringLength(50, ErrorMessage = "Nazwa Rasy nie może przekraczać 50 znaków.")]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Opis jest obowiązkowy.")]
        [StringLength(150, ErrorMessage = "Opis nie może przekraczać 150 znaków.")]
        public string Opis { get; set; }
    }

}
