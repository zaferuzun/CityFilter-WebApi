using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityFilter_WebApi.Models
{
    public class ReceivedData
    {
        public List<string> nameFilter { get; set; } = null;
        public List<string> districtFilter { get; set; } = null;
        public List<string> codefilter { get; set; } = null;

        public string sorting { get; set; } = null;
        public List<string> data { get; set; } = null;
    }
}