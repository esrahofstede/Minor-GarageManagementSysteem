using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation
{
    public class RDWConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("rdw")]
        public RdwElement RdwElement
        {
            get { return (RdwElement)this["rdw"]; }
            set { this["rdw"] = value; }
        }
    }



    public class RdwElement : ConfigurationElement
    {
        [ConfigurationProperty("address")]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
    }

}

