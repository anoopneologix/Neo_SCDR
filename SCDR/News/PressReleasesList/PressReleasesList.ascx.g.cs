﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCDR.News.PressReleasesList {
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
    
    
    public partial class PressReleasesList {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Repeater repNewsList;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Repeater rptPager;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(PressReleasesList target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.DataBoundLiteralControl @__BuildControl__control3() {
            global::System.Web.UI.DataBoundLiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.DataBoundLiteralControl(7, 6);
            @__ctrl.TemplateControl = this;
            @__ctrl.SetStaticString(0, "\r\n   <div class=\"newsrelease\">\r\n    <h4 class=\"inti_subhead font_resize\">");
            @__ctrl.SetStaticString(1, "</h4>\r\n    <img class=\"img-thumbnail\" src=\'");
            @__ctrl.SetStaticString(2, "\' />\r\n    <p class=\"newsdate font_resize\"> <i class=\"fa fa-calendar-check-o\"></i>" +
                    " Publication date: ");
            @__ctrl.SetStaticString(3, "</p>\r\n    <p class=\"font_resize boldText\">");
            @__ctrl.SetStaticString(4, " :</p>\r\n    <p class=\"releasecontent font_resize\">\r\n       <div class=\"content_li" +
                    "mit fontresizer\"> ");
            @__ctrl.SetStaticString(5, " </div> </p>\r\n    <a  href=\'");
            @__ctrl.SetStaticString(6, "\' class=\"newsread font_resize\">read more</a>\r\n       </div>     \r\n        ");
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBind__control3);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        public void @__DataBind__control3(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.RepeaterItem Container;
            System.Web.UI.DataBoundLiteralControl target;
            target = ((System.Web.UI.DataBoundLiteralControl)(sender));
            Container = ((System.Web.UI.WebControls.RepeaterItem)(target.BindingContainer));
            target.SetDataBoundString(0, global::System.Convert.ToString(Eval("Title"), global::System.Globalization.CultureInfo.CurrentCulture));
            target.SetDataBoundString(1, global::System.Convert.ToString(Eval("ImageUrl"), global::System.Globalization.CultureInfo.CurrentCulture));
            target.SetDataBoundString(2, global::System.Convert.ToString(Eval("Date", "{0:d}"), global::System.Globalization.CultureInfo.CurrentCulture));
            target.SetDataBoundString(3, global::System.Convert.ToString(Eval("Location"), global::System.Globalization.CultureInfo.CurrentCulture));
            target.SetDataBoundString(4, global::System.Convert.ToString(Eval("Description"), global::System.Globalization.CultureInfo.CurrentCulture));
            target.SetDataBoundString(5, global::System.Convert.ToString(Eval("PageID"), global::System.Globalization.CultureInfo.CurrentCulture));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControl__control2(System.Web.UI.Control @__ctrl) {
            global::System.Web.UI.DataBoundLiteralControl @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control3();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Repeater @__BuildControlrepNewsList() {
            global::System.Web.UI.WebControls.Repeater @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Repeater();
            this.repNewsList = @__ctrl;
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control2));
            @__ctrl.ID = "repNewsList";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control5() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lnkPage";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control5);
            @__ctrl.Click -= new System.EventHandler(this.Page_Changed);
            @__ctrl.Click += new System.EventHandler(this.Page_Changed);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        public void @__DataBinding__control5(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.LinkButton dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.RepeaterItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.LinkButton)(sender));
            Container = ((System.Web.UI.WebControls.RepeaterItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString(Eval("Text") , global::System.Globalization.CultureInfo.CurrentCulture);
            dataBindingExpressionBuilderTarget.CommandArgument = global::System.Convert.ToString( Eval("Value") , global::System.Globalization.CultureInfo.CurrentCulture);
            dataBindingExpressionBuilderTarget.CssClass = global::System.Convert.ToString( Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" , global::System.Globalization.CultureInfo.CurrentCulture);
            dataBindingExpressionBuilderTarget.OnClientClick = global::System.Convert.ToString( !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControl__control4(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n        "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control5();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n   "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Repeater @__BuildControlrptPager() {
            global::System.Web.UI.WebControls.Repeater @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Repeater();
            this.rptPager = @__ctrl;
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control4));
            @__ctrl.ID = "rptPager";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::SCDR.News.PressReleasesList.PressReleasesList @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<link rel=\"stylesheet\" href=\"../../_layouts/15/SCDR/css/PressPagination.css\" />" +
                        "\r\n <!----neewsrelease begins here---->\r\n"));
            global::System.Web.UI.WebControls.Repeater @__ctrl1;
            @__ctrl1 = this.@__BuildControlrepNewsList();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<div class=\"clear\"></div>\r\n<div id=\"repPaging\">\r\n"));
            global::System.Web.UI.WebControls.Repeater @__ctrl2;
            @__ctrl2 = this.@__BuildControlrptPager();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    </div>\r\n                            <!----neewsrelease ends here---->"));
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
