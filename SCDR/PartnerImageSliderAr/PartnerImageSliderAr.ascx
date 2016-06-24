﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartnerImageSliderAr.ascx.cs" Inherits="SCDR.PartnerImageSliderAr.PartnerImageSliderAr" %>
<!-- Partner Slider division begins -->
 <div class="clear"></div>

<asp:Repeater ID="repPartnerSlider" runat="server">
    <HeaderTemplate>
 

            <div class="outerpartner">
                <div class="head_out">
                <h3 class="news_heading font_resize"> <asp:Label ID="lblHeading" runat="server" ></asp:Label></h3></div>
                <div class="partner_carousel">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item font_resize">
                        <a href='<%# Eval("SiteUrl") %>'>
                            <img src='<%# Eval("ImageUrl") %>' />
                        </a>
                    </div>
    </ItemTemplate>
    <FooterTemplate>
          </div>
            </div>
      
    </FooterTemplate>
</asp:Repeater>
 
    <div class="clear"></div>
<!-- Partner Slider division ends -->