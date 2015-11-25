using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Minor.Case2.Exceptions.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Agent.Exceptions
{
    public class FunctionalErrorList
    {
        private readonly List<FunctionalErrorDetail> _details = new List<FunctionalErrorDetail>();

        public FunctionalErrorList(){}

        public FunctionalErrorList(FunctionalErrorDetail[] details)
        {
            AddRange(details);
        }

        public void Add(FunctionalErrorDetail detail)
        {
            _details.Add(detail);
        }

        public void AddRange(FunctionalErrorDetail[] details)
        {
            _details.AddRange(details);
        }

        public bool HasErrors => _details.Count > 0;

        public FunctionalErrorDetail[] Details => _details.ToArray();
    }
}