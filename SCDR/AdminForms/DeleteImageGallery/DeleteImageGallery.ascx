<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteImageGallery.ascx.cs" Inherits="SCDR.AdminForms.DeleteImageGallery.DeleteImageGallery" %>
<!-- Form Begins -->
<div class="form-horizontal">
    <div class="form-group">
       <label  class="col-sm-3 control-label">Select Language : </label>
    <div class="col-sm-9">
        <asp:RadioButton GroupName="grpLanguage" Text="Arabic"  AutoPostBack="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged" />
          <asp:RadioButton GroupName="grpLanguage" Text="English" AutoPostBack="true" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged"  />
    </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Category Name : </label>
    <div class="col-sm-9">
        <asp:DropDownList ID="ddlCategoryName" runat="server"></asp:DropDownList>
  </div>
  </div>
    <div class="form-group" id="divButton" runat="server">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnDelete"   OnClientClick="if ( ! CategoryDeleteConfirmation()) return false;"  Text="Delete" class="btn btn-default" runat="server" OnClick="btnDelete_Click"   />
         <asp:Button ID="btnCancel" Text="Cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click"    />
    </div>
  </div>
    </div>
<script>
function CategoryDeleteConfirmation() {
    return confirm("Are you sure you want to delete this category?");
}
    </script>