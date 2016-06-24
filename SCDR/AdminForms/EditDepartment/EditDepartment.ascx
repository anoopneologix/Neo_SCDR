<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDepartment.ascx.cs" Inherits="SCDR.AdminForms.EditDepartment.EditDepartment" %>
<div class="col-md-12 col-sm-12 col-xs-12">
        <!-- Form Begins -->
        <div class="form-horizontal">
            <div class="form-group">
    <label  class="col-sm-3 control-label">Name of the Department : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtDepartment" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div><div class="col-sm-3">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="chk" ControlToValidate="txtDepartment" runat="server" ErrorMessage="Please enter a department name"></asp:RequiredFieldValidator>
  </div>
        </div>
            <div class="form-group">
    <label  class="col-sm-3 control-label">Saved Image : </label>
    <div class="col-sm-6">
    <asp:Image ID="imgDepartment" Width="100px" Height="100px" runat="server"  /> </div>
                 <div class="col-sm-3">
             </div>
  </div>
                  <div class="form-group">
                         <label  class="col-sm-3 control-label">Do you want to update current image? : </label>
   <div class="col-sm-9">
    <label><input id="chkYes" type="radio" name="chkImage" value="Yes"> Yes</label>
    <label><input id="chkNo" type="radio" checked="checked" name="chkImage" value="No"> No</label>
       </div>
                      </div>
 <div class="form-group" id="divDepartmentPicture">
    <label class="col-sm-3 control-label">Choose an Image:</label>
      <div class="col-sm-6">
        <asp:FileUpload ID="fuDepartmentIcon" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" runat="server" />
    </div>
    <div class="col-sm-3">
         <span style="color:red" id="lblImageError"></span>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuDepartmentIcon"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
    </div>
  </div>
<div class="form-group">
       <label  class="col-sm-3 control-label">Status : </label>
    <div class="col-sm-9">
        <asp:RadioButton GroupName="grpStatus" Text="Active" Checked="true" ID="rbActive" runat="server" />
          <asp:RadioButton GroupName="grpStatus" Text="Inactive" ID="rbInactive" runat="server" />
    </div>
  </div>
<div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ValidationGroup="chk" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
             <asp:Button ID="btnCancel" Text="cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click"  />
    </div>
  </div>
            </div>
    </div>

<script>
    $(document).ready(function () {
        $("#divDepartmentPicture").hide();
        $("input[name='chkImage']").click(function () {
            if ($("#chkYes").is(":checked")) {
                $("#divDepartmentPicture").show();
            } else {
                $("#divDepartmentPicture").hide();
            }
        });
        $('#lblImageError').hide();
        $("input[name$=fuDepartmentIcon]").change(function () {
            $('#lblImageError').hide();
        });
    });
</script>
<script type="text/javascript">
    function validateFormat(event) {
        if (Page_ClientValidate()) {
            $('#lblGrpError').text(' ');
            $('#lblRankError').text(' ');
            var ext = $("input[name$=fuDepartmentIcon]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuDepartmentIcon]").get(0).files[i].name);
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
                        $('#lblImageError').text('Special characters except - and _ are not allowed in filename. Please select a valid image file of type .jpg,.jpeg,.png.');
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
                    var valid_filetype = ["jpg", "jpeg", "png", "PNG", "JPEG", "JPG"];

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
                        $('#lblImageError').text('Invalid File Found. Please select a valid image file of type .jpg,.jpeg,.png.');
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