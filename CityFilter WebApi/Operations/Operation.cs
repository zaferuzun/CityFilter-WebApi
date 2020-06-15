using CityFilter_WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Operations
{
    public class Operation
    {


        //public static AddressInfo getObject(string XmlorCvs)
        //{
        //    AddressInfo result = null;
        //    //if koy cvs bak
        //    result = XmlToObject(XmlorCvs);
        //    return result;
        //}

        /// <summary>
        /// Kullanıcıdan alınan şehir adlarına göre filtreler
        /// </summary>
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
        /// <summary>
        /// Kullanıcıdan alınan ilçe adlarına göre filtreler
        /// </summary>
        public static List<City> FilterDistrict(AddressInfo Gobject, List<string> filter)
        {
            List<City> cityList = Gobject.City;
            List<City> filtertList = new List<City>();
            List<District> dc2 = new List<District>();
            foreach (var item in cityList)
            {
                List<District> dc = new List<District>();
                City filterCity = new City();
                foreach (var item2 in item.District)
                {
                    foreach (var item3 in filter)
                    {
                        if (item3.Equals(item2.Name))
                        {
                            dc.Add(item2);
                            filterCity.Code = item.Code;
                            filterCity.Name = item.Name;
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
        /// <summary>
        /// Kullanıcıdan alınan kod alanına göre filtreler
        /// </summary>
        public static List<City> FilterCode(AddressInfo Gobject, List<string> filter)
        {
            City filterCity = new City();
            List<City> filterList = new List<City>();

            foreach (var item in filter)
            {
                foreach (var item2 in Gobject.City)
                {
                    if (item == item2.Code)
                    {
                        filterCity = item2;
                        filterList.Add(item2);
                    }
                }
            }
            return filterList;
        }
        /// <summary>
        /// Gelen veriyi kontrol edip uygun fonksiyonlara yönlendirir.
        /// </summary>
        public static AddressInfo XmlOrCsv(string xmlorcvs,string data)
        {
            AddressInfo result = null;

            if (xmlorcvs=="XML")
            {
                return Operations.Serialize.XmlToObject(data);
            }
            else if(xmlorcvs == "CSV")
            {
                DataTable dt = Operations.Serialize.ConvertCSVtoDataTable(data);
                result = Operations.Serialize.DataTabletoObject(dt);
                return result;
            }

            return result;
        }
        /// <summary>
        /// Boş bir şehir oluşturmak için kullanıldı
        /// </summary>
        public static City getCity()
        {
            List<Zip> zipList = new List<Zip>()
            {
                new Zip{Code="1"}
            };
            District district2 = new District()
            {
                Zip = zipList.ToList(),
                Name = ""
            };
            List<District> districtList = new List<District>();
            districtList.Add(district2);
            City city = new City()
            {
                District = districtList,
                Name = "",
                Code = ""
            };
            return city;
        }
        /// <summary>
        /// Şehir,ilçelere göre filtrelenen listeleri birleştirip tek listeye dönüştürür.
        /// </summary>
        public static AddressInfo Filter(List<City> nameFilter, List<City> districtFilter, List<City> codeFilter)
        {
            AddressInfo result = new AddressInfo();
            if(nameFilter.Count!=0 && codeFilter.Count!=0)
            {
                result.City = nameFilter;
                var list = codeFilter.Except(nameFilter).ToList();
                foreach (var item in list.ToList())
                {
                    result.City.Add(item);
                }
            }
            else
            {
                if (nameFilter.Count!= 0)
                {
                    result.City = nameFilter;
                }
                if (codeFilter.Count != 0)
                {
                    result.City = codeFilter;
                }
            }
            AddressInfo result2 = new AddressInfo();
            result2 = result;
            if (districtFilter.Count != 0 && result.City.Count != 0)
            {

                int i = 0;
                foreach (var item in result2.City.ToList())
                {
                    foreach (var item2 in districtFilter.ToList())
                    {
                        if (item2.Name==item.Name)
                        {
                            result.City[i].District = item2.District;
                            districtFilter.Remove(item2);
                        }
                    }
                    i++;
                }
                foreach (var item in result2.City.ToList())
                {
                    foreach (var item2 in districtFilter.ToList())
                    {
                        if (item2.Name != item.Name)
                        {
                            result.City.Add(item2);
                        }
                    }
                    i++;
                }
            }
            else
            {
                if(districtFilter.Count != 0)
                {
                    result.City = districtFilter;
                }
            }
            return result;
        }
        /// <summary>
        /// Gönderilen parametreler ile veriyi sıralar
        /// </summary>
        public static AddressInfo Sorting(AddressInfo G_obj,string sorting,string sortingParam)
        {
            AddressInfo result = new AddressInfo();
            result = G_obj;
            if (sortingParam == "CITY")
            {
                result = Operations.Sorting.CityName(result, sorting);
            }
            else if (sortingParam == "CODE")
            {
                result = Operations.Sorting.CityCode(result, sorting);
            }
            else if (sortingParam == "DISTRICT")
            {
                result = Operations.Sorting.DistrictName(result, sorting);
            }
            else if (sortingParam == "ZIPCODE")
            {
                result = Operations.Sorting.ZipCode(result, sorting);
            }
            else if (sortingParam == "ALL")
            {
                result = Operations.Sorting.CityName(result, sorting);
                result = Operations.Sorting.CityCode(result, sorting);
                result = Operations.Sorting.DistrictName(result, sorting);
                result = Operations.Sorting.ZipCode(result, sorting);
            }
            return result;
        }
        /// <summary>
        /// Gönderilecek veriyi gelen formata dönüştürür
        /// </summary>
        public static string XmlOrCsvPostData(AddressInfo result, string xmlorcvs)
        {
            string P_data = "";
            if (xmlorcvs == "XML")
            {
                P_data = Operations.Deserialize.ObjectToXml(result);
            }
            else if (xmlorcvs == "CSV")
            {
                DataTable dt = Operations.Deserialize.objToDataTable(result);
                P_data = Operations.Deserialize.DataTabletoCSV(dt);
            }
            return P_data;
        }

    }


}