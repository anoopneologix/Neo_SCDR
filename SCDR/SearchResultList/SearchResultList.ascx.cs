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

        /// <summary>
        /// Code for enabling Webpart property
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
        private const string DefaultPublicationList = "Publications";
        private static string publicationListName = DefaultPublicationList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultPublicationList),
        WebDisplayName("News List Name:"),
        WebDescription("Please Enter a valid Image Gallery List Name")]
        public string PublicationListName
        {
            get { return publicationListName; }
            set { publicationListName = value; }
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
       
        /// <summary>
        /// Function for binding the search result to page
        /// </summary>
        /// <param name="searchKeyword"></param>
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
                            SPList oPublicationList = oWeb.Lists[PublicationListName];
                            SPListItemCollection oPublicationItems = oPublicationList.GetItems();
                            DataTable dtPublication = MatchingPublications(oPublicationItems, searchKeyword, SiteUrl);
                            DataTable dtSearchResult = new DataTable();
                            DataColumn dcTitle = new DataColumn("Title", typeof(string));
                            dtSearchResult.Columns.Add(dcTitle);
                            DataColumn dcPageID = new DataColumn("PageUrl", typeof(string));
                            dtSearchResult.Columns.Add(dcPageID);
                            DataColumn dcDescription = new DataColumn("Content", typeof(string));
                            dtSearchResult.Columns.Add(dcDescription);
                            DataColumn dcDisp = new DataColumn("disp", typeof(string));
                            dtSearchResult.Columns.Add(dcDisp);
                            if (dtNews != null)
                            {
                                //merging first data table into second data table  
                                dtSearchResult.Merge(dtNews);
                                dtSearchResult.AcceptChanges();
                                rptrSearchResult.DataSource = dtSearchResult;
                                rptrSearchResult.DataBind();
                            }
                            if (dtImageGallery != null)
                            {
                                dtSearchResult.Merge(dtImageGallery);
                                dtSearchResult.AcceptChanges();
                                rptrSearchResult.DataSource = dtSearchResult;
                                rptrSearchResult.DataBind();
                            }
                            if (dtSitePages != null)
                            {
                                dtSearchResult.Merge(dtSitePages);
                                dtSearchResult.AcceptChanges();
                                rptrSearchResult.DataSource = dtSearchResult;
                                rptrSearchResult.DataBind();
                            }
                            if (dtPublication != null)
                            {
                                dtSearchResult.Merge(dtPublication);
                                dtSearchResult.AcceptChanges();
                                rptrSearchResult.DataSource = dtSearchResult;
                                rptrSearchResult.DataBind();
                            }
                            if (dtSearchResult.Rows.Count <= 0)
                            {
                                DataRow drow = dtSearchResult.NewRow();
                                drow["Title"] = "No results found";
                                drow["disp"] = "display: none";
                                dtSearchResult.Rows.Add(drow);
                                rptrSearchResult.DataSource = dtSearchResult;
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

         /// <summary>
        /// Function to convert SharePoint News List to DataTable  and return matching news
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <param name="searchKeyword"></param>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
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
                    DataColumn dcDisp = new DataColumn("disp", typeof(string));
                    dtNewsList.Columns.Add(dcDisp);
                    foreach (DataRow dr in dtSPList.Rows)
                    {
                        DataRow drow = dtNewsList.NewRow();
                        drow["Title"] = dr["Title"];
                        drow["PageUrl"] = siteUrl + "/SitePages/News-Content.aspx?NewsID=" + dr["NewsID"];
                        string content = ExtractSentence(searchKeyword, dr["Description"].ToString());
                        drow["Content"] = content;
                        drow["disp"] = "display: inherit";
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
     
        /// <summary>
        ///  Function to convert SharePoint Image Gallery List to DataTable  and return matching Image title
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <param name="searchKeyword"></param>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
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
                    DataColumn dcDisp = new DataColumn("disp", typeof(string));
                    dtNewsList.Columns.Add(dcDisp);
                    foreach (DataRow dr in dtSPList.Rows)
                    {
                        DataRow drow = dtNewsList.NewRow();
                        drow["Title"] = dr["Title"];
                        drow["PageUrl"] = siteUrl + "/SitePages/Image-Gallery.aspx";
                        drow["Content"] = string.Empty;
                        drow["disp"] = "display: inherit";
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

        /// <summary>
        /// Function to convert SharePoint SitePages to DataTable  and return matching .aspx pages
        /// </summary>
        /// <param name="searchKeyword"></param>
        /// <returns></returns>
        private DataTable MatchingSitePages(string searchKeyword)
        {
            try
            {
                DataTable dtResult= new DataTable();
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        KeywordQuery keywordQuery = new KeywordQuery(oWeb);
                        keywordQuery.QueryText = searchKeyword.Trim();
                        keywordQuery.ResultsProvider = SearchProvider.Default;
                        keywordQuery.KeywordInclusion = KeywordInclusion.AllKeywords;
                        SearchExecutor searchExecutor = new SearchExecutor();
                        ResultTableCollection resultTableCollection = searchExecutor.ExecuteQuery(keywordQuery);
                        var resultTable = resultTableCollection.Filter("TableType", KnownTableTypes.RelevantResults);
                        var result = resultTable.FirstOrDefault();
                        dtResult = result.Table;
                        if (dtResult.Rows.Count > 0)
                        {
                            dtResult.CaseSensitive = false;
                            string filteredString = oWeb.Url + "/SitePages/";
                            var filteredRows = dtResult.AsEnumerable()
                                .Where(r => r.Field<String>("Path").ToLower().Contains(filteredString.ToLower()));
                            if (filteredRows.Any())
                            {
                                dtResult = filteredRows.CopyToDataTable();
                                DataTable dtSitePagesList = new DataTable();
                                DataColumn dcTitle = new DataColumn("Title", typeof(string));
                                dtSitePagesList.Columns.Add(dcTitle);
                                DataColumn dcPageID = new DataColumn("PageUrl", typeof(string));
                                dtSitePagesList.Columns.Add(dcPageID);
                                DataColumn dcDescription = new DataColumn("Content", typeof(string));
                                dtSitePagesList.Columns.Add(dcDescription);
                                DataColumn dcDisp = new DataColumn("disp", typeof(string));
                                dtSitePagesList.Columns.Add(dcDisp);
                                foreach (DataRow dr in dtResult.Rows)
                                {
                                    DataRow drow = dtSitePagesList.NewRow();
                                    drow["Title"] = dr["Title"];
                                    drow["PageUrl"] = dr["Path"];
                                    drow["Content"] = dr["HitHighlightedSummary"];
                                    drow["disp"] = "display: inherit";
                                    dtSitePagesList.Rows.Add(drow);
                                }
                                return (dtSitePagesList);
                            }
                            else
                            {
                                return null;
                            }

                        }
                        else
                        {
                            return null;
                        }
                    }

                }


            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  Function to extract matching sentence from description
        /// </summary>
        /// <param name="searchKeyword"></param>
        /// <param name="text"></param>
        /// <returns></returns>
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
      
        /// <summary>
        /// function to convert sharepoint publication list to datatable and returm matching title/pdf
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <param name="searchKeyword"></param>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        private DataTable MatchingPublications(SPListItemCollection spItemCollection, string searchKeyword, string siteUrl)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                dtSPList.CaseSensitive = false;

                var filteredRows = dtSPList.AsEnumerable()
                    .Where(r => r.Field<String>("LinkFilename").Contains(searchKeyword));

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
                    DataColumn dcDisp = new DataColumn("disp", typeof(string));
                    dtNewsList.Columns.Add(dcDisp);
                    foreach (DataRow dr in dtSPList.Rows)
                    {
                        DataRow drow = dtNewsList.NewRow();
                        drow["Title"] = dr["LinkFilename"];
                        drow["PageUrl"] = siteUrl + "/"+publicationListName+"/"+dr["LinkFilename"];
                        drow["Content"] = string.Empty;
                        drow["disp"] = "display: inherit";
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

    }
}
