<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarControlAr.ascx.cs" Inherits="SCDR.CalendarControlAr.CalendarControlAr" %>
<!-- Include Bootstrap Datepicker -->
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker.min.css" />
        <link rel="stylesheet" href="../../_layouts/15/SCDR/css/datepicker3.min.css" />
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
    <Triggers>
    
    </Triggers>
    <ContentTemplate>
  <span class="radius">
<h3 class="news_heading"><asp:Label ID="lblHeading" runat="server" ></asp:Label></h3>
 <div class="event-row">
      <asp:Button  ID="btnSelectedDate" style="display:none"  class="btn today-events" runat="server" OnClick="btnSelectedDate_Click"  > </asp:Button>
				<button id="btnToday" runat="server" type="button" class="today-trigger"></button>
                <asp:Button  ID="btnTodayEvents" class="btn today-events" runat="server" OnClick="btnTodayEvents_Click"> </asp:Button>
				<asp:HiddenField ID="hfSelectedDate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hfDateValue" ClientIDMode="Static" runat="server" /> 
			</div>
 			<div class="kalendar"></div>

            </span>
<div id="editor"></div>
<div id="img-out" style="display:none"></div>
            </ContentTemplate>
    </asp:UpdatePanel>
   
 
<!--Event popup div begins-->
<asp:Panel ID="Panel1" runat="server"  style="z-index:50000 !important" class="modal fade bs-example-modal-md"  data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-md">
       <asp:UpdatePanel ID="upModal" runat="server"  UpdateMode="Always" >
           <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnTodayEvents" EventName="Click"  />
             <asp:AsyncPostBackTrigger ControlID="btnSelectedDate" EventName="Click"  />
                 <asp:AsyncPostBackTrigger ControlID="btnSearchEvent" EventName="Click"  />
         
           </Triggers>
             <ContentTemplate>
                
    <div class="modal-content">
          <div class="modal-header">
                 <h4 class="modal-title">تفاصيل الحدث</h4>  <div data-dismiss="modal" class="event-close-popup"><i class="fa fa-times"></i>
      </div></div>
         <div class="modal-body">
             <div class="row">
           <div class="col-md-12">
              <div class="event-search-wrapper">
            <div class="search-event-title">ابحث عن حدث</div>
            <div class="search-event-form">
                 <asp:TextBox ID="txtSearch" placeholder="ابحث هنا"  CssClass="form-control" runat="server"></asp:TextBox>    
