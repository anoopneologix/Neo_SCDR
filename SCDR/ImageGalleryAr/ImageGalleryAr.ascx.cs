using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Web.UI;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;

namespace SCDR.ImageGalleryAr
{
    [ToolboxItemAttribute(false)]
    public partial class ImageGalleryAr : WebPart
    {
        List<string> thumbnailUrl = new List<string>();
        int pageIndex = 1;
        int pageSize = 6;
        public ImageGalleryAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomImageGallery";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Image Gallery List Name:"),
        WebDescription("Please Enter a valid Image Gallery Name")]
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
                this.BindPicturePox(1);
              //  BindHidePictureBox();
            }
        }

        // Function to call menu list items from SharePoint Custom List
        // Bind those items to the Repeaterr
        public void BindPicturePox(int pageIndex)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    DataTable dt = new DataTable();
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<OrderBy><FieldRef Name='Rank' Ascending='True' /><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                string SiteUrl = oWeb.Url;
                                DataTable dtNewsList = ConvertSPListToDataTable(oItems, SiteUrl);
                                DataTable dtPagingList = dtNewsList.Clone();
                                foreach (DataRow dr in dtNewsList.Select("PageIndex='" + pageIndex + "'"))
                                {
                                    dtPagingList.Rows.Add(dr.ItemArray);

                                }
                                repGroupName.DataSource = dtPagingList;
                                repGroupName.DataBind();
                                int recordCount = oItems.Count;
                                this.PopulatePager(recordCount, pageIndex);

                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        // Function to convert SharePoint Picture Library to DataTable 
        public DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection, string siteUrl)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                int i = 1;
                int tempSize = pageSize;
                int rowCount = dtSPList.Rows.Count;
                DataColumn dcPageIndex = new DataColumn("PageIndex", typeof(int));
                dtSPList.Columns.Add(dcPageIndex);
                DataColumn dcImageUrl = new DataColumn("dtThumbnailUrl", typeof(string));
                dtSPList.Columns.Add(dcImageUrl);
                DataColumn dcCategoryName = new DataColumn("dtCategoryName", typeof(string));
                dtSPList.Columns.Add(dcCategoryName);
                DataColumn dcTitle = new DataColumn("dtTitle", typeof(string));
                dtSPList.Columns.Add(dcTitle);
                foreach (DataRow dr in dtSPList.Rows)
                {
                    dr["PageIndex"] = pageIndex;
                    string imgUrl = dr["ThumbnailUrl"].ToString();
                    int index = imgUrl.IndexOf(",");
                    if (index > 0)
                    {
                        imgUrl = imgUrl.Substring(0, index);
                        dr["dtThumbnailUrl"] = imgUrl;
                        thumbnailUrl.Add(imgUrl);
                    }
                    dr["dtCategoryName"] = dr["CategoryName"];
                    dr["dtTitle"] = dr["Title"];
                    // dt.Rows.Add(dr);
                    if (i == tempSize)
                    {
                        tempSize = tempSize + 6;
                        pageIndex += 1;
                    }
                    i++;
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


     /*   public void BindHidePictureBox()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    DataTable dt = new DataTable();
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                            dt.Columns.Add(dcImageUrl);
                            DataColumn dcCategoryName = new DataColumn("CategoryName", typeof(string));
                            dt.Columns.Add(dcCategoryName);
                            DataColumn dcTitle = new DataColumn("Title", typeof(string));
                            dt.Columns.Add(dcTitle);
                            foreach (SPListItem item in oItems)
                            {
                                SPAttachmentCollection collAttachments = item.Attachments;
                                if (collAttachments.Count > 0)
                                {
                                    foreach (string fileName in item.Attachments)
                                    {
                                        if (!(thumbnailUrl.Contains(SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName))))
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                            dr["CategoryName"] = item["CategoryName"];
                                            dr["Title"] = item["Title"];
                                            dt.Rows.Add(dr);
                                        }
                                    }
                                }


                            }
                            repHideGroupName.DataSource = dt;
                            repHideGroupName.DataBind();
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }

        }*/

        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.BindPicturePox(pageIndex);
        }

        protected void repGroupName_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string categoryName = (e.Item.FindControl("hfCategoryName") as HiddenField).Value;
                Repeater repHideGroupName = e.Item.FindControl("repHideGroupName") as Repeater;
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        DataTable dt = new DataTable();
                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb oWeb = oSite.OpenWeb())
                            {
                                SPList oList = oWeb.Lists[ListName];
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><Eq><FieldRef Name='CategoryName' /><Value Type='Text'>" + categoryName + "</Value></Eq></Where>";
                                SPListItemCollection oItems = oList.GetItems(query);
                                if (oItems.Count > 0)
                                {
                                    DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                                    dt.Columns.Add(dcImageUrl);
                                    DataColumn dcCategoryName = new DataColumn("CategoryName", typeof(string));
                                    dt.Columns.Add(dcCategoryName);
                                    DataColumn dcTitle = new DataColumn("Title", typeof(string));
                                    dt.Columns.Add(dcTitle);
                                    foreach (SPListItem item in oItems)
                                    {
                                        SPAttachmentCollection collAttachments = item.Attachments;
                                        if (collAttachments.Count > 0)
                                        {
                                            foreach (string fileName in item.Attachments)
                                            {
                                                if (!(thumbnailUrl.Contains(SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName))))
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                                    dr["CategoryName"] = item["CategoryName"];
                                                    dr["Title"] = item["Title"];
                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }


                                    }
                                    repHideGroupName.DataSource = dt;
                                    repHideGroupName.DataBind();
                                }
                            }
                        }
                    });
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
