<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="Pathe.inc.User_Controls.footer" %>
<%@ Import Namespace="System.IO" %>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>

    <script src="/js/material.min.js"></script>
    <script src="/js/ripples.min.js"></script>
    <script src="/js/holder.min.js"></script>

    <script type="text/javascript">
    $.material.init();
    </script>
      
    <!-- Add active class to current menu item -->
    <script type="text/javascript">
        <% string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath); %>
        $("#menu_<%=pageName%>").addClass("active");
    </script>    