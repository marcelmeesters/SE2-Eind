<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.film" %>
<%@ Import Namespace="System.IO" %>
<% if((string) Page.RouteData.Values["film"] == "none") Response.Redirect("/Films"); %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    
    <div class="jumbotron">
        <h1><%= Page.RouteData.Values["film"] %></h1>
        <p><strong>Film ID: </strong><%= Convert.ToString(Page.RouteData.Values["film"]).Substring(0, Convert.ToString(Page.RouteData.Values["film"]).IndexOf("-")) %><br/>
            <strong>Action: </strong><%= Page.RouteData.Values["action"] %><br/>
            <strong>vars: </strong><%= Page.RouteData.Values["vars"] %><br/>
        </p>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="well movie_info">
                <img src="holder.js/175x250" />
                <p>
                    Dit is informatie over de film :D
                </p>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="well movie_extra">
                Extra info
            </div>
            
        </div>
    </div>
    
</div>
<!--#include file="inc/js.aspx"-->
</body>
</html>
