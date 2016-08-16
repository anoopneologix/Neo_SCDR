using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI;

namespace SCDR.ViewPublicationsAr
{
    [ToolboxItemAttribute(false)]
    public partial class ViewPublicationsAr : WebPart
    {
        public ViewPublicationsAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        /// <summary>
        ///  Following code is for enabling custom webpart property
        /// </summary>
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

        /// <summary>
        /// fires on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPublications();
        }

        /// <summary>
        /// function for get all items from sharepoint Document library
        /// </summary>
        public void BindPublications()
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

        /// <summary>
        ///  Function to convert SharePoint Document Library to DataTable 
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <param name="listUrl"></param>
        /// <returns></returns>
        private static DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection, string listUrl)
        {
            //   GhostscriptWrapper.GeneratePageThumb(@"D:\SCDR-PROJECT FILES\SCDR-Local\SCDR\36.pdf", "output.jpg", 1, 100, 100);
            DataTable dtSPList = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                dtSPList = spItemCollection.GetDataTable();

                DataColumn dcTitle = new DataColumn("Title", typeof(string));
                dt.Columns.Add(dcTitle);
                DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                dt.Columns.Add(dcImageUrl);
                DataColumn dcPdfUrl = new DataColumn("PdfUrl", typeof(string));
                dt.Columns.Add(dcPdfUrl);
                foreach (DataRow dr in dtSPList.Rows)
                {

                    DataRow drNewRow = dt.NewRow();
                    string imgUrl = dr["ThumbnailUrl"].ToString();
                    int index = imgUrl.IndexOf(",");
                    if (index > 0)
                    {
                        imgUrl = imgUrl.Substring(0, index);
                        drNewRow["ImageUrl"] = imgUrl;
                    }
                    drNewRow["PdfUrl"] = listUrl + dr["LinkFileName"];
                    drNewRow["Title"] = dr["Title"];
                    dt.Rows.Add(drNewRow);
                    imgUrl = "";
                    index = 0;


                }
                return (dt);
            }
            catch
            {
                return (dt);
            }
        }
  
    }
}
