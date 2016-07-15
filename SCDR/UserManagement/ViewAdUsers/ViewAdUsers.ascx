<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAdUsers.ascx.cs" Inherits="SCDR.UserManagement.ViewAdUsers.ViewAdUsers" %>
<div class="col-md-12 col-sm-12 col-xs-12">
     <div class=" col-md-12"> 
     <asp:LinkButton ID="lbAddAdUsers" runat="server" ><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add New User</asp:LinkButton>
  </div>
 
   
    <div class=" col-md-12"> 
<asp:GridView ID="gdvAdUsers" class="table table-hover table-striped" runat="server" AutoGenerateColumns="False"  >
    <Columns>
        <asp:TemplateField HeaderText="First Name">
            <ItemTemplate>
                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Last Name">
            <ItemTemplate>
                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Username">
            <ItemTemplate>
                <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Email Id">
            <ItemTemplate>
                <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="Phone Number">
            <ItemTemplate>
                <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Group Name">
            <ItemTemplate>
                <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbApprove" style="margin-right:5px;" CommandName="approveme" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>Approve</asp:LinkButton>
                <asp:LinkButton ID="lbReject"  CommandName="rejectme" OnClientClick="return confirm('Do you really want to reject?');" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Reject</asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>


</asp:GridView></div>
           
    </div>