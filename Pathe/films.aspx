<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="films.aspx.cs" Inherits="Pathe.films" %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    <div class="jumbotron">
        <h1>Film overzicht</h1>
        <p><strong>Action: </strong><%= Page.RouteData.Values["action"] %><br/>
            <strong>Page: </strong><%= Page.RouteData.Values["page"] %><br/></p>
    </div>
    <div class="row">
        <div class="col-lg-3">
            <div class="well" runat="server">
                <h3>Sorteer Op</h3><br/>
                    <a runat="server" id="btnSortTitle" href="/Films" class="btn btn-primary btn-small">Titel</a><br/>
                    <a runat="server" id="btnSortDate" href="/Films" class="btn btn-primary btn-small">Release Date</a><br/>
                    <a runat="server" id="btnSortDuration" href="/Films" class="btn btn-primary btn-small">Duur</a><br/>
                    <a runat="server" id="btnSortId" href="/Films" class="btn btn-primary btn-small">Film ID</a><br/>
                
                
                <h3>Volgorde</h3><br/>
                    <a runat="server" id="btnAsc" href="/Films" class="btn btn-primary btn-small">Oplopend</a><br/>
                    <a runat="server" id="btnDesc" href="/Films" class="btn btn-primary btn-small">Aflopend</a><br/>

            </div>
        </div>
        <div class="col-lg-9">
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="FilmsDataSource">
                <ItemTemplate>
                    <a href="/Film/<%# Eval("FilmID") %>-<%# FormatTitleUrl( Eval("Titel")) %>">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="well ">
                                <div class="movie_info">
                                    <h4 class="movie_title"><%# Eval("Titel") %></h4>
                                </div>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            
        </div>
    </div>
    <asp:SqlDataSource ID="FilmsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM FILM f ORDER BY f.Titel"></asp:SqlDataSource>

</div>
<!--#include file="inc/js.aspx"-->
</body>
</html>
