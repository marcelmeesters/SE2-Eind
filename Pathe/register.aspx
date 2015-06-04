<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Pathe.register" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html>
<html lang="en">
<!--#include file="inc/header.aspx"-->
<body role="document">
<!--#include file="inc/menu.aspx"-->
<div class="container">
    <div class="row">
        <form id="registerform" runat="server">
            
            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" MembershipProvider="MyOracleMembershipProvider">
                <WizardSteps>
                    <asp:CreateUserWizardStep runat="server" />
                    <asp:CompleteWizardStep runat="server" />
                </WizardSteps>
            </asp:CreateUserWizard>
            
        </form>
    </div>
</div> <!-- /container -->

<!--#include file="inc/js.aspx"-->
</body>
</html>
