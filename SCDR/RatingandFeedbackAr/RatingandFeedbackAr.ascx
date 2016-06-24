<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RatingandFeedbackAr.ascx.cs" Inherits="SCDR.RatingandFeedbackAr.RatingandFeedbackAr" %>
 <!--floating menu area-->
                <div class="star">
                    <img src="../_layouts/15/SCDR/images/star.png" alt=""><span id="spanRating" runat="server" class="star_text"></span>
                    <div class="map_out">
                        <h1 id="headingRating" runat="server"></h1>
                        <asp:TextBox id="input21b" ClientIDMode="Static" value="0" type="number" class="rating" min="0" max="5" step="0.5" data-size="lg" data-rtl="1" runat="server"></asp:TextBox>
                      <!--  <h2 id="yourRating" runat="server" class="result-display"></h2>
                      <asp:TextBox id="input21c" ClientIDMode="Static" value="0" type="number" class="rating" min="0" max="5" step="0.5" data-size="lg" data-rtl="0" runat="server"></asp:TextBox>-->
                    
                        <h2 id="averageRating" runat="server" class="result-display"></h2>
                           <div id="divShowFeedback" runat="server">
                                    <hr />
                                 <h2 runat="server">رد الفعل لشئ ما:</h2>
                        <asp:TextBox ID="txtShowFeedBack" TextMode="MultiLine" runat="server"></asp:TextBox>
                         
                                </div>
                      <!--  <hr />
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalRating">تعليقات عرض</button>-->
                       <div id="divAddComments" runat="server">
                             <hr />
                        <h2 id="headingfeedback" runat="server"></h2>
                        <asp:TextBox ID="txtFeedback" TextMode="MultiLine" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[\u0621-\u064A0-9 ]+$"
    ControlToValidate="txtFeedback" ValidationGroup="chkFeedback" runat="server" ForeColor="Red" ErrorMessage=" فقط الأحرف العربية و الأرقام المسموح بها"
    Display="Dynamic" />
                          
                        <asp:Button  usesubmitbehavior="false" ID="btnSUbmit" Text="ارسال" ValidationGroup="chkFeedback"  CssClass="btn btn-warning" runat="server" OnClick="btnSUbmit_Click"  />
                        </div>
                        <div id="divButton" runat="server">
                                <hr />
<asp:Button ID="btnAnonymosSubmit"  usesubmitbehavior="false" CssClass="btn btn-warning" Text="ارسال" runat="server" OnClick="btnAnonymosSubmit_Click"   />
                        </div>
                    </div>
                </div>
              
                
                <!--floating menu area END-->
<!--
<div id="modalRating" style="z-index:50000 !important" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Comments</h4>
      </div>
      <div class="modal-body">
        
          <asp:Repeater ID="repFeedback" runat="server">
              <HeaderTemplate>
      <div class="row">
          </HeaderTemplate>
              <ItemTemplate>
          <div class="col-md-3"><# Eval("Username") %> :</div>
          <div class="col-md-9"><# Eval("Feedback") %></div>
                  </ItemTemplate>
              <FooterTemplate>
        </div>
                  </FooterTemplate>
              </asp:Repeater>
   
              <asp:Repeater ID="repAllComments" runat="server">
              <HeaderTemplate>
      <div class="row">
          </HeaderTemplate>
              <ItemTemplate>
          <div class="col-md-3"><# Eval("Username") %> :</div>
          <div class="col-md-9"><# Eval("Feedback") %></div>
                  </ItemTemplate>
              <FooterTemplate>
        </div>
                  </FooterTemplate>
              </asp:Repeater>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
    </div>
  </div>
</div>-->

<script>
    $(document).ready(function () {

        var _val = $("#input21b").val();
        if(_val>0)
        {
            $("#input21b").prop('disabled', true);
        }
       
    });
</script>