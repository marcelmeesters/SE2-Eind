<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.film" %>
<%@ Import Namespace="System.IO" %>
<% if((string) Page.RouteData.Values["film"] == "none") Response.Redirect("/Films"); %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
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
</div>
<!--#include file="inc/js.aspx"-->
</body>
</html>
