using Minor.Case2.PcSOnderhoud.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;

namespace Minor.Case2.PcSOnderhoud.Implementation
{
    public class PcSOnderhoudServiceHandler : IPcSOnderhoudService
    {
        public KlantenCollection GetAllKlanten()
        {
            throw new NotImplementedException();
        }

        public VoertuigCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            throw new NotImplementedException();
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            throw new NotImplementedException();
        }

        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            throw new NotImplementedException();
        }
    }
}
