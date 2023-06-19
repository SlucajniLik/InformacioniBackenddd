using Microsoft.EntityFrameworkCore;

namespace InformacioniBackand.Entities
{
    [Keyless]
    public class UPrvojPostavi
    {

        public int IdIgraca { get; set; }

        public int IdUtakmice { get; set; }
    }
}
