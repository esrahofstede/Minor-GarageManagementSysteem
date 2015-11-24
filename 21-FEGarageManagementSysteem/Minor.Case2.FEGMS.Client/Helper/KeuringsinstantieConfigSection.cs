using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.FEGMS.Client.Helper
{
    public class KeuringsinstantieConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("type")]
        public string TypeInstantie
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("kvk")]
        public string KVK
        {
            get { return (string)this["kvk"]; }
            set { this["kvk"] = value; }
        }

        [ConfigurationProperty("naam")]
        public string Naam
        {
            get { return (string)this["naam"]; }
            set { this["naam"] = value; }
        }

        [ConfigurationProperty("plaats")]
        public string Plaats
        {
            get { return (string)this["plaats"]; }
            set { this["plaats"] = value; }
        }

    }

}
