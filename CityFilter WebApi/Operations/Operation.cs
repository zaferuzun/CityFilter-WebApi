using CityFilter_WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Operations
{
    public class Operation
    {
        public AddressInfo XmlToObject(string xml)
        {
            AddressInfo result = null;
            XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            using (TextReader reader = new StringReader(xml))
            {
                result = (AddressInfo)serializer.Deserialize(reader);
            }
            return result;
        }
    }

}