using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minor.Case2.FEGMS.Client.ISRDWServiceReferenceService;

namespace Minor.Case2.FEGMS.Client.Controllers
{
    public class RdwControlleController : Controller
    {
        // GET: RdwControlle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Execute()
        {
            IISRDWService service = new ISRDWServiceClient();
            SendRdwKeuringsverzoekRequestMessage requestMessage = new SendRdwKeuringsverzoekRequestMessage
            {
                Garage = new Garage(),
                Voertuig = new Voertuig
                {
                    kenteken =  "12-AA-BB"
                },
                Keuringsverzoek = new Keuringsverzoek()
            };
            
            SendRdwKeuringsverzoekResponseMessage response = service.RequestKeuringsverzoek(requestMessage);
            return View(response.Steekproef as object);
        }
    }
}