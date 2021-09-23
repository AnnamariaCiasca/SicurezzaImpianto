using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicurezzaImpianto.Core.Entities
{
    public class Esalazione
    {
        public int Id { get; set; }
        public DateTime DataMisurazione { get; set; }
        public TimeSpan OraMisurazione { get; set; }
        public double ConcentrazionePpm { get; set; }
        public bool? Stato { get; set; } //sul db è nullable
    }
}
