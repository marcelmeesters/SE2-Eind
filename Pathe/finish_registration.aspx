<%@ Page Title="" Language="C#" MasterPageFile="~/inc/Pathe_user.Master" AutoEventWireup="true" CodeBehind="finish_registration.aspx.cs" Inherits="Pathe.finish_registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="row">
        <div class="col-lg-6 col-lg-offset-3">
            <div class="well">
                <div class="alert alert-dismissable alert-info" runat="server" id="alInfo">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <strong>Bijna klaar!</strong> Bedankt voor uw registratie. We hebben nog een paar gegevens nodig om uw account compleet te maken.
                </div>
                <div class="alert alert-dismissable alert-warning" runat="server" id="alWrong">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <strong>Oeps!</strong> Niet alle velden zijn correct ingevuld, probeer het aub nogmaals.
                </div>
                <form runat="server">
                    <div class="row">
                        <div class="form-group col-lg-4">
                            <input class="form-control floating-label input-lg" id="txtName" type="text" placeholder="Voornaam" runat="server" required/>
                        </div>
                        <div class="form-group col-lg-4">
                            <input class="form-control floating-label input-lg" id="txtTussen" type="text" placeholder="Tussenvoegsels" runat="server"/>
                        </div>
                        <div class="form-group col-lg-4">
                            <input class="form-control floating-label input-lg" id="txtLastname" type="text" placeholder="Achternaam" runat="server" required/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-4">
                            <input type="date" class="form-control floating-label" id="datBorn" runat="server" placeholder="Geboortedatum" value="2000-01-01" required/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-8">
                            <input type="text" runat="server" id="txtAdres" class="form-control floating-label" placeholder="Straat" required/>
                        </div>
                        <div class="form-group col-lg-4">
                            <input type="number" id="numHuisnr" runat="server" class="form-control floating-label" placeholder="Huisnummer" required/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-4">
                            <input type="text" runat="server" id="txtPostcode" class="form-control floating-label" placeholder="Postcode" required/>
                        </div>
                        <div class="form-group col-lg-8">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" id="chkNewsletter" runat="server"/> Nieuwsbrief ontvangen
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-10 col-lg-offset-1">
                            <center>
                                <asp:Button ID="btnSubmit" runat="server" Text="Opslaan" CssClass="btn btn-success btn-lg" OnClick="btnSubmit_Click" />
                            </center>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
