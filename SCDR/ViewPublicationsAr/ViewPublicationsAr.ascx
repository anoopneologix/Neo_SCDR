<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewPublicationsAr.ascx.cs" Inherits="SCDR.ViewPublicationsAr.ViewPublicationsAr" %>
<div class="col-md-12">
    <asp:Repeater runat="server" ID="repPublication">
  
    <ItemTemplate>
          <div class="col-md-4 col-xs-12 col-sm-6 gallery-image-thumb" style="text-align: center;">​ 
            <a href='<%# Eval("PdfUrl") %>' target="_blank"> 
               <img src='<%# Eval("ImageUrl") %>' alt=""/> </a>
            <div class="gallerydes pub_desc"> 
               <a href='<%# Eval("PdfUrl") %>' target="_blank"><%# Eval("Title") %></a></div>
         </div>
           </ItemTemplate>
        </asp:Repeater>
        </div>