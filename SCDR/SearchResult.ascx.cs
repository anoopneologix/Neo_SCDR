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

namespace SCDR.SearchResult
{
    [ToolboxItemAttribute(false)]
    public partial class SearchResult : WebPart
    {
        public SearchResult()
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
        #endregion

        //Code for Page load
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
                            SPList oList = oWeb.Lists[NewsListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtNews = ConvertSPListToDataTable(oItems, searchKeyword, SiteUrl);
                            rptrSearchResult.DataSource = dtNews;
                            rptrSearchResult.DataBind();
                        }
                    }
                });
            }
            catch
            {

            }
        }

        // Function to convert SharePoint News List to DataTable 
        private DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection, string searchKeyword, string siteUrl)
        {
            DataTable dtSPList = new DataTable();
               try
            {
                dtSPList = spItemCollection.GetDataTable();

                var filteredRows = dtSPList.AsEnumerable()
                    .Where(r => r.Field<String>("Title").Contains(searchKeyword)
                    || r.Field<String>("Location").Contains(searchKeyword)
                    || r.Field<String>("Description").Contains(searchKeyword));
                if (filteredRows.Any())
                    dtSPList = filteredRows.CopyToDataTable();
               
                DataColumn dcPageID = new DataColumn("PageUrl", typeof(string));
                dtSPList.Columns.Add(dcPageID);
                DataColumn dcDescription = new DataColumn("Content", typeof(string));
                dtSPList.Columns.Add(dcDescription);
              
                foreach (DataRow dr in dtSPList.Rows)
                {

                    dr["PageUrl"] = siteUrl + "/SitePages/News-Content.aspx?NewsID=" + dr["ID"];
                    string content = ExtractSentence(searchKeyword, dr["Description"].ToString());
                 //   string content = ExtractParagraph(dr["Description"].ToString(), searchKeyword);
                    dr["Content"] = content;
                }
                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }

        public string ExtractParagraph(string text, string searchKeyword)
        {
            string res = string.Empty;
            string expression = @"((^.{0,30}|\w*.{30})\b" + searchKeyword + @"\b(.{30}\w*|.{0,30}$))";
            Regex wordMatch = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var results = wordMatch.Matches(text);
            for (int i = 0; i < results.Count; i++)
            {
                res= results[i].Value.Trim().ToString();
            }
            return res;
          
        }

        public string ExtractSentence(string searchKeyword,string text)
        {

            var regex = new Regex(string.Format("[^.!?]*({0})[^.?!]*[.?!]", searchKeyword));
            string res = string.Empty;
            var results = regex.Matches(text);

            for (int i = 0; i < results.Count; i++)
            {
                res= results[i].Value.Trim().ToString();
            }
            return res;
        }


    }
}
