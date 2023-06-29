namespace InformacioniBackand.Entities
{
    public class Tim
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string DatumOsnivanja { get; set; }
        public string Grad { get; set; }
        public string Logo { get; set; }

        public int? IdMenadzera { get; set; }

        public ICollection<Navijac> Navijac { get; set; }

        public ICollection<Igraci> Igraci { get; set; }

        public ICollection<Utakmica> Utakmica { get; set; }

    }
}
