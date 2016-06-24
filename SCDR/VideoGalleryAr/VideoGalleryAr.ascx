<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoGalleryAr.ascx.cs" Inherits="SCDR.VideoGalleryAr.VideoGalleryAr" %>
  <h4 class="gallery_title">معرض الفيديو</h4>

                <div class="col-md-12 col-xs-12 col-sm-12 gallery-image-thumb">
                    <a id="topDiv" runat="server" class="fancybox fancybox-media">
                        <iframe id="topDivFrame" runat="server"  frameborder="0" allowfullscreen></iframe>
                    </a>
                </div>
<asp:Repeater runat="server" ID="repVideoGallery">
  
    <ItemTemplate>
          <div class="col-md-4 col-xs-12 col-sm-6 gallery-image-thumb">
                    <a class="fancybox fancybox-media" href='<%# Eval("VideoUrl") %>'>
                        <iframe src='<%# Eval("VideoUrl") %>' frameborder="0" allowfullscreen></iframe>
                    </a>
                </div>
    </ItemTemplate>
</asp:Repeater>