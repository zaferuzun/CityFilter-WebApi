using CityFilter_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityFilter_WebApi.Operations
{
    public class Sorting
    {
        public static AddressInfo CityName(AddressInfo G_obj, string sorting)
        {
            if(sorting == "ASCENDING")
            {
                G_obj.City = G_obj.City.OrderBy(item => item.Name).ToList();
            }
            else if (sorting == "DESCENDING")
            {
                G_obj.City = G_obj.City.OrderByDescending(item => item.Name).ToList();

            }

            return G_obj;
        }
        public static AddressInfo CityCode(AddressInfo G_obj, string sorting)
        {
            if (sorting == "ASCENDING")
            {
                G_obj.City = G_obj.City.OrderBy(item => item.Code).ToList();
            }
            else if (sorting == "DESCENDING")
            {
                G_obj.City = G_obj.City.OrderByDescending(item => item.Code).ToList();
            }

            return G_obj;
        }
        public static AddressInfo DistrictName(AddressInfo G_obj, string sorting)
        {
            int i = 0;
            foreach (var item in G_obj.City.ToList())
            {
                if (sorting == "ASCENDING")
                {
                    G_obj.City[i].District = item.District.OrderBy(x => x.Name).ToList();

                }
                else if (sorting == "DESCENDING")
                {
                    G_obj.City[i].District = item.District.OrderByDescending(x => x.Name).ToList();
                }
                i++;
            }
            return G_obj;
        }
        public static AddressInfo ZipCode(AddressInfo G_obj, string sorting)
        {
            int i = 0;
            int j = 0;
            foreach (var item in G_obj.City.ToList())
            {
                foreach (var item2 in item.District.ToList())
                {
                    if (sorting == "ASCENDING")
                    {
                        G_obj.City[i].District[j].Zip = item2.Zip.OrderBy(x => x.Code).ToList();

                    }
                    else if (sorting == "DESCENDING")
                    {
                        G_obj.City[i].District[j].Zip = item2.Zip.OrderByDescending(x => x.Code).ToList();
                    }
                    j++;
                }
                i++;
                j = 0;
            }
            return G_obj;
        }

    }
}