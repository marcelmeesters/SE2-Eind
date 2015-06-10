<%@ Page Title="Mijn Profiel - Pathé" Language="C#" MasterPageFile="~/inc/Pathe_user.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Pathe.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="jumbotron">
        <h1>Profiel bewerken</h1>
        <p>Op deze pagina kan je je profiel aanpassen.</p>
    </div>
    <div class="row">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
            <div class="well">
                <div class="row">
                    <div class="col-lg-12 hidden-xs"><a href="#profile" data-toggle="tab" class="btn btn-primary btn-fab btn-raised mdi-action-perm-identity"></a><br/><br/><br/></div>
                    <div class="col-lg-12 hidden-xs"><a href="#friends" data-toggle="tab" class="btn btn-success btn-fab btn-raised mdi-social-group"></a></div>
                    <div class="col-xs-6 visible-xs"><a href="#profile" data-toggle="tab" class="btn btn-primary">Profiel</a></div>
                    <div class="col-xs-6 visible-xs"><a href="#friends" data-toggle="tab" class="btn btn-success">Vrienden</a></div>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
            <div class="well">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane fade active in" id="profile">
                        <p>Profiel</p>
                    </div>
                    <div class="tab-pane fade" id="friends">
                        <p>Vrienden</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
