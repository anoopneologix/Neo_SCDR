<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNewAtSCDR.ascx.cs" Inherits="SCDR.AdminForms.AddNewAtSCDR.AddNewAtSCDR" %>
<!--Include Bootstrap RichTextBox-->
<link href="../../_layouts/15/SCDR/css/editor.css" rel="stylesheet" />
<div class="col-md-12 col-sm-12 col-xs-12">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#english" aria-controls="english" role="tab" data-toggle="tab">English</a></li>
    <li role="presentation"><a href="#arabic" aria-controls="arabic" role="tab" data-toggle="tab">Arabic</a></li>
    </ul>

  <!-- Tab panes -->
  <div class="tab-content" style="background-color:white !important">
    <div role="tabpanel" class="tab-pane fade in active" id="english">
<!-- Form Begins -->
<div class="form-horizontal">
      <div class="form-group">
    <label  class="col-sm-3 control-label">Description : </label>
    <div class="col-sm-9" id="divEngDescription">
      <asp:TextBox ID="txtDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
       <label  class="col-sm-3 control-label">Upload Image : </label>
    <div class="col-sm-9">
        <asp:FileUpload ID="fuEnNewImages"  runat="server" />
    </div>
  </div>
    <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click"  />
    </div>
  </div>
     <asp:HiddenField ID="hfNewDescription" ClientIDMode="Static" runat="server" ></asp:HiddenField>
    </div>
<!-- Form Ends -->
    </div>
    <div role="tabpanel" class="tab-pane fade" id="arabic">
<!-- Form Begins -->
<div class="form-horizontal" id="divNewAtSCDRar" style="direction:ltr !important">
      <div class="form-group">
    <label  class="col-sm-3 control-label">تفاصيل : </label>
    <div class="col-sm-9" id="divArDescription">
      <asp:TextBox ID="txtArDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
       <label  class="col-sm-3 control-label">تحميل الصور: </label>
    <div class="col-sm-9">
        <asp:FileUpload ID="fuArNewImages"  runat="server" />
    </div>
  </div>
    <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnArSubmit" Text="حفظ" class="btn btn-default" runat="server" OnClick="btnArSubmit_Click"  />
    </div>
  </div>
     <asp:HiddenField ID="hfArNewDescription" ClientIDMode="Static" runat="server" ></asp:HiddenField>
    </div>
<!-- Form Ends -->
    </div>
    </div>

</div>

<!-- RichtextBox --->
 <script>
     $(document).ready(function () {
         $("#txtDescription").Editor();
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtDescription").Editor("getText");
               $("#hfNewDescription").val(content);
           });
         $("#txtArDescription").Editor();
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtArDescription").Editor("getText");
               $("#hfArNewDescription").val(content);
           });
     });
</script>

