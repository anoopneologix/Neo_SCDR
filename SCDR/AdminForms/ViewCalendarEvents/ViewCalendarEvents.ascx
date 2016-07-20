<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCalendarEvents.ascx.cs" Inherits="SCDR.AdminForms.ViewCalendarEvents.ViewCalendarEvents" %>

<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />

<div class="col-md-12 col-sm-12 col-xs-12">
     <div class=" col-md-12"> 
     <asp:LinkButton ID="lbAddEvent" runat="server" OnClick="lbAddEvent_Click"  ><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add New Event</asp:LinkButton>
  </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rbArabic" EventName="CheckedChanged" />
          <asp:AsyncPostBackTrigger ControlID="rbEnglish" EventName="CheckedChanged" />
         <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
    </Triggers>
        <ContentTemplate>
        <div class="col-md-8">
     <table >
        
       <tr>
           <td>Select Language :</td>
           <td> <asp:RadioButton GroupName="grpLanguage" Checked="true" Text="Arabic"  AutoPostBack="true" ID="rbArabic" runat="server" OnCheckedChanged="rbArabic_CheckedChanged"   />
          <asp:RadioButton GroupName="grpLanguage" Text="English" AutoPostBack="true" ID="rbEnglish" runat="server" OnCheckedChanged="rbEnglish_CheckedChanged"    /></td>
       </tr>
         <tr>
             <td><asp:Label ID="lblEventName" runat="server" Text="Event Name :"></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtEventName" CssClass="form-control" runat="server"></asp:TextBox></td>
         </tr>
         <tr>
           <td><asp:Label ID="lblStartTime" runat="server" Text="Start Time : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtEventDate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox></td>
            <td>&nbsp;</td>  <td><asp:Label ID="lblEndTime" runat="server" Text="End Time : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtEventEndDate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox></td>
         </tr>
         <tr>
             <td>&nbsp;</td>
         <td>
             <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" CssClass="btn btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
         </td>
         </tr>
   </table>
      </div>
    <div class="clearfix"></div>
      <div class=" col-md-12"> 
          <asp:GridView ID="gdvEvents" class="table table-hover table-striped" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gdvEvents_PageIndexChanging" PageSize="30" OnRowCommand="gdvEvents_RowCommand">
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
                     <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbEdit" style="margin-right:5px;" CommandName="editme" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>Edit</asp:LinkButton>
                <asp:LinkButton ID="lbDelete"  CommandName="delme" OnClientClick="return confirm('Do you really want to delete?');" runat="server" CommandArgument='<%# Eval("ID") %>'><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete</asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
          
              </Columns>

          </asp:GridView>
          </div>
            </ContentTemplate>
          </asp:UpdatePanel>
    </div>

<script type="text/javascript" src="../../_layouts/15/SCDR/js/moment.js"></script>
<script type="text/javascript" src="../../_layouts/15/SCDR/js/bootstrap-datepicker.min.js" ></script>
<script type="text/javascript">
       $(document).ready(function () {
           $('#txtEventDate')
               .datepicker({
                   format: 'dd/MM/yyyy',
                   todayHighlight: true
               });
           $('#txtEventEndDate')
          .datepicker({
              format: 'dd/MM/yyyy',
              todayHighlight: true
          });
          
       });
      
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

     function EndRequest(sender, args) 
     {
         $('#txtEventDate')
               .datepicker({
                   format: 'dd/MM/yyyy',
                   todayHighlight: true
               });
         $('#txtEventEndDate')
        .datepicker({
            format: 'dd/MM/yyyy',
            todayHighlight: true
        });





     }
   </script>
<!--Alert Script-->

<script type="text/javascript">
    function Showalert() {
        alert('No events were found');
    }
</script>