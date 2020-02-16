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

        //public void GetCities()
        //{
        //    List<CityInfo> cities = new List<CityInfo>();
        //    foreach (KeyValuePair<string, CityInfo> kvp in Statistics.CityCatalogue)
        //    {
        //        CitiesList.Add(kvp.Value);
        //    }
        //}
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

        private void ShowError()
        {
            ErrorLabel.Text = "*Must Select a file type!";
            ErrorLabel.CssClass += "text-danger ";
            ErrorLabel.Visible = true;
        }

        #region Events


        protected void DisplayCitiesButton_OnClick(object sender, EventArgs e)
        {

            if(FileTypeRadioList.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                CitiesList = new List<CityInfo>();
                CitiesList.Add(statistics.DisplayCityInformation(CityTextBox.Text));
                BindGrids();
                GridLabel.Text = $"City Info for {CityTextBox.Text}: ";
                CitiesGrid.Visible = true;
                ProvincesGrid.Visible = false;
                PopModal();
            }
            else
            {
                ShowError();
            }
        }

        protected void CalculateDistanceButton_OnClick(object sender, EventArgs e)
        {
            double distance = 0.0;
            if (FileTypeRadioList.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(OriginCityTextBox.Text) && !string.IsNullOrWhiteSpace(DestinationCityTextBox.Text))
            {
                distance = statistics.CalculateDistanceBetweenCities(OriginCityTextBox.Text, DestinationCityTextBox.Text);
                GridLabel.Text = $"From {OriginCityTextBox.Text} to {DestinationCityTextBox.Text} - Distance = {distance} KM";
                CitiesGrid.Visible = false;
                ProvincesGrid.Visible = false;
                PopModal();
            }
            else
            {
                ShowError();
            }

            //show distance
        }

        protected void DisplayProvinceButton_Click(object sender, EventArgs e)
        {
            string method = ProvincialMethodChoice.SelectedValue;

            CitiesList = new List<CityInfo>();
            if (FileTypeRadioList.SelectedIndex != -1 && ProvincesDropDown.SelectedIndex != -1)
            {
                switch (method)
                {
                    case "Largest":
                        CitiesList.Add(statistics.DisplayLargestPopulationCity(ProvincesDropDown.SelectedItem.ToString()));
                        BindGrids();
                        GridLabel.Text = $"Largest population city in {ProvincesDropDown.SelectedItem.ToString()}: ";
                        CitiesGrid.Visible = true;
                        ProvincesGrid.Visible = false;
                        break;
                    case "Smallest":
                        CitiesList.Add(statistics.DisplaySmallestPopulation(ProvincesDropDown.SelectedItem.ToString()));
                        BindGrids();
                        GridLabel.Text = $"Smallest population city in {ProvincesDropDown.SelectedItem.ToString()}: ";
                        CitiesGrid.Visible = true;
                        ProvincesGrid.Visible = false;
                        break;
                    case "TotalPop":
                        GridLabel.Text = $"Total Population of {ProvincesDropDown.SelectedItem.ToString()} is {statistics.DisplayProvincePopulation(ProvincesDropDown.SelectedItem.ToString())}";
                        CitiesGrid.Visible = false;
                        ProvincesGrid.Visible = false;
                        break;
                }
                PopModal();
            }
            else
            {
                ShowError();
            }

           
        }

        #endregion

        protected void ComparePopulationsButton_Click(object sender, EventArgs e)
        {
            if (FileTypeRadioList.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(OriginCityTextBox.Text) && !string.IsNullOrWhiteSpace(DestinationCityTextBox.Text))
            {
                CitiesList = new List<CityInfo>();
                CitiesList.Add(statistics.CompareCitiesPopulation(OriginCityTextBox.Text, DestinationCityTextBox.Text));
                BindGrids();
                GridLabel.Text = $"Larger City between : {OriginCityTextBox.Text} and {DestinationCityTextBox.Text}: ";

                CitiesGrid.Visible = true;
                ProvincesGrid.Visible = false;
                PopModal();
            }
            else
            {
                ShowError();
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
            ErrorLabel.Visible = false;
        }

        private void PopModal()
        {
            ErrorLabel.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void ProvincesByPopulation_Click(object sender, EventArgs e)
        {
            if (FileTypeRadioList.SelectedIndex != -1)
            {
                ProvinceList = statistics.RankProvincesByPopulation().ToList();
                BindGrids();
                ProvincesGrid.Visible = true;
                CitiesGrid.Visible = false;
                GridLabel.Text = "Provinces Ranked by population: ";
                PopModal();
            }
            else
            {
                ShowError();
            }


        }

        protected void ProvincesByCities_Click(object sender, EventArgs e)
        {
            if (FileTypeRadioList.SelectedIndex != -1)
            {
                ProvinceList = statistics.RankProvincesByCities().ToList();
                BindGrids();
                ProvincesGrid.Visible = true;
                CitiesGrid.Visible = false;
                GridLabel.Text = "Provinces Ranked by number of cities: ";
                PopModal();
            }
            else
            {
                ShowError();
            }
        }
    }
}