using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI;

namespace SCDR.ViewPublications
{
    [ToolboxItemAttribute(false)]
    public partial class ViewPublications : WebPart
    {
         public ViewPublications()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                BindBannerImagesToSlider();
            }
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "PublicationLibrary";
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

        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindBannerImagesToSlider()
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
                            DataTable dtPublications = ConvertSPListToDataTable(oItems, listUrl);
                            repPublication.DataSource = dtPublications;
                            repPublication.DataBind();
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

                DataColumn dcImageUrl = new DataColumn("IframeUrl", typeof(string));
                dtSPList.Columns.Add(dcImageUrl);
                DataColumn dcPdfUrl = new DataColumn("PdfUrl", typeof(string));
                dtSPList.Columns.Add(dcPdfUrl);
                foreach (DataRow dr in dtSPList.Rows)
                {
                    dr["PdfUrl"] = listUrl + dr["LinkFileName"];
                    dr["IframeUrl"] =  listUrl + dr["LinkFileName"] + "&embedded=true";
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
