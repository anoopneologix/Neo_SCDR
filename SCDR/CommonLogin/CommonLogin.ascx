<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonLogin.ascx.cs" Inherits="SCDR.CommonLogin.CommonLogin" %>

<
 
 <script language="javascript" type="text/javascript" >
 
 $(document).ready(function () {
 //Hidding unnecessary elements from masterpage
 $('#s4-simple-card').attr("id", "s4-simple-card2");
 $('#s4-simple-header').hide();
 $('#s4-simple-card-top').hide();
 $('#s4-simple-card-content').css("margin", "0px");
 $('.s4-simple-iconcont').hide();
 $('#s4-simple-content').css("margin", "0px");
 $('#s4-simple-content h1:first').hide();
 $('.s4-die').hide();
 //-------------------
 
 //Round corners of the div
 $('.rounded').corner("10px");
 });
 </script>
 
 <style type="text/css">
 
 html, body {height:100%; margin:0; padding:0;}
 
 #page-background {position:fixed; top:0; left:0; width:100%; height:100%;}
 
 #content {position:relative; z-index:1; padding:10px;}
 
 </style>

<div style="background-color: Black; width: 100%; height: 100px">
 <br />
 <img src='/_layouts/images/zavaz.CustomLoginPage/Logo.png' alt='' />
 </div>
 <div style="background-color: Gray; width: 100%; height: 10px"></div>
 <center>
 <div id="content" class="rounded" style="width: 300px; height: auto; margin-top: 15%; background:transparent; filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000,endColorstr=#99000000); zoom: 1;">
 
 //Standard Login control with set custom MembershipProvider FBAMembershipProvider
 <asp:Login ID="signInControl" style="width: 250px" FailureText="Failed to login" MembershipProvider="FBAMembershipProvider"
 runat="server" DisplayRememberMe="true" TextBoxStyle-Width="250px" RememberMeSet="true" LoginButtonStyle-CssClass="ms-buttonheightwidth" UserNameLabelText="User name" TextLayout="TextOnTop" PasswordLabelText="Password" LabelStyle-Font-Bold="false" LabelStyle-Font-Size="Large" LabelStyle-ForeColor="White" LabelStyle-Font-Names="Calibri" LabelStyle-CssClass="ms-standardheader ms-inputformheader" TextBoxStyle-CssClass="ms-input" CheckBoxStyle-Font-Bold="false" CheckBoxStyle-Font-Names="Calibri" CheckBoxStyle-ForeColor="White" CheckBoxStyle-CssClass="ms-standardheader ms-inputformheader" CheckBoxStyle-Font-Size="Large" FailureTextStyle-Wrap="true" FailureTextStyle-Font-Names="Calibri" FailureTextStyle-Font-Size="Small" LoginButtonStyle-Font-Names="Calibri" LoginButtonStyle-Font-Size="Large" LoginButtonImageUrl="/_layouts/images/zavaz.CustomLoginPage/GoForward.png" LoginButtonType="Image" TitleText="" TitleTextStyle-ForeColor="White" TitleTextStyle-Font-Bold="true" TitleTextStyle-Wrap="true" TitleTextStyle-Font-Names="Calibri" TitleTextStyle-Font-Size="Larger" />
 
 //Link for internal users to log in using AD account
 <asp:LinkButton ID="lbInternalUsers" Text="Active Directory Login" runat="server" Font-Names="Calibri" Font-Size="Small" CssClass="ms-standardheader ms-inputformheader" Font-Bold="true" ForeColor="Wheat" OnClick="lbInternalUsers_OnClick" />
 
 //Label displaying errors
 <asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="Red" EnableViewState="false"></asp:Label>
 </div>
 </center>
 
 //Required from inherited class "FormsSignInPage"
 <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageMessage" Visible="false" />
 
