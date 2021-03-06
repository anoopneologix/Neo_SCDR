﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageGalleryAr.ascx.cs" Inherits="SCDR.ImageGalleryAr.ImageGalleryAr" %>
<link rel="stylesheet" href="../../_layouts/15/SCDR/css/PressPagination.css" />
<asp:UpdatePanel ID="UpdatePanel2"  runat="server">

    <ContentTemplate>
<asp:Repeater ID="repGroupName" runat="server" OnItemDataBound="repGroupName_ItemDataBound">
    <HeaderTemplate>
<h4 class="gallery_title">معرض الصور</h4>
    </HeaderTemplate>
<ItemTemplate>
 
                <div class="col-md-4 col-xs-12 col-sm-6 gallery-image-thumb">
                    <a class="fancybox" href='<%# Eval("dtThumbnailUrl") %>' data-fancybox-group='<%# Eval("dtCategoryName") %>' title='<%# Eval("dtTitle") %>'>
                        <img src='<%# Eval("dtThumbnailUrl") %>' alt="" />
                    </a>
                    <div class="gallerydes"><%# Eval("Title") %></div>
                </div>
     <asp:Repeater ID="repHideGroupName" runat="server">
<ItemTemplate>
                <div class="col-md-4 col-xs-12 col-sm-6 gallery-image-thumb hide">
                    <a class="fancybox" href='<%# Eval("ImageUrl") %>' data-fancybox-group='<%# Eval("CategoryName") %>' title='<%# Eval("Title") %>'>
                        <img src='<%# Eval("ImageUrl") %>' alt="" />
                    </a>
                   <div class="gallerydes"><%# Eval("Title") %></div>
                </div>
    </ItemTemplate>
        </asp:Repeater>
      <asp:HiddenField ID="hfCategoryName" runat="server" Value='<%# Eval("dtCategoryName") %>' />
     </ItemTemplate>
    </asp:Repeater>
   
   <div class="clear"></div>
<div id="repPaging">
<asp:Repeater ID="rptPager" runat="server">
    <ItemTemplate>
        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
   </ItemTemplate>
</asp:Repeater>
    </div>
        </ContentTemplate>
   </asp:UpdatePanel>