<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBanner.ascx.cs" Inherits="SCDR.AdminForms.AddBanner.AddBanner" %>
<!-- Form Begins -->
<div class="form-horizontal">
      <div class="form-group">
       <label  class="col-sm-3 control-label">Select Language : </label>
    <div class="col-sm-9">
        <asp:RadioButton GroupName="grpLanguage" Text="Arabic" Checked="true" ID="rbArabic" runat="server" />
          <asp:RadioButton GroupName="grpLanguage" Text="English" ID="rbEnglish" runat="server" />
    </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Title : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtBannerDescription" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Person : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtBannerPerson"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>

<div class="form-group">
       <label  class="col-sm-3 control-label">Upload Banner Image : </label>
    <div class="col-sm-9">
        <asp:FileUpload ID="fuBannerImage"  runat="server" />
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click"  />
    </div>
  </div>
     
</div>

<!-- Form Ends -->
