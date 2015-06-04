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
            <asp:Login ID="Login2" runat="server" CreateUserUrl="register.aspx" CssClass="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-sm-8 col-sm-offset-2 col-xs-12" PasswordRecoveryUrl="forgot.aspx" DestinationPageUrl="default.aspx" FailureText="De ingevoerde gegevens zijn niet bekend. Probeer het nogmaals." RememberMeSet="True">
                <LayoutTemplate>
                    <div class="well">
                        <h2>Log in</h2><br/>
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><br/>
                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control floating-label" placeholder="Gebruikersnaam"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                    
                        <asp:TextBox ID="Password" runat="server" CssClass="form-control floating-label" TextMode="Password" placeholder="Wachtwoord"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login2">*</asp:RequiredFieldValidator>
                        
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="RememberMe" runat="server" Text="&nbsp; Onthoud mij" />
                            </label>
                        </div>
                        
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-lg btn-primary btn-block" Text="Log In" ValidationGroup="Login2" />
                    </div>
                </LayoutTemplate>
                <LoginButtonStyle CssClass="btn btn-lg btn-primary btn-block" />
                <TextBoxStyle CssClass="form-control floating-label" />
            </asp:Login>
        </form>
    </div>
</div> <!-- /container -->

<!--#include file="inc/js.aspx"-->
</body>
</html>
