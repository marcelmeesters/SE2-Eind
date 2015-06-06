<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Pathe.admin._default" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="/inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    
    <form id="form1" runat="server">
        <div class="container">
            <strong>Action: </strong><%= Page.RouteData.Values["action"] %><br/>
            <strong>Film: </strong><%= Page.RouteData.Values["film"] %><br/>
            <strong>Vars: </strong><%= Page.RouteData.Values["vars"] %>
        </div>
    </form>

</div>
<!--#include file="/inc/js.aspx"-->
</body>
</html>

