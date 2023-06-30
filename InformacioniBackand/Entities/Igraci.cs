namespace InformacioniBackand.Entities
{
    public class Igraci
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set;}

        public string? Pozicija { get; set; }
        public int? IdTima { get; set; }
//        public Tim Tim { get; set; }

        public bool? Postava { get; set; }

    }
}
