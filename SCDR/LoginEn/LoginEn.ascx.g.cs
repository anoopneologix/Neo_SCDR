﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCDR.LoginEn {
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
    
    
    public partial class LoginEn {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.HiddenField hfloginstatus;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnOk;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox UserName;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox pwd;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnSignin;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox txtForgotEmailId;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnForgotPassword;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Label lblUsername;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnSignOut;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(LoginEn target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhfloginstatus() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hfloginstatus = @__ctrl;
            @__ctrl.ID = "hfloginstatus";
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnOk() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnOk = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnOk";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default btn-theme btn-theme-ok");
            @__ctrl.Text = "OK";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControlUserName() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.UserName = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "UserName";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControlpwd() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.pwd = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "pwd";
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.Password;
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "form-control frm-pwd");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSignin() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSignin = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSignin";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default");
            @__ctrl.Text = "Submit";
            @__ctrl.Click -= new System.EventHandler(this.btnSignin_Click1);
            @__ctrl.Click += new System.EventHandler(this.btnSignin_Click1);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtForgotEmailId() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtForgotEmailId = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtForgotEmailId";
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.Email;
            @__ctrl.ClientIDMode = global::System.Web.UI.ClientIDMode.Static;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "form-control frm-forgot-email");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnForgotPassword() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnForgotPassword = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnForgotPassword";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default btn-theme btn-forgot-submit");
            @__ctrl.Text = "Submit";
            @__ctrl.Click -= new System.EventHandler(this.btnForgotPassword_Click);
            @__ctrl.Click += new System.EventHandler(this.btnForgotPassword_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Label @__BuildControllblUsername() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblUsername = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblUsername";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSignOut() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSignOut = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSignOut";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "btn btn-default");
            @__ctrl.Text = "Signout";
            @__ctrl.Click -= new System.EventHandler(this.btnSignOut_Click);
            @__ctrl.Click += new System.EventHandler(this.btnSignOut_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::SCDR.LoginEn.LoginEn @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<!-- form begins-->\r\n"));
            global::System.Web.UI.WebControls.HiddenField @__ctrl1;
            @__ctrl1 = this.@__BuildControlhfloginstatus();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
<div class=""login"">
    <div class=""login_btn"">
        <i class=""fa""></i>
        <span class=""login_form_outer"">

            <div class=""overlayConfirm"">
                <div class=""confirmation_signout"">
                    <p>Are you sure, want to sign out from the site?</p>
                    "));
            global::System.Web.UI.WebControls.Button @__ctrl2;
            @__ctrl2 = this.@__BuildControlbtnOk();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    <button class=""btn btn-default btn-theme btn-theme-cancel"">CANCEL</button>
                </div>
            </div>

            <div class=""alert alert-custom""></div>
        
            <span class=""col-md-12 col-sm-12 col-xs-12 tab-frm signin-frm"" style=""display: block"">
                <div>
                    <div class=""form-group custom-frm-group"">
                        <label for=""email"">User Name:</label>
                        <span class=""email"">
                            "));
            global::System.Web.UI.WebControls.TextBox @__ctrl3;
            @__ctrl3 = this.@__BuildControlUserName();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                         
                        </span>
                    </div>
                    <div class=""form-group custom-frm-group"">
                        <label for=""pwd"">Password:</label>
                       
                            "));
            global::System.Web.UI.WebControls.TextBox @__ctrl4;
            @__ctrl4 = this.@__BuildControlpwd();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                          
                    </div>
                    <div class=""checkbox clearfix"">
                      <!--  <label class=""chklabel"">
                            <input type=""checkbox"">Remember me</label>-->
                        <a href=""#"" class=""forgot"">Forgot Password?</a>
                    </div>
                    "));
            global::System.Web.UI.WebControls.Button @__ctrl5;
            @__ctrl5 = this.@__BuildControlbtnSignin();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                
                </div>
            </span>
 
            <span class=""col-md-12 col-sm-12 col-xs-12 tab-frm forgot-frm"">
                <div>
                    <div class=""form-group custom-frm-group"">
                        <label for=""forgot-email"">email address:</label>
                        <span class=""email"">
                            "));
            global::System.Web.UI.WebControls.TextBox @__ctrl6;
            @__ctrl6 = this.@__BuildControltxtForgotEmailId();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                          \r\n                        </span>\r\n                  " +
                        "  </div>\r\n                   "));
            global::System.Web.UI.WebControls.Button @__ctrl7;
            @__ctrl7 = this.@__BuildControlbtnForgotPassword();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                </div>\r\n            </span>\r\n          \r\n            <span clas" +
                        "s=\"col-md-12 col-sm-12 col-xs-12 tab-frm after-signin\">\r\n\r\n                <div " +
                        "class=\"welcome_user\">\r\n                    <b>Welcome</b>\r\n                    <" +
                        "span>"));
            global::System.Web.UI.WebControls.Label @__ctrl8;
            @__ctrl8 = this.@__BuildControllblUsername();
            @__parser.AddParsedSubObject(@__ctrl8);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</span>\r\n                </div>\r\n                \r\n                  "));
            global::System.Web.UI.WebControls.Button @__ctrl9;
            @__ctrl9 = this.@__BuildControlbtnSignOut();
            @__parser.AddParsedSubObject(@__ctrl9);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
            </span>

        </span>
    </div>
</div>

<script>

        var loginStatus = $(""#hfloginstatus"").val();
        if (loginStatus == ""True"")
        {
            $("".login_form_outer"").removeClass(""expand_height"");
            $("".tab-frm"").hide();
            $("".tab-frm.after-signin"").fadeIn();
        }
        else
        {

        }
  
</script>"));
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
