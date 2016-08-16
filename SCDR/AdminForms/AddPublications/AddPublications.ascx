<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPublications.ascx.cs" Inherits="SCDR.AdminForms.AddPublications.AddPublications" %>
<div class="col-md-12 col-sm-12 col-xs-12">
    <!-- Form Begins -->
        <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Title (Arabic) : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtTitleAr" MaxLength="250" style="direction:rtl !important;" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div><div class="col-sm-3">
            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtTitleAr" runat="server" ErrorMessage="Please enter title in Arabic"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[-\u0621-\u064A0-9 ]+$"
    ControlToValidate="txtTitleAr" runat="server" ForeColor="Red" ErrorMessage="Only arabic Characters and numbers allowed."
    Display="Dynamic" />
       </div>
   </div> 
             <div class="form-group">
    <label  class="col-sm-3 control-label">Title (English) : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtTitle" MaxLength="250"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div><div class="col-sm-3">
            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtTitle" runat="server" ErrorMessage="Please enter a title in English"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[-_a-zA-Z0-9'., ]{0,250}$" 
    ControlToValidate="txtTitle" runat="server" ForeColor="Red" ErrorMessage="Only english characters and numbers allowed."
    Display="Dynamic" />
       </div>
   </div> 
              <div class="form-group">
       <label  class="col-sm-3 control-label">Select Document : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuPublication" runat="server" />
    </div>
    <div class="col-sm-3">
    <span style="color:red" id="lblImageError"></span>
          <asp:RequiredFieldValidator ValidationGroup="chk"   Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red"  ControlToValidate="fuPublication" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^.*\.([pP][dD][fF])$"
    ControlToValidate="fuPublication"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .pdf."
    Display="Dynamic" />
    </div>
 
  </div>


             <div class="form-group">
       <label  class="col-sm-3 control-label">Select Thumbnail : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuThumbnail" runat="server" />
    </div>
    <div class="col-sm-3">
    <span style="color:red" id="lblThumbnailError"></span>
          <asp:RequiredFieldValidator ValidationGroup="chk"   Display="Dynamic" ID="RequiredFieldValidator4" ForeColor="Red"  ControlToValidate="fuThumbnail" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuThumbnail"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .png."
    Display="Dynamic" />
    </div>
 
  </div>

              <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ValidationGroup="chk" Text="submit" OnClientClick="return validateFormat(event);" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
             <asp:Button ID="btnCancel" Text="cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click"  />
    </div>
  </div>

            </div>
    </div>

<script>
    $(document).ready(function () {
        $('#lblImageError').hide();
        $("input[name$=fuPublication]").change(function () {
            $('#lblImageError').hide();
        });
        $('#lblThumbnailError').hide();
        $("input[name$=fuThumbnail]").change(function () {
            $('#lblThumbnailError').hide();
        });
    });
</script>
<script type="text/javascript">
    function validateFormat(event) {
        if (Page_ClientValidate()) {
         
         //pdf checking  
            var ext = $("input[name$=fuPublication]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuPublication]").get(0).files[i].name);
                }
                var x = 0;
                for (i = 0; i < names.length; i++) {
                    var str = names[i];
                    //  /^[-\sa-zA-Z]+$/
                    if (/^[-_a-zA-Z0-9.\u0600-\u06FF ]+$/.test(str) == false) {

                        x = 1;
                    }
                    if (x == 1) {
                        $('#lblImageError').show();
                        $('#lblImageError').text('Special characters except - and _ are not allowed in filename. Please select a valid image file of type .pdf.');
                        // setTimeout();
                        return false;
                        break;
                        event.preventDefault();
                    }

                }
                if (x == 0) {

                    var ext = [];
                    for (i = 0; i < names.length; i++) {

                        ext.push(names[i].substr(names[i].indexOf(".") + 1).toLowerCase());

                    }
                    var valid_filetype = ["pdf","PDF"];

                    var i, j, result = [];
                    for (i = 0; i < valid_filetype.length; i++) {
                        for (j = 0; j < ext.length; j++) {
                            if (ext[j].indexOf(valid_filetype[i]) != -1) {
                                result.push(ext[j]);
                            }
                        }
                    }

                    if (result.length < ext.length) {
                        //   break;
                        $('#lblImageError').show();
                        $('#lblImageError').text('Invalid File Found. Please select a valid image file of type .pdf.');
                        // setTimeout();
                        return false;
                        event.preventDefault();
                    }



                }

            } else {
               
            }
            // thumbnail checking
            var ext = $("input[name$=fuThumbnail]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuThumbnail]").get(0).files[i].name);
                }
                var x = 0;
                for (i = 0; i < names.length; i++) {
                    var str = names[i];
                    //  /^[-\sa-zA-Z]+$/
                    if (/^[-_a-zA-Z0-9.\u0600-\u06FF ]+$/.test(str) == false) {

                        x = 1;
                    }
                    if (x == 1) {
                        $('#lblThumbnailError').show();
                        $('#lblThumbnailError').text('Special characters except - and _ are not allowed in filename. Please select a valid image file of type .png.');
                        // setTimeout();
                        return false;
                        break;
                        event.preventDefault();
                    }

                }
                if (x == 0) {

                    var ext = [];
                    for (i = 0; i < names.length; i++) {

                        ext.push(names[i].substr(names[i].indexOf(".") + 1).toLowerCase());

                    }
                    var valid_filetype = ["png", "PNG","jpg","JPG","jpeg","JPEG"];

                    var i, j, result = [];
                    for (i = 0; i < valid_filetype.length; i++) {
                        for (j = 0; j < ext.length; j++) {
                            if (ext[j].indexOf(valid_filetype[i]) != -1) {
                                result.push(ext[j]);
                            }
                        }
                    }

                    if (result.length < ext.length) {
                        //   break;
                        $('#lblThumbnailError').show();
                        $('#lblThumbnailError').text('Invalid File Found. Please select a valid image file of type .png.');
                        // setTimeout();
                        return false;
                        event.preventDefault();
                    }



                }

            } else {

            }
        }
    }

</script>