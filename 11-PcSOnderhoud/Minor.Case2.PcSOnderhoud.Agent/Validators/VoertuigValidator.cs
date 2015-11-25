using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Agent.Validators
{
    public static class VoertuigValidator
    {
        public static void Validate(Schema.Voertuig voertuig)
        {
            var list = new FunctionalErrorList();

            if (voertuig.Bestuurder == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Bestuurder mag niet leeg zijn"
                });
            }

            if (voertuig.Eigenaar == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Eigenaar mag niet leeg zijn"
                });
            }

            if (voertuig.Kenteken == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Kenteken mag niet leeg zijn"
                });
            }

            if (list.HasErrors)
            {
                throw new FunctionalException(list);
            }
            
        }

        public static void Validate(AgentSchema.Voertuig voertuig)
        {
            var list = new FunctionalErrorList();

            if (voertuig.Bestuurder == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Bestuurder mag niet leeg zijn"
                });
            }

            if (voertuig.Eigenaar == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Eigenaar mag niet leeg zijn"
                });
            }

            if (voertuig.Kenteken == null)
            {
                list.Add(new FunctionalErrorDetail
                {
                    Message = "Kenteken mag niet leeg zijn"
                });
            }
            throw new FunctionalException(list);
        }

    }
}
