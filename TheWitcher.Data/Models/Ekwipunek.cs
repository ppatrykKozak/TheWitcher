namespace TheWitcher.Data.Models
{
    
        public class Ekwipunek
        {
            public int Id { get; set; }
            public string Nazwa { get; set; }
            public string Typ { get; set; } // Typ ekwipunku (np. broń, zbroja)
            public int PostacId { get; set; } // Powiązanie z postacią
        }
    
}
