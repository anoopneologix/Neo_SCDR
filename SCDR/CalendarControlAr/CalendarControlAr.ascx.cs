using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using Microsoft.SharePoint.Utilities;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SCDR.CalendarControlAr
{
    [ToolboxItemAttribute(false)]
    public partial class CalendarControlAr : WebPart
    {
        public CalendarControlAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomCalendarList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Calenar List Name:"),
        WebDescription("Please Enter a valid Calenar List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultHeading = "حدث في مثل هذا اليوم";
        private static string headingName = DefaultHeading;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultHeading),
        WebDisplayName("Heading to display:"),
        WebDescription("Example : Calender")]
        public string HeadingName
        {
            get { return headingName; }
            set { headingName = value; }
        }
        private const string DefaultbtnToday = "اليوم";
        private static string buttonToday = DefaultbtnToday;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultbtnToday),
        WebDisplayName("Today Button Text:"),
        WebDescription("Example : Today")]
        public string BtnToday
        {
            get { return buttonToday; }
            set { buttonToday = value; }
        }
        private const string DefaultbtnTodayEvents = "حدث اليوم";
        private static string buttonTodayEvents = DefaultbtnTodayEvents;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultbtnTodayEvents),
        WebDisplayName("Today's Events Button Text:"),
        WebDescription("Example : Today's Events")]
        public string BtnTodayEvents
        {
            get { return buttonTodayEvents; }
            set { buttonTodayEvents = value; }
        }
        private const string DefaultEventVenue = "مكان الحدث:";
        private static string ltEventVenue = DefaultEventVenue;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultEventVenue),
        WebDisplayName("Event Venue Division Text"),
        WebDescription("Example : Event Venue :")]
        public string LtEventVenue
        {
            get { return ltEventVenue; }
            set { ltEventVenue = value; }
        }
        private const string DefaultEventDate = "تاريخ الحدث: ";
        private static string ltEventDate = DefaultEventDate;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultEventDate),
        WebDisplayName("Event Date Division Text"),
        WebDescription("Example : Event Date :")]
        public string LtEventDate
        {
            get { return ltEventDate; }
            set { ltEventDate = value; }
        }
        private const string DefaultEventTime = "وقت الحدث:";
        private static string ltEventTime = DefaultEventTime;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultEventTime),
        WebDisplayName("Event Time Division Text"),
        WebDescription("Example : Event Time :")]
        public string LtEventTime
        {
            get { return ltEventTime; }
            set { ltEventTime = value; }
        }
        private const string DefaultEventDepartment = "قسم:";
        private static string ltEventDepartment = DefaultEventDepartment;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultEventDepartment),
        WebDisplayName("Event Department : Division Text"),
        WebDescription("Example : Event Time :")]
        public string LtEventDepartment
        {
            get { return ltEventDepartment; }
            set { ltEventDepartment = value; }
        }
        #endregion

       
        public void GetAllEventDate()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string dateValue = string.Empty;
                    StringBuilder builder = new StringBuilder();
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = "<OrderBy><FieldRef Name='EventDate' Ascending='True' /></OrderBy>";
                            DataTable tempTbl = oList.GetItems(query).GetDataTable();
                            DataView v = new DataView(tempTbl);
                            String[] columns = { "EventDate" };
                            DataTable tbl = v.ToTable(true, columns);
                            foreach (DataRow row in tbl.Rows)
                            {
                                DateTime dt = Convert.ToDateTime(row["EventDate"]);
                                builder.Append(dt.ToString("dd-MM-yyyy")).Append(",");

                            }
                            builder.Length--;
                            dateValue = builder.ToString();
                            hfDateValue.Value = dateValue;
                        }
                    }


                });
            }
            catch
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
               // this.BindDummyItem();
                lblHeading.Text = CalendarControlAr.headingName;
                btnToday.InnerHtml = CalendarControlAr.buttonToday;
                btnTodayEvents.Text = CalendarControlAr.buttonTodayEvents;
             //   BindCalenderWithEventDetails();
                GetAllEventDate();
            }
        }

        public void formClear()
        {
            txtEndDate.Text = "";
            txtSearch.Text = "";
            txtStartDate.Text = "";
        }
        public void BindCalenderWithEventDetails()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            DateTime utcTime = DateTime.UtcNow;
                           
                            string dtToday = SPUtility.CreateISO8601DateTimeFromSystemDateTime(utcTime.Date);
                            query.Query = @"<Where><Eq><FieldRef Name='EventDate'  /><Value Type='DateTime'>" + dtToday + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                BindDetailsToRepeater(oItems);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "show", "$('#" + Panel1.ClientID + "').modal();", true);
                                
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "displayalertmessage", "Showalert();", true);
                            }
                        }
                    }
                });


            }
            catch (Exception ex)
            {

            }

        }
        public void BindDetailsToRepeater(SPListItemCollection oItems)
        {

            try
            {
                DataTable dt = new DataTable();
                DataColumn dcTitle = new DataColumn("Title", typeof(string));
                dt.Columns.Add(dcTitle);
                DataColumn dcEventVenue = new DataColumn("EventVenue", typeof(string));
                dt.Columns.Add(dcEventVenue);
                DataColumn dcEventDate = new DataColumn("EventDate", typeof(string));
                dt.Columns.Add(dcEventDate);
                DataColumn dcEventTime = new DataColumn("EventTime", typeof(string));
                dt.Columns.Add(dcEventTime);
                DataColumn dcDepartment = new DataColumn("Department", typeof(string));
                dt.Columns.Add(dcDepartment);
                DataColumn dcDescription = new DataColumn("Description", typeof(string));
                dt.Columns.Add(dcDescription);
                DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                dt.Columns.Add(dcImageUrl);
                foreach (SPListItem li in oItems)
                {
                    DataRow dr = dt.NewRow();
                    dr["Title"] = li["Title"].ToString();
                    dr["EventVenue"] = li["EventVenue"].ToString();
                    DateTime dtEventDate = Convert.ToDateTime(li["EventDate"]);
                    dr["EventDate"] = dtEventDate.ToString("dd/MM/yyyy");
                    dr["EventTime"] = li["EventTime"].ToString();
                    dr["Department"] = li["Department"].ToString();
                    string liDescription = li["Description"].ToString();
                    dr["Description"] = Regex.Replace(liDescription, "<.*?>", string.Empty);

                    SPAttachmentCollection collAttachments = li.Attachments;
                    foreach (string fileName in li.Attachments)
                    {
                        dr["ImageUrl"] = SPUrlUtility.CombineUrl(li.Attachments.UrlPrefix, fileName);
                    }
                    dt.Rows.Add(dr);
                }

                repEventDetails.DataSource = dt;
                repEventDetails.DataBind();
          
            }
            catch
            {

            }
        }

        protected void btnTodayEvents_Click(object sender, EventArgs e)
        {
            try
            {
                BindCalenderWithEventDetails();

            }
            catch
            {

            }

        }

        protected void btnSearchEvent_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            DateTime sDate = DateTime.Parse(txtStartDate.Text);
                            DateTime eDate = DateTime.Parse(txtEndDate.Text);
                            string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(sDate.Date);
                            string dteDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(eDate.Date);
                            query.Query = @"<Where><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + txtSearch.Text + "</Value></Contains><And><Geq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Geq><Leq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dteDate + "</Value></Leq></And></And></Where>";

                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                BindDetailsToRepeater(oItems);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "show", "$('#" + Panel1.ClientID + "').modal();", true);
                                //  upModal.Update();
                            }
                            else
                            {
                                
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", "$('#" + Panel1.ClientID + "').modal('hide');", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "displayalertmessage", "Showalert();", true);
                               
                            }
                        }
                        formClear();
                    }
                });


            }
            catch (Exception ex)
            {

            }

        }

        protected void repEventDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal EventVenue = new Literal();
                EventVenue = (Literal)e.Item.FindControl("ltEventVenue");
                EventVenue.Text = "<b>" + CalendarControlAr.ltEventVenue + "</b>";
                Literal EventDate = new Literal();
                EventDate = (Literal)e.Item.FindControl("ltEventDate");
                EventDate.Text = "<b>" + CalendarControlAr.ltEventDate + "</b>";
                Literal EventTime = new Literal();
                EventTime = (Literal)e.Item.FindControl("ltEventTime");
                EventTime.Text = "<b>" + CalendarControlAr.ltEventTime + "</b>";
                Literal EventDepartment = new Literal();
                EventDepartment = (Literal)e.Item.FindControl("ltEventDepartment");
                EventDepartment.Text = "<b>" + CalendarControlAr.ltEventDepartment + "</b>";
            }
        }

      

        protected void btnSelectedDate_Click(object sender, EventArgs e)
        {
            string selectedDate = hfSelectedDate.Value;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();

                            DateTime sDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(sDate.Date);
                            query.Query = @"<Where><Eq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                BindDetailsToRepeater(oItems);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "show", "$(function () { $('#" + Panel1.ClientID + "').modal('show'); });", true);

                            }
                            else
                            {
                                string sMessage = "Event Details Currently Unavailable";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('" + sMessage + "');", false);

                            }
                        }
                        formClear();
                    }
                });


            }
            catch (Exception ex)
            {

            }
        }
    }
}
