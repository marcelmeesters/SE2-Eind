<%@ Page Title="Registreren - Pathé" Language="C#" MasterPageFile="~/inc/Pathe_user.Master" CodeBehind="register.aspx.cs" Inherits="Pathe.register"%>
<asp:Content ID="MainContent1" ContentPlaceHolderID="ContentPlaceholder" runat="server" >
    <div class="row">
        <form runat="server">
            <div class="col-lg-6 col-lg-offset-3">
                <div class="well">
                    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" MembershipProvider="OracleProvider" Width="100%">
                        <WizardSteps>
                            <asp:CreateUserWizardStep runat="server" >
                                <ContentTemplate>
                                                <asp:TextBox ID="UserName" runat="server" cssclass="form-control floating-label" placeholder="Gebruikersnaam"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Vul aub een gebruikersnaam in" ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:TextBox ID="Password" runat="server" TextMode="Password" cssclass="form-control floating-label" placeholder="Wachtwoord"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Vul aub een wachtwoord in" ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" cssclass="form-control floating-label" placeholder="Wachtwoord (nogmaals)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Vul uw wachtwoord aub nogmaals in" ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:TextBox ID="Email" runat="server" cssclass="form-control floating-label" placeholder="Email adres"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="Vul aub een email adres in" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:TextBox ID="Question" runat="server" cssclass="form-control floating-label" placeholder="Beveiligingsvraag"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:TextBox ID="Answer" runat="server" cssclass="form-control floating-label" placeholder="Beveiligingsvraag antwoord"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Vul aub een beveiligingsvraag in" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="De twee ingevulde wachtwoorden komen niet overeen" ValidationGroup="CreateUserWizard1"></asp:CompareValidator>

                                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>

                                </ContentTemplate>
                                <CustomNavigationTemplate>
                                    <table border="0" cellspacing="5" style="width:100%;height:100%;">
                                        <tr align="right">
                                            <td align="right" colspan="0">
                                                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Registreer" ValidationGroup="CreateUserWizard1" CssClass="btn btn-primary" />
                                            </td>
                                        </tr>
                                    </table>
                                </CustomNavigationTemplate>
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep runat="server" >
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">Klaar!</td>
                                        </tr>
                                        <tr>
                                            <td>Uw account is succesvol geregistreerd</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Volgende" ValidationGroup="CreateUserWizard1" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                        <FinishNavigationTemplate>
                            <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Vorige" CssClass="btn btn-warning btn-flat"/>
                            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="Klaar" css="btn btn-success" />
                        </FinishNavigationTemplate>
                        <StartNavigationTemplate>
                            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Volgende" CssClass="btn btn-primary"/>
                        </StartNavigationTemplate>
                        <StepNavigationTemplate>
                            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Vorige" CssClass="btn btn-warning btn-flat"/>
                            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Volgende" CssClass="btn btn-primary"/>
                        </StepNavigationTemplate>
                    </asp:CreateUserWizard>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
