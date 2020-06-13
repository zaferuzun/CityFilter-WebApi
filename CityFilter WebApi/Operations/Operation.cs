using CityFilter_WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

            //City filterCity = new City();
            List<City> cityList = Gobject.City;
            List<City> filtertList = new List<City>();
            //List<District> dc = new List<District>();
            List<District> dc2 = new List<District>();

            //foreach (var item in filter)
            //{
            //    dc.Clear();
            //    City filterCity = new City();
            //    foreach (var item2 in cityList)
            //    {
            //        foreach (var item3 in item2.District)
            //        {
            //            if (item.Equals(item3.Name))
            //            {
            //                dc.Add(item3);
            //                filterCity.Code = item2.Code;
            //                filterCity.Name = item2.Name;
            //            }
            //            // filteredList = myList.Where(x => x > 7).ToList();
            //        }

            //    }
            //    if (dc?.Any() ?? false)
            //    {
            //        filterCity.District = dc;
            //        filtertList.Add(filterCity);
            //    }

            //}
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
                        filterList.Add(filterCity);
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
            AddressInfo result = null;
            List<City> cityList = new List<City>();
            int i=0;
            int j=0;
            int k = 0;
            int kont = 0;
            int kont2 = 0;
            int kont3=0;

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

    }


}