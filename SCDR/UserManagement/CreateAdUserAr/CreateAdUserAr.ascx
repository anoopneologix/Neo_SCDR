<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateAdUserAr.ascx.cs" Inherits="SCDR.UserManagement.CreateAdUserAr.CreateAdUserAr" %>
 <link rel="stylesheet" href="/sites/SCDR/ar/_catalogs/masterpage/css/signin-ar.css">
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
    <label  class="col-sm-2 control-label">الاسم الاول</label>
    <div class="col-sm-5">
      <asp:TextBox ID="txtFirstName" CausesValidation="true" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
      <div class="col-sm-5">
          <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtFirstName" runat="server" ErrorMessage="يرجى إدخال الاسم الأول."></asp:RequiredFieldValidator>
      <%--<asp:RegularExpressionValidator ID="RegExp1" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="^[a-zA-Z]{0,50}$" ControlToValidate="txtFirstName" runat="server" ErrorMessage="أقصى 50 الأحرف المسموح، أي أحرف خاصة وأرقام سمحت"></asp:RegularExpressionValidator>--%>
           </div>
  </div>
         <div class="form-group">
    <label  class="col-sm-2 control-label">اسم العائلة</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtLastName" CausesValidation="true"  CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" Display="Dynamic" runat="server" ErrorMessage="يرجى إدخال اسم العائلة"></asp:RequiredFieldValidator>
              <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="[^a-zA-Z0-9\s]{0,50}$" ControlToValidate="txtLastName"  runat="server" ErrorMessage="أقصى 50 الأحرف المسموح، أي أحرف خاصة وأرقام سمحت"></asp:RegularExpressionValidator>--%>
                     </div>
  </div>
        <div class="form-group">
    <label  class="col-sm-2 control-label">رقم الهاتف</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtPhoneNumber" CausesValidation="true" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
              <div class="col-sm-5">
                  <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" Display="Dynamic" ControlToValidate="txtPhoneNumber"  ID="RequiredFieldValidator3" runat="server" ErrorMessage="يرجى إدخال رقم الهاتف."></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="chk" ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ControlToValidate="txtPhoneNumber" ValidationExpression="^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$"   ErrorMessage="يرجى إدخال رقم الهاتف الصحيح."></asp:RegularExpressionValidator>       
                   </div>
  </div>
       
    <div class="form-group">
    <label  class="col-sm-2 control-label">عنوان البريد الإلكتروني</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtEmail" CausesValidation="true" AutoPostBack="true" CssClass="form-control" ClientIDMode="Static" runat="server" OnTextChanged="txtEmail_TextChanged" ></asp:TextBox>
    </div>
         <div class="col-sm-5">
             <asp:Label ID="lblEmailError" runat="server"></asp:Label>
             <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtEmail" Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="يرجى إدخال عنوان البريد الإلكتروني."></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="chk" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="يرجى إدخال عنوان البريد الإلكتروني الصحيح." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator> 
             
              </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-2 control-label">اسم المستخدم</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtUsername" CausesValidation="true" AutoPostBack="true" CssClass="form-control" ClientIDMode="Static" runat="server" OnTextChanged="txtUsername_TextChanged" ></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:Label ID="lblUserError" runat="server"></asp:Label>
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtUsername" Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="الرجاء إدخال اسم المستخدم"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="^[a-zA-Z0-9]{2,20}$" ControlToValidate="txtUsername"  runat="server" ErrorMessage="الحد الأدنى من 2 أحرف، بحد أقصى 20 حرفا المسموح به، لا يسمح أحرف خاصة"></asp:RegularExpressionValidator>
                    </div>
  </div>
        <div class="form-group">
    <label  class="col-sm-2 control-label">كلمة المرور</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtPassword" CausesValidation="true" TextMode="Password" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
              <div class="col-sm-5">
                  <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtPassword" ValidationGroup="chk" Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ErrorMessage="يرجى إدخال كلمة المرور."></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator Display="Dynamic" ID="Regex2" runat="server" ControlToValidate="txtPassword"
    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$"
    ErrorMessage="الحد الأدنى من 6 أحرف على الأقل 1 الأبجدية و1 عدد و1 حرف خاص" ForeColor="Red" ></asp:RegularExpressionValidator>
                   </div>
  </div>
         <div class="form-group">
    <label  class="col-sm-2 control-label">تأكيد كلمة المرور</label>
    <div class="col-sm-5">
     <asp:TextBox ID="txtCpassword" CausesValidation="true" TextMode="Password" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>
               <div class="col-sm-5">
                   <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtCpassword" Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ErrorMessage="يرجى إعادة كلمة المرور."></asp:RequiredFieldValidator>
                   <asp:CompareValidator ForeColor="Red" Display="Dynamic" ControlToCompare="txtPassword" ControlToValidate="txtCpassword" ID="CompareValidator1" runat="server" ErrorMessage="كلمة المرور غير مطابقة . الرجاء المحاولة مرة أخرى "></asp:CompareValidator>
               </div>
  </div>
         <div id="messages"></div>
        <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
  <%--<asp:CheckBox ID="chkGetNotification" runat="server" Text="Get updates from SCDR" />--%>
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <asp:Button ID="btnRegister" CssClass="btn btn-default" ValidationGroup="chk" runat="server" Text="تسجيل" OnClick="btnRegister_Click"  />
         <asp:Button ID="btncancel" CssClass="btn btn-default" runat="server" Text="إلغاء" OnClick="btncancel_Click"  />
    </div>
  </div>
</div>
    </div>
                
                </ContentTemplate>
            </asp:UpdatePanel>
    </section>