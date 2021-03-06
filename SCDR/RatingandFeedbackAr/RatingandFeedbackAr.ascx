﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
                     
     <asp:RegularExpressionValidator  Display="Dynamic" ID="RegExp1" ValidationGroup="chkFeedback" ForeColor="Red"  ValidationExpression="^[-_\u0621-\u064A0-9'., ]{0,250}$" ControlToValidate="txtFeedback" runat="server" ErrorMessage="أقصى 250 حرفا، حرفا الخاصة باستثناء '. _ -، لا يسمح"></asp:RegularExpressionValidator>                      
                        <asp:Button OnClientClick="if ( ! UserConfirmation(event)) return false;"  usesubmitbehavior="false" ID="btnSUbmit" Text="ارسال" ValidationGroup="chkFeedback"  CssClass="btn btn-warning" runat="server" OnClick="btnSUbmit_Click"  />
                        </div>
                        <div id="divButton" runat="server">
                                <hr />
<asp:Button ID="btnAnonymosSubmit" OnClientClick="if ( ! UserConfirmation(event)) return false;"  usesubmitbehavior="false" CssClass="btn btn-warning" Text="ارسال" runat="server" OnClick="btnAnonymosSubmit_Click"   />
                        </div>
                    </div>
                </div>
              
                
                <!--floating menu area END-->


<script>
    $(document).ready(function () {

        var _val = $("input[name$=input21b]").val();
        if(_val>0)
        {
            $("input[name$=input21b]").prop('disabled', true);
            $("input[name$=input21b]").css('cursor', 'default');
        }
       
    });
</script>

<script type="text/javascript">
    function UserConfirmation(event) {
     

            var ext = $("input[name$=input21b]").val();
            if (ext <= 0) {
              
                alert('موقع الكتروني غير مصنف');
                return false;
            }
            else
            {
                return true;
            }
           
      
    }
    </script>