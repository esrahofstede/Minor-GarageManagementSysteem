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
        /// <summary>
        /// Serialize object to a XML string
        /// </summary>
        /// <typeparam name="T">ObjectType to serialize</typeparam>
        /// <param name="toSerialize">Object to serialize</param>
        /// <returns>Serialized XML string</returns>
        public static string SerializeToXML<T>(T toSerialize)
        {
            if (toSerialize == null)
            {
                throw new ArgumentNullException(nameof(toSerialize), "The object that needs te be serialized, can not be null");
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriterUtf8 textWriter = new StringWriterUtf8())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Deserialize XML string to an object
        /// </summary>
        /// <typeparam name="T">ObjectType to deserialize</typeparam>
        /// <param name="xmlText">Object to deserialize</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeFromXML<T>(string xmlText)
        {
            if (string.IsNullOrWhiteSpace(xmlText))
            {
                throw new ArgumentNullException(nameof(xmlText),"The string that needs te be deserialized, can not be null or whitespace");
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                return (T) xmlSerializer.Deserialize(stringReader);
            }
        }
    }

    /// <summary>
    /// Stringwriter for utf-8
    /// </summary>
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
