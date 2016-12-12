<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressReleasesList.ascx.cs" Inherits="SCDR.News.PressReleasesList.PressReleasesList" %>
<link rel="stylesheet" href="../../_layouts/15/SCDR/css/PressPagination.css" />
 <!----neewsrelease begins here---->
<asp:Repeater ID="repNewsList" runat="server">
    <ItemTemplate>
   <div class="newsrelease">
    <h4 class="inti_subhead font_resize"><%# Eval("Title") %></h4>
    <img class="img-thumbnail" src='<%# Eval("ImageUrl") %>' />
    <p class="newsdate font_resize"> <i class="fa fa-calendar-check-o"></i> Publication date: <%# Eval("Date", "{0:d}") %></p>
    <p class="font_resize boldText"><%# Eval("Location") %> :</p>
    <p class="releasecontent font_resize">
       <div class="content_limit fontresizer"> <%# Eval("Description") %> </div> </p>
    <a  href='<%# Eval("PageID") %>' class="newsread font_resize">read more</a>
       </div>     
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
                            <!----neewsrelease ends here---->