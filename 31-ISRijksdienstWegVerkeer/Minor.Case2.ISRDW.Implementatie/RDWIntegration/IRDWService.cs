using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation.RDWIntegration
{
    public interface IRDWService
    {
        string SubmitAPKVerzoek(string message);
    }
}
