﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNews.ascx.cs" Inherits="SCDR.AdminForms.AddNews.AddNews" %>

<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<!--Include Bootstrap RichTextBox-->
<link href="../../_layouts/15/SCDR/css/editor.css" rel="stylesheet" />
<div class="col-md-12 col-sm-12 col-xs-12">
 <!-- Nav tabs -->
    <table>
        <tr>
            <td>Select Language : </td>
            <td>
                 <asp:RadioButton GroupName="grpLanguage" ClientIDMode="Static" Text="Arabic"  ID="rbArabic" runat="server" />
                 <asp:RadioButton GroupName="grpLanguage" ClientIDMode="Static"  Text="English" ID="rbEnglish" runat="server" />
                <asp:RadioButton GroupName="grpLanguage" ClientIDMode="Static"  Text="Both" Checked="true" ID="rbBoth" runat="server" />
            </td>
        </tr>
    </table>
  <ul id="myTabs" class="nav nav-tabs" role="tablist">
    <li role="presentation" id="panelEn" ><a href="#english" data-toggle="tab" aria-controls="english" role="tab" >English</a></li>
    <li role="presentation" id="panelAr" ><a href="#arabic" data-toggle="tab" aria-controls="arabic" role="tab" >Arabic</a></li>
    </ul>

 <!-- Tab panes -->
    <div  style="background-color:white !important">
  <div class="tab-content">
    <div role="tabpanel" class="tab-pane fade in active" id="english">
<!-- Form Begins -->
        <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Heading : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsHeading" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div><div class="col-sm-3">
      <asp:RegularExpressionValidator ID="RegExp1" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_a-zA-Z0-9'., ]{0,250}$" ControlToValidate="txtNewsHeading" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator></div>
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
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_a-zA-Z0-9'., ]{0,250}$" ControlToValidate="txtNewsLocation" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator>

        </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Description : </label>
    <div class="col-sm-9" id="divNewsDescription">
      <asp:TextBox ID="txtNewsDescription"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
            <asp:HiddenField ID="hfNewsDescription" ClientIDMode="Static" runat="server" ></asp:HiddenField>
</div>
        <!-- Form Ends -->
    </div>
    <div role="tabpanel" class="tab-pane fade" id="arabic">
<!-- Form Begins -->
        <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Heading : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsHeadingAr" style="direction:rtl" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
       <div class="col-sm-3">
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_0-9\u0600-\u06FF'., ]{0,250}$" ControlToValidate="txtNewsHeadingAr" runat="server" ErrorMessage="Maximum 250 characters allowed. Only Arabic characters and Numbers are allowed. Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator>

        </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Date : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsDateAr" style="direction:rtl"   ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Location : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtNewsLocationAr" style="direction:rtl"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
          <div class="col-sm-3">
      <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" Display="Dynamic"  ValidationExpression="^[-_0-9\u0600-\u06FF'., ]{0,250}$" ControlToValidate="txtNewsLocationAr" runat="server" ErrorMessage="Maximum 250 characters allowed. Only Arabic characters and Numbers are allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator>

        </div>
      
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Description : </label>
    <div class="col-sm-9" id="divNewsDescriptionAr">
      <asp:TextBox ID="txtNewsDescriptionAr" style="direction:rtl"   ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
      <asp:HiddenField ID="hfNewsDescriptionAr" ClientIDMode="Static" runat="server" ></asp:HiddenField>
  
</div>
        <!-- Form Ends -->
    </div> 
    </div>
    <div class="form-horizontal">
      <div class="form-group">
       <label  class="col-sm-3 control-label">Is there Images to upload : </label>
    <div class="col-sm-9">
        <asp:RadioButton ClientIDMode="Static" GroupName="grpImage" Text="Yes" Checked="true" ID="rbYes" runat="server" />
          <asp:RadioButton ClientIDMode="Static" GroupName="grpImage" Text="No" ID="rbNo" runat="server" />
    </div>
  </div>
<div class="form-group" id="uploadImages">
       <label  class="col-sm-3 control-label">Upload News Images : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuThumbnailImage" AllowMultiple="true" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" runat="server" />
    </div>
     <div class="col-sm-3">
                          <span style="color:red" id="lblImageError"></span>
         
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuThumbnailImage" runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
                       </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit"  OnClientClick="if ( ! UserConfirmation(event)) return false;" ClientIDMode="Static" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
    </div>
  </div>
       <asp:HiddenField ID="lblUrl" ClientIDMode="Static" runat="server" ></asp:HiddenField>
      
            </div>
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
                <asp:HiddenField ID="hfListItemIdAr" ClientIDMode="Static" runat="server" />
    <div class="row">
     
        <asp:Repeater ID="repThumbnail" runat="server">
          
            <ItemTemplate>
  <div class="col-xs-6 col-md-3">
    <a id="thubUrl" href='<%# Eval("ImageUrl") %>'  class="thumbnail thumbLink">
      <img  src='<%# Eval("ImageUrl") %>' class="thumbImage">
    </a>
      
  </div>
                </ItemTemplate>
            <FooterTemplate><hr></FooterTemplate>
            </asp:Repeater>
  
       
        
</div>
      </div>
      <div class="modal-footer">
        
      
          <asp:Button ID="btnSaveThumbnail" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveThumbnail_Click"  />
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
                     /*   $.fn.datepicker.dates['ar-tn'] = {
                            days: ["الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت", "الأحد"],
                            daysShort: ["أحد", "اثنين", "ثلاثاء", "أربعاء", "خميس", "جمعة", "سبت", "أحد"],
                            daysMin: ["ح", "ن", "ث", "ع", "خ", "ج", "س", "ح"],
                            months: ["جانفي", "فيفري", "مارس", "أفريل", "ماي", "جوان", "جويليه", "أوت", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"],
                            monthsShort: ["جانفي", "فيفري", "مارس", "أفريل", "ماي", "جوان", "جويليه", "أوت", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"],
                            today: "هذا اليوم",
                            rtl: true
                        };*/
                        $('#txtNewsDateAr')
                            .datepicker({
                              /*  language: 'ar-tn',*/
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
   

            
    });

</script>

<!-- RichtextBox --->
 <script>
     $(document).ready(function () {
         $("#txtNewsDescription").Editor();
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtNewsDescription").Editor("getText");
               $("#hfNewsDescription").val(content);
           });
         $("#txtNewsDescriptionAr").Editor();
         $(".Editor-editor")
           .blur(function () {
               var content = $("#txtNewsDescriptionAr").Editor("getText");
               $("#hfNewsDescriptionAr").val(content);
           });
     });
