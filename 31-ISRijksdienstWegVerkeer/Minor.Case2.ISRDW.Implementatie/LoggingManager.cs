using System;
using System.Collections.Generic;
using Minor.Case2.ISRDW.DAL.Entities;
using Minor.Case2.ISRDW.DAL;

namespace Minor.Case2.ISRDW.Implementation
{
    /// <summary>
    /// Manager for logging
    /// </summary>
    public class LoggingManager
    {
        IDataMapper<Logging, long> _loggingMapper;

        public LoggingManager()
        {
            _loggingMapper = new LoggingDataMapper();
        }

        /// <summary>
        /// Creates an instance of the LoggingManager and can be injected with an IDataMapper<Logging, long>
        /// </summary>
        /// <param name="loggingMapper">Injectable IDataMapper<Logging, long></param>
        public LoggingManager(IDataMapper<Logging, long> loggingMapper)
        {
            _loggingMapper = loggingMapper;
        }

        /// <summary>
        /// Logs an apkKeuringsverzoekRequestMessage
        /// </summary>
        /// <param name="requestMessage">an apkKeuringsverzoekRequestMessage to log</param>
        /// <param name="dateTime">Time of the logging</param>
        public void Log(apkKeuringsverzoekRequestMessage requestMessage, DateTime dateTime)
        {
            if(requestMessage == null)
            {
                throw new ArgumentNullException(nameof(requestMessage), "The request message that needs to be mapped, cannot be null");
            }

            _loggingMapper.Insert(Mapper.MapToLogging(requestMessage, dateTime));
        }

        /// <summary>
        /// Logs an apkKeuringsverzoekResponseMessage
        /// </summary>
        /// <param name="responseMessage">an apkKeuringsverzoekResponseMessage to log</param>
        /// <param name="dateTime">Time of the logging</param>
        public void Log(apkKeuringsverzoekResponseMessage responseMessage, DateTime dateTime)
        {
            if (responseMessage == null)
            {
                throw new ArgumentNullException(nameof(responseMessage), "The response message that needs to be mapped, cannot be null");
            }

            _loggingMapper.Insert(Mapper.MapToLogging(responseMessage, dateTime));
        }

        /// <summary>
        /// Gets all loggings objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Logging> FindAll()
        {
            return _loggingMapper.FindAll();
        }
    }
}
