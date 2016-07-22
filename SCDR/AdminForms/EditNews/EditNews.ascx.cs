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

namespace SCDR.AdminForms.EditNews
{
    [ToolboxItemAttribute(false)]
    public partial class EditNews : WebPart
    {
         public EditNews()
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
        private const string DefaultNewsList = "CustomNewsList";
        private static string newsListName = DefaultNewsList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultNewsList),
        WebDisplayName("News List Name:"),
        WebDescription("Please Enter a valid News List Name")]
        public string NewsListName
        {
            get { return newsListName; }
            set { newsListName = value; }
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

                BindNews();

            }
        }

        /// <summary>
        /// function for binding the news to gridview based on langauage
        /// </summary>
        public void BindNews()
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
                            SPList oList = oWeb.Lists[NewsListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtVenue = ConvertSPListToDataTable(oItems);
                            gdvNews.DataSource = dtVenue;
                            gdvNews.DataBind();
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
        /// fires when the linkbutton 'lbAddNews' gets clicked
        /// page redirect to AddVenue.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lbAddNews_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddNews.aspx");
        }
        /// <summary>
        /// binds the news on gridview from Arabic list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            BindNews();
        }
        /// <summary>
        /// binds the news on gridview from english list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindNews();
        }

        /// <summary>
        /// fires when the rows on gridview get clicked
        /// for edit and delete venue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void gdvNews_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
                        SPList oList = oWeb.Lists[NewsListName];
                        int listItemId = Convert.ToInt32(e.CommandArgument);
                        if (e.CommandName == "delme")
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPListItem itemToDelete = oList.GetItemById(listItemId);
                            itemToDelete.Delete();
                            oWeb.AllowUnsafeUpdates = false;
                            BindNews();
                        }
                        else if (e.CommandName == "editme")
                        {
                            Page.Response.Redirect("EditNews.aspx?NewsID=" + listItemId + "&SiteName=" + subsiteName);
                        }

                    }
                }
            });

        }
    }
}
