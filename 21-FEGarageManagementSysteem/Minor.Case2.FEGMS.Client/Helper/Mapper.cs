using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Script.Serialization;

namespace Minor.Case2.FEGMS.Client.Helper
{
    public static class Mapper
    {
        public static Onderhoudsopdracht MapToOnderhoudsopdracht(InsertOnderhoudsopdrachtVM onderhoudsopdracht, InsertLeasemaatschappijGegevensVM leasemaatschappijgegevens, InsertKlantgegevensVM klantgegevens, InsertVoertuiggegevensVM voertuiggegevens)
        {
            var bestuurder = new Persoon
            {
                Voornaam = klantgegevens.Voornaam,
                Tussenvoegsel = klantgegevens.Tussenvoegsel,
                Achternaam = klantgegevens.Achternaam,
                Adres = klantgegevens.Adres,
                Postcode = klantgegevens.Postcode,
                Woonplaats = klantgegevens.Woonplaats,
                Emailadres = klantgegevens.Emailadres,
                Telefoonnummer = klantgegevens.Telefoonnummer,
            };

            Klant eigenaar = null;

            if (klantgegevens.Lease)
            {
                eigenaar = new Leasemaatschappij
                {
                    Naam = leasemaatschappijgegevens.Naam,
                    Telefoonnummer = leasemaatschappijgegevens.Telefoonnummer,
                };
            }
            else
            {
                eigenaar = bestuurder;
            }

            return new Onderhoudsopdracht
            {
                Aanmeldingsdatum = onderhoudsopdracht.AanmeldingsDatum,
                APK = onderhoudsopdracht.APK,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                Onderhoudsomschrijving = onderhoudsopdracht.Onderhoudsomschrijving,
                Voertuig = new Voertuig
                {
                    Bestuurder = bestuurder,
                    Eigenaar = eigenaar,
                    Kenteken = voertuiggegevens.Kenteken,
                    Merk = voertuiggegevens.Merk,
                    Type = voertuiggegevens.Type,
                },
            };
        }

        public static Voertuig MapToVoertuig(InsertLeasemaatschappijGegevensVM leasemaatschappijgegevens, InsertKlantgegevensVM klantgegevens, InsertVoertuiggegevensVM voertuiggegevens)
        {
            if(klantgegevens == null)
            {
                throw new ArgumentNullException(nameof(klantgegevens), "Value cannot be null");
            }

            if (voertuiggegevens == null)
            {
                throw new ArgumentNullException(nameof(voertuiggegevens), "Value cannot be null");
            }

            var bestuurder = new Persoon
            {
                Voornaam = klantgegevens.Voornaam,
                Tussenvoegsel = klantgegevens.Tussenvoegsel,
                Achternaam = klantgegevens.Achternaam,
                Adres = klantgegevens.Adres,
                Postcode = klantgegevens.Postcode,
                Woonplaats = klantgegevens.Woonplaats,
                Emailadres = klantgegevens.Emailadres,
                Telefoonnummer = klantgegevens.Telefoonnummer,
            };

            Klant eigenaar = null;

            if (klantgegevens.Lease)
            {
                if (leasemaatschappijgegevens == null)
                {
                    throw new ArgumentNullException(nameof(leasemaatschappijgegevens), "Value cannot be null");
                }

                eigenaar = new Leasemaatschappij
                {
                    Naam = leasemaatschappijgegevens.Naam,
                    Telefoonnummer = leasemaatschappijgegevens.Telefoonnummer,
                };
            }
            else
            {
                eigenaar = bestuurder;
            }

            return new Voertuig
            {
                Bestuurder = bestuurder,
                Eigenaar = eigenaar,
                Kenteken = voertuiggegevens.Kenteken,
                Merk = voertuiggegevens.Merk,
                Type = voertuiggegevens.Type,
            };
        }
    }
}