<%@ Page Title="" Language="C#" MasterPageFile="~/Cities.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CanadaCities_ASP.Pages.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#infoModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="text-center">
                <h1 class="display-4">Canada Cities Parser</h1>
                <p>Select a file type below</p>
                <div class="row">
                    <div class="col-centered">
                        <asp:Label ID="ErrorLabel" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="form-check-inline">
                    <asp:RadioButtonList ID="FileTypeRadioList" runat="server" CssClass="form-check-input" OnSelectedIndexChanged="FileTypeRadioList_SelectedIndexChanged">
                        <asp:ListItem Text="CSV" Value="CSV"></asp:ListItem>
                        <asp:ListItem Text="JSON" Value="JSON"></asp:ListItem>
                        <asp:ListItem Text="XML" Value="XML"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="mt-4">
                    <div class="row">
                        <div class="col-centered">
                            <div class="card" style="width: 400px">
                                <div class="card-header bg-secondary">Province Methods</div>
                                <div class="card-body bg-dark text-white">
                                    <div class="row mb-4">
                                        <div class="col-centered">
                                            <asp:DropDownList ID="ProvincesDropDown" runat="server" CssClass="btn btn-success">
                                                <asp:ListItem Text="Alberta" Value="Alberta"></asp:ListItem>
                                                <asp:ListItem Text="British Columbia" Value="BC"></asp:ListItem>
                                                <asp:ListItem Text="Saskatchewan" Value="Saskatchewan"></asp:ListItem>
                                                <asp:ListItem Text="Manitoba" Value="Manitoba"></asp:ListItem>
                                                <asp:ListItem Text="Ontario" Value="Ontario"></asp:ListItem>
                                                <asp:ListItem Text="Quebec" Value="Quebec"></asp:ListItem>
                                                <asp:ListItem Text="Prince Edward Island" Value="PEI"></asp:ListItem>
                                                <asp:ListItem Text="Nova Scotia" Value="NS"></asp:ListItem>
                                                <asp:ListItem Text="New Brunswick" Value="NB"></asp:ListItem>
                                                <asp:ListItem Text="Newfoundland and Labrador" Value="NFLD"></asp:ListItem>
                                                <asp:ListItem Text="Yukon" Value="Yukon"></asp:ListItem>
                                                <asp:ListItem Text="Northwest Territories" Value="NWT"></asp:ListItem>
                                                <asp:ListItem Text="Nunavut" Value="Nunavut"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-centered">
                                            <asp:RadioButtonList ID="ProvincialMethodChoice" runat="server">
                                                <asp:ListItem Text="Largest City" Value="Largest"></asp:ListItem>
                                                <asp:ListItem Text="Smallest City" Value="Smallest"></asp:ListItem>
                                                <asp:ListItem Text="Total Population" Value="TotalPop"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col-centered">
                                            <asp:Button ID="DisplayProvinceButton" runat="server" Text="Display Province Info" OnClick="DisplayProvinceButton_Click" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-dark text-white">
                                    <div class="row mb-4 mt-4">
                                        <div class="col mb-4">
                                            <asp:Button ID="ProvincesByPopulation" runat="server" Text="Rank Provinces by Population" OnClick="ProvincesByPopulation_Click" CssClass="btn btn-success"></asp:Button>
                                        </div>
                                        <div class="col mb-4">
                                            <asp:Button ID="ProvincesByCities" runat="server" Text="Rank Provinces by Total Cities" OnClick="ProvincesByCities_Click" CssClass="btn btn-success"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-centered">
                            <div class="card" style="width: 400px">
                                <div class="card-header bg-secondary">City Methods</div>
                                <div class="card-body bg-dark text-white">
                                    <div class="row">
                                        <div class="col-centered">
                                            <div class="mb-4">
                                                <asp:TextBox ID="CityTextBox" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="mb-4">
                                                <asp:Button ID="DisplayCityButton" runat="server" Text="Display City Info" OnClick="DisplayCitiesButton_OnClick" CssClass="btn btn-success" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-dark text-white">
                                    <div class="row">
                                        <div class="col-centered">
                                            <label>Enter Two Cities: </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col mb-4">
                                            <asp:TextBox ID="OriginCityTextBox" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col mb-4">
                                            <asp:TextBox ID="DestinationCityTextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-4 mt-4">
                                        <div class="col mb-4">
                                            <asp:Button ID="CalculateDistanceButton" runat="server" Text="Calculate Distance" OnClick="CalculateDistanceButton_OnClick" CssClass="btn btn-success" />
                                        </div>
                                        <div class="col mb-4">
                                            <asp:Button ID="ComparePopulationsButton" runat="server" Text="Compare Populations" OnClick="ComparePopulationsButton_Click" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal bg-dark info-modal" id="infoModal" style="width: 800px; height: 800px;">
                    <div class="modal-header">
                        <button type="button" class="close text-white" data-dismiss="modal">X</button>
                    </div>
                    <div class="modal-body">
                        <div class="card" style="width: 700px">
                            <div class="card-header bg-secondary">
                                <h3>Info Card</h3>
                            </div>
                            <div class="card-body bg-dark text-white">
                                <div class="row">
                                    <div class="col-centered">
                                        <asp:Label ID="GridLabel" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-centered">
                                        <asp:GridView ID="CitiesGrid" runat="server">
                                        </asp:GridView>
                                        <asp:GridView ID="ProvincesGrid" runat="server">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

