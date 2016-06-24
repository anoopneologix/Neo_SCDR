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

namespace SCDR.Image_Gallery.ImageGalleryUpload
{
    [ToolboxItemAttribute(false)]
    public partial class ImageGalleryUpload : WebPart
    {
        int SpListItemId = 0;
        string subsiteName = string.Empty;
        public ImageGalleryUpload()
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
                BindCategory();
            }
        }

        public void BindCategory()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtImageGallery = ConvertSPListToDataTable(oItems);
                            ddlCategoryName.DataSource = dtImageGallery;
                            ddlCategoryName.DataValueField = "ID"; // List field holding value 
                            ddlCategoryName.DataTextField = "CategoryName"; // List field holding name to be displayed on page 
                            ddlCategoryName.DataBind();
                            ddlCategoryName.Items.Insert(0, new ListItem("--Select Category--", "0"));
                        }
                    }
                });
            }
            catch
            {

            }
        }

        // Function to convert SharePoint Picture Library to DataTable 
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection)
        {
            DataTable dtSPList = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                return (dtSPList);
            }
            catch
            {
                return (dtSPList);
            }
        }
    }
}
