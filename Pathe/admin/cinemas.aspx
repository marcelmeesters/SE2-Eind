<%@ Page Title="" Language="C#" MasterPageFile="~/inc/Pathe_admin.master" AutoEventWireup="true" CodeBehind="cinemas.aspx.cs" Inherits="Pathe.admin.cinemas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="jumbotron">
        <h1>Bioscoopoverzicht</h1>
        <p>
            Beheer alle bioscopen..
        </p>
    </div>
    <div class="row">
        <div class="col-lg-3">
            <div class="well" runat="server">
            </div>
        </div>
        <div class="col-lg-9">
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Cinemas">
                <ItemTemplate>
                    <a href="/Admin/Cinema/<%# Eval("BioscoopID") %>-<%# FormatUrl( Eval("Naam")) %>">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="well ">
                                <div class="movie_info">
                                    <h4 class="movie_title"><%# Eval("Naam") %></h4>
                                </div>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            
        </div>
    </div>
    <asp:SqlDataSource ID="Cinemas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM BIOSCOOP b ORDER BY b.Naam"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceholder" runat="server">
</asp:Content>
