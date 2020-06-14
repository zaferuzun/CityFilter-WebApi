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
        public static AddressInfo XmlOrCsv(string xmlorcvs,string data)
        {
            AddressInfo result = null;

            if (xmlorcvs=="XML")
            {
                return Operation.getObject(data);
            }
            else if(xmlorcvs == "CSV")
            {
                DataTable dt = ConvertCSVtoDataTable(data);
                return DataTabletoObject(dt);
            }
            return result;
        }
        public static DataTable ConvertCSVtoDataTable(string csvStr)
        {
            DataTable dt = new DataTable();

            string[] tableData = csvStr.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var col = from cl in tableData[0].Split(",".ToCharArray())
                      select new DataColumn(cl);
            dt.Columns.AddRange(col.ToArray());

            (from st in tableData.Skip(1)
             select dt.Rows.Add(st.Split(",".ToCharArray()))).ToList();

            return dt;
        }
        public static string DataTabletoCSV (DataTable dtDataTable)
        {

            StringBuilder sb = new StringBuilder();
            string[] columnNames = dtDataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dtDataTable.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }
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
        public static AddressInfo DataTabletoObject(DataTable dt)
        {
            AddressInfo result = new AddressInfo();
            List<City> cityList = new List<City>();
            int i=0;
            int j=0;
            int k = 0;
            int kont = 0;
            int kont2 = 0;
            int kont3=0;
            cityList.Add(getCity());
            int rowCount =dt.Rows.Count;

            foreach (DataRow row in dt.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                if (cityList[i].Name != fields[0])
                {
                    if(kont!=0)
                    {
                        i += 1;
                        cityList.Add(getCity());
                    }
                    cityList[i].Name = fields[0];
                    cityList[i].Code = fields[1];
                    k = 0;
                    j = 0;
                    kont2 = 0;
                }
                if (cityList[i].District[j].Name != fields[2])
                {
                    if (kont2 != 0)
                    {
                        j +=1;
                        cityList[i].District.Add(new District());
                    }
                    cityList[i].District[j].Name = fields[2];

                    cityList[i].District[j].Zip = new List<Zip>();
                    cityList[i].District[j].Zip.Add(new Zip());
                    k = 0;
                    kont3 = 0;
                }
                if (kont3 != 0 )
                    cityList[i].District[j].Zip.Add(new Zip());

                cityList[i].District[j].Zip[k].Code= fields[3];

                k += 1;
                kont = +1;
                kont2 += 1;
                kont3 += 1;
                rowCount -= 1;
            }
            result.City=cityList;
            return result;
        }
        public static DataTable objToDataTable(AddressInfo G_obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CityName", typeof(string));
            dt.Columns.Add("CityCode", typeof(string));
            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("ZipCode", typeof(string));
            foreach (var item in G_obj.City)
            {
                foreach (var item2 in item.District)
                {
                    foreach (var item3 in item2.Zip)
                    {
                        dt.Rows.Add(item.Name,item.Code,item2.Name,item3.Code);
                    }
                }
            }
            return dt;
        }
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
        public static AddressInfo Sorting(AddressInfo G_obj,string sorting,string sortingParam)
        {
            AddressInfo result = new AddressInfo();

            //G_obj.City = G_obj.City.OrderByDescending(item => item.Name).ToList();
            //var azalanSiralama = G_obj.City.OrderBy(item => item).ToList();
            ////result.City.Sort();
            //result.City = artanSiralama;
            result = G_obj;
            if (sorting == "ASCENDING")
            {
                if (sortingParam == "CITY")
                {
                    result.City = G_obj.City.OrderByDescending(item => item.Name).ToList();
                }
                else if (sortingParam == "CODE")
                {
                    result.City = G_obj.City.OrderByDescending(item => item.Code).ToList();
                }
                else if (sortingParam == "DISTRICT")
                {
                    int i = 0;
                    foreach (var item in G_obj.City.ToList())
                    {
                        result.City[i].District = item.District.OrderByDescending(x => x.Name).ToList();
                        i++;
                    }
                }
                else if (sortingParam == "ZIPCODE")
                {
                    int i = 0;
                    int j = 0;
                    foreach (var item in G_obj.City.ToList())
                    {
                        foreach (var item2 in item.District.ToList())
                        {
                            result.City[i].District[j].Zip = item2.Zip.OrderByDescending(x => x.Code).ToList();
                            j++;
                        }
                        i++;
                        j = 0;
                    }
                }
                else
                {
                    //artanSiralama = G_obj.City.OrderByDescending(item => item.Name).ToList();
                }

            }
            else if (sorting == "DESCENDING")
            {

            }
            return result;
        }
    }


}