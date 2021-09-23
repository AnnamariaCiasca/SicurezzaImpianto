using SicurezzaImpianto.Core.Interfaces;
using System;
using System.Collections.Generic;
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


        }
    }
}
