﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCDR.AdminForms.AddNewAtSCDR {
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
    
    
    public partial class AddNewAtSCDR {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox txtDescription;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.FileUpload fuEnNewImages;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnSubmit;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.HiddenField hfNewDescription;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox txtArDescription;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.FileUpload fuArNewImages;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnArSubmit;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.HiddenField hfArNewDescription;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(AddNewAtSCDR target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtDescription() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtDescription = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtDescription";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.MultiLine;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "form-control");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.FileUpload @__BuildControlfuEnNewImages() {
            global::System.Web.UI.WebControls.FileUpload @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.FileUpload();
            this.fuEnNewImages = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "fuEnNewImages";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSubmit() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSubmit = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSubmit";
            @__ctrl.Text = "submit";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default");
            @__ctrl.Click -= new System.EventHandler(this.btnSubmit_Click);
            @__ctrl.Click += new System.EventHandler(this.btnSubmit_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhfNewDescription() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hfNewDescription = @__ctrl;
            @__ctrl.ID = "hfNewDescription";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtArDescription() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtArDescription = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtArDescription";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.MultiLine;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "form-control");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.FileUpload @__BuildControlfuArNewImages() {
            global::System.Web.UI.WebControls.FileUpload @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.FileUpload();
            this.fuArNewImages = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "fuArNewImages";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnArSubmit() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnArSubmit = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnArSubmit";
            @__ctrl.Text = "حفظ";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default");
            @__ctrl.Click -= new System.EventHandler(this.btnArSubmit_Click);
            @__ctrl.Click += new System.EventHandler(this.btnArSubmit_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhfArNewDescription() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hfArNewDescription = @__ctrl;
            @__ctrl.ID = "hfArNewDescription";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::SCDR.AdminForms.AddNewAtSCDR.AddNewAtSCDR @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
<!--Include Bootstrap RichTextBox-->
<link href=""../../_layouts/15/SCDR/css/editor.css"" rel=""stylesheet"" />
<div class=""col-md-12 col-sm-12 col-xs-12"">

  <!-- Nav tabs -->
  <ul class=""nav nav-tabs"" role=""tablist"">
    <li role=""presentation"" class=""active""><a href=""#english"" aria-controls=""english"" role=""tab"" data-toggle=""tab"">English</a></li>
    <li role=""presentation""><a href=""#arabic"" aria-controls=""arabic"" role=""tab"" data-toggle=""tab"">Arabic</a></li>
    </ul>

  <!-- Tab panes -->
  <div class=""tab-content"" style=""background-color:white !important"">
    <div role=""tabpanel"" class=""tab-pane fade in active"" id=""english"">
<!-- Form Begins -->
<div class=""form-horizontal"">
      <div class=""form-group"">
    <label  class=""col-sm-3 control-label"">Description : </label>
    <div class=""col-sm-9"" id=""divEngDescription"">
      "));
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtDescription();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n  </div>\r\n  </div>\r\n    <div class=\"form-group\">\r\n       <label  class=\"col-sm-" +
                        "3 control-label\">Upload Image : </label>\r\n    <div class=\"col-sm-9\">\r\n        "));
            global::System.Web.UI.WebControls.FileUpload @__ctrl2;
            @__ctrl2 = this.@__BuildControlfuEnNewImages();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    </div>\r\n  </div>\r\n    <div class=\"form-group\">\r\n    <div class=\"col-sm-offs" +
                        "et-3 col-sm-9\">\r\n        "));
            global::System.Web.UI.WebControls.Button @__ctrl3;
            @__ctrl3 = this.@__BuildControlbtnSubmit();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    </div>\r\n  </div>\r\n     "));
            global::System.Web.UI.WebControls.HiddenField @__ctrl4;
            @__ctrl4 = this.@__BuildControlhfNewDescription();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
    </div>
<!-- Form Ends -->
    </div>
    <div role=""tabpanel"" class=""tab-pane fade"" id=""arabic"">
<!-- Form Begins -->
<div class=""form-horizontal"" id=""divNewAtSCDRar"" style=""direction:ltr !important"">
      <div class=""form-group"">
    <label  class=""col-sm-3 control-label"">تفاصيل : </label>
    <div class=""col-sm-9"" id=""divArDescription"">
      "));
            global::System.Web.UI.WebControls.TextBox @__ctrl5;
            @__ctrl5 = this.@__BuildControltxtArDescription();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n  </div>\r\n  </div>\r\n    <div class=\"form-group\">\r\n       <label  class=\"col-sm-" +
                        "3 control-label\">تحميل الصور: </label>\r\n    <div class=\"col-sm-9\">\r\n        "));
            global::System.Web.UI.WebControls.FileUpload @__ctrl6;
            @__ctrl6 = this.@__BuildControlfuArNewImages();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    </div>\r\n  </div>\r\n    <div class=\"form-group\">\r\n    <div class=\"col-sm-offs" +
                        "et-3 col-sm-9\">\r\n        "));
            global::System.Web.UI.WebControls.Button @__ctrl7;
            @__ctrl7 = this.@__BuildControlbtnArSubmit();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    </div>\r\n  </div>\r\n     "));
            global::System.Web.UI.WebControls.HiddenField @__ctrl8;
            @__ctrl8 = this.@__BuildControlhfArNewDescription();
            @__parser.AddParsedSubObject(@__ctrl8);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
    </div>
<!-- Form Ends -->
    </div>
    </div>

</div>

<!-- RichtextBox --->
 <script>
     $(document).ready(function () {
         $(""#txtDescription"").Editor();
         $("".Editor-editor"")
           .blur(function () {
               var content = $(""#txtDescription"").Editor(""getText"");
               $(""#hfNewDescription"").val(content);
           });
         $(""#txtArDescription"").Editor();
         $("".Editor-editor"")
           .blur(function () {
               var content = $(""#txtArDescription"").Editor(""getText"");
               $(""#hfArNewDescription"").val(content);
           });
     });
</script>

"));
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
