using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace TheWitcher.Data.Models
{
    
        public class Postac
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Imię postaci jest wymagane.")]
            [StringLength(100, ErrorMessage = "Imię postaci nie może mieć więcej niż 100 znaków.")]
            public string Imie { get; set; }

            [Required(ErrorMessage = "Wybór jest obowiązkowy")]
            public int RasaId { get; set; }


        // public Rasa Rasa { get; set; } // Powiązanie z modelem Rasa


            public List<Ekwipunek> Ekwipunek { get; set; } = new List<Ekwipunek>();// Powiązanie z ekwipunkiem

           [Required(ErrorMessage = "Poziom jest wymagany")]
           [Range(1,100, ErrorMessage = "Poziom musi być większy niż 0 i mniejszy niż 101.")]
           public int Poziom { get; set; }

           [Required(ErrorMessage = "Umiejętności postaci są wymagane.")]
           [StringLength(500, ErrorMessage = "Umiejętności nie mogą przekroczyć 500 znaków.")]
           public string Umiejetnosci { get; set; } 
        }
    
}
