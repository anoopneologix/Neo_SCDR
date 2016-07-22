<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewVideoGallery.ascx.cs" Inherits="SCDR.AdminForms.ViewVideoGallery.ViewVideoGallery" %>
<div class="col-md-12 col-sm-12 col-xs-12">
     <div class=" col-md-12"> 
     <asp:LinkButton ID="lbAddVideoGallery" runat="server" OnClick="lbAddVideoGallery_Click" ><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add New Video Gallery</asp:LinkButton>
  </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers>
          <asp:AsyncPostBackTrigger ControlID="rbArabic" EventName="CheckedChanged1" />
          <asp:AsyncPostBackTrigger ControlID="rbEnglish" EventName="CheckedChanged" />
 </Triggers>
        <ContentTemplate>
        <div class="col-md-8">
     <table >
        
       <tr>
           <td>Select Language :</td>
           <td> <asp:RadioButton GroupName="grpLanguage" Checked="true" Text="Arabic"  AutoPostBack="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged1"   />
          <asp:RadioButton GroupName="grpLanguage" Text="English" AutoPostBack="true" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged"    /></td>
       </tr>
         </table>
      <div class=" col-md-12"> 
<asp:GridView ID="gdvVideoGallery" class="table table-hover table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvVideoGallery_RowCommand"  >
    <Columns>
        <asp:TemplateField HeaderText="Title">
            <ItemTemplate>
                <asp:Label ID="lblTitle"  runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Video Url">
            <ItemTemplate>
                <asp:Label ID="lblVideoUrl" runat="server" Text='<%# Eval("VideoUrl") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
            
                <asp:LinkButton ID="lbDelete"  CommandName="delme" OnClientClick="return confirm('Do you really want to delete?');" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete</asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>


</asp:GridView></div>
           </div>
            </ContentTemplate>
          </asp:UpdatePanel>
    </div>