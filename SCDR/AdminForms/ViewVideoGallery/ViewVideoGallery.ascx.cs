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

namespace SCDR.AdminForms.ViewVideoGallery
{
    [ToolboxItemAttribute(false)]
    public partial class ViewVideoGallery : WebPart
    {
        public ViewVideoGallery()
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
        private const string DefaultList = "CustomVideoGallery";
        private static string listName = DefaultList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultList),
        WebDisplayName("Venue List Name:"),
        WebDescription("Please Enter a valid Venue List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

                BindVideo();

            }
        }

        protected void lbAddVideoGallery_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddVideoGallery.aspx");
        }

        /// <summary>
        /// function for binding the video to gridview based on langauage
        /// </summary>
        public void BindVideo()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subSiteName = string.Empty;
                        if(rbArabic.Checked)
                        {
                            subSiteName = "ar/";
                        }
                        else
                        {
                            subSiteName = "en/";
                        }
                        using (SPWeb oWeb = oSite.OpenWeb(subSiteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtVenue = ConvertSPListToDataTable(oItems);
                            gdvVideoGallery.DataSource = dtVenue;
                            gdvVideoGallery.DataBind();
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
                DataTable dt = new DataTable();
                if (dtSPList.Rows.Count > 0)
                {
                   
                    DataColumn dcImageUrl = new DataColumn("VideoUrl", typeof(string));
                    dt.Columns.Add(dcImageUrl);
                    DataColumn dcTitle = new DataColumn("Title", typeof(string));
                    dt.Columns.Add(dcTitle);
                    DataColumn dcID= new DataColumn("ID", typeof(string));
                    dt.Columns.Add(dcID);
                    foreach (DataRow item  in dtSPList.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        string imgUrl = item["VideoUrl"].ToString();
                        int index = imgUrl.IndexOf(",");
                        if (index > 0)
                        {
                            imgUrl = imgUrl.Substring(0, index);
                            dr["VideoUrl"] = imgUrl;
                        }
                        dr["Title"] = item["Title"].ToString();
                        dr["ID"] = item["ID"].ToString(); 
                        dt.Rows.Add(dr);
                        imgUrl = "";
                        index = 0;
                    }
                 
                }
                return (dt);
            }
            catch
            {
                return (dtSPList);
            }
        }

        protected void gdvVideoGallery_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        string subSiteName = string.Empty;
                        if (rbArabic.Checked)
                        {
                            subSiteName = "ar/";
                        }
                        else
                        {
                            subSiteName = "en/";
                        }
                        using (SPWeb oWeb = oSite.OpenWeb(subSiteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            int listItemId = Convert.ToInt32(e.CommandArgument);
                            if (e.CommandName == "delme")
                            {
                                oWeb.AllowUnsafeUpdates = true;
                                SPListItem itemToDelete = oList.GetItemById(listItemId);
                                itemToDelete.Delete();
                                oWeb.AllowUnsafeUpdates = false;
                                BindVideo();
                            }
                        }
                    }
                });
            }
            catch
            {

            }

        }

  


        protected void rbArabic_CheckedChanged1(object sender, EventArgs e)
        {
            BindVideo();
        }

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            BindVideo();
        }
    }
}
