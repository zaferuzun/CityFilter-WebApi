using CityFilter_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace CityFilter_WebApi.Operations
{
    public class Deserialize
    {
        /// <summary>
        /// Objeyi Datatableye çevirir.
        /// </summary>
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
                        dt.Rows.Add(item.Name, item.Code, item2.Name, item3.Code);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// DataTable öğesini CSV stringine çevirir.
        /// </summary>
        public static string DataTabletoCSV(DataTable dtDataTable)
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
        /// <summary>
        /// String öğesini parçalayıp listeye çevirir.
        /// </summary>
        public static List<string> StringtoList(string Gdata)
        {
            List<string> Pdata = Gdata.Split(',').ToList();

            return Pdata;
        }
        /// <summary>
        /// Objeyi Xml stringine dönüştürür.
        /// </summary>
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

    }
}