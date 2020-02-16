using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace CanadaCities_ASP.Classes
{
    //ryan test comment
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
        
    }
}
