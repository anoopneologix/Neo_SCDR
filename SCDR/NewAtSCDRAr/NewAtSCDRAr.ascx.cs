//--------------------------------------------------
// Project Name : SCDR
// Program Name : NewAtSCDR (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 09/05/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;

namespace SCDR.NewAtSCDRAr
{
    [ToolboxItemAttribute(false)]
    public partial class NewAtSCDRAr : WebPart
    {
        public NewAtSCDRAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "NewAtSCDR_List";
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
        private const string DefaultHeading = "جديد المركز";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindImagesToSlider();
           

            }
        }
        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindImagesToSlider()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            DataTable dt = new DataTable();
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                                dt.Columns.Add(dcImageUrl);
                                DataColumn dcDescription = new DataColumn("Description", typeof(string));
                                dt.Columns.Add(dcDescription);
                                foreach (SPListItem item in oItems)
                                {
                                    SPAttachmentCollection collAttachments = item.Attachments;

                                    foreach (var attachment in collAttachments)
                                    {


                                        if (collAttachments.Count > 0)
                                        {
                                            foreach (string fileName in item.Attachments)
                                            {
                                                DataRow dr = dt.NewRow();
                                                dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                                dr["Description"] = item["EventDescription"];
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                                repEventsSlider.DataSource = dt;
                                repEventsSlider.DataBind();
                                repEventPop.DataSource = dt;
                                repEventPop.DataBind();
                                Control HeaderTemplate = repEventsSlider.Controls[0].Controls[0];
                                Label lab = HeaderTemplate.FindControl("lblHeading") as Label;
                                lab.Text = NewAtSCDRAr.headingName;
                            }



                        }
                    }



                });
            }
            catch (Exception ex)
            { }
        }
    }
}
