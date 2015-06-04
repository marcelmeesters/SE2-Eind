<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Pathe.login" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    <div class="row">
        <form id="loginform" runat="server">
        <asp:Login ID="Login1" runat="server" RememberMeSet="True" cssclass="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-sm-8 col-sm-offset-2 col-xs-12" MembershipProvider="SqlProvider">
            <LayoutTemplate>
                <div class="well">
                    <h2 class="form-signin-heading">Log in om verder te gaan</h2>
                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control floating-label" placeholder="Gebruikersnaam"> </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="UserNameRequired" ControlToValidate="UserName" ErrorMessage="Je moet een gebruikersnaam invoeren" ToolTip="Je moet een gebruikersnaam invullen" ValidationGroup="Login1"></asp:RequiredFieldValidator>
                            
                    <asp:TextBox runat="server" ID="Password" CssClass="form-control floating-label" placeholder="Wachtwoord" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="PasswordRequired" ControlToValidate="Password" ErrorMessage="Je moet een wachtwoord invoeren" ToolTip="Je moet een wachtwoord invullen" ValidationGroup="Login1"></asp:RequiredFieldValidator>
                            
                    <asp:Button runat="server" cssclass="btn btn-lg btn-primary btn-block" Text="Log in"/>
                    <div class="checkbox">
                        <label>
                        <input type="checkbox" value="remember-me"> Remember me
                        </label>
                    &nbsp;&nbsp;&nbsp;</div>
                </div>
            </LayoutTemplate>
        </asp:Login>
        </form>
    </div>
</div> <!-- /container -->

<!--#include file="inc/js.aspx"-->
</body>
</html>
