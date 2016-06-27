<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCalendarEvents.ascx.cs" Inherits="SCDR.AdminForms.ViewCalendarEvents.ViewCalendarEvents" %>
<div class="col-md-12 col-sm-12 col-xs-12">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rbArabic" EventName="CheckedChanged" />
          <asp:AsyncPostBackTrigger ControlID="rbEnglish" EventName="CheckedChanged" />
    </Triggers>
        <ContentTemplate>
        <div class="col-md-6">
     <table >
       <tr>
           <td>Select Language :</td>
           <td> <asp:RadioButton GroupName="grpLanguage" Checked="true" Text="Arabic"  AutoPostBack="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged"   />
          <asp:RadioButton GroupName="grpLanguage" Text="English" AutoPostBack="true" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged"    /></td>
       </tr>
   </table>
      </div>
    <div class="clearfix"></div>
      <div class=" col-md-12"> 
          <asp:GridView ID="gdvEvents" class="table table-hover table-striped" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gdvEvents_PageIndexChanging" PageSize="30">
              <Columns>
              
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venue">
            <ItemTemplate>
                <asp:Label ID="lblVenue" runat="server" Text='<%# Eval("EventVenue") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EventDate","{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Time">
            <ItemTemplate>
                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("EventTime") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 
                <asp:TemplateField HeaderText="Department">
            <ItemTemplate>
                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>   
          
              </Columns>

          </asp:GridView>
          </div>
            </ContentTemplate>
          </asp:UpdatePanel>
    </div>