using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minor.Case2.ISRDW.Implementation
{
    public class Util
    {
        public static string SerializeToXML<T>(T toSerialize)
        {
            if (toSerialize == null)
            {
                throw new ArgumentNullException("The object that needs te be serialized, can not be null");
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriterUtf8 textWriter = new StringWriterUtf8())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T DeserializeFromXML<T>(string xmlText)
        {
            if (string.IsNullOrWhiteSpace(xmlText))
            {
                throw new ArgumentNullException("The string that needs te be deserialized, can not be null or whitespace");
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                return (T) xmlSerializer.Deserialize(stringReader);
            }
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    class StringWriterUtf8 : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
