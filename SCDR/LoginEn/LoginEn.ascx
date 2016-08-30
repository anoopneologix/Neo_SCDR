<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>

<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginEn.ascx.cs" Inherits="SCDR.LoginEn.LoginEn" %>
<!-- form begins-->
<asp:HiddenField ID="hfloginstatus" ClientIDMode="Static" runat="server" />
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
                    <%--<div class="form-group custom-frm-group">
                        <label for="email">User Name:</label>
                        <span class="email">
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                         
                        </span>
                    </div>
                    <div class="form-group custom-frm-group">
                        <label for="pwd">Password:</label>
                       
                            <asp:TextBox ID="pwd" TextMode="Password" runat="server" ClientIDMode="Static" class="form-control frm-pwd"></asp:TextBox>
                          
                    </div>--%>
                    <%--<div class="checkbox clearfix">
                      <!--  <label class="chklabel">
                            <input type="checkbox">Remember me</label>-->
                        <a href="#" class="forgot">Forgot Password?</a>
                    </div>--%>
                    <asp:Button ID="btnSignin" class="btn btn-default btn-theme btn-submit" runat="server" Text="Login" OnClick="btnSignin_Click1" />
                    <asp:Button ID="btnSignUp" class="btn btn-primary btn-signup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
                    <%--<button type="button" class="btn btn-primary btn-signup"> Signup</button>--%>
                //Link for internal users to log in using AD account
                <%--<asp:LinkButton ID="lbInternalUsers" Text="Active Directory Login" runat="server" Font-Names="Calibri" Font-Size="Small" CssClass="ms-standardheader ms-inputformheader" Font-Bold="true" ForeColor="Wheat" OnClick="lbInternalUsers_OnClick" />--%>
                </div>
            </span>
 
            <span class="col-md-12 col-sm-12 col-xs-12 tab-frm forgot-frm">
                <div>
                    <div class="form-group custom-frm-group">
                        <label for="forgot-email">email address:</label>
                        <span class="email">
                            <asp:TextBox ID="txtForgotEmailId" runat="server" TextMode="Email" ClientIDMode="Static" class="form-control frm-forgot-email"></asp:TextBox>
                          
                        </span>
                    </div>
                   <asp:Button ID="btnForgotPassword" class="btn btn-default btn-theme btn-forgot-submit" Text="Submit" runat="server" OnClick="btnForgotPassword_Click" />
                </div>
            </span>
          
            <span class="col-md-12 col-sm-12 col-xs-12 tab-frm after-signin">

                <div class="welcome_user">
                    <b>Welcome</b>
                    <span><asp:Label ID="lblUsername" runat="server"></asp:Label></span>
                </div>
                
                  <asp:Button ID="btnSignOut" class="btn btn-default" Text="Signout" runat="server" OnClick="btnSignOut_Click" />
            </span>

        </span>
    </div>
</div>

<script>

        var loginStatus = $("#hfloginstatus").val();
        if (loginStatus == "True")
        {
            $(".login_form_outer").removeClass("expand_height");
            $(".tab-frm").hide();
            $(".tab-frm.after-signin").fadeIn();
        }
        else
        {

        }
  
</script>