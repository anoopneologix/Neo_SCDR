using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;

namespace SCDR.VideoGallery
{
    [ToolboxItemAttribute(false)]
    public partial class VideoGallery : WebPart
    {
        public VideoGallery()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomVideoGallery";
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
                BindVideos();


            }
        }

        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindVideos()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            DataTable dt = new DataTable();
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                DataColumn dcImageUrl = new DataColumn("VideoUrl", typeof(string));
                                dt.Columns.Add(dcImageUrl);
                                foreach (SPListItem item in oItems)
                                {
                                    DataRow dr = dt.NewRow();
                                    string imgUrl = item["VideoUrl"].ToString();
                                    int index = imgUrl.IndexOf(",");
                                    if (index > 0)
                                    {
                                        imgUrl = imgUrl.Substring(0, index);
                                        dr["VideoUrl"] = imgUrl;
                                    }
                                   
                              
                                    dt.Rows.Add(dr);
                                    imgUrl = "";
                                    index = 0;
                                }
                            }
                            topDiv.Attributes.Add("href", dt.Rows[0]["VideoUrl"].ToString() + "&feature=youtu.be");
                            topDivFrame.Attributes.Add("src", dt.Rows[0]["VideoUrl"].ToString() + "&feature=youtu.be");
                            dt.Rows[0].Delete();
                            dt.AcceptChanges();
                            repVideoGallery.DataSource = dt;
                            repVideoGallery.DataBind();


                        }



                    }
             



                });
            }
            catch (Exception ex)
            { }
        }
    }
}
