using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.ServiceBus.PfSLocatorService.DAL
{
    public interface IServiceLocationDataMapper
    {
        string FindMetadataEndpointAddress(string name, string profile);
        string FindMetadataEndpointAddress(string name, string profile, decimal? version);
    }
}
