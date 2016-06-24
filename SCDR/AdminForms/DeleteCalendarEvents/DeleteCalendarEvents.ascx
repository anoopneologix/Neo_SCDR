<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteCalendarEvents.ascx.cs" Inherits="SCDR.AdminForms.DeleteCalendarEvents.DeleteCalendarEvents" %>
<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<!-- Form Begins -->
<div class="form-horizontal">
    <div class="form-group">
       <label  class="col-sm-3 control-label">Select Language : </label>
    <div class="col-sm-9">
        <asp:RadioButton GroupName="grpLanguage" Text="Arabic"   ID="rbArabic" runat="server"  />
          <asp:RadioButton GroupName="grpLanguage" Text="English"  ID="rbEnglish" runat="server"   />
    </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Event Date : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventDate" AutoPostBack="true" ClientIDMode="Static" runat="server" class="form-control" OnTextChanged="txtEventDate_TextChanged" ></asp:TextBox>
  </div>
          <div class="col-sm-3">
          <asp:RequiredFieldValidator  ControlToValidate="txtEventDate" ValidationGroup="chk" ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      </div>
  </div>
             <div class="form-group">
    <label  class="col-sm-3 control-label">Event Name : </label>
                 <div class="col-sm-6">
                     <asp:DropDownList  ID="ddlEventName" runat="server"></asp:DropDownList>   </div>
                 </div>
    <div class="form-group" id="divButton" runat="server">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnDelete" ValidationGroup="chk"  OnClientClick="if ( ! EventDeleteConfirmation()) return false;"  Text="Delete" class="btn btn-default" runat="server" OnClick="btnDelete_Click"   />
         <asp:Button ID="btnCancel" Text="Cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click"    />
    </div>
  </div>
    </div>
<script>
    function EventDeleteConfirmation() {
        return confirm("Are you sure you want to delete this event?");
    }
    </script>

 <!-- datetime picker script-->
<script type="text/javascript" src="../../_layouts/15/SCDR/js/moment.js"></script>
<script type="text/javascript" src="../../_layouts/15/SCDR/js/bootstrap-datepicker.min.js" ></script>
<script type="text/javascript" src="../../_layouts/15/SCDR/js/bootstrap-datetimepicker.js"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $('#txtEventDate')
               .datepicker({
                   format: 'dd/MM/yyyy',
                   todayHighlight: true
               });
       });
    </script>