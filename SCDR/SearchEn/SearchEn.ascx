<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchEn.ascx.cs" Inherits="SCDR.SearchEn.SearchEn" %>
<!--Code Begins-->
<div class="search_outer">
    <button class="search_btn">
        <i class="fa"></i>
    </button>
    <span class="input_outer"><span
        class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="btnSearch" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="Search" ></asp:TextBox>
       </span> </span>
</div>
<!--Code Ends-->