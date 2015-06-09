<%@ Page Title="Film - " MasterPageFile="~/inc/Pathe_admin.master" Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.admin.film" %>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    <div class="jumbotron">
        <h1><asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h1>
        <p>
        </p>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="well">
                <h2>Sidebar</h2>
            </div>
        </div>
        <div class="col-md-9">
            <div class="well">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                <form runat="server" ID="addFilmForm" enctype="multipart/form-data">
                    <div class="row">
                        <div class="col-lg-10">
                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <input class="form-control floating-label input-lg" id="txtTitel" type="text" placeholder="Titel" data-hint="Naam van de film" runat="server" required/>
                                    <br/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4">
                                    <input class="form-control floating-label" id="datRelease" type="date" placeholder="Release date" data-hint="Dag dat de film voor het eerst draait" runat="server" value="2015-06-01" required/>
                                </div>
                                <div class="form-group col-lg-4">
                                    <input class="form-control floating-label" id="numDuur" type="number" placeholder="Speeltijd" data-hint="Speeltijd van de film, in minuten" runat="server" required/>
                                </div>
                                <div class="form-group col-lg-4">
                                    <select class="form-control floating-label" placeholder="Film Type" id="cmbType" required>
                                        <option value="norm">Normaal</option>
                                        <option value="3d">3D</option>
                                        <option value="imax">IMAX</option>
                                        <option value="i3d">IMAX 3D</option>
                                    </select>
                                    <br/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                     <div class="form-group">
                                         <textarea class="form-control flaoting-label" id="txtDescription" placeholder="Beschrijving" rows="10" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <input type="text" readonly="" class="form-control floating-label" placeholder="Afbeeldingen"/>
                                        <input type="file" id="imgPoster" runat="server" accept="image/*"/>
                                    </div>
                                </div>
                                <div class="col-lg-7">
                                    <asp:Button ID="btnAddFilm" runat="server" OnClick="btnAddFilm_OnClick" Text="Film toevoegen" CssClass="btn btn-success btn-lg" />
                                    
                                </div>
                            </div>
                            
                        </div>
                        <div class="col-lg-2">
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_Al" runat="server"/> <img src="/img/kw_al.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_zes" runat="server"/> <img src="/img/kw_zes.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_negen" runat="server"/> <img src="/img/kw_negen.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_twaalf" runat="server"/> <img src="/img/kw_twaalf.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_zestien" runat="server"/> <img src="/img/kw_zestien.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_geweld" runat="server"/> <img src="/img/kw_geweld.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_angst" runat="server"/> <img src="/img/kw_angst.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_seks" runat="server"/> <img src="/img/kw_seks.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_discriminatie" runat="server"/> <img src="/img/kw_disriminatie.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_drugsalcohol" runat="server"/> <img src="/img/kw_drugsalcohol.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            <div class="togglebutton">
                                <label>
                                    <input type="checkbox" id="kw_taalgebruik" runat="server"/> <img src="/img/kw_taalgebruik.png" class="kijkwijzer" alt=""/>
                                </label>
                            </div>
                            
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" id="scripts" ContentPlaceHolderID="ScriptsPlaceholder">
    <script src="http://cdn.ckeditor.com/4.4.7/basic/ckeditor.js"></script>
    <script>
        CKEDITOR.replace('ContentPlaceholder_txtDescription');
    </script>
</asp:Content>