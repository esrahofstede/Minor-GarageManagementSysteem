﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("urn:minor-case2-bsvoertuigenklantbeheer:v1:schema", ClrNamespace="Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema")]

namespace Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Klant", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Persoon))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Leasemaatschappij))]
    public partial class Klant : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long idField;
        
        private long klantnummerField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long klantnummer
        {
            get
            {
                return this.klantnummerField;
            }
            set
            {
                this.klantnummerField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Persoon", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class Persoon : Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant
    {
        
        private string VoornaamField;
        
        private string TussenvoegselField;
        
        private string AchternaamField;
        
        private string AdresField;
        
        private string PostcodeField;
        
        private string WoonplaatsField;
        
        private string TelefoonnummerField;
        
        private string EmailadresField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Voornaam
        {
            get
            {
                return this.VoornaamField;
            }
            set
            {
                this.VoornaamField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public string Tussenvoegsel
        {
            get
            {
                return this.TussenvoegselField;
            }
            set
            {
                this.TussenvoegselField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=2)]
        public string Achternaam
        {
            get
            {
                return this.AchternaamField;
            }
            set
            {
                this.AchternaamField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public string Adres
        {
            get
            {
                return this.AdresField;
            }
            set
            {
                this.AdresField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public string Postcode
        {
            get
            {
                return this.PostcodeField;
            }
            set
            {
                this.PostcodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public string Woonplaats
        {
            get
            {
                return this.WoonplaatsField;
            }
            set
            {
                this.WoonplaatsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public string Telefoonnummer
        {
            get
            {
                return this.TelefoonnummerField;
            }
            set
            {
                this.TelefoonnummerField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=7)]
        public string Emailadres
        {
            get
            {
                return this.EmailadresField;
            }
            set
            {
                this.EmailadresField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Leasemaatschappij", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class Leasemaatschappij : Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant
    {
        
        private string NaamField;
        
        private string AdresField;
        
        private string PostcodeField;
        
        private string WoonplaatsField;
        
        private string TelefoonnummerField;
        
        private string EmailadresField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Naam
        {
            get
            {
                return this.NaamField;
            }
            set
            {
                this.NaamField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=1)]
        public string Adres
        {
            get
            {
                return this.AdresField;
            }
            set
            {
                this.AdresField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=2)]
        public string Postcode
        {
            get
            {
                return this.PostcodeField;
            }
            set
            {
                this.PostcodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=3)]
        public string Woonplaats
        {
            get
            {
                return this.WoonplaatsField;
            }
            set
            {
                this.WoonplaatsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=4)]
        public string Telefoonnummer
        {
            get
            {
                return this.TelefoonnummerField;
            }
            set
            {
                this.TelefoonnummerField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=5)]
        public string Emailadres
        {
            get
            {
                return this.EmailadresField;
            }
            set
            {
                this.EmailadresField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="KlantenCollection", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema", ItemName="Klant")]
    public class KlantenCollection : System.Collections.Generic.List<Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant>
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="VoertuigCollection", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema", ItemName="Voertuig")]
    public class VoertuigCollection : System.Collections.Generic.List<Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig>
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Voertuig", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class Voertuig : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        private string KentekenField;
        
        private string MerkField;
        
        private string TypeField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant EigenaarField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Persoon BestuurderField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Kenteken
        {
            get
            {
                return this.KentekenField;
            }
            set
            {
                this.KentekenField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Merk
        {
            get
            {
                return this.MerkField;
            }
            set
            {
                this.MerkField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Type
        {
            get
            {
                return this.TypeField;
            }
            set
            {
                this.TypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=4)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant Eigenaar
        {
            get
            {
                return this.EigenaarField;
            }
            set
            {
                this.EigenaarField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=5)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Persoon Bestuurder
        {
            get
            {
                return this.BestuurderField;
            }
            set
            {
                this.BestuurderField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VoertuigenSearchCriteria", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class VoertuigenSearchCriteria : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        private string KentekenField;
        
        private string MerkField;
        
        private string TypeField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant EigenaarField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Persoon BestuurderField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Kenteken
        {
            get
            {
                return this.KentekenField;
            }
            set
            {
                this.KentekenField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Merk
        {
            get
            {
                return this.MerkField;
            }
            set
            {
                this.MerkField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Type
        {
            get
            {
                return this.TypeField;
            }
            set
            {
                this.TypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=4)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Klant Eigenaar
        {
            get
            {
                return this.EigenaarField;
            }
            set
            {
                this.EigenaarField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=5)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Persoon Bestuurder
        {
            get
            {
                return this.BestuurderField;
            }
            set
            {
                this.BestuurderField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Onderhoudsopdracht", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class Onderhoudsopdracht : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        private System.DateTime AanmeldingsdatumField;
        
        private long KilometerstandField;
        
        private string OnderhoudsomschrijvingField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig VoertuigField;
        
        private bool ApkField;
        
        private Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.OnderhoudsWerkzaamheden OnderhoudswerkzaamhedenField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public System.DateTime Aanmeldingsdatum
        {
            get
            {
                return this.AanmeldingsdatumField;
            }
            set
            {
                this.AanmeldingsdatumField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long Kilometerstand
        {
            get
            {
                return this.KilometerstandField;
            }
            set
            {
                this.KilometerstandField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=3)]
        public string Onderhoudsomschrijving
        {
            get
            {
                return this.OnderhoudsomschrijvingField;
            }
            set
            {
                this.OnderhoudsomschrijvingField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=4)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig Voertuig
        {
            get
            {
                return this.VoertuigField;
            }
            set
            {
                this.VoertuigField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public bool Apk
        {
            get
            {
                return this.ApkField;
            }
            set
            {
                this.ApkField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.OnderhoudsWerkzaamheden Onderhoudswerkzaamheden
        {
            get
            {
                return this.OnderhoudswerkzaamhedenField;
            }
            set
            {
                this.OnderhoudswerkzaamhedenField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OnderhoudsWerkzaamheden", Namespace="urn:minor-case2-bsvoertuigenklantbeheer:v1:schema")]
    public partial class OnderhoudsWerkzaamheden : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        private System.DateTime AfmeldingsdatumField;
        
        private string KilometerstandField;
        
        private string OnderhoudswerkzaamhedenomschrijvingField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public System.DateTime Afmeldingsdatum
        {
            get
            {
                return this.AfmeldingsdatumField;
            }
            set
            {
                this.AfmeldingsdatumField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=2)]
        public string Kilometerstand
        {
            get
            {
                return this.KilometerstandField;
            }
            set
            {
                this.KilometerstandField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=3)]
        public string Onderhoudswerkzaamhedenomschrijving
        {
            get
            {
                return this.OnderhoudswerkzaamhedenomschrijvingField;
            }
            set
            {
                this.OnderhoudswerkzaamhedenomschrijvingField = value;
            }
        }
    }
}
