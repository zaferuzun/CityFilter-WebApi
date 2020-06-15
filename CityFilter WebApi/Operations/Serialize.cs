using CityFilter_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Operations
{
    public class Serialize
    {

        /// <summary>
        /// Xml stringini Objeye çevirir.
        /// </summary>
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
        /// <summary>
        /// CSV stringini DataTable ye çevirir.
        /// </summary>
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
        /// <summary>
        /// Datatable öğesini Objeye çevirir.
        /// </summary>
        public static AddressInfo DataTabletoObject(DataTable dt)
        {
            AddressInfo result = new AddressInfo();
            List<City> cityList = new List<City>();
            int i = 0;
            int j = 0;
            int k = 0;
            int kont = 0;
            int kont2 = 0;
            int kont3 = 0;
            cityList.Add(Operations.Operation.getCity());
            int rowCount = dt.Rows.Count;

            foreach (DataRow row in dt.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                if (cityList[i].Name != fields[0])
                {
                    if (kont != 0)
                    {
                        i += 1;
                        cityList.Add(Operations.Operation.getCity());
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
                        j += 1;
                        cityList[i].District.Add(new District());
                    }
                    cityList[i].District[j].Name = fields[2];

                    cityList[i].District[j].Zip = new List<Zip>();
                    cityList[i].District[j].Zip.Add(new Zip());
                    k = 0;
                    kont3 = 0;
                }
                if (kont3 != 0)
                    cityList[i].District[j].Zip.Add(new Zip());

                cityList[i].District[j].Zip[k].Code = fields[3];

                k += 1;
                kont = +1;
                kont2 += 1;
                kont3 += 1;
                rowCount -= 1;
            }
            result.City = cityList;
            return result;
        }


    }
}