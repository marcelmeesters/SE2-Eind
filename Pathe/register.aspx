<%@ Page Title="Pathé - Login" Language="C#" MasterPageFile="~/inc/Pathe_user.Master" CodeBehind="register.aspx.cs" Inherits="Pathe.register"%>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    <div class="row">
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" MembershipProvider="OracleProvider">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" />
                <asp:CompleteWizardStep runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
</asp:Content>
