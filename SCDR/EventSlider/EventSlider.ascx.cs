//--------------------------------------------------
// Project Name : SCDR
// Program Name : EventsrImageSlider (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 24/03/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;

namespace SCDR.EventSlider
{
    [ToolboxItemAttribute(false)]
    public partial class EventSlider : WebPart
    {
          public EventSlider()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
           
        }

        // Fuction for loading the Slider on Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindEventImagesToSlider();
                Control HeaderTemplate = repEventsSlider.Controls[0].Controls[0];
                Label lab = HeaderTemplate.FindControl("lblHeading") as Label;
                lab.Text = EventSlider.headingName;
               
            }
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CalenderEvents_List";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Picture Library Name:"),
        WebDescription("Please Enter a valid Picture Library Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultHeading = "Events";
        private static string headingName = DefaultHeading;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultHeading),
        WebDisplayName("Heading to display:"),
        WebDescription("Please Enter the heading to be displayed")]

        public string HeadingName
        {
            get { return headingName; }
            set { headingName = value; }
        }
        #endregion

        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindEventImagesToSlider()
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
                            query.Query = @"<Where><Geq><FieldRef Name='EventDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>"+dtToday+"</Value></Geq></Where><OrderBy><FieldRef Name='Modified' Ascending='TRUE' /></OrderBy>";
                          
                            SPListItemCollection oItems = oList.GetItems(query);
                            if(oItems.Count>0)
                            {
                            DataTable dtEventsImages = ConvertSPListToDataTable(oItems);
                            repEventsSlider.DataSource = dtEventsImages;
                            repEventsSlider.DataBind();
                            }
                            else
                            {
                                SPList SpList = oWeb.Lists[ListName];
                                SPQuery queryAlt = new SPQuery();
                                string dtAltToday = SPUtility.CreateISO8601DateTimeFromSystemDateTime(utcTime.Date);
                                queryAlt.Query = @"<Where><Leq><FieldRef Name='Modified' /><Value IncludeTimeValue='FALSE' Type='DateTime'>"+dtAltToday+"</Value></Leq></Where><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                                queryAlt.RowLimit = 3;
                                SPListItemCollection spItems = SpList.GetItems(queryAlt);
                                DataTable dtEventsImages = ConvertSPListToDataTable(spItems);
                                repEventsSlider.DataSource = dtEventsImages;
                                repEventsSlider.DataBind();
                                repEventPop.DataSource = dtEventsImages;
                                repEventPop.DataBind();

                            }
                        }
                    }
                });
            }
            catch(Exception ex)
            { }
        }

        // Function to convert SharePoint Picture Library to DataTable 
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                dtSPList.Columns.Add(dcImageUrl);
                foreach (DataRow dr in dtSPList.Rows)
                {
                    string imgUrl = dr["Image"].ToString();
                    int index = imgUrl.IndexOf(",");
                    if (index > 0)
                    {
                        imgUrl = imgUrl.Substring(0, index);
                        dr["ImageUrl"] = imgUrl;
                    }
                    imgUrl = "";
                    index = 0;
                }
                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }
    }
}
