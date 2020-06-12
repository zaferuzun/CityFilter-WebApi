using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityFilter_WebApi.Models
{
    public class ReceivedData
    {
        public List<string> nameFilter { get; set; }
        public List<string> districtFilter { get; set; }
        public List<string> codefilter { get; set; }

        public string sorting { get; set; }
        public List<string> data { get; set; }
    }
}