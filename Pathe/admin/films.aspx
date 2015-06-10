<%@ Page Title="Films - Pathé" Language="C#" MasterPageFile="~/inc/Pathe_admin.master" CodeBehind="films.aspx.cs" Inherits="Pathe.admin.films" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    <form id="FilmsForm" runat="server">
    <div class="jumbotron">
        <h1>Film overzicht</h1>
        <p><a href="/Admin/Films/Add" class="floating-action"><i class="btn btn-success btn-fab btn-raised mdi-content-add"></i></a></p>
        
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
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="well ">
                            <div class="movie_info">
                                <h4 class="movie_title"><%# Eval("Titel") %></h4>
                            </div>
                            <a href="/Admin/Film/<%# Eval("FilmID") %>-<%# FormatTitleUrl(Eval("Titel")) %>/del" class="col-lg-6"><i class="btn btn-danger btn-fab btn-raised mdi-content-remove-circle"></i></a>&nbsp;
                            <a href="/Admin/Film/<%# Eval("FilmID") %>-<%# FormatTitleUrl( Eval("Titel")) %>" class="col-lg-6 push-right"><i class="btn btn-info btn-fab btn-raised mdi-action-info push-right"></i></a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            
        </div>
    </div>
    <asp:SqlDataSource ID="FilmsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM FILM f ORDER BY f.Titel"></asp:SqlDataSource>
        </form>
</asp:Content>
