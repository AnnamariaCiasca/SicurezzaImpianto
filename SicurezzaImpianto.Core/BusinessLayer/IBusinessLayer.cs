using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicurezzaImpianto.Core
{
    public interface IBusinessLayer
    {
        void EseguiCalcoli();
        void InserisciTemperatura(double valoreTemp);

        void InserisciEsalazione(double concentraz);
    }
}
