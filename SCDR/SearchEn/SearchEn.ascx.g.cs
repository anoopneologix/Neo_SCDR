﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCDR.SearchEn {
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
    
    
    public partial class SearchEn {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton btnSearch;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox txtSearch;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(SearchEn target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControlbtnSearch() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.btnSearch = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSearch";
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("<i class=\"fa fa-search\"></i>"));
            @__ctrl.Click -= new System.EventHandler(this.btnSearch_Click);
            @__ctrl.Click += new System.EventHandler(this.btnSearch_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtSearch() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtSearch = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtSearch";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            @__ctrl.CssClass = "form-control";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("placeholder", "Search");
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("onkeydown", "return (event.keyCode!=13);");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::SCDR.SearchEn.SearchEn @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
<!--Code Begins-->
<script src=""../_layouts/15/SCDR/js/jquery-ui.js""></script>
<div class=""search_outer"">
    <a class=""search_btn"">
        <i class=""fa""></i>
    </a>
    <span class=""input_outer""><span
        class=""col-md-12 col-sm-12 col-xs-12"">
        "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            @__ctrl1 = this.@__BuildControlbtnSearch();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n       \r\n   "));
            global::System.Web.UI.WebControls.TextBox @__ctrl2;
            @__ctrl2 = this.@__BuildControltxtSearch();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n       </span> </span>\r\n</div>\r\n<!--Code Ends-->\r\n\r\n<!--\r\n\r\n  <script type=\"tex" +
                        "t/javascript\">\r\n\r\n    // Settings\r\n    var siteurl = _spPageContextInfo.webAbsol" +
                        "uteUrl;\r\n    var field = \"Title\";\r\n    // Onload\r\n    $(document).ready(function" +
                        " () {\r\n     \r\n        $(\"input[name$=txtSearch]\").autocomplete({\r\n            so" +
                        "urce: function (req, add) {\r\n              // \r\n                var suggestions " +
                        "= search(req.term, field);\r\n               \r\n                add(suggestions);\r\n" +
                        "            }\r\n        });\r\n    });\r\n\r\n\r\n    function search(value, field) {\r\n  " +
                        "      var coll = new Array();\r\n        var url = siteurl + \"/_vti_bin/listdata.s" +
                        "vc/CustomImageGallery?$select=Title&$filter=startswith(Title, \'\" + value + \"\')\";" +
                        "\r\n        $.ajax({\r\n            cache: true,\r\n            type: \"GET\",\r\n        " +
                        "    headers: { \"Accept\": \"application/json; odata=verbose\" },\r\n            async" +
                        ": false,\r\n            dataType: \"json\",\r\n            url: url,\r\n            succ" +
                        "ess: function (data) {\r\n                var results = data.d.results;\r\n         " +
                        "       for (att in results) {\r\n                    var object = results[att];\r\n " +
                        "                   for (attt in object) {\r\n                        if (attt == f" +
                        "ield) {\r\n                            coll.push(object[attt]);\r\n                 " +
                        "       }\r\n                    }\r\n                }\r\n                return coll;" +
                        "\r\n            },\r\n            error: function (data) {\r\n                alert(\"E" +
                        "rror: No search suggestion.\");\r\n            }\r\n        });\r\n        return coll;" +
                        "\r\n    }\r\n\r\n      </script>-->\r\n\r\n  "));
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
