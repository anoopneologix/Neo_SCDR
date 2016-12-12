﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResultListAr.ascx.cs" Inherits="SCDR.SearchResultListAr.SearchResultListAr" %>
<section class="container resiazable">

<div class="col-md-12 col-sm-12 col-xs-12">
    <asp:Repeater ID="rptrSearchResult" runat="server">
        <ItemTemplate>
           <div class="newsrelease search-results">
                  <h4 class="inti_subhead font_resize"><a href='<%# Eval("PageUrl") %>'><%# Eval("Title") %></a></h4>
            <p class="releasecontent font_resize"> <%# Eval("Content") %>  </p>
    <a  href='<%# Eval("PageUrl") %>' class="newsread font_resize" style='<%# Eval("disp") %>' >اقرأ المزيد</a>
       </div> 
    
        </ItemTemplate>
    </asp:Repeater>
    </div>
    </section>