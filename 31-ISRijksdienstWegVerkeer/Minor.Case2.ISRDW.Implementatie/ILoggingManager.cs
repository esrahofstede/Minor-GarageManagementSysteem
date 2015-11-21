using System;
using System.Collections.Generic;
using Minor.Case2.ISRDW.DAL.Entities;

namespace Minor.Case2.ISRDW.Implementation
{
    public interface ILoggingManager
    {
        IEnumerable<Logging> FindAll();
        void Log(apkKeuringsverzoekResponseMessage responseMessage, DateTime dateTime);
        void Log(apkKeuringsverzoekRequestMessage requestMessage, DateTime dateTime);
    }
}