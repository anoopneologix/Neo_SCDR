<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditNewsContent.ascx.cs" Inherits="SCDR.AdminForms.EditNewsContent.EditNewsContent" %>
<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<!--Include Bootstrap RichTextBox-->
<link href="../../_layouts/15/SCDR/css/editor.css" rel="stylesheet" />
<div class="col-md-12 col-sm-12 col-xs-12">
    <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Heading : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsHeading" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div><div class="col-sm-3">
      <asp:RegularExpressionValidator ValidationGroup="chk" ID="RegExp1" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_a-zA-Z0-9\u0600-\u06FF'.,() ]{0,250}$" ControlToValidate="txtNewsHeading" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , ( ) are not allowed"></asp:RegularExpressionValidator></div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Date : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsdate"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Location : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsLocation" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
        <div class="col-sm-3">
      <asp:RegularExpressionValidator ValidationGroup="chk" ID="RegularExpressionValidator2" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_a-zA-Z0-9\u0600-\u06FF'.,() ]{0,250}$" ControlToValidate="txtNewsLocation" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , ( ) are not allowed"></asp:RegularExpressionValidator>

        </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Description : </label>
    <div class="col-sm-9" id="divNewsDescription" style="background-color:white">
      <asp:TextBox ID="txtNewsDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
            <asp:HiddenField ID="hfNewsDescription" ClientIDMode="Static" runat="server" ></asp:HiddenField>
   <div class="form-group">
    <label  class="col-sm-3 control-label">select the image you want to delete </label>
    <div class="col-sm-9">
      <asp:GridView ID="gdvAttachments" runat="server" AutoGenerateColumns="False">
          <Columns>
              <asp:TemplateField HeaderText="Image">
                  <ItemTemplate>
                      <asp:Image Width="100px" Height="100px" ID="imgAttachments" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Manage">
    <ItemTemplate>
       <asp:CheckBox ID="chkSelect" runat="server" />
    </ItemTemplate>
    <HeaderTemplate>
    </HeaderTemplate>
   </asp:TemplateField>
          </Columns>
      </asp:GridView>
  </div>
              
  </div>
                  <div class="form-group">
                         <label  class="col-sm-3 control-label">Do you want to update current image? : </label>
   <div class="col-sm-9">
   
        <asp:RadioButton GroupName="chkImage" ClientIDMode="Static" Text="Yes"  ID="chkYes" runat="server" />
                 <asp:RadioButton GroupName="chkImage" ClientIDMode="Static" Checked="true" Text="No" ID="chkNo" runat="server" />
       </div>
                      </div>
 <div class="form-group" id="divNewsPicture">
    <label class="col-sm-3 control-label">Choose an Image:</label>
      <div class="col-sm-6">
     <asp:FileUpload ID="fuNewsImage" AllowMultiple="true" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" runat="server" />
          </div>
     <div class="col-md-3">
            <span style="color:red" id="lblImageError"></span>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuNewsImage"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
     </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ValidationGroup="chk"  OnClientClick="if ( ! UserConfirmation(event)) return false;" ClientIDMode="Static" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
         <asp:Button ID="btnCancel"  Text="cancel" class="btn btn-warning" runat="server" OnClick="btnCancel_Click"  />
    </div>
  </div>
       <asp:HiddenField ID="lblUrl" ClientIDMode="Static" runat="server" ></asp:HiddenField>

</div>
    </div>


<div id="ModalTumbnail" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
          <div class="modal-header">
      
        <h4 class="modal-title" id="myModalLabel">Choose a Thumbnail</h4>
      </div>
         <div class="modal-body">
             <asp:HiddenField ID="hfListItemId" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hfSubsiteName" ClientIDMode="Static" runat="server" />
    <div class="row">
        <asp:Repeater ID="repThumbnail" runat="server">
            <ItemTemplate>
  <div class="col-xs-6 col-md-3">
    <a id="thubUrl" href='<%# Eval("ImageUrl") %>'  class="thumbnail thumbLink">
      <img  src='<%# Eval("ImageUrl") %>' class="thumbImage">
    </a></div>
                </ItemTemplate>
            </asp:Repeater>
