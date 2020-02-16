using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanadaCities_ASP.Classes
{
    /// <summary>
    /// Class for parsing the raw WorldCities data into
    /// </summary>
    [Serializable]
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
