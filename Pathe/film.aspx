<%@ Page Title="Film - " MasterPageFile="~/inc/Pathe_user.Master" Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.film" %>
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
                <div class="row">
                    <div class="col-lg-4">
                        <img src="holder.js/175x250" class="movie_poster" runat="server" ID="imgPoster"/>
                    </div>
                    <div class="col-lg-8">
                        <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
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