/* Authors: Brandon Florian, Tristan Kornacki, Ryan Fisher
 * File: CityDto.cs
 * Purpose: Class for serializing WorldCities data into
 * Date: Feb 16, 2020
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CanadaCities_ASP.Classes
{
    [Serializable]
    [XmlRoot("CanadaCities")]
    public class CityDto 
    {
        public string city { get; set; }
        public string city_ascii { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string country { get; set; }
        public string admin_name { get; set; }
        public string capital { get; set; }
        public string population { get; set; }
        public int id { get; set; }
    }
}
