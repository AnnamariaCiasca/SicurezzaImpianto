//Un’applicazione industriale deve monitorare la sicurezza di un
//impianto. Sono in tal senso presenti un sensore di temperatura
//ed uno di presenza esalazioni tossiche, che inviano opportuni
//eventi qualora vengono superati valori prefissati di temperatura
//o di concentrazione in ppm, rispettivamente. Un unico sistema
//di monitoraggio deve produrre messaggi di avviso al personale.

using SicurezzaImpianto.ADORepository;
using SicurezzaImpianto.Core;
using SicurezzaImpianto.Core.Repositories;

using System;

namespace SicurezzaImpianto
{
    class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new TemperaturaRepository(), new EsalazioneRepository()); 
        static void Main(string[] args)
        {
            try
            {
                bl.EseguiCalcoli();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
