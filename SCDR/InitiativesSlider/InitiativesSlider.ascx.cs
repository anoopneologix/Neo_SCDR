//--------------------------------------------------
// Project Name : SCDR
// Program Name : OurInitiativesSlider (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 22/03/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SCDR.InitiativesSlider
{
    [ToolboxItemAttribute(false)]
    public partial class InitiativesSlider : WebPart
    {
        public InitiativesSlider()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "OurInitiatives_Library";
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
        private const string DefaultHeading = "Our initiatives";
        private static string headingName = DefaultHeading;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultHeading),
        WebDisplayName("Heading to display:"),
        WebDescription("Example : Our initiatives")]

        public string HeadingName
        {
            get { return headingName; }
            set { headingName = value; }
        }
        private const string DefaultAnchorText = "read more";
        private static string anchorText = DefaultAnchorText;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultAnchorText),
        WebDisplayName("Anchor Text to display:"),
        WebDescription("Example : read more")]

        public string AnchorText
        {
            get { return anchorText; }
            set { anchorText = value; }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindInitiativeImagesToSlider();
                Control HeaderTemplate = repOurInitiativesSlider.Controls[0].Controls[0];
                Label lab = HeaderTemplate.FindControl("lblHeading") as Label;
                lab.Text = InitiativesSlider.headingName;


            }
        }

     
        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindInitiativeImagesToSlider()
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
                            SPListItemCollection oItems = oList.GetItems();
                            string listUrl = oWeb.Url + "/" + ListName + "/";
                            DataTable dtBannerImages = ConvertSPListToDataTable(oItems, listUrl);
                            repOurInitiativesSlider.DataSource = dtBannerImages;
                            repOurInitiativesSlider.DataBind();
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }
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
                    string PageUrl = dr["PageUrl"].ToString();
                    int index = PageUrl.IndexOf(",");
                    if (index > 0)
                    {
                        PageUrl = PageUrl.Substring(0, index);
                        dr["PageUrl"] = PageUrl;
                    }

                    dr["ImageUrl"] = listUrl + dr["LinkFileName"];
                    PageUrl = "";
                    index = 0;
                }
                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }

        protected void repOurInitiativesSlider_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor anchor = new HtmlAnchor();
                anchor = (HtmlAnchor)e.Item.FindControl("ancReadMore");
                anchor.InnerText = InitiativesSlider.anchorText;
            }
        }

    }
}
