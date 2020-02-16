using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace CanadaCities_ASP.Classes
{
    public class Statistics
    {
        #region Member Variables

        public static Dictionary<string, CityInfo> CityCatalogue { get; set; }
        private const string GoogleAPIKey = "AIzaSyCjG5jPvjNTzJaFfdymVh99im31b39GIzs";
        private const string BingMapsAPIKey = "AnnLUUFtpgulvHcQwuoI2yBBsmlqkgku22Q7UODjA3OhrPDlwbga6o7vdXreitNg";
        static DataModeler Data { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Determines the filetype for parsing.
        /// </summary>
        /// <param name="filename">a string representing the filename.</param>
        /// <param name="filetype">a string representing the type of file to parse.</param>
        public Statistics(string filename, string filetype)
        {
            Data = new DataModeler();
            CityCatalogue = Data.ParseFile(filename, filetype);
        }

        #endregion

        #region City Methods

        /// <summary>
        /// Find the CityInfo object in the CityCatalogue using the city name as a key.
        /// </summary>
        /// <param name="cityname">a string reperesenting the city to lookup</param>
        /// <returns>CityInfo from the dictionary corresponding to cityname</returns>
        public CityInfo DisplayCityInformation(string cityname)
        {
            cityname = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cityname.ToLower());

            CityInfo city;
            if (CityCatalogue.TryGetValue(cityname, out city))
            {
                return city;

            }
            return null;
        }

        /// <summary>
        /// Looks for the largest population city in a province.
        /// </summary>
        /// <param name="province">a string representing the province</param>
        /// <returns>A CityInfo object</returns>
        public CityInfo DisplayLargestPopulationCity(string province)
        {
            List<CityInfo> cityList = DisplayProvinceCities(province);
            int largestPopulation = 0;
            CityInfo largestCity = null;
            foreach (CityInfo city in cityList)
            {
                if (city.Population > largestPopulation)
                {
                    largestPopulation = city.Population;
                    largestCity = city;
                }
            }

            return largestCity;

        }

        /// <summary>
        /// Looks for the smallest population city in a province.
        /// </summary>
        /// <param name="province">a string representing the province</param>
        /// <returns>A CityInfo object</returns>
        public CityInfo DisplaySmallestPopulation(string province)
        {

            List<CityInfo> cityList = DisplayProvinceCities(province);
            int smallestPopulation = cityList[0].Population;
            CityInfo smallest = null;
            foreach (CityInfo city in cityList)
            {
                if (city.Population < smallestPopulation)
                {
                    smallestPopulation = city.Population;
                    smallest = city;
                }
            }

            return smallest;
        }

        /// <summary>
        /// Compare the two passed in cities population and return the larger
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public CityInfo CompareCitiesPopulation(string city1, string city2)
        {
            //this method will need some error checking on the passed in string
            CityInfo cityA;
            CityInfo cityB;
            if (CityCatalogue.TryGetValue(city1, out cityA) && CityCatalogue.TryGetValue(city2, out cityB))
            {
                if (cityA.Population > cityB.Population)
                    return cityA;
                else
                    return cityB;

            }
            return null;
        }

        public void ShowCityOnMap()
        {
            // use www.latlong.net
        }

        /// <summary>
        /// Calculates the distance between 2 cities.
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public double CalculateDistanceBetweenCities(string city1, string city2)
        {
            CityInfo origin = DisplayCityInformation(city1);
            CityInfo destination = DisplayCityInformation(city2);
          
            string bingUrl = $"https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?origins={origin.Latitude},{origin.Longitude}&destinations={destination.Latitude},{destination.Longitude}&travelMode=driving&key={BingMapsAPIKey}";

            string json = string.Empty;

            var request = WebRequest.Create(bingUrl);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            response.Close();

            JObject jObject = JObject.Parse(json);

            var distanceJToken = jObject["resourceSets"][0]["resources"][0]["results"][0]["travelDistance"];
            double distance = Double.Parse(distanceJToken.ToString());

            return distance;
        }
        #endregion

        #region Province Methods
        
       // public List<CityInfo> GetAllCities

        /// <summary>
        /// Sums the population of each city in the province
        /// </summary>
        /// <param name="province"></param>
        /// <returns>an int representing total population for all cities in a province</returns>
        public int DisplayProvincePopulation(string province)
        {
            List<CityInfo> cityList = DisplayProvinceCities(province);

            int count = 0;
            foreach (CityInfo city in cityList)
            {
                count += city.Population;
            }

            return count;
        }

        /// <summary>
        /// Finds all cities for a province
        /// </summary>
        /// <param name="province"></param>
        /// <returns>A list of CityInfo representing the cities in the province.</returns>
        public static List<CityInfo> DisplayProvinceCities(string province)
        {
            List<CityInfo> cityList = new List<CityInfo>();

            foreach (KeyValuePair<string, CityInfo> kvp in CityCatalogue)
            {
                if (kvp.Value.Province.ToLower() == province.ToLower())
                {
                    cityList.Add(kvp.Value);
                }
            }
            return cityList;
        }

        /// <summary>
        /// Rank and show all provinces by population in ascending order.
        /// </summary>
        public List<KeyValuePair<string, int>> RankProvincesByPopulation()
        {
            List < KeyValuePair<string, int>> keyValuePairs = GetProvincePopulation().OrderBy(key => key.Value).ToList();

            return keyValuePairs;
        }
            //=> GetProvincePopulation().OrderBy(key => key.Value);

        /// <summary>
        ///  Rank and show all provinces by the number of cities in each in ascending order.
        /// </summary>
        public List<KeyValuePair<string, int>> RankProvincesByCities()
        {
            List<KeyValuePair<string, int>> keyValuePairs = GetProvinceCityTotal().OrderBy(key => key.Value).ToList();

            return keyValuePairs;
        }

        /// <summary>
        /// Gets all provinces and their total populations
        /// </summary>
        /// <returns>A dictionary representing each province and total population</returns>
        public Dictionary<string, int> GetProvincePopulation()
        {
            Dictionary<string, int> provinces = new Dictionary<string, int>();
            
            //get all provinces
            foreach(CityInfo city in CityCatalogue.Values)
            {
                if (!provinces.ContainsKey(city.Province))
                {
                    provinces.Add(city.Province, DisplayProvincePopulation(city.Province));
                }
            }

            return provinces;
        }

        /// <summary>
        /// Gets the number of cities in each province
        /// </summary>
        /// <returns>a dictionary of all provinces and number of cities.</returns>
        public Dictionary<string, int> GetProvinceCityTotal()
        {
            Dictionary<string, int> provinces = GetProvincePopulation();
            Dictionary<string, int> provinceTotals = new Dictionary<string, int>();
            //get all provinces
            foreach (KeyValuePair<string, int> city in provinces)
            {
                List<CityInfo> cities = DisplayProvinceCities(city.Key);
                provinceTotals.Add(city.Key, cities.Count);
            }

            return provinceTotals;
        }
        #endregion
    }
}
