<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
  <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#english" aria-controls="english" role="tab" data-toggle="tab">English</a></li>
    <li role="presentation"><a href="#arabic" aria-controls="arabic" role="tab" data-toggle="tab">Arabic</a></li>
    </ul>

 <!-- Tab panes -->
    <div  style="background-color:white !important">
  <div class="tab-content">
    <div role="tabpanel" class="tab-pane fade in active" id="english">
<!-- Form Begins -->
        <div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Heading : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsHeading" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Date : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsdate"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Location : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsLocation" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
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
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsHeadingAr" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Date : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsDateAr"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Location : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtNewsLocationAr" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
    <div class="form-group">
    <label  class="col-sm-3 control-label">Description : </label>
    <div class="col-sm-9" id="divNewsDescriptionAr">
      <asp:TextBox ID="txtNewsDescriptionAr"  ClientIDMode="Static"  TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
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
        <asp:RadioButton GroupName="grpImage" Text="Yes" Checked="true" ID="rbYes" runat="server" />
          <asp:RadioButton GroupName="grpImage" Text="No" ID="rbNo" runat="server" />
    </div>
  </div>
<div class="form-group">
       <label  class="col-sm-3 control-label">Upload News Images : </label>
    <div class="col-sm-9">
        <asp:FileUpload ID="fuThumbnailImage" AllowMultiple="true" runat="server" />
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" ClientIDMode="Static" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
    </div>
  </div>
       <asp:HiddenField ID="lblUrl" ClientIDMode="Static" runat="server" ></asp:HiddenField>
          <asp:HiddenField ID="lblUrlAr" ClientIDMode="Static" runat="server" ></asp:HiddenField>
            </div>
    </div>
</div>

<div id="ModalTumbnail" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
          <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Choose a Thumbnail</h4>
      </div>
         <div class="modal-body">
             <asp:HiddenField ID="hfListItemId" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hfListItemIdAr" ClientIDMode="Static" runat="server" />
    <div class="row">
        <div class="col-md-12">
        <asp:Repeater ID="repThumbnail" runat="server">
            <HeaderTemplate>
<h3>Select Thumbnail for English News</h3>
            </HeaderTemplate>
            <ItemTemplate>
  <div class="col-xs-6 col-md-3">
    <a id="thubUrl" href='<%# Eval("ImageUrl") %>'  class="thumbnail thumbLink">
      <img  src='<%# Eval("ImageUrl") %>' class="thumbImage">
    </a>
       <a style="display:none" id="thubUrlAr" href='<%# Eval("ImageUrlAr") %>'  class="thumbnail thumbLinkAr">
      <img style="display:none"  src='<%# Eval("ImageUrlAr") %>' class="thumbImage">
    </a>
  </div>
                </ItemTemplate>
            <FooterTemplate><hr></FooterTemplate>
            </asp:Repeater>
            </div>
        <div class="clear"></div>
        <div class="col-md-12">
        <asp:Repeater ID="repthumbnailAr" runat="server">
            <HeaderTemplate>
<h3>Select Thumbnail for Arabic News</h3>
            </HeaderTemplate>
            <ItemTemplate>
  <div class="col-xs-6 col-md-3">
    <a  id="thubUrlAr" href='<%# Eval("ImageUrlAr") %>'  class="thumbnail thumbLinkAr">
      <img  src='<%# Eval("ImageUrlAr") %>' class="thumbImage">
    </a>
      
  </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
</div>
      </div>
      <div class="modal-footer">
        
      
          <asp:Button ID="btnSaveThumbnail" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveThumbnail_Click"  />
        </div>
    </div>
  </div>
</div>
<!--alert div begins-->
 <div id="ModalAlert" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
          <div class="modal-header">
        <h4 class="modal-title" id="myModalAlert">Warning!</h4>
      </div>
         <div class="modal-body">
             <div class="row">
           <div class="col-md-12">
  <p>
  You are about to submit the news without contents on both language
  </p></div>
</div>
      </div>
      <div class="modal-footer">
          <asp:Button ID="btnContinue" runat="server" Text="Continue" />
          <button ID="btnCancel">Cancel</button>
        
        </div>
    </div>
  </div>
</div>
<!--alert div ends-->
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
            $('#ModalTumbnail').modal('show');
        });
        }
 
</script>

<!-- Style for Thumbnail Highlight-->
<style>
.highLightthumbLink
{
    border:1px solid red;
}
.highLightthumbLinkAr
{
    border:1px solid red;
}
</style>

<!-- Thumbnail select script-->
<script>
  
    $(document).ready(function () {
        var addclass = 'highLightthumbLink';
        var addclassAr = 'highLightthumbLinkAr';
        var $cols = $(document).on('click', '.thumbLink', function (e) {
            e.preventDefault();
            $cols.removeClass(addclass);
            $(this).addClass(addclass);
            _this = $(this);
            _href = _this.attr('href');
            $("#lblUrl").val(_href);
        });
        var $colsAr = $(document).on('click', '.thumbLinkAr', function (e) {
            e.preventDefault();
            $colsAr.removeClass(addclassAr);
            $(this).addClass(addclassAr);
            _this = $(this);
            _href = _this.attr('href');
            $("#lblUrlAr").val(_href);
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
  
        $(document).ready(function () {
            $('#btnSubmit').click(function (e) {
                var txtHead = $('#txtNewsHeading');
                var txtHeadAr = $('#txtNewsHeadingAr');
                var txtDate = $('#txtNewsdate');
                var txtDateAr = $('#txtNewsDateAr');
                var txtLocation = $('#txtNewsLocation');
                var txtLocationAr = $('#txtNewsLocationAr');
                var txtDescription = $('#hfNewsDescription');
                var txtDescriptionAr = $('#hfNewsDescriptionAr');
                if (txtHead.val() != null && txtHead.val() != ''
                    && txtHeadAr.val() != null && txtHeadAr.val() != ''
                    && txtDate.val() != null && txtDate.val() != ''
                    && txtDateAr.val() != null && txtDateAr.val() != ''
                    && txtLocation.val() != null && txtLocation.val() != ''
                    && txtLocationAr.val() != null && txtLocationAr.val() != ''
                    && txtDescription.val() != null && txtDescription.val() != ''
                    && txtDescriptionAr.val() != null && txtDescriptionAr.val() != '') {


                }
                else {
                    e.preventDefault();
                    $('#ModalAlert').modal('show');

                }
            });
        });
  
</script>

