using Microsoft.EntityFrameworkCore;

namespace InformacioniBackand.Entities
{
    [Keyless]
    public class NaKlupi
    {

        public int IdIgraca { get; set; }

        public int IdUtakmice { get; set; }


    }
}
