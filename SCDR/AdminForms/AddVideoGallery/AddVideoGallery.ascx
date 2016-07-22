<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddVideoGallery.ascx.cs" Inherits="SCDR.AdminForms.AddVideoGallery.AddVideoGallery" %>
<!-- Form Begins -->
<div class="form-horizontal">
      <div class="form-group">
       <label  class="col-sm-3 control-label">Select Language : </label>
    <div class="col-sm-6">
        <asp:RadioButton GroupName="grpLanguage" Text="Arabic" Checked="true" ID="rbArabic" runat="server" />
          <asp:RadioButton GroupName="grpLanguage" Text="English" ID="rbEnglish" runat="server" />
    </div>

  </div>
      <div class="form-group">
    <label  class="col-sm-3 control-label">Title: </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtTitle" MaxLength="250" runat="server" class="form-control"></asp:TextBox>
  </div>
          <div class="col-sm-3">
            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtTitle" runat="server" ErrorMessage="Please enter a title"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[-_a-zA-Z\u0621-\u064A0-9,() ]+$"
    ControlToValidate="txtTitle" runat="server" ForeColor="Red" ErrorMessage="Maximum 250 characters allowed. Special characters except - _ , ( ) are not allowed"
    Display="Dynamic" />
       </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Url of the Video : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtVideoUrl" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
        <div class="col-sm-3">
            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtVideoUrl" runat="server" ErrorMessage="Please enter a valid youtube Url"></asp:RequiredFieldValidator>
 
       </div>
  </div>
   

  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ValidationGroup="chk" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click"  />
    </div>
  </div>
    </div>

<!-- Form Ends -->