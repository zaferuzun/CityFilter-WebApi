using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityFilter_WebApi.Models
{
    public class ReceivedData
    {
        public string nameFilter { get; set; }
        public string districtFilter { get; set; }
        public string codefilter { get; set; }
        public string sortingParam { get; set; }  
        public string sorting { get; set; }
        public string data { get; set; }
        public string formatType { get; set; }
    }
}