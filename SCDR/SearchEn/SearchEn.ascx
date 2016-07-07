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

<!--<script>
   
        var siteurl = _spPageContextInfo.webAbsoluteUrl;
        $.ajax({
                   url: siteurl + "/_api/web/lists/getbytitle('CustomImageGallery')/items",
                   method: "GET",
                   headers: { "Accept": "application/json; odata=verbose" },
                   success: function (data) {
                        if (data.d.results.length > 0 ) {
                             //This section can be used to iterate through data and show it on screen
                        }       
                  },
                  error: function (data) {
                      alert("Error: "+ data);
                 }
          });
  
</script>-->

 <!--  <script type="text/javascript">

    // Settings
    var siteurl = _spPageContextInfo.webAbsoluteUrl;
    var url = siteurl + "/_api/web/lists/getbytitle('CustomImageGallery')/Items?";
    var field = "Title";

    // Onload
    $(document).ready(function () {
        SP.SOD.executeFunc('sp.js', 'SP.ClientContext', prepareTables);
        $("input[name$=txtSearch]").autocomplete({
            source: function (req, add) {
              //  search(req.term, url, field);
                var suggestions=  retrieveListItems();
               
                add(suggestions);
            }
        });
    });

    // Search all the listitems by using the REST Service
    // Value is the text that needs to be used in the query
    // listurl is the listdata.svc url withouth the filter params
    // field is the name of the field where the value in exists
   /* function search(value, listurl, field) {
        var coll = new Array();
       // var url =listurl + "$filter=startswith(" + field + ",'" + value + "')";
        var url = siteurl + "/_api/web/lists/getbytitle('CustomImageGallery')/Items?filter=startswith(Title,t)";
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
            },
            error: function (data) {
                alert("Error: " + data);
            }
        });
        return coll
    }*/

   
    var siteUrl = _spPageContextInfo.webAbsoluteUrl;
    function retrieveListItems() {

        var clientContext = new SP.ClientContext(siteUrl);
        var oList = clientContext.get_web().get_lists().getByTitle('CustomImageGallery');

        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml('<View><Query><Where><Contains><FieldRef Name=Title /><Value Type=Text>t</Value></Contains></Where></Query><RowLimit>10</RowLimit></View>');
        this.collListItem = oList.getItems(camlQuery);

        clientContext.load(collListItem);
     //   clientContext.executeQueryAsync(Function.createDelegate(this, function () { _returnParam = onQuerySucceeded(); }), Function.createDelegate(this, this.onQueryFailed));
       clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));
      //  return (_returnParam);
    }

    function onQuerySucceeded(sender, args) {

        var listItemInfo = '';
        var coll = new Array();
        var listItemEnumerator = collListItem.getEnumerator();

        while (listItemEnumerator.moveNext()) {
            var oListItem = listItemEnumerator.get_current();
            coll.push(oListItem.get_item('Title'));
        
        }
        return coll;
      //  alert(listItemInfo.toString());
    }

    function onQueryFailed(sender, args) {

        alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
    }


</script> -->

<script>


    $(document).ready(function () {
     
        $("input[name$=txtSearch]").autocomplete({
            source: function (req, add) {
                //  search(req.term, url, field);
                var suggestions = prepareTables();
                //don't exectute any jsom until sp.js file has loaded.        
                SP.SOD.executeFunc('sp.js', 'SP.ClientContext', prepareTables);
                add(suggestions);
            }
        });
    });

    function prepareTables() {
        var coll = new Array();
        getItemsWithCaml('CustomImageGallery',
                function (camlItems) {
                    var listItemEnumerator = camlItems.getEnumerator();
                    while (listItemEnumerator.moveNext()) {
                        var listItem = listItemEnumerator.get_current();
                        coll.push(listItem.get_item('Title'));
                    }
                    return coll;
                },
                function (sender, args) {
                    console.log('An error occured while retrieving list items:' + args.get_message());
                });
       
    }

    function getItemsWithCaml(listTitle, success, error) {
        var clientContext = new SP.ClientContext.get_current();
        var list = clientContext.get_web().get_lists().getByTitle(listTitle);
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml('<View><Query><Where><Contains><FieldRef Name=Title /><Value Type=Text>t</Value></Contains></Where></Query><RowLimit>10</RowLimit></View>');
        var camlItems = list.getItems(camlQuery);
        clientContext.load(camlItems);
        clientContext.executeQueryAsync(
                function () {
                    success(camlItems);
                },
                error
            );
    };

</script>