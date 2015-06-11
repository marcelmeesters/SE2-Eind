<%@ Page Title="Bioscoop Toevoegen - Pathé" Language="C#" MasterPageFile="~/inc/Pathe_admin.master" AutoEventWireup="true" CodeBehind="cinemaAdd.aspx.cs" Inherits="Pathe.admin.cinemaAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="jumbotron">
        <h1>Bioscoop toevoegen</h1>
        <p>Gebruik deze pagina om een bioscoop toe te voegen aan het systeem.</p>
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
                <form runat="server" ID="addCinemaForm" enctype="multipart/form-data">
                    <div class="row">
                        <div class="col-lg-10">
                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <input class="form-control floating-label input-lg" id="txtName" type="text" placeholder="Naam" data-hint="Naam van de bioscoop" runat="server" required/>
                                    <br/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-8">
                                    <input class="form-control floating-label" id="txtAddress" type="text" placeholder="Adres" data-hint="Het adres van de bioscoop, inclusief postcode" runat="server" required/>
                                </div>
                                <div class="form-group col-lg-4">
                                    <input class="form-control floating-label" id="txtCity" type="text" placeholder="Stad" data-hint="Stad van de bioscoop" runat="server" required/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3">
                                     <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkLift" runat="server"/> Lift aanwezig
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkToilet" runat="server"/> Invalidetoilet
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkRing" runat="server"/> Ringleiding
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkImax" runat="server"/> IMAX
                                        </label>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                     <div class="form-group">
                                         <textarea class="form-control flaoting-label" id="txtOpeningsTijden" placeholder="Openingstijden" rows="3" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-7">
                                    <span onClick="addCinema()" class="btn btn-success btn-lg">Bioscoop Toevoegen</span>
                                    <a class="btn btn-warning btn-flat btn-sm" href="/Admin/Cinemas/List">Annuleren</a>
                                    
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceholder" runat="server">
    
    <script type="text/javascript">
        // Ask for confirmation before removing a cinema
        function addCinema() {
            var naam = $("#ContentPlaceholder_txtName").val();
            var adres = $("#ContentPlaceholder_txtAddress").val();
            var stad = $("#ContentPlaceholder_txtCity").val();
            var openingstijden = $("#ContentPlaceholder_txtOpeningsTijden").val();
            var lift = $("#ContentPlaceholder_chkLift").is(':checked');
            var toilet = $("#ContentPlaceholder_chkToilet").is(':checked');
            var ring = $("#ContentPlaceholder_chkRing").is(':checked');
            var imax = $("#ContentPlaceholder_chkImax").is(':checked');
                
            xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", "/admin/cinemaAddProcess.aspx", false);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xmlhttp.send("name=" + naam + "&address=" + adres + "&city=" + stad + "&open=" + openingstijden +
                         "&lift=" + lift + "&toilet=" + toilet + "&ring=" + ring + "&imax=" + imax);

                if (xmlhttp.responseText == 'success') {
                    // Deleted successfully
                    swal({
                        title: "Toegevoegd!",
                        text: "De bioscoop <strong>" + naam + "</strong> is toegevoegd",
                        type: "success",
                        timer: 3000,
                        html: true,
                        showConfirmButton: false
                    });
                    setTimeout('Redirect()', 3200);
                }
                else {
                    // Something went wrong, show an error
                    swal({
                        title: "Oeps!",
                        text: "Er is iets fout gegaan tijdens het toevoegen van de bioscoop, probeer het aub nogmaals.<br><br><strong>Foutmelding</strong><br><br>" + xmlhttp.responseText,
                        type: "error",
                        html: true
                    });
                }
        }
        
        function Redirect() {
            window.location = "/Admin/Cinemas/List";
        }
    
    </script>
</asp:Content>
