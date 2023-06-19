namespace InformacioniBackand.Entities
{
    public class Rezultati
    {

        public int Id { get; set; }
        public string Sezona { get; set; }
        public int BrOdigranihSusreta { get; set; }
        public int BrPobeda { get; set; }
        public int BrNeresenih { get; set; }

        public int BrIzgubljenih { get; set; }

        public int BrBodova { get; set; }
        public int IdTima { get; set; }

    }
}
