namespace InformacioniBackand.Entities
{
    public class Utakmica
    {
        public int? Id { get; set; }
        public string? Datum { get; set; }
        public string? Vreme { get; set; }
        public string? Rezultat { get; set; }
        public string? BrZutihKartona { get; set; }

        public string? BrCrvenihKartona { get; set; }

        public int? IdTima1 { get; set; }
        public int? IdTima2 { get; set; }
    }
}
