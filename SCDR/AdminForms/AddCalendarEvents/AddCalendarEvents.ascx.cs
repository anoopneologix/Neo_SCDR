using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SCDR.AdminForms.AddCalendarEvents
{
    [ToolboxItemAttribute(false)]
    public partial class AddCalendarEvents : WebPart
    {
        string subsiteName = string.Empty;
        public AddCalendarEvents()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        /// <summary>
        /// for enabling custom webpart properties
        /// </summary>
        #region CustomWebPartProperty
        private const string DepartmentListName = "CustomDepartmentList";
        private static string dListName = DepartmentListName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DepartmentListName),
        WebDisplayName("Department List Name:"),
        WebDescription("Please Enter a valid Department List Name")]
        public string DlistName
        {
            get { return dListName; }
            set { dListName = value; }
        }
        private const string DefaultLibraryName = "CustomCalendarList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Calendar List Name:"),
        WebDescription("Please Enter a valid Calendar List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultVenueList = "CustomVenueList";
        private static string venueListName = DefaultVenueList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultVenueList),
        WebDisplayName("Venue List Name:"),
        WebDescription("Please Enter a valid Venue List Name")]
        public string VenueListName
        {
            get { return venueListName; }
            set { venueListName = value; }
        }
        #endregion

        /// <summary>
        /// for binding the event venue to dropdownlist
        /// active event venue were selected
        /// </summary>
        public void BindEventVenue()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {




                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[VenueListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>Active</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (rbArabic.Checked)
                            {

                                if (oItems != null)
                                {
                                    if (oItems.Count > 0)
                                    {
                                        DataTable dtEventVenue = ConvertSPListToDataTable(oItems);
                                        ddlEventVenue.DataSource = dtEventVenue;
                                        ddlEventVenue.Items.Clear();
                                        ddlEventVenue.DataValueField = "ID"; // List field holding value 
                                        ddlEventVenue.DataTextField = "TitleAr"; // List field holding name to be displayed on page 
                                        ddlEventVenue.DataBind();
                                        ddlEventVenue.Items.Insert(0, new ListItem("--Select Venue--", "0"));
                                    }
                                }
                            }
                            else if (rbEnglish.Checked)
                            {
                                if (oItems != null)
                                {
                                    if (oItems.Count > 0)
                                    {
                                        DataTable dtEventVenue = ConvertSPListToDataTable(oItems);
                                        ddlEventVenue.DataSource = dtEventVenue;
                                        ddlEventVenue.Items.Clear();
                                        ddlEventVenue.DataValueField = "ID"; // List field holding value 
                                        ddlEventVenue.DataTextField = "Title"; // List field holding name to be displayed on page 
                                        ddlEventVenue.DataBind();
                                        ddlEventVenue.Items.Insert(0, new ListItem("--Select Venue--", "0"));
                                    }
                                }
                            }
                        }

                    }
                });
            }
            catch
            {

            }
        }

        /// <summary>
        /// for binding the department to dropdownlist
        /// active department were selected
        /// </summary>
        public void BindDepartment()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                       
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList oList = oWeb.Lists[DlistName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>Active</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (rbArabic.Checked)
                            {
                                if (oItems != null)
                                {
                                    if (oItems.Count > 0)
                                    {
                                        DataTable dtDepartment = ConvertSPListToDataTable(oItems);
                                        ddlDepartment.DataSource = dtDepartment;
                                        ddlDepartment.DataValueField = "ID"; // List field holding value 
                                        ddlDepartment.DataTextField = "TitleAr"; // List field holding name to be displayed on page 
                                        ddlDepartment.DataBind();
                                        ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
                                    }
                                }
                            }
                            else if (rbEnglish.Checked)
                            {
                                if (oItems != null)
                                {
                                    if (oItems.Count > 0)
                                    {
                                        DataTable dtDepartment = ConvertSPListToDataTable(oItems);
                                        ddlDepartment.DataSource = dtDepartment;
                                        ddlDepartment.DataValueField = "ID"; // List field holding value 
                                        ddlDepartment.DataTextField = "Title"; // List field holding name to be displayed on page 
                                        ddlDepartment.DataBind();
                                        ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
                                    }
                                }
                            }

                          
                           
                         
                        }
                    }
                });
            }
            catch
            {

            }
        }

       /// <summary>
       /// function for converting the sharepoint listitem collection to datatable
       /// </summary>
       /// <param name="spItemCollection"></param>
       /// <returns>datatable</returns>
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindEventVenue();
                BindDepartment();
            }
        }

        /// <summary>
        /// for submiting the calendar events to custom sharepoint list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckForExixtingEvents())
            {
                string subsiteName = string.Empty;
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            if (rbArabic.Checked)
                            {
                                subsiteName = "ar/";
                            }
                            else if (rbEnglish.Checked)
                            {
                                subsiteName = "en/";
                            }

                            using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                            {
                                if (rbNo.Checked)
                                {
                                    SPList list = oWeb.Lists[ListName];
                                    SPListItem item = list.Items.Add();
                                    item["Title"] = txtEventName.Text;
                                    item["EventVenue"] = ddlEventVenue.SelectedItem.Text;
                                    item["EventDate"] = txtEventDate.Text;
                                    item["EventTime"] = txtEventStartTime.Text + " - " + txtEventEndTime.Text;
                                    item["Department"] = ddlDepartment.SelectedItem.Text;
                                    item["Description"] = txtEventDescription.Text;
                                    if (fuCalendarEvent.HasFile)
                                    {

                                        foreach (HttpPostedFile postedFile in fuCalendarEvent.PostedFiles)
                                        {

                                            Stream fs = postedFile.InputStream;
                                            byte[] fileContents = new byte[fs.Length];
                                            fs.Read(fileContents, 0, (int)fs.Length);
                                            fs.Close();
                                            SPAttachmentCollection attach = item.Attachments;
                                            string fileName = Path.GetFileName(postedFile.FileName);
                                            attach.Add(fileName, fileContents);

                                        }
                                    }
                                    oWeb.AllowUnsafeUpdates = true;
                                    item.Update();
                                    oWeb.AllowUnsafeUpdates = false;
                                }
                                else if (rbYes.Checked)
                                {
                                    string frequency = ddlFrequency.SelectedItem.Text;
                                    byte[] fileContents = new byte[16 * 1024];
                                    string fileName = string.Empty;
                                    List<DateTime> EventDates = new List<DateTime>();
                                    if (frequency == "Daily")
                                    {
                                        DateTime selectedDate = DateTime.Parse(txtEventDate.Text);
                                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                                        for (int i = 0; i < Occurance; i++)
                                        {
                                            EventDates.Add(selectedDate.AddDays(i));
                                        }
                                    }
                                    else if (frequency == "Weekly")
                                    {
                                        DateTime selectedDate = DateTime.Parse(txtEventDate.Text);
                                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                                        EventDates.Add(selectedDate);
                                        Occurance = Occurance - 1;
                                        for (int i = 0; i < Occurance; i++)
                                        {
                                            selectedDate = EventDates[i];
                                            EventDates.Add(selectedDate.AddDays(7));

                                        }

                                    }
                                    else if (frequency == "Monthly")
                                    {
                                        DateTime selectedDate = DateTime.Parse(txtEventDate.Text);
                                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                                        for (int i = 0; i < Occurance; i++)
                                        {
                                            EventDates.Add(selectedDate.AddMonths(i));
                                        }

                                    }
                                    if (fuCalendarEvent.HasFile)
                                    {

                                        foreach (HttpPostedFile postedFile in fuCalendarEvent.PostedFiles)
                                        {

                                            Stream fs = postedFile.InputStream;
                                            fileContents = new byte[fs.Length];
                                            fs.Read(fileContents, 0, (int)fs.Length);
                                            fs.Close();
                                            fileName = Path.GetFileName(postedFile.FileName);
                                        }
                                    }

                                    foreach (DateTime dt in EventDates)
                                    {
                                        SPList list = oWeb.Lists[ListName];
                                        SPListItem item = list.Items.Add();
                                        item["Title"] = txtEventName.Text;
                                        item["EventVenue"] = ddlEventVenue.SelectedItem.Text;
                                        item["EventDate"] = dt.Date;
                                        item["EventTime"] = txtEventStartTime.Text + " - " + txtEventEndTime.Text;
                                        item["Department"] = ddlDepartment.SelectedItem.Text;
                                        item["Description"] = txtEventDescription.Text;
                                        SPAttachmentCollection attach = item.Attachments;
                                        attach.Add(fileName, fileContents);
                                        oWeb.AllowUnsafeUpdates = true;
                                        item.Update();
                                        oWeb.AllowUnsafeUpdates = false;
                                    }
                                }
                            }
                            string sMessage = "successfully completed";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddCalenderEvents.aspx';</script>", false);
                        }
                        formClear();
                    });

                }
                catch { }
            }

        }

        /// <summary>
        /// for clearing the form on clicking cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
        }

        /// <summary>
        /// function for clearing the form
        /// </summary>
        void formClear()
        {
            txtEventName.Text = "";
            ddlEventVenue.SelectedIndex = 0;
            txtEventDate.Text = "";
            txtEventStartTime.Text = "";
            txtEventEndTime.Text = "";
            txtEventDescription.Text = "";
            ddlDepartment.SelectedIndex = 0;
            rbNo.Checked = true;
            ddlOccurance.SelectedIndex = 0;
            ddlFrequency.SelectedIndex = 0;
        }

        /// <summary>
        /// for checking the newly added event were already created or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckForExixtingEvents()
        {
            bool returnvalue = true;
            try
            {
                string subsiteName = string.Empty;
                string eventVenue = ddlEventVenue.SelectedItem.Text.Trim();
                DataTable dt = new DataTable();
                string frequency = ddlFrequency.SelectedItem.Text;
                List<DateTime> EventDates = new List<DateTime>();
                DateTime selectedDate = DateTime.Parse(txtEventDate.Text);
                string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(selectedDate.Date);
                if (rbYes.Checked)
                {
                  
                    if (frequency == "Daily")
                    {

                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                        for (int i = 0; i < Occurance; i++)
                        {
                            EventDates.Add(selectedDate.AddDays(i));
                        }
                    }
                    else if (frequency == "Weekly")
                    {

                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                        EventDates.Add(selectedDate);
                        Occurance = Occurance - 1;
                        for (int i = 0; i < Occurance; i++)
                        {
                            selectedDate = EventDates[i];
                            EventDates.Add(selectedDate.AddDays(7));

                        }

                    }
                    else if (frequency == "Monthly")
                    {

                        int Occurance = Convert.ToInt32(ddlOccurance.SelectedItem.Text);
                        for (int i = 0; i < Occurance; i++)
                        {
                            EventDates.Add(selectedDate.AddMonths(i));
                        }

                    }
                
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            if (rbArabic.Checked)
                            {
                                subsiteName = "ar/";
                            }
                            else if (rbEnglish.Checked)
                            {
                                subsiteName = "en/";
                            }
                            using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                            {
                                foreach (DateTime ed in EventDates)
                                {
                                    bool exitLoop = false;
                                    string eventStartTime = ed.Date.ToString("dd/MM/yyyy") + " " + txtEventStartTime.Text.Trim();
                                    string evnetEndTime = ed.Date.ToString("dd/MM/yyyy") + " " + txtEventEndTime.Text.Trim();
                                    dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(ed.Date);
                                    SPList oList = oWeb.Lists[ListName];
                                    SPQuery query = new SPQuery();
                                    query.Query = @"<Where><And><Eq><FieldRef Name='EventVenue' /><Value Type='Text'>" + eventVenue + "</Value></Eq><Eq><FieldRef Name='EventDate'/><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Eq></And></Where>";
                                    SPListItemCollection oItems = oList.GetItems(query);
                                    if (oItems != null)
                                    {
                                        if (oItems.Count > 0)
                                        {
                                            DataColumn colVenue = new DataColumn("EventVenue");
                                            colVenue.DataType = System.Type.GetType("System.String");
                                            dt.Columns.Add(colVenue);
                                            DataColumn colStartDateTime = new DataColumn("EventStartTime");
                                            colStartDateTime.DataType = System.Type.GetType("System.DateTime");
                                            dt.Columns.Add(colStartDateTime);
                                            DataColumn colEndDateTime = new DataColumn("EventEndTime");
                                            colEndDateTime.DataType = System.Type.GetType("System.DateTime");
                                            dt.Columns.Add(colEndDateTime);
                                            foreach (SPListItem li in oItems)
                                            {
                                                DateTime eventDate = DateTime.Parse(li["EventDate"].ToString());
                                                string eventTime = li["EventTime"].ToString();
                                                string[] eventTimes = eventTime.Split('-');
                                                string eventStartDateTime = eventDate.Date.ToString("dd/MM/yyyy") + " " + eventTimes[0].Trim();
                                                string eventEndDateTime = eventDate.Date.ToString("dd/MM/yyyy") + " " + eventTimes[1].Trim();
                                                DataRow dr = dt.NewRow();
                                                dr["EventStartTime"] = GetDateTimeFromString(eventStartDateTime);
                                                dr["EventEndTime"] = GetDateTimeFromString(eventEndDateTime);
                                                dr["EventVenue"] = li["EventVenue"].ToString();
                                                dt.Rows.Add(dr);

                                            }
                                            DateTime dtStartTime = GetDateTimeFromString(eventStartTime);
                                            DateTime dtEndTime = GetDateTimeFromString(evnetEndTime);
                                           
                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                DateTime eventSDate = DateTime.Parse(dr["EventStartTime"].ToString());
                                                DateTime eventEDate = DateTime.Parse(dr["EventEndTime"].ToString());
                                                if ((DateTime.Compare(dtStartTime, eventSDate) <= 0) && (DateTime.Compare(dtEndTime, eventSDate) <= 0))
                                                {

                                                }
                                                else if ((DateTime.Compare(dtStartTime, eventEDate) >= 0) && (DateTime.Compare(dtEndTime, eventEDate) >= 0))
                                                {

                                                }
                                                else
                                                {
                                                    returnvalue = false;
                                                    exitLoop = true;
                                                    formClear();
                                                    string sMessage = "There is already an event added. Please change venue, date or time and try again";
                                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                                }

                                                if (exitLoop) break;

                                            }
                                            if (dt.Select("EventStartTime = '" + dtStartTime + "' AND EventEndTime = '" + dtEndTime + "'").Any())
                                            {
                                                returnvalue = false;
                                                exitLoop = true;
                                                formClear();
                                                string sMessage = "There is already an event added. Please change venue, date or time and try again";
                                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                            }
                                        }
                                    }
                                    if (exitLoop) break;
                                }
                               
                            }
                        }
                    });
               
                }
                else if(rbNo.Checked)
                {
                    string eventStartTime = selectedDate.Date.ToString("dd/MM/yyyy") + " " + txtEventStartTime.Text.Trim();
                    string evnetEndTime = selectedDate.Date.ToString("dd/MM/yyyy") + " " + txtEventEndTime.Text.Trim();
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            if (rbArabic.Checked)
                            {
                                subsiteName = "ar/";
                            }
                            else if (rbEnglish.Checked)
                            {
                                subsiteName = "en/";
                            }
                            using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                            {
                                SPList oList = oWeb.Lists[ListName];
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><And><Eq><FieldRef Name='EventVenue' /><Value Type='Text'>" + eventVenue + "</Value></Eq><Eq><FieldRef Name='EventDate'/><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Eq></And></Where>";
                                SPListItemCollection oItems = oList.GetItems(query);
                                if (oItems != null)
                                {
                                    if (oItems.Count > 0)
                                    {
                                        DataColumn colVenue = new DataColumn("EventVenue");
                                        colVenue.DataType = System.Type.GetType("System.String");
                                        dt.Columns.Add(colVenue);
                                        DataColumn colStartDateTime = new DataColumn("EventStartTime");
                                        colStartDateTime.DataType = System.Type.GetType("System.DateTime");
                                        dt.Columns.Add(colStartDateTime);
                                        DataColumn colEndDateTime = new DataColumn("EventEndTime");
                                        colEndDateTime.DataType = System.Type.GetType("System.DateTime");
                                        dt.Columns.Add(colEndDateTime);
                                        foreach (SPListItem li in oItems)
                                        {
                                            DateTime eventDate = DateTime.Parse(li["EventDate"].ToString());
                                            string eventTime = li["EventTime"].ToString();
                                            string[] eventTimes = eventTime.Split('-');
                                            string eventStartDateTime = eventDate.Date.ToString("dd/MM/yyyy") + " " + eventTimes[0].Trim();
                                            string eventEndDateTime = eventDate.Date.ToString("dd/MM/yyyy") + " " + eventTimes[1].Trim();
                                            DataRow dr = dt.NewRow();
                                            dr["EventStartTime"] = GetDateTimeFromString(eventStartDateTime);
                                            dr["EventEndTime"] = GetDateTimeFromString(eventEndDateTime);
                                            dr["EventVenue"] = li["EventVenue"].ToString();
                                            dt.Rows.Add(dr);

                                        }
                                        DateTime dtStartTime = GetDateTimeFromString(eventStartTime);
                                        DateTime dtEndTime = GetDateTimeFromString(evnetEndTime);
                                        bool exitLoop = false;
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            DateTime eventSDate = DateTime.Parse(dr["EventStartTime"].ToString());
                                            DateTime eventEDate = DateTime.Parse(dr["EventEndTime"].ToString());
                                            if ((DateTime.Compare(dtStartTime, eventSDate) <= 0) && (DateTime.Compare(dtEndTime, eventSDate) <= 0))
                                            {

                                            }
                                            else if ((DateTime.Compare(dtStartTime, eventEDate) >= 0) && (DateTime.Compare(dtEndTime, eventEDate) >= 0))
                                            {

                                            }
                                            else
                                            {
                                                returnvalue = false;
                                                exitLoop = true;
                                                formClear();
                                                string sMessage = "Another Event is already added!";
                                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                            }

                                            if (exitLoop) break;

                                        }
                                        if (dt.Select("EventStartTime = '" + dtStartTime + "' AND EventEndTime = '" + dtEndTime + "'").Any())
                                        {
                                            returnvalue = false;
                                            formClear();
                                            string sMessage = "Another Event is already added!";
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                        }
                                    }
                                }
                            }
                        }
                    });
                 
                }
            }
           
            catch
            {
                return false;
            }
            return returnvalue;
        }

        /// <summary>
        /// converting the string to valid DateTime format
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns>DateTime</returns>
        public DateTime GetDateTimeFromString(string sDate)
        {
            DateTime myDate = DateTime.ParseExact(sDate, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);
            return myDate;
        }

        /// <summary>
        /// for binding arabic event venue to dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            
            BindEventVenue();
            BindDepartment();
        }

        /// <summary>
        /// for binding English event venue to dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            
            BindEventVenue();
            BindDepartment();
        }
    }
}
