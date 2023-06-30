namespace InformacioniBackand.Entities
{
    public class Navijac
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public byte[] Lozinka { get; set; }
        public byte[] LozinkaKljuc { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool? StatusReg { get; set; }
        public int? IdTima { get; set; }
         
        public int? BrojClanskeKarte { get; set; }

        public string? DatumIstekaRoka { get; set; }

       
    }
}
