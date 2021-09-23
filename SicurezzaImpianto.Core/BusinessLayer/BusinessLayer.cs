using SicurezzaImpianto.Core.Entities;
using SicurezzaImpianto.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicurezzaImpianto.Core
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly ITemperaturaRepository tempRep;
        private readonly IEsalazioneRepository esalazRep;

        public BusinessLayer(ITemperaturaRepository temperaturaRepository, IEsalazioneRepository esalazioneRepository)
        {
            tempRep = temperaturaRepository;
            esalazRep = esalazioneRepository;

        }

        public void EseguiCalcoli()
        {
            List<Esalazione> esalazioni = new List<Esalazione>();
            List<Temperatura> temperature = new List<Temperatura>();
            try
            {
                esalazioni = esalazRep.GetItemsWithOutState();
                temperature = tempRep.GetItemsWithOutState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if(esalazioni.Count()>0 && temperature.Count()> 0)
            {
                foreach(var esalazione in esalazioni)
                {
                    if(esalazione.ConcentrazionePpm > 10)
                    {
                        esalazione.Stato = true; //true = superamento soglia
                    }
                    else
                    {
                        esalazione.Stato = false;
                    }

                    Temperatura tmt = temperature.Where(t => t.DataMisurazione == esalazione.DataMisurazione && t.OraMisurazione == esalazione.OraMisurazione).SingleOrDefault();
              
                if(tmt.ValoreTemperatura > 30)
                    {
                        tmt.Stato = true;
                    }
                else
                    {
                        tmt.Stato = false;
                    }

                    esalazRep.Update(esalazione);
                    tempRep.Update(tmt);

                    Evento evento = new Evento();
                    evento.InviaNotifica += ScriviSuFile;
                 
                    if(esalazione.Stato == true || tmt.Stato == true)
                    {
                        evento.SeSogliaSuperata(esalazione, tmt);
                    }
                
                }
            }
        }

        private void ScriviSuFile(Evento evento, Esalazione esalazione, Temperatura tmt)
        {
            string path = @"C:\Users\annamaria.ciasca\source\repos\SicurezzaImpianto\Risultati.txt";
       
            using(StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{esalazione.DataMisurazione.ToShortDateString()}, {esalazione.OraMisurazione} - Temperatura: {tmt.ValoreTemperatura} °C - Esalazione: {esalazione.ConcentrazionePpm} ppm");
            }
        
        
        }

        public void InserisciTemperatura(double valoreTemp)     

        {
            Temperatura temp = new Temperatura();

            temp.ValoreTemperatura = valoreTemp;
            temp.DataMisurazione = DateTime.Now;
            temp.OraMisurazione = DateTime.Now.TimeOfDay;
            tempRep.Insert(temp);
            Console.WriteLine("\nLa temperatura è stata correttamente inserita.");
        }


        public void InserisciEsalazione(double concentraz)

        {
            Esalazione es = new Esalazione();

            es.ConcentrazionePpm = concentraz;
            es.DataMisurazione = DateTime.Now;
            es.OraMisurazione = DateTime.Now.TimeOfDay;
            esalazRep.Insert(es);
            Console.WriteLine("\nLa Concentrazione di Esalazioni Tossiche è stata correttamente inserita.");
        }
    }
}
