﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36366
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCDR.AdminForms.AdminPanel {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class AdminPanel {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Label LblRate;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Label lblViews;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(AdminPanel target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Label @__BuildControlLblRate() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.LblRate = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "LblRate";
            @__ctrl.Text = "Label";
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("4"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Label @__BuildControllblViews() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblViews = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblViews";
            @__ctrl.Text = "Label";
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("100"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::SCDR.AdminForms.AdminPanel.AdminPanel @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<style>\r\n\r\n    .dashboard-stat .more {\r\n    clear: both;\r\n    display: block;\r\n" +
                        "    padding: 6px 10px 6px 10px;\r\n    position: relative;\r\n    text-transform: up" +
                        "percase;\r\n    font-weight: 300;\r\n    font-size: 11px;\r\n    opacity: 0.7;\r\n    fi" +
                        "lter: alpha(opacity=70);\r\n}\r\n    .row {\r\n    margin-right: -15px;\r\n    margin-le" +
                        "ft: -15px;\r\n}\r\n    .col-lg-3 {\r\n    width: 25%;\r\n}\r\n\r\n    .col-lg-1, .col-lg-2, " +
                        ".col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .co" +
                        "l-lg-10, .col-lg-11, .col-lg-12 {\r\n    float: left;\r\n}\r\n    .col-xs-1, .col-sm-1" +
                        ", .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, ." +
                        "col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col" +
                        "-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg" +
                        "-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8," +
                        " .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, " +
                        ".col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-" +
                        "12, .col-sm-12, .col-md-12, .col-lg-12 {\r\n    position: relative;\r\n    min-heigh" +
                        "t: 1px;\r\n    padding-right: 15px;\r\n    padding-left: 15px;\r\n}\r\n    .dashboard-st" +
                        "at.blue-madison {\r\n    background-color: #578ebe;\r\n}\r\n    .dashboard-stat {\r\n   " +
                        " margin-bottom: 25px;\r\n    overflow: hidden;\r\n}\r\n\r\n    .dashboard-stat .visual {" +
                        "\r\n    width: 80px;\r\n    height: 80px;\r\n    display: block;\r\n    float: left;\r\n  " +
                        "  padding-top: 10px;\r\n    padding-left: 15px;\r\n    margin-bottom: 15px;\r\n    fon" +
                        "t-size: 35px;\r\n    line-height: 35px;\r\n}\r\n    .dashboard-stat.blue-madison .visu" +
                        "al > i {\r\n    color: white;\r\n    opacity: 0.3;\r\n    filter: alpha(opacity=30);\r\n" +
                        "}\r\n    .dashboard-stat .visual > i {\r\n    margin-left: -27px;\r\n    font-size: 11" +
                        "0px;\r\n    line-height: 110px;\r\n}\r\n   \r\n    .dashboard-stat .details {\r\n    posit" +
                        "ion: absolute;\r\n    right: 15px;\r\n    padding-right: 15px;\r\n}\r\n    .dashboard-st" +
                        "at.blue-madison .details .number {\r\n    color: white;\r\n}\r\n    .dashboard-stat .d" +
                        "etails .number {\r\n    padding-top: 25px;\r\n    text-align: right;\r\n    font-size:" +
                        " 34px;\r\n    line-height: 36px;\r\n    letter-spacing: -1px;\r\n    margin-bottom: 0p" +
                        "x;\r\n    font-weight: 300;\r\n}\r\n    .dashboard-stat.blue-madison .more {\r\n    colo" +
                        "r: white;\r\n    background-color: #4884b8;\r\n}\r\n\r\n    .dashboard-stat.blue-madison" +
                        " .details .desc {\r\n    color: white;\r\n    opacity: 0.8;\r\n    filter: alpha(opaci" +
                        "ty=80);\r\n}\r\n    .dashboard-stat .details .desc {\r\n    text-align: right;\r\n    fo" +
                        "nt-size: 16px;\r\n    letter-spacing: 0px;\r\n    font-weight: 300;\r\n}\r\n\r\n    .dashb" +
                        "oard-stat.red-intense {\r\n    background-color: #e35b5a;\r\n}\r\n\r\n    .dashboard-sta" +
                        "t.red-intense .visual > i {\r\n    color: white;\r\n    opacity: 0.3;\r\n    filter: a" +
                        "lpha(opacity=30);\r\n}\r\n    .dashboard-stat.red-intense .details .number {\r\n    co" +
                        "lor: white;\r\n}\r\n    .dashboard-stat.red-intense .details .desc {\r\n    color: whi" +
                        "te;\r\n    opacity: 0.8;\r\n    filter: alpha(opacity=80);\r\n}\r\n    .dashboard-stat.r" +
                        "ed-intense .more {\r\n    color: white;\r\n    background-color: #e04a49;\r\n}\r\n\r\n    " +
                        ".dashboard-stat.purple-plum {\r\n    background-color: #8775a7;\r\n}\r\n    .dashboard" +
                        "-stat.purple-plum .visual > i {\r\n    color: white;\r\n    opacity: 0.3;\r\n    filte" +
                        "r: alpha(opacity=30);\r\n}\r\n    .dashboard-stat.purple-plum .details .number {\r\n  " +
                        "  color: white;\r\n}\r\n    .dashboard-stat.purple-plum .details .desc {\r\n    color:" +
                        " white;\r\n    opacity: 0.8;\r\n    filter: alpha(opacity=80);\r\n}\r\n    .dashboard-st" +
                        "at.purple-plum .more {\r\n    color: white;\r\n    background-color: #7c699f;\r\n}\r\n</" +
                        "style>\r\n<div class=\"row\">\r\n\t\t\t\t<div class=\"col-lg-3 col-md-3 col-sm-6 col-xs-12\"" +
                        ">\r\n\t\t\t\t\t<div class=\"dashboard-stat blue-madison\">\r\n\t\t\t\t\t\t<div class=\"visual\">\r\n\t" +
                        "\t\t\t\t\t\t<i class=\"fa fa-comments\"></i>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"details\">\r" +
                        "\n\t\t\t\t\t\t\t<div class=\"number\">\r\n                                \r\n\t\t\t\t\t\t\t</div>\r\n\t" +
                        "\t\t\t\t\t\t<div class=\"desc\">\r\n\t\t\t\t\t\t\t\t New Request\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t" +
                        "\t\t\t<a class=\"more\" href=\"/sites/scdr/SitePages/Services_Menu.aspx\">\r\n\t\t\t\t\t\tView " +
                        "more <i class=\"m-icon-swapright m-icon-white\"></i>\r\n\t\t\t\t\t\t</a>\r\n\t\t\t\t\t</div>\r\n\t\t\t" +
                        "\t</div>\r\n\t\t\t\t<div class=\"col-lg-3 col-md-3 col-sm-6 col-xs-12\">\r\n\t\t\t\t\t<div class" +
                        "=\"dashboard-stat red-intense\">\r\n\t\t\t\t\t\t<div class=\"visual\">\r\n\t\t\t\t\t\t\t<i class=\"fa " +
                        "fa-bar-chart-o\"></i>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"details\">\r\n\t\t\t\t\t\t\t<div cla" +
                        "ss=\"number\">\r\n                                "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControlLblRate();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@" Stars
							</div>
							<div class=""desc"">
								 Ratting
							</div>
						</div>
						<a class=""more"" href=""/sites/scdr/SitePages/Ratting%20and%20Feedback.aspx"">
						View more <i class=""m-icon-swapright m-icon-white""></i>
						</a>
					</div>
				</div>
				
				<div class=""col-lg-3 col-md-3 col-sm-6 col-xs-12"">
					<div class=""dashboard-stat purple-plum"">
						<div class=""visual"">
							<i class=""fa fa-globe""></i>
						</div>
						<div class=""details"">
							<div class=""number"">
                                "));
            global::System.Web.UI.WebControls.Label @__ctrl2;
            @__ctrl2 = this.@__BuildControllblViews();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
							</div>
							<div class=""desc"">
								 Views
							</div>
						</div>
						<a class=""more"" href=""/sites/SCDR/_layouts/15/Reporting.aspx?Category=AnalyticsSiteCollection"">
						View more <i class=""m-icon-swapright m-icon-white""></i>
						</a>
					</div>
				</div>
			</div>"));
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
