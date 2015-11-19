using Minor.ServiceBus.PfSLocatorService.DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web.Hosting;

namespace Minor.ServiceBus.PfSLocatorService.DAL
{
    public class ServiceLocationXMLDataMapper : IServiceLocationDataMapper
    {
        private string _filePath;

        public ServiceLocationXMLDataMapper(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                _filePath = filePath;
            }
            else
            {
                throw new FilePathNotDefinedException("File path is not defined.");
            }
        }

        public string FindMetadataEndpointAddress(string name, string profile)
        {
            return GetMetaDataEndPointAdress(name, profile);
            //return "http://localhost:30412/BSKlantbeheer/mex";
        }

        public string FindMetadataEndpointAddress(string name, string profile, decimal? version)
        {
            //return "http://localhost:30412/BSKlantbeheer/mex";
            return GetMetaDataEndPointAdress(name, profile, version);
        }

        public string GetMetaDataEndPointAdress(string name, string profile, decimal? version = null)
        {
            var data = LoadXMLFile<locationData>();
            foreach (var servicelocation in data.serviceLocation)
            {
                if (version != null)
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
            throw new NoRecordsFoundException("The given service location doesn't exists inside of the given xml file.");
        }

        public T LoadXMLFile<T>()
        {
            string relativePath = Path.Combine(Directory.GetCurrentDirectory(), _filePath);
            //relativePath = MakeAbsolutePath(_filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(locationData));
            using (StreamReader reader = new StreamReader(relativePath))
            {
                var data = serializer.Deserialize(reader);
                return (T)data;
            }
        }

        public string MakeAbsolutePath(string relativePath)
        {
            string path = null;

            if (HttpContext.Current != null)
            {
                path = HttpContext.Current.Server.MapPath(relativePath);
            }
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            }

            return path;
        }
    }
}
