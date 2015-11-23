﻿using Minor.ServiceBus.Agent.Implementation;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    
    public class AgentISRDW
    {
        private ServiceFactory<IISRDWService> _factory;

        public AgentISRDW()
        {
            _factory = new ServiceFactory<IISRDWService>("ISRDWService");
        }

        [CLSCompliant(false)]
        public AgentISRDW(ServiceFactory<IISRDWService> factory)
        {
            _factory = factory;
        }
        public SendRdwKeuringsverzoekResponseMessage SendAPKKeuringsverzoek(Voertuig voertuig, Garage garage, Keuringsverzoek keuringsverzoek)
        {
            var proxy =_factory.CreateAgent();
            var apkKeuringsverzoek = new SendRdwKeuringsverzoekRequestMessage
            {
                Voertuig = voertuig,
                Garage =  garage,
                Keuringsverzoek = keuringsverzoek
            };
            var result = proxy.RequestKeuringsverzoek(apkKeuringsverzoek);
            return result;
        }
    }
}
