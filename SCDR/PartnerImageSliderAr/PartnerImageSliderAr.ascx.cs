﻿using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCDR.PartnerImageSliderAr
{
    [ToolboxItemAttribute(false)]
    public partial class PartnerImageSliderAr : WebPart
    {
        public PartnerImageSliderAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Function will bind the images to partner slider when page load occur.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindPartnerImagesToSlider();
                Control HeaderTemplate = repPartnerSlider.Controls[0].Controls[0];
                Label lab = HeaderTemplate.FindControl("lblHeading") as Label;
                lab.Text = PartnerImageSliderAr.headingName;
            }
        }

        // Following code is for enabling custom webpart properties
        #region CustomWebPartProperties
        private const string DefaultLibraryName = "PartnerSlider_Library";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Picture Library Name:"),
        WebDescription("Please Enter a valid Picture Library Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultHeading = "الشراكات والعضويات";
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

        // Function for binding images and their corresponding site url's to Repeater
        public void BindPartnerImagesToSlider()
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
                            SPListItemCollection oItems = oList.GetItems();
                            string listUrl = oWeb.Url + "/" + ListName + "/";
                            DataTable dtFirst = ConvertSPListToDataTable(oItems, listUrl);
                            repPartnerSlider.DataSource = dtFirst;
                            repPartnerSlider.DataBind();
                        }
                    }
                });
            }
            catch(Exception ex)
            { }
        }

        // function for converting SharePoint list to DataTable 
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection, string lUrl)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = spItemCollection.GetDataTable();
                DataColumn dc = new DataColumn("ImageUrl", typeof(string));
                dt.Columns.Add(dc);
                foreach (DataRow r in dt.Rows)
                {
                    string siteUrl = r["SiteUrl"].ToString();
                    int index = siteUrl.IndexOf(",");
                    if (index > 0)
                    {
                        siteUrl = siteUrl.Substring(0, index);
                        r["SiteUrl"] = siteUrl;
                    }
                    siteUrl = "";
                    index = 0;

                    r["ImageUrl"] = lUrl + r["LinkFileName"];
                }

                return (dt);
            }
            catch
            {
                return (dt);
            }
        }
  
    }
}
