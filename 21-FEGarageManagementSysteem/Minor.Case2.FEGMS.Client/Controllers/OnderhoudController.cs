using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
using Minor.Case2.FEGMS.Client.Helper;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Minor.Case2.FEGMS.Client.Controllers
{
    public class OnderhoudController : Controller
    {
        IAgentPcSOnderhoud _agent;

        /// <summary>
        /// Contstructor to instantiate the agent
        /// </summary>
        public OnderhoudController()
        {
            _agent = new AgentPcSOnderhoud();
        }

        /// <summary>
        /// Creates an instance of the OnderhoudController and can be injected with an IAgentPcSOnderhoud
        /// </summary>
        /// <param name="agent">Injectable IAgentPcSOnderhoud</param>
        public OnderhoudController(IAgentPcSOnderhoud agent)
        {
            _agent = agent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var onderhoud = Mapper.MapToOnderhoudsopdracht(GetOnderhoudsopdracht(), GetLeasemaatschappijGegevens(), GetKlantGegevens(true), GetVoertuiggegevens());
            //onderhoud.Onderhoudswerkzaamheden = new BSVoertuigenEnKlantBeheer.V1.Schema.Onderhoudswerkzaamheden
            //{
            //    Afmeldingsdatum = DateTime.Now,
            //    Kilometerstand = 123,
            //    Onderhoudswerkzaamhedenomschrijving = "werk",
            //};
            //onderhoud.Status = "Aan";


            _agent.AddOnderhoudsOpdrachtWithKlantAndVoertuig(onderhoud);
            return View();

        }

        /// <summary>
        /// Show the form to insert Klantgegevens
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertKlantgegevens()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a Klant and insert it in a cookie
        /// </summary>
        /// <param name="model">Inserted klantgegevens viewmodel</param>
        /// <returns>RedirectToAction depends on lease or not. If lease to InsertLeasemaatschappijGegevens else to InsertVoertuiggegevens</returns>
        [HttpPost]
        public ActionResult InsertKlantgegevens(InsertKlantgegevensVM model)
        {
            if(ModelState.IsValid)
            {

                var serializedKlantgegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie KlantgegevensCookie = new HttpCookie("Klantgegevens", serializedKlantgegevens);
                Response.Cookies.Add(KlantgegevensCookie);

                if (model.Lease)
                {
                    return RedirectToAction("InsertLeasemaatschappijGegevens");
                }
                else
                {
                    return RedirectToAction("InsertVoertuiggegevens");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Show the form to insert Leasemaatschappijgegevens
        /// </summary>
        /// <returns>view</returns>
        public ActionResult InsertLeasemaatschappijGegevens()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a leasemaatschappij and insert it in a cookie
        /// </summary>
        /// <param name="model">Inserted leasemaatschappij viewmodel</param>
        /// <returns>RedirectToAction to InsertVoertuiggegevens</returns>
        [HttpPost]
        public ActionResult InsertLeasemaatschappijGegevens(InsertLeasemaatschappijGegevensVM model)
        {
            if(ModelState.IsValid)
            {
                var serializedLeasmaatschappijGegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializedLeasmaatschappijGegevens);
                Response.Cookies.Add(leasemaatschappijCookie);

                return RedirectToAction("InsertVoertuiggegevens");
            }
           
            return View(model);
        }

        /// <summary>
        /// Show the form to insert voertuiggegevens
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertVoertuiggegevens()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a voertuig and send it to all of the data to the agent
        /// Deserialize the previous cookies
        /// </summary>
        /// <param name="model">Inserted voertuig viewmodel</param>
        /// <returns>RedirectToAction to InsertOnderhoudsopdracht</returns>
        [HttpPost]
        public ActionResult InsertVoertuiggegevens(InsertVoertuiggegevensVM model)
        {
            if (ModelState.IsValid)
            {
                var serializer = new JavaScriptSerializer();
                var serializedVoertuiggegevens = serializer.Serialize(model);
       
                HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");

                InsertLeasemaatschappijGegevensVM leasemaatschappijgegevens = null;
                if (leasemaatschappijCookie != null)
                {
                    leasemaatschappijgegevens = serializer.Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);
                }

                HttpCookie klantgegevensCookie = Request.Cookies.Get("Klantgegevens");
                var klantgegevens = serializer.Deserialize<InsertKlantgegevensVM>(klantgegevensCookie.Value);


                HttpCookie voertuiggegevensCookie = new HttpCookie("Voertuiggegevens", serializedVoertuiggegevens);
                Response.Cookies.Add(voertuiggegevensCookie);

                var voertuig = Mapper.MapToVoertuig(leasemaatschappijgegevens, klantgegevens, model);

                _agent.VoegVoertuigMetKlantToe(voertuig);

                return RedirectToAction("InsertOnderhoudsopdracht");
            }

            return View(model);
        }

        /// <summary>
        /// Show the form to insert onderhoudsopdracht
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertOnderhoudsopdracht()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a onderhoudsopdracht and send it to all of the data to the agent
        /// Deserialize the previous cookies
        /// </summary>
        /// <param name="model">Inserted onderhoudsopdracht viewmodel</param>
        /// <returns>RedirectToAction to Index</returns>
        [HttpPost]
        public ActionResult InsertOnderhoudsopdracht(InsertOnderhoudsopdrachtVM model)
        {
            if (ModelState.IsValid)
            {
                HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");
                var leasemaatschappijgegevens = new JavaScriptSerializer().Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);

                HttpCookie klantgegevensCookie = Request.Cookies.Get("Klantgegevens");
                var klantgegevens = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(klantgegevensCookie.Value);

                HttpCookie voertuiggegevensCookie = Request.Cookies.Get("Voertuiggegevens");
                var voertuiggegevens = new JavaScriptSerializer().Deserialize<InsertVoertuiggegevensVM>(voertuiggegevensCookie.Value);
                var onderhoudsopdracht = Mapper.MapToOnderhoudsopdracht(model, leasemaatschappijgegevens, klantgegevens, voertuiggegevens);
                //var o = Dumm
                _agent.AddOnderhoudsOpdrachtWithKlantAndVoertuig(onderhoudsopdracht);

                return RedirectToAction("Index");
            }

            return View(model);
        }


        internal static InsertKlantgegevensVM GetKlantGegevens(bool lease)
        {
            return new InsertKlantgegevensVM
            {
                Voornaam = "Kees",
                Achternaam = "Caespi",
                Adres = "Akkerstraat 12",
                Postcode = "4322 AS",
                Woonplaats = "Utrecht",
                Lease = lease,
                Telefoonnummer = "0612345678",
                Emailadres = "info@caespi.nl",
                Tussenvoegsel = "g",
            };
        }

        internal static InsertLeasemaatschappijGegevensVM GetLeasemaatschappijGegevens()
        {
            return new InsertLeasemaatschappijGegevensVM
            {
                Naam = "Sixt",
                Telefoonnummer = "0687654321",
                
            };
        }

        internal static InsertVoertuiggegevensVM GetVoertuiggegevens()
        {
            return new InsertVoertuiggegevensVM
            {
                Kenteken = "KS-23-GF",
                Merk = "Volkswagen",
                Type = "Polo",
            };
        }

        internal static InsertOnderhoudsopdrachtVM GetOnderhoudsopdracht()
        {
            return new InsertOnderhoudsopdrachtVM
            {
                AanmeldingsDatum = new DateTime(2015, 11, 23),
                APK = true,
                Kilometerstand = 12345,
                Onderhoudsomschrijving = "APK + Koppeling vervangen",
            };
        }

    }
}
