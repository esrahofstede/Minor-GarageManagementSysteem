using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.ServiceBus.PfSLocatorService.DAL.Exceptions
{
    public class MultipleRecordsFoundException: Exception
    {
        public MultipleRecordsFoundException(){}
        public MultipleRecordsFoundException(string message): base(message){}

        public MultipleRecordsFoundException(string message, Exception inner): base(message, inner){}
    }
}
