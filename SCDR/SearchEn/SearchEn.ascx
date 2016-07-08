<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchEn.ascx.cs" Inherits="SCDR.SearchEn.SearchEn" %>
<!--Code Begins-->
<script src="../_layouts/15/SCDR/js/jquery-ui.js"></script>
<div class="search_outer">
    <a class="search_btn">
        <i class="fa"></i>
    </a>
    <span class="input_outer"><span
        class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
       
   <asp:TextBox ID="txtSearch" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Search" ></asp:TextBox>
       </span> </span>
</div>
<!--Code Ends-->

<!--

  <script type="text/javascript">

    // Settings
    var siteurl = _spPageContextInfo.webAbsoluteUrl;
    var field = "Title";
    // Onload
    $(document).ready(function () {
     
        $("input[name$=txtSearch]").autocomplete({
            source: function (req, add) {
              // 
                var suggestions = search(req.term, field);
               
                add(suggestions);
            }
        });
    });


    function search(value, field) {
        var coll = new Array();
        var url = siteurl + "/_vti_bin/listdata.svc/CustomImageGallery?$select=Title&$filter=startswith(Title, '" + value + "')";
        $.ajax({
            cache: true,
            type: "GET",
            headers: { "Accept": "application/json; odata=verbose" },
            async: false,
            dataType: "json",
            url: url,
            success: function (data) {
                var results = data.d.results;
                for (att in results) {
                    var object = results[att];
                    for (attt in object) {
                        if (attt == field) {
                            coll.push(object[attt]);
                        }
                    }
                }
                return coll;
            },
            error: function (data) {
                alert("Error: No search suggestion.");
            }
        });
        return coll;
    }

      </script>-->

  