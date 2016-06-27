<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewVenue.ascx.cs" Inherits="SCDR.AdminForms.ViewVenue.ViewVenue" %>
<div class="col-md-12 col-sm-12 col-xs-12">
     <div class=" col-md-12"> 
     <asp:LinkButton ID="lbAddVenue" runat="server" OnClick="lbAddVenue_Click" ><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add New Venue</asp:LinkButton>
  </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rbArabic" EventName="CheckedChanged" />
             <asp:AsyncPostBackTrigger ControlID="rbEnglish" EventName="CheckedChanged" />
        </Triggers>
        <ContentTemplate>
    <div class=" col-md-6">
     <table >
       <tr>
           <td>Select Language :</td>
           <td> <asp:RadioButton GroupName="grpLanguage" Checked="true" Text="Arabic"  AutoPostBack="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged"   />
          <asp:RadioButton GroupName="grpLanguage" Text="English" AutoPostBack="true" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged"    /></td>
       </tr>
   </table>
      </div>
    <div class=" col-md-12"> 
<asp:GridView ID="gdvVenue" class="table table-hover table-striped" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvVenue_RowCommand" >
    <Columns>
        <asp:TemplateField HeaderText="Venue">
            <ItemTemplate>
                <asp:Label ID="lblVenue" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Address">
            <ItemTemplate>
                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Latitude">
            <ItemTemplate>
                <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("Latitude") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="Longitude">
            <ItemTemplate>
                <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("Longitude") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbEdit" style="margin-right:5px;" CommandName="editme" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>Edit</asp:LinkButton>
                <asp:LinkButton ID="lbDelete"  CommandName="delme" OnClientClick="return confirm('Do you really want to delete?');" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete</asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>


</asp:GridView></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>