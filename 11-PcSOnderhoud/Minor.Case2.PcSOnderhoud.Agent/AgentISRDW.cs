using Minor.ServiceBus.Agent.Implementation;
using minorcase2bsvoertuigenklantbeheer.v1.schema;
using minorcase2isrijksdienstwegverkeer.v1.schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class AgentISRDW
    {
        ServiceFactory<IISRDWService> factory = new ServiceFactory<IISRDWService>("ISRDWService");

        public AgentISRDW()
        {
            var proxy = factory.CreateAgent();
            var test = proxy.RequestKeuringsverzoek(new minorcase2isrijksdienstwegverkeer.v1.messages.SendRdwKeuringsverzoekRequestMessage
            {
                Garage = new Garage
                {
                    Naam = "test",
                    Kvk = "123112344",
                    Plaats = "Utrecht"
                },
                Voertuig = new Voertuig
                {
                    kenteken = "12-AA-BB",
                    merk = "ford",
                    type = "focus",
                    bestuurder = new Persoon
                    {
                        voornaam = "jan",
                        achternaam = "jansen"
                    },
                    eigenaar = new Persoon
                    {
                        voornaam = "jan",
                        achternaam = "jansen"
                    },
                    id = 1
                },
                Keuringsverzoek = new Keuringsverzoek
                {
                    CorrolatieId = "asdf",
                    Date = DateTime.Now,
                    Type = "personenauto"
                }
            });
        }
    }
}
