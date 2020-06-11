using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Models
{
    //public partial class Temperatures
    //{
    //    public AddressInfo AddressInfo { get; set; }
    //}

    //public partial class AddressInfo
    //{
    //    public City City { get; set; }
    //}

    //public partial class City
    //{
    //    public string @name { get; set; }
    //    public string @code { get; set; }
    //    public List<District> District { get; set; }
    //}

    //public partial class District
    //{
    //    public string @name { get; set; }
    //    public ZipUnion @code { get; set; }

    //}

    //public partial class ZipElement
    //{
    //    public string @code { get; set; }
    //}
    //public partial struct ZipUnion
    //{
    //    public ZipElement ZipElement;
    //    public List<ZipElement> ZipElementArray;

    //    public static implicit operator ZipUnion(ZipElement ZipElement) => new ZipUnion { ZipElement = ZipElement };
    //    public static implicit operator ZipUnion(List<ZipElement> ZipElementArray) => new ZipUnion { ZipElementArray = ZipElementArray };
    //}
    //public class Zip
    //{
    //    public string code { get; set; }
    //}

    //public class District
    //{
    //    public List<Zip> Zip { get; set; }
    //    public string name { get; set; }
    //}

    //public class City
    //{
    //    public string code { get; set; }
    //    public List<District> District { get; set; }

    //}

    //public class AddressInfo
    //{
    //    public List<City> City { get; set; }
    //}

    [XmlRoot(ElementName = "Zip")]
    public class Zip
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "District")]
    public class District
    {
        [XmlElement(ElementName = "Zip")]
        public List<Zip> Zip { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "City")]
    public class City
    {
        [XmlElement(ElementName = "District")]
        public List<District> District { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "AddressInfo")]
    public class AddressInfo
    {
        [XmlElement(ElementName = "City")]
        public List<City> City { get; set; }
    }


}