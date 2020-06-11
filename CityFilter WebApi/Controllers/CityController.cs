using CityFilter_WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Controllers
{
    public class CityController : ApiController
    {
        [HttpPost]
        //[FromBody] ReceivedData value
        public IHttpActionResult getCity([FromBody] ReceivedData value)
        {
            string data = "";
            string sorting;
            string name;
            string district;
            string code;
            if (value.data[0] != null)
            {
                data = value.data[0];
                if (value.sorting != null)
                {
                    sorting = value.sorting;
                }
                if (value.nameFilter[0] != null)
                {
                    name = value.nameFilter[0];
                }
                if (value.districtFilter[0] != null)
                {
                    district = value.districtFilter[0];
                }
                if (value.codefilter[0] != null)
                {
                    code = value.codefilter[0];
                }
            }
            else
            {

            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(value.data[0]);
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            //var listOb = JsonConvert.DeserializeObject<List<AddressInfo>>(jsonText);

            AddressInfo account = JsonConvert.DeserializeObject<AddressInfo>(jsonText);
            // XmlDocument doc2 = JsonConvert.DeserializeXmlNode(jsonText);
            //AddressInfo result = null;
            //XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            //using (TextReader reader = new StringReader(value.data[0]))
            //{
            //    result = (AddressInfo)serializer.Deserialize(reader);
            //}

            return Ok(jsonText);
        }
    }
}
