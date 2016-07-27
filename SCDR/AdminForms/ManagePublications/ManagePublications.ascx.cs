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

namespace SCDR.AdminForms.ManagePublications
{
    [ToolboxItemAttribute(false)]
    public partial class ManagePublications : WebPart
    {
        public ManagePublications()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
       
        /// <summary>
        /// function for enabiling custom webpart properties
        /// </summary>
        #region CustomWebPartProperty
        private const string DefaultList = "PublicationLibrary";
        private static string listName = DefaultList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultList),
        WebDisplayName("Publications List Name:"),
        WebDescription("Please Enter a valid Publications List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string ThumbnailLibraryName = "PublicationThumbnailLibrary";
        private static string thubnailListName = ThumbnailLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(ThumbnailLibraryName),
        WebDisplayName("Publication Thumbnail Library Name:"),
        WebDescription("Please Enter a valid Publication Thumbnail Library Name")]
        public string ThumbnailListName
        {
            get { return thubnailListName; }
            set { thubnailListName = value; }
        }
        #endregion
       
        /// <summary>
        /// fires on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

                BindPublications();

            }
        }

        /// <summary>
        /// binds the Publications on gridview from Arabic list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindPublications();
        }

        /// <summary>
        /// binds the Publications on gridview from Arabic list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            BindPublications();
        }

        /// <summary>
        /// fires when the rows on gridview get clicked
        /// for edit and delete Publications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvPublications_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
                        SPList oList = oWeb.Lists[ListName];
                        int listItemId = Convert.ToInt32(e.CommandArgument);
                        if (e.CommandName == "delme")
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPListItem itemToDelete = oList.GetItemById(listItemId);
                            int thumbnailId = Convert.ToInt32(itemToDelete["ThumbnailId"]);
                            itemToDelete.Delete();
                            SPList thubnailList = oWeb.Lists[ThumbnailListName];
                            SPListItem thumbnailToDelete = thubnailList.GetItemById(thumbnailId);
                            thumbnailToDelete.Delete();
                            oWeb.AllowUnsafeUpdates = false;
                            BindPublications();
                        }
                        else if (e.CommandName == "editme")
                        {
                            Page.Response.Redirect("EditNews.aspx?NewsID=" + listItemId + "&SiteName=" + subsiteName);
                        }

                    }
                }
            });

        }
       
        /// <summary>
        /// function for binding the Publications to gridview based on langauage
        /// </summary>
        public void BindPublications()
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
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtVenue = ConvertSPListToDataTable(oItems);
                            gdvPublications.DataSource = dtVenue;
                            gdvPublications.DataBind();
                        }
                    }
                });
            }
            catch
            {

            }

        }
        
        /// <summary>
        ///  Function to convert SharePoint List to DataTable 
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
        /// fires when the linkbutton 'lbAddPublications' gets clicked
        /// page redirect to AddPublication.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAddPublications_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddPublication.aspx");
        }
      
    }
}
