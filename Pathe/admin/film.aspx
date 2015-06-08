<%@ Page Title="Film - " MasterPageFile="~/inc/Pathe_admin.master" Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.admin.film" %>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    <form id="filminfo" runat="server">
    <div class="jumbotron">
        <h1><asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h1>
        <p>
        </p>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="well movie_info">
                <img src="holder.js/175x250" />
                <p>
                    <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                </p>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="well movie_extra">
                <strong>Release Date</strong> <asp:Label ID="lblRelease" runat="server" Text="Onbekend"></asp:Label><br/>
                <asp:Label runat="server" ID="lblKijkwijzer" Text="Kijkwijzer"></asp:Label>
            </div>
            
        </div>
    </div>
    </form>
</asp:Content>