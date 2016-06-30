using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.SharePoint;
using System.Text;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCDR.News.LatestNews
{
    [ToolboxItemAttribute(false)]
    public partial class LatestNews : WebPart
    {
        public LatestNews()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
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
        private const string DefaultHeading = "Latest News";
        private static string headingName = DefaultHeading;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultHeading),
        WebDisplayName("Heading to display:"),
        WebDescription("Please Enter the heading to be displayed")]

        public string HeadingName
        {
            get { return headingName; }
            set { headingName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindLatestNewsToSlider();

                lblHeading.Text = LatestNews.headingName;
            }
        }

      

        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindLatestNewsToSlider()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    int batchSize = 3;
                    int j = 0;
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            DateTime utcTime = DateTime.UtcNow;
                            string dtToday = SPUtility.CreateISO8601DateTimeFromSystemDateTime(utcTime.Date);
                            query.Query = @"<Where><Leq><FieldRef Name='Modified' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtToday + "</Value></Leq></Where><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                            query.RowLimit = 9;
                            SPListItemCollection oItems = oList.GetItems(query);
                            string listUrl = oWeb.Url + "/" + ListName + "/";
                            DataTable dtNewsList = ConvertSPListToDataTable(oItems, listUrl);
                            StringBuilder htmlStr = new StringBuilder("");
                            htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                            if (dtNewsList != null)
                            {
                                if (dtNewsList.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtNewsList.Rows.Count; i++)
                                    {
                                        string pageUrl = oWeb.Url + "/SitePages/News-Content.aspx?NewsID=" + dtNewsList.Rows[i]["NewsID"].ToString();
                                        string thumbnailUrl = dtNewsList.Rows[i]["ThumbnailUrl"].ToString();
                                        int index = thumbnailUrl.IndexOf(",");
                                        if (index > 0)
                                        {
                                            thumbnailUrl = thumbnailUrl.Substring(0, index);
                                            thumbnailUrl = thumbnailUrl.Replace(" ", "%20");
                                        

                                        }
                                        htmlStr.Append("<li>");
                                        htmlStr.Append("<a class=" + "\"" + "font_resize" + "\"" + " href=" + pageUrl + ">");
                                        htmlStr.Append(" <span class=" + "\"" + "news-thumb-img-hold"+"\""+"><img src=" + thumbnailUrl + "  " + "alt=" + "" + "></span><span>");
                                        htmlStr.Append(dtNewsList.Rows[i]["Title"]);
                                        htmlStr.Append("</span></a>");
                                        htmlStr.Append("</li>");
                                        if ((i + 1) == dtNewsList.Rows.Count)
                                        {
                                        }
                                        else if (++j == batchSize)
                                        {
                                            htmlStr.Append("</ul>");
                                            htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                                            j = 0;
                                        }
                                        pageUrl = "";
                                        thumbnailUrl = "";
                                        index = 0;
                                    }
                                    switch (dtNewsList.Rows.Count)
                                    {
                                        case 1:
                                            htmlStr.Append("<li></li><li></li></ul>");
                                            htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                                            htmlStr.Append("<li></li><li></li><li></li>");
                                            break;
                                        case 2:
                                            htmlStr.Append("<li></li></ul>");
                                            htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                                            htmlStr.Append("<li></li><li></li><li></li>");
                                            break;
                                        case 3:
                                            htmlStr.Append("</ul>");
                                            htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                                            htmlStr.Append("<li></li><li></li><li></li>");
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                htmlStr.Append("<li></li><li></li><li></li></ul>");
                                htmlStr.Append("<ul class=" + "\"" + "item font_resize" + "\"" + ">");
                                htmlStr.Append("<li></li><li></li><li></li>");
                            }

                            htmlStr.Append("</ul>");
                            divNews.InnerHtml = htmlStr.ToString();
                        }
                    }
                });
            }
            catch (Exception ex)
            { }

        }

        // Function to convert SharePoint Picture Library to DataTable 
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection, string listUrl)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                dtSPList.Columns.Add(dcImageUrl);
                foreach (DataRow dr in dtSPList.Rows)
                {
                    dr["ImageUrl"] = listUrl + dr["LinkFileName"];
                }

                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }

    }
}
