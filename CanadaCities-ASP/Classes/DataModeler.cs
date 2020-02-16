/* Authors: Brandon Florian, Tristan Kornacki, Ryan Fisher
 * File: DataModeler.cs
 * Purpose: Class for parsing file types
 * Date: Feb 16, 2020
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CsvHelper;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace CanadaCities_ASP.Classes
{
    public class DataModeler
    {
        #region Member Variables

        public delegate void DataModelerDelegate(string filename);
        public Dictionary<string, CityInfo> CityCatalogue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Data modeler constructor
        /// </summary>
        public DataModeler()
        {
            CityCatalogue = new Dictionary<string, CityInfo>();
        }

        #endregion

        #region Methods

        public void ParseXML(string filename)
        {
           

        }

        /// <summary>
        /// Parses a csv into a Dictionary<string, CityInfo>
        /// </summary>
        /// <param name="filename">a string representing the path of the file</param>
        public void ParseCSV(string filename)
        {
            try
            {
                List<CityDto> cities = new List<CityDto>();

                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    cities = csv.GetRecords<CityDto>().ToList();
                }

                //Add to catalogue
                foreach (CityDto city in cities)
                {
                    if (!CityCatalogue.ContainsKey(city.city) && !string.IsNullOrWhiteSpace(city.city))
                    {
                        CityInfo newCity = new CityInfo(city);

                        CityCatalogue.Add(city.city, newCity);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Parses a json into a Dictionary<string, CityInfo>
        /// </summary>
        /// <param name="filename">a string representing the path of the file</param>
        public void ParseJSON(string filename)
        {
            try
            {
                string json = File.ReadAllText(filename);
                List<CityDto> cities = JsonConvert.DeserializeObject<List<CityDto>>(json);

                //Add to catalogue
                foreach (CityDto city in cities)
                {
                    if (!CityCatalogue.ContainsKey(city.city) && !string.IsNullOrWhiteSpace(city.city))
                    {
                        CityInfo newCity = new CityInfo(city);

                        CityCatalogue.Add(city.city, newCity);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Adds the correct method to the delegate and then calls the delegate to parse the correct file type
        /// </summary>
        /// <param name="filename">a string representing the file to parse</param>
        /// <param name="filetype">a string representing the file type</param>
        /// <returns></returns>
        public Dictionary<string, CityInfo> ParseFile(string filename, string filetype)
        {
            DataModelerDelegate dataModelerDelegate = null;
            switch (filetype.ToLower())
            {
                case "json":
                    dataModelerDelegate += ParseJSON;
                    break;
                case "xml":
                    dataModelerDelegate += ParseXML;
                    break;
                case "csv":
                    dataModelerDelegate += ParseCSV;
                    break;
                default:
                    break;
            }

            dataModelerDelegate(filename);

            return CityCatalogue;
        }

        #endregion

    }
}
