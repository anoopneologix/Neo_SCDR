<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBlogPost.ascx.cs" Inherits="SCDR.AdminForms.AddBlogPost.AddBlogPost" %>
<!--Include Bootstrap RichTextBox-->
<link href="../../_layouts/15/SCDR/css/editor.css" rel="stylesheet" />
<!-- Form Begins -->
<div class="form-horizontal">
    <div class="form-group">
    <label  class="col-sm-3 control-label">Subject : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtSubject" ClientIDMode="Static" runat="server" class="form-control" ></asp:TextBox>
    </div> <div class="col-sm-3">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="chk" ForeColor="Red" ControlToValidate="txtSubject" runat="server" ErrorMessage="Please enter the subject"></asp:RequiredFieldValidator>     
 <asp:RegularExpressionValidator ID="RegExp1" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_]|[\u0600-\u06FF])*" ControlToValidate="txtSubject" runat="server" ErrorMessage="Special characters not allowed"></asp:RegularExpressionValidator> </div>
       </div> 
     <div class="form-group">
                         <label  class="col-sm-3 control-label">Attach : </label>
   <div class="col-sm-9">
       <asp:RadioButton ID="rbImage" ClientIDMode="Static" Text="Image" GroupName="chkMedia" runat="server" />
         <asp:RadioButton ID="rbVideo" ClientIDMode="Static" Text="Video"  GroupName="chkMedia" runat="server" />
        <asp:RadioButton ID="rbNone" ClientIDMode="Static" Checked="true" Text="None" GroupName="chkMedia" runat="server" />
       </div>
                      </div>
            <div  id="divImage">
<div class="form-group">
       <label  class="col-sm-3 control-label">Upload Image : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuBlogImage" runat="server" />
    </div>
  
  </div>
                </div> 
         <div  id="divVideo">
                <div class="form-group">
    <label class="col-sm-3 control-label">Url of the Video:</label>
      <div class="col-sm-6">
          <asp:TextBox ID="txtVideoUrl" TextMode="MultiLine" ClientIDMode="Static" runat="server" class="form-control" ></asp:TextBox>
          </div>
  </div>

    </div> 
     <div class="form-group">
    <label  class="col-sm-3 control-label">Body : </label>
    <div class="col-sm-6" id="divBlogBody" style="background-color:white">
      <asp:TextBox ID="txtBody" ClientIDMode="Static" runat="server" class="required form-control"></asp:TextBox>
        <asp:HiddenField ID="hfBody" ClientIDMode="Static" runat="server" ></asp:HiddenField>
        </div> 
  </div>
     
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ClientIDMode="Static"  ID="btnSubmit" Text="submit" ValidationGroup="chk" class="btn btn-default" runat="server" OnClick="btnSubmit_Click1"   />
    </div>
  </div>
   </div>


<!-- RichtextBox --->
 <script>
     $(document).ready(function () {
         $("#txtBody").Editor({
         });
      
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtBody").Editor("getText");
               $("#hfBody").val(content);
           });
     
         var rbName = $("input[type=radio][name$=chkMedia]:checked").val();
         if (rbName == "rbNone")
         {
             $("#divImage").hide();
             $("#divVideo").hide();
         }
         else if (rbName == "rbImage") {
             $("#divImage").show();
             $("#divVideo").hide();
         }
         else {
             $("#divImage").hide();
             $("#divVideo").show();
         }
       
         $("input[name$=chkMedia]").click(function () {
             if ($("#rbImage").is(":checked")) {
                 $("#divImage").show();
                 $("#divVideo").hide();
             } else if ($("#rbVideo").is(":checked")) {
                 $("#divImage").hide();
                 $("#divVideo").show();
             } else if ($("#rbNone").is(":checked")) {
                 $("#divImage").hide();
                 $("#divVideo").hide();
             }
         });
       
     });
</script>