using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;
using System.Web.Caching;
using System.Collections.Generic;

namespace SCDR.News.PressReleasesList
{
    [ToolboxItemAttribute(false)]
    public partial class PressReleasesList : WebPart
    {
        int pageIndex = 1;
        int pageSize = 5;
        public PressReleasesList()
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
        WebDisplayName("Picture Library Name:"),
        WebDescription("Please Enter a valid Picture Library Name")]
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
                this.GetNewsPageWise(1);
            }
        }

        

        // Function to convert SharePoint Picture Library to DataTable 
        public  DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection,string siteUrl)
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
                DataColumn dcPageID = new DataColumn("PageID", typeof(string));
                dtSPList.Columns.Add(dcPageID);
                DataColumn dcNewsImage = new DataColumn("ImageUrl", typeof(string));
                dtSPList.Columns.Add(dcNewsImage);
                foreach (DataRow dr in dtSPList.Rows)
                {
                    dr["PageIndex"] = pageIndex;
                    dr["PageID"] = siteUrl + "/SitePages/News-Content.aspx?NewsID=" + dr["NewsID"];
                  //  dr["PageID"] = siteUrl + "/SitePages/News-Content.aspx?NewsID=" + dr["NewsID"];
                    string imgUrl = dr["ThumbnailUrl"].ToString();
                    int index = imgUrl.IndexOf(",");
                    if (index > 0)
                    {
                        imgUrl = imgUrl.Substring(0, index);
                        dr["ImageUrl"] = imgUrl;
                    }

                    if (i == tempSize)
                    {
                        tempSize = tempSize + 5 ;
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

        private void GetNewsPageWise(int pageIndex)
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
                            query.Query = @"<OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                string SiteUrl = oWeb.Url;
                                DataTable dtNewsList = ConvertSPListToDataTable(oItems,SiteUrl);
                                DataTable dtPagingList = dtNewsList.Clone();
                                foreach (DataRow dr in dtNewsList.Select("PageIndex='" + pageIndex + "'"))
                                {
                                    dtPagingList.Rows.Add(dr.ItemArray);

                                }
                                repNewsList.DataSource = dtPagingList;
                                repNewsList.DataBind();
                                int recordCount = oItems.Count;
                                this.PopulatePager(recordCount, pageIndex);
                            }
                        }
                    }
                });
            }

            catch
            {

            }
        }
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
            this.GetNewsPageWise(pageIndex);
        }
    }
}
