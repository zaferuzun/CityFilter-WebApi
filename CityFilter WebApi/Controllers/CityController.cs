using CityFilter_WebApi.Models;
using CityFilter_WebApi.Operations;
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
            string sorting = "";
            string name = "";
            string district = "";
            string code = "";
            List<string> st2;
            var vv="";
            AddressInfo Gdata = new AddressInfo();
            AddressInfo Pdata = new AddressInfo();
            List<City> nameFilter = new List<City>();
            List<City> districtFilter = new List<City>();
            List<City> codeFilter = new List<City>();


            if (value.data!=null)
            {
                Gdata = Operation.XmlOrCsv("CSV",value.data);
                Operation.objToDataTable(Gdata);
                if (value.nameFilter != null)
                {
                    st2 = Operation.StringtoList(value.nameFilter);
                    nameFilter = Operation.FilterName(Gdata,st2);

                }
                if (value.districtFilter != null)
                {
                    st2 = Operation.StringtoList(value.districtFilter);
                    districtFilter = Operation.FilterDistrict(Gdata, st2);

                }
                if (value.codefilter != null) 
                {
                    st2 = Operation.StringtoList(value.codefilter);
                    codeFilter = Operation.FilterCode(Gdata, st2);
                }
                Gdata = Operation.Filter(nameFilter, districtFilter, codeFilter);
                if (value.sorting != null)
                {
                    sorting = value.sorting;
                    Pdata=Operation.Sorting(Gdata,value.sorting,value.sortingParam);
                }
            }
            else
            {

            }
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(value.data[0]);
            //string jsonText = JsonConvert.SerializeXmlNode(doc);

            //var listOb = JsonConvert.DeserializeObject<List<AddressInfo>>(jsonText);

            //AddressInfo account = JsonConvert.DeserializeObject<AddressInfo>(jsonText);
            // XmlDocument doc2 = JsonConvert.DeserializeXmlNode(jsonText);
            //AddressInfo result = null;
            //XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            //using (TextReader reader = new StringReader(value.data[0]))
            //{
            //    result = (AddressInfo)serializer.Deserialize(reader);
            //}
            AddressInfo st = Operation.XmlToObject(data);
            string  str= Operation.ObjectToXml(st);


            return Ok(str);
        }
    }
}
