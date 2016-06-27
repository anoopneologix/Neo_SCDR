<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddImageGallery.ascx.cs" Inherits="SCDR.AdminForms.AddImageGallery.AddImageGallery" %>
<!--Validation Scripts-->

<!-- Form Begins -->
<div class="form-horizontal">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtGroupName" EventName="TextChanged" />
             <asp:AsyncPostBackTrigger ControlID="txtRanking" EventName="TextChanged" />
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
  
  <div class="form-group">
    <label  class="col-sm-3 control-label">Category Name : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtGroupName" MaxLength="250"  AutoPostBack="true"  ClientIDMode="Static" runat="server" class="required form-control" OnTextChanged="txtGroupName_TextChanged"></asp:TextBox>
  
         </div> <div class="col-sm-3">
        <asp:Label ID="lblGrpError" ClientIDMode="Static" CssClass="control-label" runat="server"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="chk" ForeColor="Red" ControlToValidate="txtGroupName" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>     
 <asp:RegularExpressionValidator ID="RegExp1" ForeColor="Red" Display="Dynamic" ValidationGroup="chk" ValidationExpression="^[-_a-zA-Z0-9\u0600-\u06FF'., ]{0,250}$" ControlToValidate="txtGroupName" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator> </div>
       </div> 
     <div class="form-group">
    <label  class="col-sm-3 control-label">Rank : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtRanking"  MaxLength="5" AutoPostBack="true"  ClientIDMode="Static" runat="server" class="required form-control" OnTextChanged="txtRanking_TextChanged"></asp:TextBox>
     </div>  <div class="col-sm-3">
          <asp:Label ID="lblRankError" ClientIDMode="Static" CssClass="control-label" runat="server"></asp:Label>
         <span style="color:red" id="errmsg"></span> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="chk" ForeColor="Red" ControlToValidate="txtRanking" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
 
    </div>
  </div> 
     <div class="form-group">
    <label  class="col-sm-3 control-label">Title : </label>
    <div class="col-sm-6">
      <asp:TextBox ID="txtTitle" MaxLength="250" runat="server" class="required form-control"></asp:TextBox>
        </div> <div class="col-sm-3">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="chk" Display="Dynamic" ControlToValidate="txtTitle" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="chk" ForeColor="Red" Display="Dynamic" ValidationExpression="^[-_a-zA-Z0-9\u0600-\u06FF'., ]{0,250}$" ControlToValidate="txtTitle" runat="server" ErrorMessage="Maximum 250 characters allowed.Special characters except ' . _ - , are not allowed"></asp:RegularExpressionValidator>
       
             </div>
  </div>
            </ContentTemplate>
    </asp:UpdatePanel>

<div class="form-group">
       <label  class="col-sm-3 control-label">Upload Images : </label>
    <div class="col-sm-6">
        <asp:FileUpload ID="fuThumbnailImage" accept="image/jpeg,image/jpg,image/png,image/JPEG,image/PNG,image/JPG" AllowMultiple="true" runat="server" />
    </div>
    <div class="col-sm-3">
       <span style="color:red" id="lblImageError"></span>
    <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidator4" ValidationGroup="chk" ForeColor="Red" ControlToValidate="fuThumbnailImage" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>  
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^.*\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG])$"
    ControlToValidate="fuThumbnailImage"  runat="server" ForeColor="Red" ErrorMessage="Please select a valid image file of type .jpg,.jpeg,.png."
    Display="Dynamic" />
    </div>
 
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ClientIDMode="Static" OnClientClick="return validateFormat(event);"  ID="btnSubmit" Text="submit" ValidationGroup="chk" class="btn btn-default" runat="server" OnClick="btnSubmit_Click"  />
    </div>
  </div>
      <asp:HiddenField ID="lblUrl" ClientIDMode="Static" runat="server" ></asp:HiddenField>
</div>

<!-- Form Ends -->

<div id="ModalTumbnail"  class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
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
        
      
          <asp:Button ClientIDMode="Static"  ID="btnSaveThumbnail" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveThumbnail_Click"   />
        </div>
    </div>
  </div>
</div>

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
<script>
  
    $(document).ready(function () {
        var _fhref = $('.thumbLink:first').attr('href');
        $("#lblUrl").val(_fhref);
        $(document).on('click', '.thumbLink', function (e) {
            e.preventDefault();
            _this = $(this);
            _href = _this.attr('href');
            $("#lblUrl").val(_href);
        })
            /*.on('click', '.thumbImage', function (e) {
            e.preventDefault();
            _this = $(this);
            _href = _this.parent().attr('href');
            $("#lblUrl").val(_href);
        })*/
    });

</script>
<!-- Modal Script-->
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



                }

            } else {
                event.preventDefault();
            }

            }
        }
    
</script>

<!-- Panel Refresh-->
 <script type="text/javascript"> 
     // if you use jQuery, you can load them when dom is read.
     $(document).ready(function () {
         var prm = Sys.WebForms.PageRequestManager.getInstance();    
         prm.add_initializeRequest(InitializeRequest);
         prm.add_endRequest(EndRequest);

     });        

     function InitializeRequest(sender, args) {
      
     }

     function EndRequest(sender, args) {

         //called when key is pressed in textbox
         $("#txtRanking").keypress(function (e) {
             //if the letter is not digit then display error and don't type anything
             if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                 //display error message
                 $("#errmsg").html("Digits Only").show().fadeOut("slow");
                 return false;
             }
         });
     }
     </script>
<!-- Panel Refresh-->
