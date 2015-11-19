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
                Garage = new Garage
                {
                    Naam = "test",
                    Kvk = "123112344",
                    Plaats = "Utrecht"
                },
                Voertuig = new Voertuig
                {
                    kenteken =  "12-AA-BB",
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
            };
            
            SendRdwKeuringsverzoekResponseMessage response = service.RequestKeuringsverzoek(requestMessage);
            return View(response.Steekproef as object);
        }
    }
}