<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCalendarEvents.ascx.cs" Inherits="SCDR.AdminForms.EditCalendarEvents.EditCalendarEvents" %>
<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<link href="../../_layouts/15/SCDR/css/bootstrap-datetimepicker.css" rel="stylesheet" />
<div class="col-md-12 col-sm-12 col-xs-12">
        <!-- Form Begins -->
        <div class="form-horizontal">
            
       <div class="form-group">
    <label  class="col-sm-3 control-label">Event Name : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventName" MaxLength="250" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
      <div class="col-sm-3">
          <asp:RequiredFieldValidator Display="Dynamic"  ControlToValidate="txtEventName" ValidationGroup="chk" ID="req1" runat="server" ForeColor="Red" ErrorMessage="Please enter event name"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegExp1" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="^[-_a-zA-Z0-9\u0600-\u06FF'., ]{0,250}$" ControlToValidate="txtEventName" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator>
            </div>
  </div>
            <div class="form-group">
    <label  class="col-sm-3 control-label">Event Venue : </label>
    <div class="col-sm-6">
        <asp:DropDownList CausesValidation="true" runat="server" class="form-control" ID="ddlEventVenue">

        </asp:DropDownList>
    
  </div>
        <div class="col-sm-3">
         <asp:RequiredFieldValidator InitialValue="0"  ControlToValidate="ddlEventVenue" ValidationGroup="chk" ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Please select a Venue"></asp:RequiredFieldValidator>
      </div>
  </div>
       
            <div class="form-group">
    <label  class="col-sm-3 control-label">Event Date : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventDate" ClientIDMode="Static" runat="server"  class="form-control"  ></asp:TextBox>
  </div>
          <div class="col-sm-3">
          <asp:RequiredFieldValidator  ControlToValidate="txtEventDate" ValidationGroup="chk" ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      </div>
  </div>
            
       <div class="form-group">
    <label  class="col-sm-3 control-label">Event Time : </label>
 
         <div class="col-sm-2">
                  <asp:TextBox ID="txtEventStartTime" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
             </div>
           <div class="col-sm-2">
               <center>
                <label  class="control-label">to </label></center>
               </div>
         <div class="col-sm-2">
      <asp:TextBox ID="txtEventEndTime" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
             </div>
                  <div class="col-sm-3">
          <asp:RequiredFieldValidator Display="Dynamic"  ControlToValidate="txtEventStartTime" ValidationGroup="chk" ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
           <asp:RequiredFieldValidator Display="Dynamic"   ControlToValidate="txtEventEndTime" ValidationGroup="chk" ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      </div>
 
  </div>

            <div class="form-group">
    <label  class="col-sm-3 control-label">Event Description : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
         <div class="col-sm-3">
          <asp:RequiredFieldValidator  ControlToValidate="txtEventDescription" ValidationGroup="chk" ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      </div>
  </div>

            <div class="form-group">
    <label  class="col-sm-3 control-label">Department : </label>
    <div class="col-sm-6">
      <asp:DropDownList CausesValidation="true" class="form-control" ID="ddlDepartment" runat="server"></asp:DropDownList>
  </div>
                 <div class="col-sm-3">
          <asp:RequiredFieldValidator InitialValue="0"  ControlToValidate="ddlDepartment" ValidationGroup="chk" ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      </div>
  </div>

               
                 <div class="form-group">
    <label  class="col-sm-3 control-label">Saved Image : </label>
    <div class="col-sm-6">
    <asp:Image ID="imgEvent" Width="100px" Height="100px" runat="server" /> </div>
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
 <div class="form-group" id="divEventPicture">
    <label class="col-sm-3 control-label">Choose an Image:</label>
      <div class="col-sm-6">
     <asp:FileUpload ID="fuEventImage" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" runat="server" />
          </div>
     <div class="col-md-3">
            <span style="color:red" id="lblImageError"></span>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuEventImage"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
     </div>
  </div>
                <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ValidationGroup="chk" Text="submit" OnClientClick="return validateFormat(event);" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
             <asp:Button ID="btnCancel" Text="cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click" />
    </div>
  </div>
                   
         
    
            </div>
    <!-- Form Ends -->
    </div>
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

                       $('#txtEventStartTime').datetimepicker({
                           format: 'LT'
                       });
                       $('#txtEventEndTime').datetimepicker({
                           format: 'LT'
                       });
                       $("#divEventPicture").hide();
                       $("input[name='chkImage']").click(function () {
                           if ($("#chkYes").is(":checked")) {
                               $("#divEventPicture").show();
                           } else {
                               $("#divEventPicture").hide();
                           }
                       });

            
       });
    </script>

<!--fileupload Script-->
<script>
    $(document).ready(function () {
        $('#lblImageError').hide();
        $("input[name$=fuEventImage]").change(function () {
            $('#lblImageError').hide();
        });


    });
</script>

<!--fileupload Script-->
<script type="text/javascript">
    function validateFormat(event) {
        if (Page_ClientValidate()) {
            $('#lblGrpError').text(' ');
            $('#lblRankError').text(' ');
            var ext = $("input[name$=fuEventImage]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuEventImage]").get(0).files[i].name);
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