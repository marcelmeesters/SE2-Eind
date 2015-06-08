<%@ Page Title="Pathé - Admin" MasterPageFile="~/inc/Pathe_admin.master" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Pathe.admin._default" %>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    
    <form id="form1" runat="server">
        <div class="container">
            <strong>Action: </strong><%= Page.RouteData.Values["action"] %><br/>
            <strong>Film: </strong><%= Page.RouteData.Values["film"] %><br/>
            <strong>Vars: </strong><%= Page.RouteData.Values["vars"] %>
        </div>
    </form>

</asp:Content>