<asp:RequiredFieldValidator ID="rq1" ClientIDMode="Static" runat="server" ErrorMessage="مطلوب هذه البيانات" ControlToValidate="txtSearch" ValidationGroup="chk"></asp:RequiredFieldValidator>
                <div class="row" id="date_pair">
                    <div class="col-xs-12 col-sm-6 mt10">
                        <div class="form-group">
                            <label for="startDate">تاريخ البدء:</label>
                            <asp:TextBox ID="txtStartDate" ClientIDMode="Static" CssClass="form-control" class="date" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="rq2" ClientIDMode="Static" runat="server" ErrorMessage="مطلوب هذه البيانات" ControlToValidate="txtStartDate" ValidationGroup="chk"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 mt10">
                        <div class="form-group">
                            <label for="endDate">تاريخ الانتهاء:</label>
                            <asp:TextBox ID="txtEndDate" ClientIDMode="Static" CssClass="form-control" class="date" runat="server"></asp:TextBox>
 <asp:RequiredFieldValidator ID="rq3" ClientIDMode="Static" runat="server" ErrorMessage="مطلوب هذه البيانات" ControlToValidate="txtEndDate" ValidationGroup="chk"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <asp:Button ValidationGroup="chk" ID="btnSearchEvent" CssClass="btn btn-info"   class="event-search" runat="server" Text="ابحث هنا" OnClick="btnSearchEvent_Click"  />
            </div>
        </div>

                   <!-- search div ends-->
               <!-- event list div begins-->
               <div class="event-details-wrapper" >
            <asp:Repeater ID="repEventDetails" runat="server" OnItemDataBound="repEventDetails_ItemDataBound" >
               <ItemTemplate>
 <div class="event-view-wrapper">
                <div class="event-name"><b><%# Eval("Title") %></b>
                </div>
                <div class="event-expand-details">
                    <div class="event-details-container">
                        <div class="event-img-wrapper">
                            <img src='<%# Eval("ImageUrl") %>'>
                        </div>
                        <div class="event-info-wrapper">
                            <div class="event-info"><asp:Literal ID="ltEventVenue" runat="server"></asp:Literal><span><%# Eval("EventVenue") %></span>
                            </div>
                            <div class="event-info"><asp:Literal ID="ltEventDate" runat="server"></asp:Literal><span><%# Eval("EventDate","{0:dd/MM/yyyy}") %></span>
                            </div>
                            <div class="event-info"><asp:Literal ID="ltEventTime" runat="server"></asp:Literal><span><%# Eval("EventTime") %> </span>
                            </div>
                            <div class="event-info"><asp:Literal ID="ltEventDepartment" runat="server"></asp:Literal><span><%# Eval("Department") %></span>
                            </div>
                        </div>
                        <!-- /.event-info-wrapper -->
                        <p><%# Eval("Description") %></p>
                    </div>
                    <!-- /.event-details-container -->
                    <div class="event-options-wrapper">
                        <a href="javascript:;" class="event-option" onClick="printSelectedDiv(event);"  data-toggle="tooltip" title="Print"><i class="fa fa-print"></i></a>
                      <a href="javascript:;" class="event-option" onClick="emailSelectedDiv(event);" title="Send event as Email"><i class="fa fa-envelope"></i></a>
                        <a href="javascript:;" class="event-option" onClick="downloadSelectedDiv(event);" title="Download as PDF"><i class="fa fa-file-pdf-o"></i></a>
                      <!--  <a href="javascript:;" onClick="downloadSelectedDiv(event);" class="event-option" title="Add event to outlook calendar"><i class="fa fa-calendar"></i></a>-->
                    </div>
                    <!-- /.event-options-wrapper -->
                </div>
            </div>
            <!-- /.event-view-wrapper -->
               </ItemTemplate>
            </asp:Repeater>
      
           
        </div>
  </div>
</div>
      </div>
      <div class="modal-footer">
      
       </div>
    </div>
              </ContentTemplate>
            </asp:UpdatePanel>
  </div>
</asp:Panel>


<!--Alert Script-->

<script type="text/javascript">
    function Showalert() {

        $(function () {
            $(".kalendar").ionCalendar({
                lang: "en",
                //years: "1915-1995",
                onClick: function (date) {
                    //  dispevent();
                }
            });
          /*  var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            var today = mm + '/' + dd + '/' + yyyy;
            $(".date").val(today);*/

            $('#date_pair .date').datepicker({
                format: 'm/d/yyyy',
                autoclose: true,
            });
            $('#date_pair').datepair();

            dispevent();
            day_rows();
            day_four_rows();
        });


        alert('لا يوجد أحداث تم العثور عليها');
    }
