﻿using System;
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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;

namespace SCDR.AdminForms.EditCalendarEvents
{
    [ToolboxItemAttribute(false)]
    public partial class EditCalendarEvents : WebPart
    {
       
        
        public EditCalendarEvents()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        /// <summary>
        /// function for enabling custom webpart properties
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
                        string subSiteName = Page.Request.QueryString["SiteName"].ToString();
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[DlistName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>Active</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (subSiteName=="ar/")
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
                            else if (subSiteName=="en/")
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


                        string subSiteName = Page.Request.QueryString["SiteName"].ToString();

                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[VenueListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>Active</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (subSiteName=="ar/")
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
                            else if (subSiteName == "en/")
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
        ///  Function to convert list to DataTable 
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// fires on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (Page.Request.QueryString["ItemID"] != null && Page.Request.QueryString["SiteName"] != null)
                {

                    int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                    string subSiteName = Page.Request.QueryString["SiteName"].ToString();
                    BindDetails(itemID,subSiteName);
                }
            }
        }

     
 

        /// <summary>
        /// bind the deatils of event to the page
        /// event will be loaded based on event name selected
        /// get the deatils from sharepoint list
        /// </summary>
        /// <param name="txt"></param>
        public void BindDetails(int itemID, string subSiteName)
        {
            try
            {
                BindEventVenue();
                BindDepartment();
         
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb(subSiteName))
                        {
                            string liDescription = "";
                            SPList oList = oWeb.Lists[ListName];
                            SPListItem item = oList.GetItemById(itemID);
                            txtEventName.Text = item["Title"].ToString();
                            DateTime dtNewsDate = Convert.ToDateTime(item["EventDate"]);
                            string newsDate = dtNewsDate.ToString("dd/MMM/yyyy");
                            txtEventDate.Text = newsDate;
                          
                            liDescription = item["Description"].ToString();
                            txtEventDescription.Text = Regex.Replace(liDescription, "<.*?>", string.Empty);
                            string timeString = item["EventTime"].ToString();
                            string[] indTime = Regex.Split(timeString, "-");
                            txtEventStartTime.Text = indTime[0];
                            txtEventEndTime.Text = indTime[1];
                          //  txtEventVenue.Text = item["EventVenue"].ToString();
                            using (SPWeb dWeb = oSite.OpenWeb())
                            {
                            SPList dList = dWeb.Lists[DlistName];
                            SPQuery query = new SPQuery();
                            string departmentName = item["Department"].ToString();
                            if (subSiteName=="ar/")
                            {
                                query.Query = @"<Where><Eq><FieldRef Name='TitleAr'/><Value Type='Text'>" + departmentName + "</Value></Eq></Where>";

                            }
                            else if (subSiteName == "en/")
                            {
                                query.Query = @"<Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" + departmentName + "</Value></Eq></Where>";
                            }
                            SPListItemCollection dItems = dList.GetItems(query);
                            foreach (SPListItem dItem in dItems)
                            {
                                ddlDepartment.SelectedValue = dItem["ID"].ToString();
                            }
                            }
                            using (SPWeb dWeb = oSite.OpenWeb())
                            {
                                SPList dList = dWeb.Lists[VenueListName];
                                SPQuery query = new SPQuery();
                                string eventVenueName = item["EventVenue"].ToString();
                                if (subSiteName == "ar/")
                                {
                                    query.Query = @"<Where><Eq><FieldRef Name='TitleAr'/><Value Type='Text'>" + eventVenueName + "</Value></Eq></Where>";
                                }
                                else if (subSiteName == "en/")
                                {
                                    query.Query = @"<Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" + eventVenueName + "</Value></Eq></Where>";
                                }
                                SPListItemCollection dItems = dList.GetItems(query);
                                foreach (SPListItem dItem in dItems)
                                {
                                    ddlEventVenue.SelectedValue = dItem["ID"].ToString();
                                }
                            }
                            SPAttachmentCollection objAttchments = item.Attachments;
                             if (objAttchments.Count > 0)
                            {
                                foreach (string fileName in item.Attachments)
                                {
                                    imgEvent.ImageUrl = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);

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
        /// update the deatils to sharepoint list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtEventName.Text != "" && txtEventStartTime.Text != "" && txtEventEndTime.Text != "" && txtEventDate.Text != "" && txtEventDescription.Text != "")
            {
                if (CheckForExixtingEvents())
                {
                    try
                    {
                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {
                            using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                            {
                                int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                                string subSiteName = Page.Request.QueryString["SiteName"].ToString();
                                using (SPWeb oWeb = oSite.OpenWeb(subSiteName))
                                {
                                    SPList list = oWeb.Lists[ListName];
                                    SPListItem item = list.GetItemById(itemID);
                                    //item["Title"] = txtEventName.Text;
                                    item["EventVenue"] = ddlEventVenue.SelectedItem.Text;
                                    item["EventDate"] = txtEventDate.Text;
                                    item["EventTime"] = txtEventStartTime.Text + " - " + txtEventEndTime.Text;
                                    item["Department"] = ddlDepartment.SelectedItem.Text;
                                    item["Description"] = txtEventDescription.Text;

                                    if (fuEventImage.HasFile)
                                    {
                                        SPAttachmentCollection ocollAttachments = item.Attachments;
                                        if (ocollAttachments.Count > 0)
                                        {
                                            List<string> fileNames = new List<string>();

                                            foreach (string fileName in item.Attachments)
                                            {
                                                fileNames.Add(fileName);
                                            }

                                            foreach (string fileName in fileNames)
                                            {
                                                item.Attachments.Delete(fileName);
                                            }
                                        }
                                        foreach (HttpPostedFile postedFile in fuEventImage.PostedFiles)
                                        {
                                            try
                                            {
                                                Stream fs = postedFile.InputStream;
                                                byte[] fileContents = new byte[fs.Length];
                                                fs.Read(fileContents, 0, (int)fs.Length);
                                                fs.Close();
                                                SPAttachmentCollection attach = item.Attachments;
                                                string fileName = Path.GetFileName(postedFile.FileName);
                                                attach.Add(fileName, fileContents);
                                            }
                                            catch
                                            {
                                                string sMessage = "Image Already Exists!";
                                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                            }

                                        }
                                    }
                                    oWeb.AllowUnsafeUpdates = true;
                                    item.Update();
                                    oWeb.AllowUnsafeUpdates = false;


                                    formClear();
                                    string cMessage = "Event has been updated successfully";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + cMessage + "');window.location='ViewCalendarEvents.aspx';</script>", false);

                                }
                            }
                        });



                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                string cMessage = "Insufficient data. Please try again.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + cMessage + "');</script>", false);

            }

        }
       
        /// <summary>
        /// fires on the cancel button click event
        /// function for clearing the fields in page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ViewCalendarEvents.aspx';</script>", false);

        }
      
        /// <summary>
        /// function for clearing the controls
        /// </summary>
        void formClear()
        {
           
            ddlEventVenue.SelectedIndex = 0;
            txtEventDate.Text = "";
            txtEventStartTime.Text = "";
            txtEventEndTime.Text = "";
            txtEventDescription.Text = "";
     
        }
       
     

      

        /// <summary>
        /// function for checking the current details of event with already added events
        /// venue, date , time are taken for checking
        /// </summary>
        /// <returns></returns>
        public bool CheckForExixtingEvents()
       {
           bool returnvalue = true;
           try
           {
               
               string eventVenue = ddlEventVenue.SelectedItem.Text.Trim();
               DataTable dt = new DataTable();
               DateTime selectedDate = DateTime.Parse(txtEventDate.Text);
               string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(selectedDate.Date);
               string eventStartTime = selectedDate.Date.ToString("dd/MM/yyyy") + " " + txtEventStartTime.Text.Trim();
               string evnetEndTime = selectedDate.Date.ToString("dd/MM/yyyy") + " " + txtEventEndTime.Text.Trim();
               SPSecurity.RunWithElevatedPrivileges(delegate()
               {
                   using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                   {
                       int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                       string subSiteName = Page.Request.QueryString["SiteName"].ToString();
                       using (SPWeb oWeb = oSite.OpenWeb(subSiteName))
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
                                       if (Convert.ToInt32(li["ID"].ToString()) == itemID)
                                       {
                                       }
                                       else
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
                                           string sMessage = "There is already an event added. Please change venue, date or time and try again";
                                           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                       }

                                       if (exitLoop) break;

                                   }
                                   if (dt.Select("EventStartTime = '" + dtStartTime + "' AND EventEndTime = '" + dtEndTime + "'").Any())
                                   {
                                       returnvalue = false;
                                       formClear();
                                       string sMessage = "There is already an event added. Please change venue, date or time and try again";
                                       ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                   }
                               }
                           }
                       }
                   }
               });

           }


           catch
           {
               return false;
           }
           return returnvalue;
       }

        /// <summary>
        /// function for converting string to valid DateTime
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public DateTime GetDateTimeFromString(string sDate)
       {
           DateTime myDate = DateTime.ParseExact(sDate, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);
           return myDate;
       }

    }
    
}