</script>

<!--Submit/Validation Query-->
<script>
  
    function UserConfirmation(event) {
        var rbName = $("input[type=radio][name$=grpLanguage]:checked").val();
        var txtHead = $('#txtNewsHeading');
        var txtHeadAr = $('#txtNewsHeadingAr');
        var txtDate = $('#txtNewsdate');
        var txtDateAr = $('#txtNewsDateAr');
        var txtLocation = $('#txtNewsLocation');
        var txtLocationAr = $('#txtNewsLocationAr');
        var txtDescription = $('#hfNewsDescription');
        var txtDescriptionAr = $('#hfNewsDescriptionAr');
        var fileLength = $("input[name$=fuThumbnailImage]").get(0).files.length;
        var rbImageName = $("input[type=radio][name$=grpImage]:checked").val();
       
        if (rbName == "rbBoth") {
            if (rbImageName == "rbYes") {
                if (txtHead.val() != null && txtHead.val() != ''
                 && txtHeadAr.val() != null && txtHeadAr.val() != ''
                 && txtDate.val() != null && txtDate.val() != ''
                 && txtDateAr.val() != null && txtDateAr.val() != ''
                 && txtLocation.val() != null && txtLocation.val() != ''
                 && txtLocationAr.val() != null && txtLocationAr.val() != ''
                 && txtDescription.val() != null && txtDescription.val() != ''
                 && txtDescriptionAr.val() != null && txtDescriptionAr.val() != ''
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
            
            
           

            if (rbImageName == "rbNo") {
                if (txtHead.val() != null && txtHead.val() != ''
                 && txtHeadAr.val() != null && txtHeadAr.val() != ''
                 && txtDate.val() != null && txtDate.val() != ''
                 && txtDateAr.val() != null && txtDateAr.val() != ''
                 && txtLocation.val() != null && txtLocation.val() != ''
                 && txtLocationAr.val() != null && txtLocationAr.val() != ''
                 && txtDescription.val() != null && txtDescription.val() != ''
                 && txtDescriptionAr.val() != null && txtDescriptionAr.val() != '')
                 {
                    return true;
                }
                else {
                    alert("All fields are mandatory");
                    return false;
                    event.preventDefault();

                }
            }
           
       
            

            }
              
        
        else if (rbName == "rbEnglish")
        {
            if (rbImageName == "rbYes") {
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
            if (rbImageName == "rbNo") {
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
        else if (rbName == "rbArabic") {
            if (rbImageName == "rbYes") {
                if (txtHeadAr.val() != null && txtHeadAr.val() != ''
                    && txtDateAr.val() != null && txtDateAr.val() != ''
                    && txtLocationAr.val() != null && txtLocationAr.val() != ''
                    && txtDescriptionAr.val() != null && txtDescriptionAr.val() != ''
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
            if (rbImageName == "rbNo") {
                if (txtHeadAr.val() != null && txtHeadAr.val() != ''
                    && txtDateAr.val() != null && txtDateAr.val() != ''
                    && txtLocationAr.val() != null && txtLocationAr.val() != ''
                    && txtDescriptionAr.val() != null && txtDescriptionAr.val() != '') {
                    return true;

                }
                else {
                    alert("All fields are mandatory");
                    return false;
                    event.preventDefault();
                }
            }

        }
         
        }
  
</script>

<!--fileupload Script-->
<script>
    $(document).ready(function () {
        $('#lblImageError').hide();
        $("input[name$=fuThumbnailImage]").change(function () {
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
            var ext = $("input[name$=fuThumbnailImage]").get(0).files.length;
            if (ext > 0) {
                var names = [];
                for (var i = 0; i < ext; ++i) {
                    names.push($("input[name$=fuThumbnailImage]").get(0).files[i].name);
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

<script>
    $(document).ready(function () {
/* language selection for panel update */
        function ArabicLanguage()
        {
            $("#panelEn").removeClass('active');
            $("#panelEn").addClass('disabled');
            $("#panelEn").find('a').removeAttr("data-toggle", "tab");
            $("#panelEn").find('a').removeAttr("aria-expanded", "true");
            $("#panelAr").removeClass('disabled');
            $("#panelAr").addClass('active');
            $("#panelAr").find('a').attr("data-toggle", "tab");
            $("#panelAr").find('a').removeAttr("aria-expanded", "false");
            $("#panelAr").find('a').attr("aria-expanded", "true");
            $("#arabic").addClass('active in');
            $("#english").removeClass('active in');
        }
        function EnglishLanguage() {
            $("#panelAr").removeClass('active');
            $("#panelAr").addClass('disabled');
            $("#panelAr").find('a').removeAttr("data-toggle", "tab");
            $("#panelAr").find('a').removeAttr("aria-expanded", "true");
            $("#panelAr").find('a').attr("aria-expanded", "false");
            $("#panelEn").removeClass('disabled');
            $("#panelEn").addClass('active');
            $("#panelEn").find('a').attr("data-toggle", "tab");
            $("#panelEn").find('a').removeAttr("aria-expanded", "false");
            $("#panelEn").find('a').attr("aria-expanded", "true");
            $("#english").addClass('active in');
            $("#arabic").removeClass('active in');
        }
        function BothLanguage() {
            $("#panelEn").addClass('active');
            $("#panelEn").removeClass('disabled');
            $("#panelEn").find('a').attr("data-toggle", "tab");
            $("#panelEn").find('a').removeAttr("aria-expanded", "false");
            $("#panelEn").find('a').attr("aria-expanded", "true");
            $("#panelAr").removeClass('active');
            $("#panelAr").removeClass('disabled');
            $("#panelAr").find('a').attr("data-toggle", "tab");
            $("#panelAr").find('a').removeAttr("aria-expanded", "true");
            $("#panelAr").find('a').attr("aria-expanded", "false");
            $("#english").addClass('active in');
            $("#arabic").removeClass('active in');
        }
        var rbName = $("input[type=radio][name$=grpLanguage]:checked").val();
        if (rbName == "rbBoth") {
            BothLanguage();
        }
        else if (rbName == "rbEnglish") {
           
            EnglishLanguage();
        }
        else if (rbName == "rbArabic") {
            ArabicLanguage();
        }
        $("input[name$=grpLanguage]").click(function () {
            if ($("#rbEnglish").is(":checked")) {
                EnglishLanguage();

            } else if ($("#rbArabic").is(":checked")) {

                ArabicLanguage();

            }
            else if ($("#rbBoth").is(":checked")) {
                BothLanguage();
            }
        });
        /* language selection for panel update */
        /* Image upload selection for file upload control update */
        var rbImageName = $("input[type=radio][name$=grpImage]:checked").val();
        if (rbImageName == "rbYes") {
            $('#uploadImages').show();
        }
        else if (rbImageName == "rbNo") {

            $('#uploadImages').hide();
        }
        $("input[name$=grpImage]").click(function () {
            if ($("#rbYes").is(":checked")) {
                $('#uploadImages').show();

            } else if ($("#rbNo").is(":checked")) {

                $('#uploadImages').hide();

            }
          
        });
      
        /* Image upload selection for file upload control update */


        
    });
    </script>