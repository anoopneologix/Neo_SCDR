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

namespace SCDR.AdminForms.ViewVenue
{
    [ToolboxItemAttribute(false)]
    public partial class ViewVenue : WebPart
    {
        public ViewVenue()
        {
        }

        #region CustomWebPartProperty
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

                BindVenue();

            }
        }
        public void BindVenue()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subsiteName = string.Empty;
                        if(rbArabic.Checked)
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
                            gdvVenue.DataSource = dtDepartment;
                            gdvVenue.DataBind();
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

        protected void lbAddVenue_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddVenue.aspx");
        }

        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            BindVenue();
        }

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindVenue();
        }

        protected void gdvVenue_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            BindVenue();
                        }
                        else if (e.CommandName == "editme")
                        {
                            Page.Response.Redirect("EditVenue.aspx?ItemID=" + listItemId + "&SiteName=" + subsiteName);
                        }

                    }
                }
            });
        }
    }
}
