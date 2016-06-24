<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAtSCDR.ascx.cs" Inherits="SCDR.NewAtSCDR.NewAtSCDR" %>


<asp:Repeater ID="repEventsSlider" runat="server">
    <HeaderTemplate>
 
                <span class="radius font_resize slider1 ovreflow_hidden">
             
<h3 class="news_heading"><asp:Label ID="lblHeading" runat="server" ></asp:Label></h3>
<div class="owl_carosal1">
    </HeaderTemplate>
    <ItemTemplate>
  <div class="item"><img src='<%# Eval("ImageUrl") %>' alt="" onclick="lightbox(0)"></div>

    </ItemTemplate>
    <FooterTemplate>
</div>
        </span>

         
    </FooterTemplate>
</asp:Repeater>


   
 <!--2 column popup-->
    <div style="display:none;">
        <div id="ninja-slider">
            <div class="slider-inner">
                <div id="fsBtn" class="fs-icon" title="Expand/Close"></div>
                <ul>
                    <asp:Repeater ID="repEventPop" runat="server">
                        <ItemTemplate>
                    <li>
                        <a class="ns-img" href='<%# Eval("ImageUrl") %>'></a>
                        <div class="caption">
                             <p>
                           <%# Eval("Description") %>
                            </p>
                        </div>
                    </li>
                            </ItemTemplate>
                    </asp:Repeater>
                   
                </ul>

            </div>
        </div>
    </div>
    <!--2 column popup END-->