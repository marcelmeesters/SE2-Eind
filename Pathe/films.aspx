<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="films.aspx.cs" Inherits="Pathe.films" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<form id="form1" runat="server">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    <div class="jumbotron">
        <h1>Film overzicht</h1>
        <p><strong>Action: </strong><%= Page.RouteData.Values["action"] %><br/>
            <strong>Page: </strong><%= Page.RouteData.Values["page"] %><br/></p>
    </div>
    <div class="row">
        <div class="col-lg-2">
            <div class="well">
                <h3>Filter</h3><br/>

            </div>
        </div>
        <div class="col-lg-10">
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="FilmsDataSource">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="well ">
                            <div class="movie_thumb holderjs" data-background-src="?holder.js/175x250?auto=yes&amp;textmode=exact">
                                
                            </div>
                            <div class="movie_info">
                                <h4 class="movie_title"><%# Eval("Titel") %></h4>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            
        </div>
    </div>
    <asp:SqlDataSource ID="FilmsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM FILM f ORDER BY f.Titel"></asp:SqlDataSource>

</div>
<!--#include file="inc/js.aspx"-->
        
</form>
</body>
</html>
