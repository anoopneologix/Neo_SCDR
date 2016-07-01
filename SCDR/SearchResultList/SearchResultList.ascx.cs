//--------------------------------------------------
// Project Name : SCDR
// Program Name : Search Result (Visual WebPart)
// Developed by : Neo 250
// Created Date : 28/06/2016
//---------------------------------------------------
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
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;
using System.Linq;

namespace SCDR.SearchResultList
{
    [ToolboxItemAttribute(false)]
    public partial class SearchResultList : WebPart
    {
        public SearchResultList()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        //Code for enabling Webpart property
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
        private const string DefaultImageList = "CustomImageGallery";
        private static string imageListName = DefaultImageList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultImageList),
        WebDisplayName("News List Name:"),
        WebDescription("Please Enter a valid Image Gallery List Name")]
        public string ImageListName
        {
            get { return imageListName; }
            set { imageListName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (Page.Request.QueryString["kw"] != null)
                {
                    string searchKeyword = Page.Request.QueryString["kw"].ToString();
                    GetResult(searchKeyword);

                }

            }
        }
        //Function for binding the search result to page
        public void GetResult(string searchKeyword)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            string SiteUrl = oWeb.Url;
                            SPList oNewsList = oWeb.Lists[NewsListName];
                            SPListItemCollection oNewsItems = oNewsList.GetItems();
                            DataTable dtNews = MatchingNews(oNewsItems, searchKeyword, SiteUrl);
                            SPList oImageList = oWeb.Lists[ImageListName];
                            SPListItemCollection oImageItems = oImageList.GetItems();
                            DataTable dtImageGallery = MatchingImageGallery(oImageItems, searchKeyword, SiteUrl);
                            DataTable dtSitePages = MatchingSitePages(searchKeyword);
                            if (dtNews != null && dtImageGallery != null)
                            {
                                //merging first data table into second data table  
                                dtNews.Merge(dtImageGallery);
                                dtNews.AcceptChanges(); 
                                rptrSearchResult.DataSource = dtNews;
                                rptrSearchResult.DataBind();
                            }
                            else if(dtNews != null && dtImageGallery == null)
                            {
                                rptrSearchResult.DataSource = dtNews;
                                rptrSearchResult.DataBind();
                            }
                            else if (dtNews == null && dtImageGallery != null)
                            {
                                rptrSearchResult.DataSource = dtImageGallery;
                                rptrSearchResult.DataBind();
                            }
                        }
                    }
                });
            }
            catch
            {

            }
        }

        // Function to convert SharePoint News List to DataTable  and return matching news
        private DataTable MatchingNews(SPListItemCollection spItemCollection, string searchKeyword, string siteUrl)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                dtSPList.CaseSensitive = false;
               
                var filteredRows = dtSPList.AsEnumerable()
                    .Where(r => r.Field<String>("Title").ToLower().Contains(searchKeyword.ToLower())
                    || r.Field<String>("Location").ToLower().Contains(searchKeyword.ToLower())
                    || r.Field<String>("Description").ToLower().Contains(searchKeyword.ToLower()));
                if (filteredRows.Any())
                {
                    dtSPList = filteredRows.CopyToDataTable();
                    DataTable dtNewsList = new DataTable();
                    DataColumn dcTitle = new DataColumn("Title", typeof(string));
                    dtNewsList.Columns.Add(dcTitle);
                    DataColumn dcPageID = new DataColumn("PageUrl", typeof(string));
                    dtNewsList.Columns.Add(dcPageID);
                    DataColumn dcDescription = new DataColumn("Content", typeof(string));
                    dtNewsList.Columns.Add(dcDescription);
                    foreach (DataRow dr in dtSPList.Rows)
                    {
                        DataRow drow = dtNewsList.NewRow();
                        drow["Title"] = dr["Title"];
                        drow["PageUrl"] = siteUrl + "/SitePages/News-Content.aspx?NewsID=" + dr["ID"];
                        string content = ExtractSentence(searchKeyword, dr["Description"].ToString());
                        drow["Content"] = content;
                        dtNewsList.Rows.Add(drow);
                    }
                    return (dtNewsList);
                }
                else
                {
                    return null;
                }
               
            }
            catch
            {
                return null;
            }
        }
     
        // Function to convert SharePoint Image Gallery List to DataTable  and return matching Image title
        private DataTable MatchingImageGallery(SPListItemCollection spItemCollection, string searchKeyword, string siteUrl)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                dtSPList.CaseSensitive = false;

                var filteredRows = dtSPList.AsEnumerable()
                    .Where(r => r.Field<String>("Title").ToLower().Contains(searchKeyword.ToLower())
                    || r.Field<String>("CategoryName").ToLower().Contains(searchKeyword.ToLower()));
                    
                if (filteredRows.Any())
                {
                    dtSPList = filteredRows.CopyToDataTable();
                    DataTable dtNewsList = new DataTable();
                    DataColumn dcTitle = new DataColumn("Title", typeof(string));
                    dtNewsList.Columns.Add(dcTitle);
                  
                    DataColumn dcPageID = new DataColumn("PageUrl", typeof(string));
                    dtNewsList.Columns.Add(dcPageID);
                    DataColumn dcDescription = new DataColumn("Content", typeof(string));
                    dtNewsList.Columns.Add(dcDescription);

                    foreach (DataRow dr in dtSPList.Rows)
                    {
                        DataRow drow = dtNewsList.NewRow();
                        drow["Title"] = dr["Title"];
                        drow["PageUrl"] = siteUrl + "/SitePages/Image-Gallery.aspx";
                        drow["Content"] = string.Empty;
                        dtNewsList.Rows.Add(drow);
                    }
                    return (dtNewsList);
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }


        private DataTable MatchingSitePages(string searchKeyword)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb("en/"))
                    {
                        KeywordQuery keywordQuery = new KeywordQuery(oWeb);
                        keywordQuery.QueryText = searchKeyword.Trim();
                        keywordQuery.ResultsProvider = SearchProvider.Default;
                        keywordQuery.KeywordInclusion = KeywordInclusion.AllKeywords;

                        SearchExecutor searchExecutor = new SearchExecutor();
                        ResultTableCollection resultTableCollection = searchExecutor.ExecuteQuery(keywordQuery);
                        var resultTable = resultTableCollection.Filter("TableType", KnownTableTypes.RelevantResults);
                        var result = resultTable.FirstOrDefault();
                        dataTable = result.Table;
                    }
                    if(dataTable.Rows.Count>0)
                    {
                        return dataTable;
                    }
                    else
                    {
                        return null;
                    }
                }



            }
            catch
            {
                return null;
            }
        }

          
        



        // Function to extract matching sentence from description
        public string ExtractSentence(string searchKeyword, string text)
        {
            string resultString = string.Empty;
            var regex = new Regex(string.Format("[^.!?]*({0})[^.?!]*[.?!]", searchKeyword));
            var results = regex.Matches(text);
            if (results.Count > 0)
            {
                resultString = Regex.Replace(results[0].Value.Trim().ToString(), "<.*?>", string.Empty);
            }
            else
            {

                resultString = string.Empty;
            }
            return resultString;
        }
    }
}
