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
            List<string> st2;
            AddressInfo Gdata = new AddressInfo();
            List<City> nameFilter = new List<City>();
            List<City> districtFilter = new List<City>();
            List<City> codeFilter = new List<City>();

            try
            {
                if (value.data != null && value.formatType != null)
                {
                    Gdata = Operation.XmlOrCsv(value.formatType, value.data);

                    if (value.nameFilter != null)
                    {
                        st2 = Operations.Deserialize.StringtoList(value.nameFilter);
                        nameFilter = Operation.FilterName(Gdata, st2);

                    }
                    if (value.districtFilter != null)
                    {
                        st2 = Operations.Deserialize.StringtoList(value.districtFilter);
                        districtFilter = Operation.FilterDistrict(Gdata, st2);

                    }
                    if (value.codefilter != null)
                    {
                        st2 = Operations.Deserialize.StringtoList(value.codefilter);
                        codeFilter = Operation.FilterCode(Gdata, st2);
                    }
                    Gdata = Operation.Filter(nameFilter, districtFilter, codeFilter);
                    if (value.sorting != null)
                    {
                        Gdata = Operation.Sorting(Gdata, value.sorting, value.sortingParam);
                    }
                }
                else
                {
                    var response = new HttpResponseMessage()
                    {
                        Content = new StringContent("Veri veya veri tipi yanlış gönderildi.", System.Text.Encoding.UTF8, "text/plain"),
                    };
                    return ResponseMessage(response);
                }
            }
            catch (Exception)
            {
                var response = new HttpResponseMessage()
                {
                    Content = new StringContent("Parametreleri yanlış gönderdiniz.", System.Text.Encoding.UTF8, "text/plain"),
                };
                return ResponseMessage(response);
                throw;
            }


            string P_data = Operations.Operation.XmlOrCsvPostData(Gdata,value.formatType);

            return Ok(P_data);
        }
    }
}
