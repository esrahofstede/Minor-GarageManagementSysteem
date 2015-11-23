using Minor.Case2.BSVoertuigEnKlantBeheer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using minorcase2bsvoertuigenklantbeheer.v1.schema;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation
{
    [ServiceBehavior]
    public class BSVoertuigEnKlantbeheerHandler : IBSVoertuigEnKlantbeheer
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
