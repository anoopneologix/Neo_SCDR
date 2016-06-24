<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InitiativesSliderAr.ascx.cs" Inherits="SCDR.InitiativesSliderAr.InitiativesSliderAr" %>
<asp:Repeater ID="repOurInitiativesSlider" runat="server" OnItemDataBound="repOurInitiativesSlider_ItemDataBound"  >
  <HeaderTemplate>
      <div class="right_side_container font_resize initiatives-block">
       <h3 class="news_heading font_resize">
           <asp:Label ID="lblHeading" runat="server" ></asp:Label></h3>
                <div class="owl_carosal">
  </HeaderTemplate>
    <ItemTemplate>
       <div class="item">
                        <img src='<%# Eval("ImageUrl") %>'>
                        <h4 class="font_resize"><%# Eval("Title") %></h4>
                        <p class="item_desc_wrap font_resize read-block1">
                            <span class="item_desc"><%# Eval("InitDescription") %></span>

                        </p>
          
                <a id="ancReadMore" runat="server" class="read_more" href='<%# Eval("PageUrl") %>'></a>      
                    </div>
    </ItemTemplate>
<FooterTemplate>
    </div>
    </div>
</FooterTemplate>
</asp:Repeater>