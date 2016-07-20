<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCalendarEvents.ascx.cs" Inherits="SCDR.AdminForms.AddCalendarEvents.AddCalendarEvents" %>
<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<link href="../../_layouts/15/SCDR/css/bootstrap-datetimepicker.css" rel="stylesheet" />
<div class="col-md-12 col-sm-12 col-xs-12">
    <!-- Form Begins -->
        <div class="form-horizontal">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbArabic" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rbEnglish" EventName="CheckedChanged" />
                </Triggers>
                <ContentTemplate>
                <div class="form-group">
       <label  class="col-sm-3 control-label">Select Language : </label>
    <div class="col-sm-9">
        <asp:RadioButton GroupName="grpLanguage" AutoPostBack="true" Text="Arabic" Checked="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged" />
          <asp:RadioButton GroupName="grpLanguage" AutoPostBack="true" Text="English" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged" />
    </div>
  </div>
</ContentTemplate>
                  </asp:UpdatePanel>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            
                <ContentTemplate>
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
                    </ContentTemplate>
</asp:UpdatePanel>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Event Date : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventDate" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
          <div class="col-sm-3">
          <asp:RequiredFieldValidator  ControlToValidate="txtEventDate" ValidationGroup="chk" ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Please enter event date"></asp:RequiredFieldValidator>
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
          <asp:RequiredFieldValidator Display="Dynamic"  ControlToValidate="txtEventStartTime" ValidationGroup="chk" ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ErrorMessage="Please enter event start time"></asp:RequiredFieldValidator>
           <asp:RequiredFieldValidator Display="Dynamic"   ControlToValidate="txtEventEndTime" ValidationGroup="chk" ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ErrorMessage="Please enter event end time"></asp:RequiredFieldValidator>
      </div>
 
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Event Description : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtEventDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
         <div class="col-sm-3">
          <asp:RequiredFieldValidator  ControlToValidate="txtEventDescription" ValidationGroup="chk" ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="Please enter an event description"></asp:RequiredFieldValidator>
      </div>
  </div>
            <asp:UpdatePanel ID="upDepartment" runat="server">
                <ContentTemplate>
            <div class="form-group">
    <label  class="col-sm-3 control-label">Department : </label>
    <div class="col-sm-6">
      <asp:DropDownList CausesValidation="true" class="form-control" ID="ddlDepartment" runat="server"></asp:DropDownList>
  </div>
                 <div class="col-sm-3">
          <asp:RequiredFieldValidator InitialValue="0"  ControlToValidate="ddlDepartment" ValidationGroup="chk" ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ErrorMessage="Please select a department"></asp:RequiredFieldValidator>
      </div>
  </div>
                    </ContentTemplate>
          </asp:UpdatePanel>
                 <div class="form-group">
                         <label  class="col-sm-3 control-label">Is recurring? : </label>
   <div class="col-sm-9">
       <asp:RadioButton ID="rbYes" ClientIDMode="Static" Text="Yes" GroupName="chkRecurring" runat="server" />
         <asp:RadioButton ID="rbNo" ClientIDMode="Static" Text="No" Checked="true" GroupName="chkRecurring" runat="server" />
       </div>
                      </div>
            <div  id="divEventRecurring">
 <div class="form-group">
    <label class="col-sm-3 control-label">Frequency:</label>
      <div class="col-sm-9">
          <asp:DropDownList ID="ddlFrequency" runat="server">
              <asp:ListItem Value="0">Daily</asp:ListItem>
              <asp:ListItem Value="1">Weekly</asp:ListItem>
              <asp:ListItem Value="2">Monthly</asp:ListItem>
          </asp:DropDownList>
          </div>
  </div>
                <div class="form-group">
    <label class="col-sm-3 control-label">Number of Occurrence:</label>
      <div class="col-sm-9">
          <asp:DropDownList ID="ddlOccurance" runat="server">
              <asp:ListItem Value="0">2</asp:ListItem>
              <asp:ListItem Value="1">3</asp:ListItem>
              <asp:ListItem Value="2">4</asp:ListItem>
              <asp:ListItem Value="3">5</asp:ListItem>
              <asp:ListItem Value="4">6</asp:ListItem>
              <asp:ListItem Value="5">7</asp:ListItem>
              <asp:ListItem Value="6">8</asp:ListItem>
              <asp:ListItem Value="7">9</asp:ListItem>
              <asp:ListItem Value="8">10</asp:ListItem>
              <asp:ListItem Value="9">11</asp:ListItem>
              <asp:ListItem Value="10">12</asp:ListItem>
           </asp:DropDownList>
          </div>
  </div>

</div>
            
            <div class="form-group">
    <label  class="col-sm-3 control-label">Upload Photo : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuCalendarEvent" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" runat="server" />
   </div>
                  <div class="col-sm-3">
                          <span style="color:red" id="lblImageError"></span>
          <asp:RequiredFieldValidator  ControlToValidate="fuCalendarEvent" ValidationGroup="chk" ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuCalendarEvent" ValidationGroup="chk" runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
                       </div>
  </div>
              <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" OnClientClick="return validateFormat(event);" ValidationGroup="chk" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click"  />
             <asp:Button ID="btnCancel" Text="cancel" class="btn btn-danger" runat="server" OnClick="btnCancel_Click"  />
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
       });
       var rbName = $("input[type=radio][name$=chkRecurring]:checked").val();
       if (rbName == "rbYes")
       {
           $("#divEventRecurring").show();
       }
       else
       {
           $("#divEventRecurring").hide();
       }
      
       $("input[name$=chkRecurring]").click(function () {
             if ($("#rbYes").is(":checked")) {
               $("#divEventRecurring").show();
           } else {
               $("#divEventRecurring").hide();
           }
       });
    </script>

<!--fileupload Script-->
<script>
    $(document).ready(function () {
        $('#lblImageError').hide();
        $("input[name$=fuCalendarEvent]").change(function () {
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
            var ext = $("input[name$=fuCalendarEvent]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuCalendarEvent]").get(0).files[i].name);
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
                event.preventDefault();
            }

        }
    }

</script>