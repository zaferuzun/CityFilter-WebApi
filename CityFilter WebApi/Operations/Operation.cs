using CityFilter_WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Operations
{
    public class Operation
    {
        public static AddressInfo XmlToObject(string xml)
        {
            AddressInfo result = null;
            XmlSerializer serializer = new XmlSerializer(typeof(AddressInfo));
            using (TextReader reader = new StringReader(xml))
            {
                result = (AddressInfo)serializer.Deserialize(reader);
            }
            return result;
        }
        public static string ObjectToXml(AddressInfo obj)
        {
            XmlSerializer XML = new XmlSerializer(typeof(AddressInfo));
            using (var StringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(StringWriter))
                {
                    XML.Serialize(writer, obj);
                    return StringWriter.ToString();
                }
            }
        }
        public static List<string> StringtoList(string Gdata)
        {
            List<string> Pdata = Gdata.Split(',').ToList();

            return Pdata;
        }
        public static AddressInfo getObject(string XmlorCvs)
        {
            AddressInfo result = null;
            //if koy cvs bak
            result = XmlToObject(XmlorCvs);
            return result;
        }
        public static List<City> FilterName (AddressInfo Gobject,List<string> filter)
        {

            City filterCity = new City();
            List<City> filterList = new List<City>();

            foreach (var item in filter)
            {
                foreach(var item2 in Gobject.City)
                {
                    if(item ==item2.Name)
                    {
                        filterCity = item2;
                        filterList.Add(filterCity);
                    }
                }
            }
            return filterList;
        }
        public static List<City> DistrictName(AddressInfo Gobject, List<string> filter)
        {

            City filterCity = new City();
            List<City> cityList = Gobject.City;
            List<City> filtertList = new List<City>();
            List<District> dc = new List<District>();
            foreach (var item in filter)
            {
                dc.Clear();
                foreach (var item2 in cityList)
                {
                    foreach (var item3 in item2.District)
                    {
                        if (item.Equals(item3.Name))
                        {
                            dc.Add(item3);
                            filterCity.Code = item2.Code;
                            filterCity.Name = item2.Name;
                        }
                        // filteredList = myList.Where(x => x > 7).ToList();
                    }

                }
                if (dc?.Any() ?? false)
                {
                    filterCity.District = dc;
                    filtertList.Add(filterCity);
                }

            }

            //filtertList = cityList.Where(x => x.District[0].Name.Equals("Alaçatı")).ToList();
            return filtertList;
        }


    }


}