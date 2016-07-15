<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateADUserEn.ascx.cs" Inherits="SCDR.UserManagement.CreateADUserEn.CreateADUserEn" %>
<section class="container resiazable">
      <asp:UpdatePanel ID="upUsername" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtEmail" EventName="TextChanged" />
                 <asp:AsyncPostBackTrigger ControlID="txtUsername" EventName="TextChanged" />
                <asp:PostBackTrigger ControlID="btnRegister" />
            </Triggers>
            <ContentTemplate>
<div class="col-md-12 col-sm-12 col-xs-12">
    <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-2 control-label">First Name</label>
    <div class="col-sm-5">
      <asp:TextBox ID="txtFirstName" CausesValidation="true" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
      <div class="col-sm-5">
          <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtFirstName" runat="server" ErrorMessage="Please enter your first name"></asp:RequiredFieldValidator>
      </div>
  </div>
         <div class="form-group">
    <label  class="col-sm-2 control-label">Last Name</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtLastName" CausesValidation="true"  CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" Display="Dynamic" runat="server" ErrorMessage="Please enter your last name"></asp:RequiredFieldValidator>
               </div>
  </div>
        <div class="form-group">
    <label  class="col-sm-2 control-label">Phone Number</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtPhoneNumber" CausesValidation="true" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
              <div class="col-sm-5">
                  <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" Display="Dynamic" ControlToValidate="txtPhoneNumber"  ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter phone number"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="chk" ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ControlToValidate="txtPhoneNumber" ValidationExpression="^\s*\+?[0-9]\d?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"   ErrorMessage="please enter a valid phone number"></asp:RegularExpressionValidator>       
                   </div>
  </div>
       
    <div class="form-group">
    <label  class="col-sm-2 control-label">Email ID</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtEmail" CausesValidation="true" AutoPostBack="true" CssClass="form-control" ClientIDMode="Static" runat="server" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
    </div>
         <div class="col-sm-5">
             <asp:Label ID="lblEmailError" runat="server"></asp:Label>
             <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtEmail" Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter the email address"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="chk" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please enter a valid email id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator> 
             
              </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-2 control-label">Username</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtUsername" CausesValidation="true" AutoPostBack="true" CssClass="form-control" ClientIDMode="Static" runat="server" OnTextChanged="txtUsername_TextChanged"></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:Label ID="lblUserError" runat="server"></asp:Label>
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtUsername" Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter a username"></asp:RequiredFieldValidator>
                   </div>
  </div>
        <div class="form-group">
    <label  class="col-sm-2 control-label">Password</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtPassword" CausesValidation="true" TextMode="Password" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
              <div class="col-sm-5">
                  <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtPassword" ValidationGroup="chk" Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter your password"></asp:RequiredFieldValidator>
              </div>
  </div>
         <div class="form-group">
    <label  class="col-sm-2 control-label">Confirm Password</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtCpassword" CausesValidation="true" TextMode="Password" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtCpassword" Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please re-enter your password"></asp:RequiredFieldValidator>
                   <asp:CompareValidator ForeColor="Red" Display="Dynamic" ControlToCompare="txtPassword" ControlToValidate="txtCpassword" ID="CompareValidator1" runat="server" ErrorMessage="Password miss match. Please check the password re-entered"></asp:CompareValidator>
               </div>
  </div>
        <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
  <asp:CheckBox ID="chkGetNotification" runat="server" Text="Get updates from SCDR" />
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <asp:Button ID="btnRegister" CssClass="btn btn-default" ValidationGroup="chk" runat="server" Text="Sign Up" OnClick="btnRegister_Click" />
         <asp:Button ID="btncancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btncancel_Click" />
    </div>
  </div>
</div>
    </div>
                
                </ContentTemplate>
            </asp:UpdatePanel>
    </section>