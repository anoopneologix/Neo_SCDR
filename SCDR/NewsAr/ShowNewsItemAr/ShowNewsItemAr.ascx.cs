﻿using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;
using System.Web.Caching;
using System.Collections.Generic;

namespace SCDR.NewsAr.ShowNewsItemAr
{
    [ToolboxItemAttribute(false)]
    public partial class ShowNewsItemAr : WebPart
    {
        string SpListItemId = string.Empty;
        public ShowNewsItemAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomNewsList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
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
                SpListItemId = HttpContext.Current.Request.QueryString["NewsID"];
                if (SpListItemId != "")
                {
                    BindNewsItem(SpListItemId);
                }
            }
        }

        public void BindNewsItem(string ItemId)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                       /* using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='NewsID' /><Value Type='Text'>" + SpListItemId + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                foreach (SPListItem item in oItems)
                                {
                                    DateTime dtNewsDate = Convert.ToDateTime(item["Date"]);
                                    divNewsDate.InnerHtml = "تاريخ النشر: " + String.Format("{0:MM/dd/yyyy}", dtNewsDate);
                                    headingNews.InnerHtml = item["Title"].ToString();
                                    pNewsLocation.InnerHtml = item["Location"].ToString();
                                    pNewsDescription.InnerHtml = item["Description"].ToString();
                                    SPAttachmentCollection docs = item.Attachments;
                                    DataTable dt = new DataTable();
                                    DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                                    dt.Columns.Add(dcImageUrl);
                                    if (docs.Count > 0)
                                    {
                                        foreach (string fileName in item.Attachments)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                            dt.Rows.Add(dr);
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["ImageUrl"] = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
                                        dt.Rows.Add(dr);
                                    }

                                    repMainSlider.DataSource = dt;
                                    repMainSlider.DataBind();
                                }
                            }
                        }*/
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList list = oWeb.Lists[ListName];
                            int intId = Convert.ToInt32(ItemId);
                            SPListItem item = list.GetItemById(intId);
                            DateTime dtNewsDate = Convert.ToDateTime(item["Date"]);
                            divNewsDate.InnerHtml = "تاريخ النشر: " + String.Format("{0:MM/dd/yyyy}", dtNewsDate);
                            headingNews.InnerHtml = item["Title"].ToString();
                            pNewsLocation.InnerHtml = item["Location"].ToString();
                            pNewsDescription.InnerHtml = item["Description"].ToString();
                            SPAttachmentCollection docs = item.Attachments;
                            DataTable dt = new DataTable();
                            DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                            dt.Columns.Add(dcImageUrl);
                            if (docs.Count > 0)
                            {
                                foreach (string fileName in item.Attachments)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr["ImageUrl"] = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
                                dt.Rows.Add(dr);
                            }

                            repMainSlider.DataSource = dt;
                            repMainSlider.DataBind();


                        }
                           
                  
                    }
                });
            }
            catch
            {

            }
        }
    }
}