</script>
<!--Calendar Date Click-->
<script type="text/javascript">
    $(document).ready(function () {

        $(document).on("click", ".ic__day.ic__day_state_current.day_state_holiday", function () {

            var selected_day = $(this).text();
            if (selected_day < 10) {
                selected_day = "0" + selected_day;
            }
            var selected_year = $(".ic__year-select").val();
            var selected_month = $(".ic__month-select").val();
            var currentMonth = parseInt(selected_month) + 1;
            var actualMonth = "";
            if (currentMonth > 9) {

                actualMonth = currentMonth;
            }
            else {

                actualMonth = "0" + currentMonth;
            }
            var selected_date = selected_day + '/' + actualMonth + '/' + selected_year;
            $("#<%=hfSelectedDate.ClientID%>").val(selected_date);
            $("#<%=btnSelectedDate.ClientID%>").click();
            //  $("#<=txtSelectedDate.ClientID%>").ontextchanged();
            // $("#<=txtDemoBox.ClientID %>").focus();


        });
        function ShowAlert() {
            alert("لا يوجد أحداث تم العثور عليها");
        }

        $(document).ready(function () {
            $(document).on("click", ".ic__day.day_state_holiday", function () {

                var selected_day = $(this).text();
                if (selected_day < 10) {
                    selected_day = "0" + selected_day;
                }
                var selected_year = $(".ic__year-select").val();
                var selected_month = $(".ic__month-select").val();
                var currentMonth = parseInt(selected_month) + 1;
                var actualMonth = "";
                if (currentMonth > 9) {

                    actualMonth = currentMonth;
                }
                else {

                    actualMonth = "0" + currentMonth;
                }
                var selected_date = selected_day + '/' + actualMonth + '/' + selected_year;
                $("#<%=hfSelectedDate.ClientID%>").val(selected_date);
                $("#<%=btnSelectedDate.ClientID%>").click();
                //  $("#<=txtSelectedDate.ClientID%>").ontextchanged();
                //   $("#<=txtDemoBox.ClientID %>").focus();
                //   __doPostBack("#<=txtSelectedDate.ClientID%>", '');

            });

        });
    });
    </script>

<!--Calendar Script-->
<script type="text/javascript">
    function day_rows() {
        var day_row_length = $(".ic__days tbody tr:not(:empty)").length;;
        if (day_row_length > 5) {
            $(".ic__days").addClass("day_more");
        }
        else {
            $(".ic__days").removeClass("day_more");
        }
    }
    function day_four_rows() {
        var day_four_rows_length = $(".ic__days tbody tr:not(:empty)").length;;
        if (day_four_rows_length == 4) {
            $(".ic__days").addClass("day_less");
        }
        else {
            $(".ic__days").removeClass("day_less");
        }
    }
    function dispevent() {
        var dateval = $("#hfDateValue").val();
        var event_date = dateval.split(',');
        var event_array = [];
        var eventall = [];
        var eventdays = [];
        var items = [];
        var selected_year = $(".ic__year-select").val();
        var selected_month = $(".ic__month-select").val();
        var currentMonth = parseInt(selected_month) + 1;
        var actualMonth = "";
        if (currentMonth > 9) {

            actualMonth = currentMonth;
        }
        else {

            actualMonth = "0" + currentMonth;
        }
        for (var i = 0; i < event_date.length; i++) {
            var item = event_date[i];
            var arrayItem = item.split('-');
            if (arrayItem[1] == actualMonth && arrayItem[2] == selected_year) {
                // if (item.indexOf(selected_year) != -1 && item.indexOf(actualMonth) != -1) {
                items.push(item);
            }
        }
        for (i = 0; i < items.length; i++) {
            eventall[i] = items[i].split("-");
            event_array.push(eventall[i][0]);
        }



        //if(selected_year==2016 && selected_month==1){
        k = 0;
        event_array.sort(function (a, b) { return a - b });

        $(".ic__days .ic__day").each(function () {
            var eventnum = parseInt($(this).text());
            if (eventnum == event_array[k]) {
                $(this).addClass("day_state_holiday");
                if (k < items.length) {
                    k++;
                }
            }

        });
        //}
    }
    $(function () {
        $(".kalendar").ionCalendar({
            lang: "en",
            //years: "1915-1995",
            onClick: function (date) {
                //  dispevent();
            }
        });
        day_rows();
        day_four_rows();
    });
    $(document).on("click", ".today-trigger", function () {
        var custom_date = new Date();
        year_current = custom_date.getFullYear();
        month_current = custom_date.getMonth();
        $(".ic__year-select option[value=" + year_current + "]").attr("selected", "selected").change();
        $(".ic__month-select option[value=" + month_current + "]").attr("selected", "selected").change();
        day_rows();
        day_four_rows();
    });
    $(window).load(function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        var today = mm + '/' + dd + '/' + yyyy;
        $(".date").val(today);

        $('#date_pair .date').datepicker({
            format: 'm/d/yyyy',
            autoclose: true,
        });
        $('#date_pair').datepair();

        dispevent();
        day_rows();
        day_four_rows();
    });

    $(document).on("change", ".ic__month-select", function () {
        dispevent();
        day_rows();
        day_four_rows();
    });
    $(document).on("change", ".ic__year-select", function () {
        dispevent();
        day_rows();
        day_four_rows();
    });
    $(document).on("click", function () {
        dispevent();
        day_rows();
        day_four_rows();
    });
    $(document).on("click", ".event-name", function () {
        if ($(this).parent().find(".event-expand-details").is(":hidden")) {
            $(".expand-event").removeClass("expand-event");
            $(".event-expand-details").slideUp();
            $(this).addClass("expand-event");
            $(this).parent().find(".event-expand-details").slideDown();
        }
        else {
            $(this).removeClass("expand-event");
            $(".event-expand-details").slideUp();
        }
    });
    $(document).on("click", ".search-event-title", function () {
        $(".search-event-form").slideToggle();
    });
    $(document).on("click", ".event-close-popup", function () {
        $(".popupbg_event").fadeOut(200);
        $(".popup_container_event").fadeOut(150);
        $(".expand-event").removeClass("expand-event");
        $(".event-expand-details").hide();
        $(".search-event-form").hide();
    });
    $(document).on("click", ".popupbg_event", function () {
        $(".popupbg_event").fadeOut(200);
        $(".popup_container_event").fadeOut(150);
        $(".expand-event").removeClass("expand-event");
        $(".event-expand-details").hide();
        $(".search-event-form").hide();
    });

