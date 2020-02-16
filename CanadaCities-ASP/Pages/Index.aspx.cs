/* Authors: Brandon Florian, Tristan Kornacki, Ryan Fisher
 * File: Index.aspx.cs
 * Purpose: Codebehind for index page
 * Date: Feb 16, 2020
 */

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

        #region Events


        /// <summary>
        /// Fires on page load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (statistics != null)
                {
                    statistics = new Statistics(Server.MapPath($"\\Data\\{FileName}"), FileType);
                }

                BindGrids();
            }
        }

        /// <summary>
        /// Binds the grids to their data sources.
        /// </summary>
        protected void BindGrids()
        {
            if (CitiesList.Count > 0)
            {
                CitiesGrid.DataSource = CitiesList;
                CitiesGrid.DataBind();
            }
            if (ProvinceList.Count > 0)
            {
                ProvincesGrid.DataSource = ProvinceList;
                ProvincesGrid.DataBind();
            }
        }

        /// <summary>
        /// Displays the information for an entered city.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Gets the distance between two cities.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Displays one of three options for a selected province (Largest city, smallest city, Total population).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Displays the larger of two cities.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Instantiates the Statistics class with the chosen file type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            statistics = new Statistics(Server.MapPath($"\\Data\\{FileName}"), FileType);
            ErrorLabel.Visible = false;
        }


        /// <summary>
        /// Displays the provinces ranked by population.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Displays the provinces ranked by total cities.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Displays the error label when a file type has not been selected.
        /// </summary>
        private void ShowError()
        {
            ErrorLabel.Text = "*Must Select a file type!";
            ErrorLabel.CssClass += "text-danger ";
            ErrorLabel.Visible = true;
        }

        /// <summary>
        /// Pops the modal.
        /// </summary>
        private void PopModal()
        {
            ErrorLabel.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        #endregion
    }
}