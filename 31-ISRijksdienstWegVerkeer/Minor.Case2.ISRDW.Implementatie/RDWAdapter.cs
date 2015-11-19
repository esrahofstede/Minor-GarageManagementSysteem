using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Minor.Case2.ISRDW.Implementation
{
    public class RDWAdapter
    {
        private IRDWService _rdwService;

        public apkKeuringsverzoekResponseMessage SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage message)
        {
            var response = _rdwService.SubmitAPKVerzoek(Util.SerializeToXML(message));
            return Util.DeserializeFromXML<apkKeuringsverzoekResponseMessage>(response);
        }


        public RDWAdapter()
        {
            _rdwService = new RDWService();
        }

        public RDWAdapter(IRDWService rdwService)
        {
            _rdwService = rdwService;
        }
    }
}