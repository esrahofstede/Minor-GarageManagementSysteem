using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.ServiceBus.Agent.Implementation
{
    public class ServiceLocatorConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("active")]
        public string Active {
            get { return (string)this["active"];  }
            set { this["active"] = value; }
        }

        [ConfigurationProperty("profile")]
        public string Profile
        {
            get { return (string)this["profile"]; }
            set { this["profile"] = value; }
        }

        [ConfigurationProperty("fileServiceLocator")]
        public FileServiceLocatorElement FileServiceLocator
        {
            get { return (FileServiceLocatorElement)this["fileServiceLocator"]; }
            set { this["fileServiceLocator"] = value; }
        }

        [ConfigurationProperty("webServiceLocator")]
        public WebServiceLocatorElement WebServiceLocator
        {
            get { return (WebServiceLocatorElement)this["webServiceLocator"]; }
            set { this["webServiceLocator"] = value; }
        } 
    }

    public class FileServiceLocatorElement : ConfigurationElement
    {
        [ConfigurationProperty("filePath")]
        public string FilePath
        {
            get { return (string)this["filePath"]; }
            set { this["filePath"] = value; }
        }
    }

    public class WebServiceLocatorElement : ConfigurationElement
    {
        [ConfigurationProperty("address")]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("binding")]
        public string Binding
        {
            get { return (string)this["binding"]; }
            set { this["binding"] = value; }
        }
    }
}