</script>

<script type="text/javascript" src="../../_layouts/15/SCDR/js/bootstrap-datepicker.min.js" ></script>
 <script type="text/javascript">
     $(document).ready(function () {
         $('#txtStartDate')
             .datepicker({
                 format: 'dd/MM/yyyy',
                 todayHighlight: true,

             });
         $('#txtEndDate')
           .datepicker({
               format: 'dd/MM/yyyy',
               todayHighlight: true,

           });

     });
    </script>
<style>
.datepicker{z-index:55000 !important;}
</style>
<script src="../_layouts/15/SCDR/js/html2canvas.js"></script>
<script src="../_layouts/15/SCDR/js/canvas2image.js"></script>
<script src="../_layouts/15/SCDR/js/jspdf.min.js"></script>
<!--Modal Popup download as PDF, print div and Email Event Details -->
<script type="text/javascript">
    function printSelectedDiv(event) {
    
        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        //Create a new HTML document.
       
        $div = $(event.target).closest(".event-view-wrapper");
        var textTitle = $div.children(".event-name").text();
        var textDate = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(2)").find('span').text();
        var textTime = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(3)").find('span').text();
        var textVenue = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(1)").find('span').text();
        var textDepartment = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(4)").find('span').text();
        var textDescription = $div.children(".event-expand-details").children(".event-details-container").find('p').text();
        var textImageUrl = $div.children(".event-expand-details").children(".event-details-container").children(".event-img-wrapper").find('img').attr("src");
        var contentTable = "<table><tr><td>حدث: </td><td>" + textTitle + "</td></tr>";
        contentTable += "<tr><td>مكان الحدث:</td><td>" + textVenue + "</td></tr>";
        contentTable += "<tr><td>تاريخ الحدث:</td><td>" + textDate + "</td></tr>";
        contentTable += "<tr><td>وقت الحدث:</td><td style=" + "\"" + "dir:ltr" + "\"" + ">" + textTime.replace(/\s/g, '') + "</td></tr>";
        contentTable += "<tr><td>قسم:</td><td>" + textDepartment + "</td></tr></table>";
        contentTable += "<img src=" + textImageUrl + ">";
        contentTable += "<p>" + textDescription + "</p>";
        //Create a new HTML document.
        frameDoc.document.write('<html><head><title>Event Details</title>');
        frameDoc.document.write('<style>body{direction:rtl;}img{width:200px;height:200px}table {width: auto;}tr td {white-space: nowrap;word-wrap:break-word;}</style>');
        frameDoc.document.write('</head><body>');
        //Append CSS here
        //Append the DIV contents.
        frameDoc.document.write(contentTable);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 500);
    }
 /*   function downloadSelectedDiv(event) {
        var contents = $(event.target).closest(".event-view-wrapper");

        html2canvas(contents, {
            onrendered: function (canvas) {
                theCanvas = canvas;
                document.body.appendChild(canvas);

                var imgData = canvas;
                var doc = new jsPDF();

                doc.setFontSize(40);
                doc.addImage(imgData, 'PNG', 20, 20);
                doc.save('Event.pdf');
                // Convert and download as image 
                // Canvas2Image.saveAsPNG(canvas);
                //  $("#img-out").append(canvas);
                // Clean up 
                document.body.removeChild(canvas);
            }
        });


    }*/

    function downloadSelectedDiv(event) {
        var contents = $(event.target).closest(".event-view-wrapper");
        var scaleBy = 5;
        var w = 1000;
        var h = 1000;
        var canvas = document.createElement('canvas');
        canvas.width = w * scaleBy;
        canvas.height = h * scaleBy;
        canvas.style.width = w + 'px';
        canvas.style.height = h + 'px';
        var context = canvas.getContext('2d');
        context.scale(scaleBy, scaleBy);

        html2canvas(contents, {
            onrendered: function (canvas) {
                theCanvas = canvas;
                document.body.appendChild(canvas);

                var imgData = canvas;
                var doc = new jsPDF();

                doc.setFontSize(100);
                doc.addImage(imgData, 'JPEG', 40, 40);
                doc.save('testing.pdf');
                document.body.removeChild(canvas);
            }
        });

    }


    //popup send event as email
    function emailSelectedDiv(event) {
        $div = $(event.target).closest(".event-view-wrapper");
        var textTitle = $div.children(".event-name").text();
        var textDate = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(2)").find('span').text();
        var textTime = $div.children(".event-expand-details").children(".event-details-container").children(".event-info-wrapper").children(".event-info:nth-child(3)").find('span').text();
        var url = window.location.href;
        var formattedBody = "\nEvent Date : " + textDate + "\n\nEvent Time : " + textTime + "\n\nFor more details, visit " + url;

        var mailToLink = "mailto:?";
        var mailContent = "Subject=" + textTitle + "&";
        mailContent += "body=" + encodeURIComponent(formattedBody);

        $(event.target).attr("href", mailToLink + mailContent);
        window.location = $(event.target).attr('href');

    }






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

     function EndRequest(sender, args) {
         //calendar script

         $(function () {
             $(".kalendar").ionCalendar({
                 lang: "en",
                 //years: "1915-1995",
                 onClick: function (date) {
                     //  dispevent();
                 }
             });
          /*   var today = new Date();
             var dd = today.getDate();
             var mm = today.getMonth() + 1;
             var yyyy = today.getFullYear();
             var today = mm + '/' + dd + '/' + yyyy;
             $(".date").val(today);*/

             $('#date_pair .date').datepicker({
                 format: 'm/d/yyyy',
                 autoclose: true,
             });
             $('#date_pair').datepair();

             dispevent();
             day_rows();
             day_four_rows();
         });


         //date picker

         $('#txtStartDate')
             .datepicker({
                 format: 'dd/MM/yyyy',
                 todayHighlight: true,

             });
         $('#txtEndDate')
           .datepicker({
               format: 'dd/MM/yyyy',
               todayHighlight: true,

           });

       


     }
   </script>