<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginEn.ascx.cs" Inherits="SCDR.LoginEn.LoginEn" %>
<!-- form begins-->
<div class="login">
    <div class="login_btn">
        <i class="fa"></i>
        <span class="login_form_outer">

            <div class="overlayConfirm">
                <div class="confirmation_signout">
                    <p>Are you sure, want to sign out from the site?</p>
                    <asp:Button ID="btnOk" runat="server" class="btn btn-default btn-theme btn-theme-ok" Text="OK" />
                    <button class="btn btn-default btn-theme btn-theme-cancel">CANCEL</button>
                </div>
            </div>

            <div class="alert alert-custom"></div>
            <span class="col-md-12 col-sm-12 col-xs-12 tab-frm signin-frm" style="display: block">
                <div>
                    <div class="form-group custom-frm-group">
                        <label for="email">User Name:</label>
                        <span class="email">
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                         
                        </span>
                    </div>
                    <div class="form-group custom-frm-group">
                        <label for="pwd">Password:</label>
                       
                            <asp:TextBox ID="pwd" TextMode="Password" runat="server" ClientIDMode="Static" class="form-control frm-pwd"></asp:TextBox>
                          
                    </div>
                   <!-- <div class="checkbox clearfix">
                        <label class="chklabel">
                            <input type="checkbox">Remember me</label>
                        <a href="#" class="forgot">Forgot Password?</a>
                    </div>-->
                    <asp:Button ID="btnSignin" class="btn btn-default btn-theme btn-submit" runat="server" Text="Submit" />
                  
                </div>
            </span>

            <span class="col-md-12 col-sm-12 col-xs-12 tab-frm forgot-frm">
                <div>
                    <div class="form-group custom-frm-group">
                        <label for="forgot-email">email address:</label>
                        <span class="email">
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="Email" ClientIDMode="Static" class="form-control frm-forgot-email"></asp:TextBox>
                          
                        </span>
                    </div>
                   <asp:Button ID="btnForgotPassword" class="btn btn-default btn-theme btn-forgot-submit" Text="Submit" runat="server" />
                </div>
            </span>

            <span class="col-md-12 col-sm-12 col-xs-12 tab-frm after-signin">

                <div class="welcome_user">
                    <b>Welcome</b>
                    <span>Mr. Abhilash Nair</span>
                </div>
                
                  <asp:Button ID="btnSignOut" class="btn btn-default btn-theme btn-signout" Text="Signout" runat="server" />
            </span>

        </span>
    </div>
</div>