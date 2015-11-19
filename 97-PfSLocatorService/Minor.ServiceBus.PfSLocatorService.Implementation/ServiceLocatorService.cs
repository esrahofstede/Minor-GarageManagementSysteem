using Minor.ServiceBus.PfSLocatorService.Contract;
using Minor.ServiceBus.PfSLocatorService.Contract.DTO;
using Minor.ServiceBus.PfSLocatorService.DAL;
using Minor.ServiceBus.PfSLocatorService.DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Minor.ServiceBus.PfSLocatorService.Implementation
{
    public class ServiceLocatorService : Contract.IServiceLocatorService
    {
        private DAL.IServiceLocationDataMapper _datamapper;

        public ServiceLocatorService()
        {
            _datamapper = new ServiceLocationXMLDataMapper("../../../Minor.ServiceBus.PfSLocatorService.DAL/XML/locationData.xml");
        }

        public ServiceLocatorService(DAL.IServiceLocationDataMapper datamapper)
        {
            _datamapper = datamapper;
        }

        public string FindMetadataEndpointAddress(Contract.DTO.ServiceLocation serviceLocation)
        {
            FunctionalErrorList errorList = new FunctionalErrorList();
            if (serviceLocation.Name == null || serviceLocation.Profile == null || 
                serviceLocation.Name == "" || serviceLocation.Profile == "")
            {
                errorList.Add(new FunctionalErrorDetail
                {
                    Message = "Name or Profile is null"
                });
                throw new FunctionalException() { Errors = errorList };
            }
            
            try
            {
                if (serviceLocation.Version == null)
                {
                    return _datamapper.FindMetadataEndpointAddress(serviceLocation.Name, serviceLocation.Profile);
                }
                return _datamapper.FindMetadataEndpointAddress(serviceLocation.Name, serviceLocation.Profile, serviceLocation.Version);
            }
            catch (MultipleRecordsFoundException ex)
            {
                errorList.Add(new FunctionalErrorDetail
                {
                    Message = ex.Message,
                    Data = ex.Data
                });
            }
            catch (NoRecordsFoundException ex)
            {
                errorList.Add(new FunctionalErrorDetail
                {
                    Message = ex.Message,
                    Data = ex.Data
                });
            }
            catch (VersionedRecordFoundException ex)
            {
                errorList.Add(new FunctionalErrorDetail
                {
                    Message = ex.Message,
                    Data = ex.Data
                });
            }

            if (errorList.HasErrors)
            {
                throw new FunctionalException() { Errors = errorList };
            }

            return null;
        }
    }
}
