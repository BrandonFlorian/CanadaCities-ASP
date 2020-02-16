/* Authors: Brandon Florian, Tristan Kornacki, Ryan Fisher
 * File: Location.cs
 * Purpose: Class for retuning lat long values.
 * Date: Feb 16, 2020
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace CanadaCities_ASP.Classes
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>
        /// Location constructor
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public Location(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
        
    }
}
