using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CanadaCities_ASP.Classes;
namespace CanadaCities_ASP.Pages
{
    public partial class Index : System.Web.UI.Page
    {

        #region Member Variables

        private static Statistics statistics = null;
        
        List<CityInfo> CitiesList = new List<CityInfo>();
        List<KeyValuePair<string, int>> ProvinceList = new List<KeyValuePair<string, int>>();
        private string FileType = string.Empty;
        private string FileName = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(statistics != null)
                {
                    statistics = new Statistics($"D:\\School\\CanadaCities-ASP\\CanadaCities-ASP\\CanadaCities-ASP\\Data\\{FileName}", FileType);
                }
                
                BindGrids();
            }
        }

        public void GetCities()
        {
            List<CityInfo> cities = new List<CityInfo>();
            foreach (KeyValuePair<string, CityInfo> kvp in Statistics.CityCatalogue)
            {
                CitiesList.Add(kvp.Value);
            }
        }



        protected void BindGrids()
        {
            if(CitiesList.Count > 0)
            {
                CitiesGrid.DataSource = CitiesList;
                CitiesGrid.DataBind();
            }
            if(ProvinceList.Count > 0)
            {
                ProvincesGrid.DataSource = ProvinceList;
                ProvincesGrid.DataBind();
            }
        }


        #region Events


        protected void DisplayCitiesButton_OnClick(object sender, EventArgs e)
        {

            CitiesList = new List<CityInfo>();
            CitiesList.Add(statistics.DisplayCityInformation(CityTextBox.Text));
            BindGrids();
        }

        protected void CalculateDistanceButton_OnClick(object sender, EventArgs e)
        {
            double distance = 0.0;
            if (!string.IsNullOrWhiteSpace(OriginCityTextBox.Text) && !string.IsNullOrWhiteSpace(DestinationCityTextBox.Text))
            {
                distance = statistics.CalculateDistanceBetweenCities(OriginCityTextBox.Text, DestinationCityTextBox.Text);
            }
            DistanceLabel.Text = $"From {OriginCityTextBox.Text} to {DestinationCityTextBox.Text} - Distance = {distance}";
            DistanceLabel.Visible = true;
            GridLabel.Text = "Distance: ";
            //show distance
        }

        protected void DisplayProvinceButton_Click(object sender, EventArgs e)
        {
            string method = ProvincialMethodChoice.SelectedValue;
            //List<CityInfo> cities = new List<CityInfo>();
            CitiesList = new List<CityInfo>();
            if (ProvincesDropDown.SelectedIndex != -1)
            {
                switch (method)
                {
                    case "Largest":
                        CitiesList.Add(statistics.DisplayLargestPopulationCity(ProvincesDropDown.SelectedItem.ToString()));
                        BindGrids();
                        GridLabel.Text = $"Largest population city in {ProvincesDropDown.SelectedItem.ToString()}: ";
                        break;
                    case "Smallest":
                        CitiesList.Add(statistics.DisplaySmallestPopulation(ProvincesDropDown.SelectedItem.ToString()));
                        BindGrids();
                        GridLabel.Text = $"Smallest population city in {ProvincesDropDown.SelectedItem.ToString()}: ";
                        break;
                    case "TotalPop":
                        break;

                }
            }
        }

        #endregion

        protected void ComparePopulationsButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(OriginCityTextBox.Text) && !string.IsNullOrWhiteSpace(DestinationCityTextBox.Text))
            {
                CitiesList = new List<CityInfo>();
                CitiesList.Add(statistics.CompareCitiesPopulation(OriginCityTextBox.Text, DestinationCityTextBox.Text));
                BindGrids();
                GridLabel.Text = $"Larger City between : {OriginCityTextBox.Text} and {DestinationCityTextBox.Text}: ";
            }

        }

        protected void FileTypeRadioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileType = FileTypeRadioList.SelectedValue;
            switch (FileType)
            {
                case "JSON":
                    FileName = "Canadcities-JSON.json";
                    break;
                case "XML":
                    FileName = "Canadcities-XML.xml";
                    break;
                case "CSV":
                    FileName = "Canadacities.csv";
                    break;
            }
            statistics = new Statistics($"D:\\School\\CanadaCities-ASP\\CanadaCities-ASP\\CanadaCities-ASP\\Data\\{FileName}", FileType);

        }

        protected void ProvincesByPopulation_Click(object sender, EventArgs e)
        {
            ProvinceList = statistics.RankProvincesByPopulation().ToList();
            BindGrids();
        }

        protected void ProvincesByCities_Click(object sender, EventArgs e)
        {
            ProvinceList = statistics.RankProvincesByCities().ToList();
            BindGrids();
        }
    }
}