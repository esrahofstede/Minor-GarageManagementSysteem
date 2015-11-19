using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation.RDWIntegration
{
    /// <summary>
    /// Interface to communicate to the RDW API
    /// </summary>
    public interface IRDWService
    {
        /// <summary>
        /// Send a request to the RDW API
        /// </summary>
        /// <param name="message">Requestmessage in format of RDW</param>
        /// <returns>Responsemessage</returns>
        string SubmitAPKVerzoek(string message);
    }
}
