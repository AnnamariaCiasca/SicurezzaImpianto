using SicurezzaImpianto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicurezzaImpianto.Core
{
 public class Evento
    {
        public delegate void ScriviSuFile(Evento evento, Esalazione esalazione, Temperatura tmt);

        public event ScriviSuFile InviaNotifica;

        public void SeSogliaSuperata(Esalazione esalazione, Temperatura tmt)
        {
            if(InviaNotifica != null)
            {
                InviaNotifica(this, esalazione, tmt);
            }
        }
    }
}
