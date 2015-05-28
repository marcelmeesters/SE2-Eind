<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="film.aspx.cs" Inherits="Pathe.film" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>
    Expense Report for
    <asp:Literal ID="prettyurl" 
      Text="<%$RouteValue:prettyurl%>" 
      runat="server"></asp:Literal>
</h1>
    </div>
    </form>
</body>
</html>
