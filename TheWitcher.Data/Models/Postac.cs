using System.Security.Cryptography;

namespace TheWitcher.Data.Models
{
    
        public class Postac
        {
            public int Id { get; set; }
            public string Imie { get; set; }
            public int RasaId { get; set; }
        // public Rasa Rasa { get; set; } // Powiązanie z modelem Rasa
        public List<Ekwipunek> Ekwipunek { get; set; } = new List<Ekwipunek>();// Powiązanie z ekwipunkiem
            public int Poziom { get; set; }
            public string Umiejetnosci { get; set; } // Lista umiejętności w formie stringa
        }
    
}
