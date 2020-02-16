using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace CanadaCities_ASP.Classes
{
    public class CityInfo
    {
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public int CityId { get; set; }

        /// <summary>
        /// CityInfo constructor, probably adding more params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="population"></param>
        /// <param name="province"></param>
        public CityInfo(CityDto city)
        {
            CityName = city.city;
            CityAscii = city.city_ascii;
            Latitude = city.lat;
            Longitude = city.lng;
            Country = city.country;
            Province = city.admin_name;
            Capital = city.capital;
            Population = Int32.Parse(city.population);
            CityId = city.id;
        }

        /// <summary>
        /// Gets the province associated with the city.
        /// </summary>
        /// <returns>a string representing the province.</returns>
        public string GetProvince()
        {
            return Province;
        }

        /// <summary>
        /// Gets the population of the city.
        /// </summary>
        /// <returns>an integer representing the population</returns>
        public int GetPopulation()
        {
            return Population;
        }

        /// <summary>
        /// Gets the geographic location in longitude and latitude.
        /// </summary>
        /// <returns>a new location object containing the cities lat/long coords.</returns>
        public Location GetLocation()
        {
            return new Location(Latitude, Longitude);
        }
    }
}
