<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerImageSliderAr.ascx.cs" Inherits="SCDR.BannerImageSliderAr.BannerImageSliderAr" %>
<!-- Banner Slider division begins -->
<asp:Repeater ID="repBannerSlider" runat="server">
    <HeaderTemplate>
        <section class="banner_outer  clearfix">
            <div class="banner_carosal col-md-12 col-sm-12 col-xs-12">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item">
            <asp:Image ID="imgBannerImage" ImageUrl='<%# Eval("ImageUrl") %>' runat="server" />
          
            <div class="banner_caption" style="text-align:right">
                <h1><%# Eval("Description") %></h1>
                <h3><%# Eval("Person") %></h3>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
        </section>

    </FooterTemplate>
</asp:Repeater>
<!-- Banner division ends -->
