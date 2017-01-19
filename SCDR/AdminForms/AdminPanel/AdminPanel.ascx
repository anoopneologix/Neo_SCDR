<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.ascx.cs" Inherits="SCDR.AdminForms.AdminPanel.AdminPanel" %>
<style>

    .dashboard-stat .more {
    clear: both;
    display: block;
    padding: 6px 10px 6px 10px;
    position: relative;
    text-transform: uppercase;
    font-weight: 300;
    font-size: 11px;
    opacity: 0.7;
    filter: alpha(opacity=70);
}
    .row {
    margin-right: -15px;
    margin-left: -15px;
}
    .col-lg-3 {
    width: 25%;
}

    .col-lg-1, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-10, .col-lg-11, .col-lg-12 {
    float: left;
}
    .col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 {
    position: relative;
    min-height: 1px;
    padding-right: 15px;
    padding-left: 15px;
}
    .dashboard-stat.blue-madison {
    background-color: #578ebe;
}
    .dashboard-stat {
    margin-bottom: 25px;
    overflow: hidden;
}

    .dashboard-stat .visual {
    width: 80px;
    height: 80px;
    display: block;
    float: left;
    padding-top: 10px;
    padding-left: 15px;
    margin-bottom: 15px;
    font-size: 35px;
    line-height: 35px;
}
    .dashboard-stat.blue-madison .visual > i {
    color: white;
    opacity: 0.3;
    filter: alpha(opacity=30);
}
    .dashboard-stat .visual > i {
    margin-left: -27px;
    font-size: 110px;
    line-height: 110px;
}
   
    .dashboard-stat .details {
    position: absolute;
    right: 15px;
    padding-right: 15px;
}
    .dashboard-stat.blue-madison .details .number {
    color: white;
}
    .dashboard-stat .details .number {
    padding-top: 25px;
    text-align: right;
    font-size: 34px;
    line-height: 36px;
    letter-spacing: -1px;
    margin-bottom: 0px;
    font-weight: 300;
}
    .dashboard-stat.blue-madison .more {
    color: white;
    background-color: #4884b8;
}

    .dashboard-stat.blue-madison .details .desc {
    color: white;
    opacity: 0.8;
    filter: alpha(opacity=80);
}
    .dashboard-stat .details .desc {
    text-align: right;
    font-size: 16px;
    letter-spacing: 0px;
    font-weight: 300;
}

    .dashboard-stat.red-intense {
    background-color: #e35b5a;
}

    .dashboard-stat.red-intense .visual > i {
    color: white;
    opacity: 0.3;
    filter: alpha(opacity=30);
}
    .dashboard-stat.red-intense .details .number {
    color: white;
}
    .dashboard-stat.red-intense .details .desc {
    color: white;
    opacity: 0.8;
    filter: alpha(opacity=80);
}
    .dashboard-stat.red-intense .more {
    color: white;
    background-color: #e04a49;
}

    .dashboard-stat.purple-plum {
    background-color: #8775a7;
}
    .dashboard-stat.purple-plum .visual > i {
    color: white;
    opacity: 0.3;
    filter: alpha(opacity=30);
}
    .dashboard-stat.purple-plum .details .number {
    color: white;
}
    .dashboard-stat.purple-plum .details .desc {
    color: white;
    opacity: 0.8;
    filter: alpha(opacity=80);
}
    .dashboard-stat.purple-plum .more {
    color: white;
    background-color: #7c699f;
}
</style>
<div class="row">
				<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
					<div class="dashboard-stat blue-madison">
						<div class="visual">
							<i class="fa fa-comments"></i>
						</div>
						<div class="details">
							<div class="number">
                                <%--<asp:Label ID="LblReqNum" runat="server" Text="Label"></asp:Label>--%>
							</div>
							<div class="desc">
								 New Request
							</div>
						</div>
						<a class="more" href="/sites/scdr/SitePages/Services_Menu.aspx">
						View more <i class="m-icon-swapright m-icon-white"></i>
						</a>
					</div>
				</div>
				<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
					<div class="dashboard-stat red-intense">
						<div class="visual">
							<i class="fa fa-bar-chart-o"></i>
						</div>
						<div class="details">
							<div class="number">
                                <asp:Label ID="LblRate" runat="server" Text="Label">4</asp:Label> Stars
							</div>
							<div class="desc">
								 Ratting
							</div>
						</div>
						<a class="more" href="/sites/scdr/SitePages/Ratting%20and%20Feedback.aspx">
						View more <i class="m-icon-swapright m-icon-white"></i>
						</a>
					</div>
				</div>
				
				<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
					<div class="dashboard-stat purple-plum">
						<div class="visual">
							<i class="fa fa-globe"></i>
						</div>
						<div class="details">
							<div class="number">
                                <asp:Label ID="lblViews" runat="server" Text="Label">100</asp:Label>
							</div>
							<div class="desc">
								 Views
							</div>
						</div>
						<a class="more" href="/sites/SCDR/_layouts/15/Reporting.aspx?Category=AnalyticsSiteCollection">
						View more <i class="m-icon-swapright m-icon-white"></i>
						</a>
					</div>
				</div>
			</div>