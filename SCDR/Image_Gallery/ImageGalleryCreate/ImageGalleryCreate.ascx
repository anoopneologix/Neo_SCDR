<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageGalleryCreate.ascx.cs" Inherits="SCDR.Image_Gallery.ImageGalleryCreate.ImageGalleryCreate" %>
<!-- Form Begins -->
<div class="form-horizontal">
  <div class="form-group">
    <label  class="col-sm-3 control-label">Title : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-3 control-label">Category Name : </label>
    <div class="col-sm-9">
      <asp:TextBox ID="txtGroupName"  ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
  </div>
  </div> 
<div class="form-group">
       <label  class="col-sm-3 control-label">Upload Images : </label>
    <div class="col-sm-9">
        <asp:FileUpload ID="fuThumbnailImage" AllowMultiple="true" runat="server" />
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-3 col-sm-9">
        <asp:Button ID="btnSubmit" Text="submit" class="btn btn-default" runat="server" OnClick="btnSubmit_Click" />
    </div>
  </div>
      <asp:HiddenField ID="lblUrl" ClientIDMode="Static" runat="server" ></asp:HiddenField>
</div>

<!-- Form Ends -->

<div id="ModalTumbnail" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
          <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Choose a Thumbnail</h4>
      </div>
         <div class="modal-body">
             <asp:HiddenField ID="hfListItemId" ClientIDMode="Static" runat="server" />
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
        
      
          <asp:Button ID="btnSaveThumbnail" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveThumbnail_Click"  />
        </div>
    </div>
  </div>
</div>

<!-- Modal Script-->
<script type="text/javascript">
   
    function openModal() {
        $(document).ready(function () {
            $('#ModalTumbnail').modal('show');
        });
        }
 
</script>

<script>
  
    $(document).ready(function () {

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