</div>
      </div>
      <div class="modal-footer">
        
      
          <asp:Button ID="btnSaveThumbnail" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveThumbnail_Click"   />
        </div>
    </div>
  </div>
</div>
    <!-- datetime picker script-->
<script type="text/javascript" src="../../_layouts/15/SCDR/js/bootstrap-datepicker.min.js" ></script>

 <script type="text/javascript">
                    $(document).ready(function () {
                        $('#txtNewsdate')
                            .datepicker({
                                format: 'dd/MM/yyyy',
                                todayHighlight:true
                            });
                   
                    });
        </script>

<!-- Modal Script-->
<script type="text/javascript">
   
    function openModal() {
        $(document).ready(function () {
            //   $('#ModalTumbnail').modal('show');
            $('#ModalTumbnail').modal({
                backdrop: 'static',
                keyboard: false
            });
        });
    }
</script>

<!-- Style for Thumbnail Highlight-->
<style>
.highLightthumbLink
{
    border:1px solid red;
}

</style>

<!-- Thumbnail select script-->
<script>
  
    $(document).ready(function () {
        var _fhref = $('.thumbLink:first').attr('href');
        $("#lblUrl").val(_fhref);
     
        var addclass = 'highLightthumbLink';
    
        var $cols = $(document).on('click', '.thumbLink', function (e) {
            e.preventDefault();
            $cols.removeClass(addclass);
            $(this).addClass(addclass);
            _this = $(this);
            _href = _this.attr('href');
            $("#lblUrl").val(_href);
       
        });
        //image selection
        $("#divNewsPicture").hide();
       
        $("input[name$=chkImage]").click(function () {
            if ($("#chkYes").is(":checked")) {
                $("#divNewsPicture").show();

            } else {

                $("#divNewsPicture").hide();

            }
           
        });
     

            
    });

</script>

<!-- RichtextBox --->
 <script>
     $(document).ready(function () {
         $("#txtNewsDescription").Editor();
         var savedText = $("#hfNewsDescription").val();
         $("#txtNewsDescription").Editor("setText", savedText);
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtNewsDescription").Editor("getText");
               $("#hfNewsDescription").val(content);

              
           });
        
      
     });
</script>

<!--Submit/Validation Query-->
<script>
  
    function UserConfirmation(event) {
        var rbName = $("input[type=radio][name$=chkImage]:checked").val();
        var txtHead = $('#txtNewsHeading');
     
        var txtDate = $('#txtNewsdate');
    
        var txtLocation = $('#txtNewsLocation');
       
        var txtDescription = $('#hfNewsDescription');
    
        var fileLength = $("input[name$=fuNewsImage]").get(0).files.length;
      
      
     
        if (rbName == "chkYes") {
                if (txtHead.val() != null && txtHead.val() != ''
                    && txtDate.val() != null && txtDate.val() != ''
                    && txtLocation.val() != null && txtLocation.val() != ''
                    && txtDescription.val() != null && txtDescription.val() != ''
                    && fileLength > 0) {
                    var x = validateFormat(event);
                    if (x == true) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
                else {
                    alert("All fields are mandatory");
                    return false;
                    event.preventDefault();
                }
            }
        if (rbName == "chkNo") {
                if (txtHead.val() != null && txtHead.val() != ''
                    && txtDate.val() != null && txtDate.val() != ''
                    && txtLocation.val() != null && txtLocation.val() != ''
                    && txtDescription.val() != null && txtDescription.val() != '') {
                    return true;

                }
                else {
                    alert("All fields are mandatory");
                    return false;
                    event.preventDefault();
                }
            }

         
        }
  
</script>

<!--fileupload Script-->
<script>
    $(document).ready(function () {
        $('#lblImageError').hide();
        $("input[name$=fuNewsImage]").change(function () {
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
            var ext = $("input[name$=fuNewsImage]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuNewsImage]").get(0).files[i].name);
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
                    else
                    {
                        return true;
                    }



                }

            } else {
                return false;
                event.preventDefault();
            }

        }
    }

</script>
