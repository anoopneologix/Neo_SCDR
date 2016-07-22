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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;

namespace SCDR.AdminForms.ViewCalendarEvents
{
    [ToolboxItemAttribute(false)]
    public partial class ViewCalendarEvents : WebPart
    {
        public ViewCalendarEvents()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region CustomWebPartProperty
        private const string DefaultVenueList = "CustomCalendarList";
        private static string venueListName = DefaultVenueList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultVenueList),
        WebDisplayName("Venue List Name:"),
        WebDescription("Please Enter a valid Calendar List Name")]
        public string VenueListName
        {
            get { return venueListName; }
            set { venueListName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

                BindEvents();

            }
        }
        public void BindEvents()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subsiteName = string.Empty;
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
                            SPList oList = oWeb.Lists[VenueListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtDepartment = ConvertSPListToDataTable(oItems);
                            gdvEvents.DataSource = dtDepartment;
                            gdvEvents.DataBind();
                        }
                    }
                });
            }
            catch
            {

            }

        }

        // Function to convert SharePoint Picture Library to DataTable 
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

        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            BindEvents();
        }

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindEvents();
        }

        protected void gdvEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEvents.PageIndex = e.NewPageIndex;
            BindEvents();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subsiteName = string.Empty;
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
                          
                            SPList oList = oWeb.Lists[VenueListName];
                            SPQuery query = new SPQuery();
                            DateTime sDate = DateTime.Parse(txtEventDate.Text);
                            DateTime eDate = DateTime.Parse(txtEventEndDate.Text);
                            string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(sDate.Date);
                            string dteDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(eDate.Date);
                            query.Query = @"<Where><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + txtEventName.Text + "</Value></Contains><And><Geq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Geq><Leq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dteDate + "</Value></Leq></And></And></Where>";

                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                BindDetailsToRGridView(oItems);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "displayalertmessage", "Showalert();", true);
                            }
                            txtEventDate.Text = "";
                            txtEventEndDate.Text = "";
                            txtEventName.Text = "";
                        }
                    }
                });
            }
            catch
            {

            }

        }

        public void BindDetailsToRGridView(SPListItemCollection oItems)
        {
            try
            {

                DataTable dt = new DataTable();
                DataColumn dcID= new DataColumn("ID", typeof(string));
                dt.Columns.Add(dcID);
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
               
                foreach (SPListItem li in oItems)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = li["ID"].ToString();
                    dr["Title"] = li["Title"].ToString();
                    dr["EventVenue"] = li["EventVenue"].ToString();
                    DateTime dtEventDate = Convert.ToDateTime(li["EventDate"]);
                    dr["EventDate"] = dtEventDate.ToString("dd/MM/yyyy");

                    dr["EventTime"] = li["EventTime"].ToString().Replace(" ", String.Empty);
                    dr["Department"] = li["Department"].ToString();
                   
                    dt.Rows.Add(dr);
                }

                gdvEvents.DataSource = dt;
                gdvEvents.DataBind();

            }
            catch
            {

            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            BindEvents();
            txtEventDate.Text = "";
            txtEventEndDate.Text = "";
            txtEventName.Text = "";
        }

        protected void gdvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subsiteName = string.Empty;
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
                            SPList oList = oWeb.Lists[VenueListName];
                            int listItemId = Convert.ToInt32(e.CommandArgument);
                            if (e.CommandName == "delme")
                            {
                                oWeb.AllowUnsafeUpdates = true;
                                SPListItem itemToDelete = oList.GetItemById(listItemId);
                                itemToDelete.Delete();
                                oWeb.AllowUnsafeUpdates = false;
                                BindEvents();
                             
                            }
                            else if (e.CommandName == "editme")
                            {
                                Page.Response.Redirect("EditCalendarEvents.aspx?ItemID=" + listItemId +"&SiteName="+subsiteName);
                            }

                        }
                    }
                });
            }
            catch
            {

            }
        }

        protected void lbAddEvent_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddCalenderEvents.aspx");
        }
    }
}
