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
                <div class="row">
                    <div class="col-lg-4">
                        <img src="holder.js/175x250" class="movie_poster"/>
                    </div>
                    <div class="col-lg-8">
                        <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div id="myCarousel" class="carousel slide" data-ride="carousel">
                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" role="listbox">
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlImageSource1">
                                    <ItemTemplate>
                                        <div class="item">
                                            <img src="/img/upload/<%# Eval("FILMID") %>/<%# Eval("FILENAME") %>" alt="Image">
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>
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
    <asp:SqlDataSource ID="SqlImageSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT FILMID, FILENAME FROM PATHE.FILM_IMAGE WHERE (FILMID = 1)"></asp:SqlDataSource>
</asp:Content>