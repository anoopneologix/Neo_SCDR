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

namespace SCDR.AdminForms.ViewImageGallery
{
    [ToolboxItemAttribute(false)]
    public partial class ViewImageGallery : WebPart
    {
        public ViewImageGallery()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region CustomWebPartProperty
        private const string DefaultVenueList = "CustomImageGallery";
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

                BindImageGalleryDetails();

            }
        }
        public void BindImageGalleryDetails()
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
                            gdvImageGallery.DataSource = dtDepartment;
                            gdvImageGallery.DataBind();
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

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindImageGalleryDetails();
        }

        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            BindImageGalleryDetails();
        }

        protected void gdvImageGallery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvImageGallery.PageIndex = e.NewPageIndex;
            BindImageGalleryDetails();
        }
    }
}
