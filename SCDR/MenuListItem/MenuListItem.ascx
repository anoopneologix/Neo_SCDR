<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuListItem.ascx.cs" Inherits="SCDR.MenuListItem.MenuListItem" %>
<asp:Repeater ID="repMenuItem" runat="server" OnItemDataBound="repMenuItem_ItemDataBound">
    <HeaderTemplate>
          <ul>
    </HeaderTemplate>
    <ItemTemplate>
          <li><a  href='<%# Eval("linkUrl") %>'><%# Eval("Title") %></a>
        <asp:Repeater ID="repChildMenuItem" runat="server">
             <HeaderTemplate>
          <ul>
    </HeaderTemplate>
            <ItemTemplate>
          <li><a href='<%# Eval("linkUrl") %>'><%# Eval("Title") %></a></li>
                </ItemTemplate>
             <FooterTemplate>
        </ul>
    </FooterTemplate>
        </asp:Repeater>
        </li>  <asp:HiddenField ID="hfParentItem" runat="server" Value='<%# Eval("Title") %>' />                            
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
