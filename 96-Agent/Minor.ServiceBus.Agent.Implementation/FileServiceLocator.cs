using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using minor.servicebus.pfslocatorservice.schema;
using System.Web.Hosting;
using System.Web;

namespace Minor.ServiceBus.Agent.Implementation
{
    public class FileServiceLocator : IServiceLocator
    {
        private string _filePath;

        public FileServiceLocator(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                _filePath = filePath;
            } else
            {
                throw new FilePathNotDefinedException("File path is not defined.");
            }
        }

        public string FindMetadataEndpointAddress(string name, string profile)
        {
            return GetMetaDataEndPointAdress(name, profile);
        }

        public string FindMetadataEndpointAddress(string name, string profile, decimal version)
        {
            return GetMetaDataEndPointAdress(name, profile, version);
        }

        public string GetMetaDataEndPointAdress(string name, string profile, decimal? version = null)
        {
            var data = LoadXMLFile<locationData>();
            foreach (var servicelocation in data.serviceLocation)
            {
                if(version != null)
                {
                    if (servicelocation.name == name 
                        && servicelocation.profile == profile
                        && servicelocation.version == version)
                    {
                        return servicelocation.metadataAddress;
                    }
                }
                if (servicelocation.name == name && servicelocation.profile == profile)
                {
                    return servicelocation.metadataAddress;
                }
            }
            throw new ServiceLocationDoesntExistsException("The given service location doesn't exists inside of the given xml file.");
        }

        public T LoadXMLFile<T>() {

            string relativePath = _filePath;
            if (_filePath.StartsWith("~"))
            {
                relativePath = HostingEnvironment.MapPath(_filePath);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(locationData));
            using (StreamReader reader = new StreamReader(relativePath))
            {
                var data = serializer.Deserialize(reader);
                return (T)data;
            }
        }

    }
}
