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
using System.Collections;



namespace SCDR.AdminForms.AddPublications
{
    [ToolboxItemAttribute(false)]
    public partial class AddPublications : WebPart
    {
        public AddPublications()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region CustomWebPartProperty
        private const string DefaultLibraryName = "PublicationLibrary";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Publication Library Name:"),
        WebDescription("Please Enter a valid Publication Library Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string ThumbnailLibraryName = "PublicationThumbnailLibrary";
        private static string thubnailListName = ThumbnailLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(ThumbnailLibraryName),
        WebDisplayName("Publication Thumbnail Library Name:"),
        WebDescription("Please Enter a valid Publication Thumbnail Library Name")]
        public string ThumbnailListName
        {
            get { return thubnailListName; }
            set { thubnailListName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                   
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        Stream StreamDocument = null;
                        Stream StreamImage = null;
                        if (fuPublication.HasFile)
                        {
                            StreamDocument = fuPublication.PostedFile.InputStream;
                        }
                        if (fuThumbnail.HasFile)
                        {
                            StreamImage = fuThumbnail.PostedFile.InputStream;
                        }
                       
                        using (SPWeb oWeb = oSite.OpenWeb("en/"))
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            //add to picture library
                            SPDocumentLibrary thumbnailLibrary = (SPDocumentLibrary)oWeb.Lists[ThumbnailListName];
                            SPFileCollection thumbnailfiles = thumbnailLibrary.RootFolder.Files;
                            SPFile oPic = thumbnailfiles.Add(thumbnailLibrary.RootFolder.Url + "/" + fuThumbnail.FileName, StreamImage, true);
                            SPList thumbnailLibraryAsList = oWeb.Lists[ThumbnailListName];
                            SPListItem thubnailJustAdded = thumbnailLibraryAsList.GetItemById(oPic.ListItemAllFields.ID);
                            string thumbnailListUrl = oWeb.Url + "/" + ThumbnailListName + "/";


                            if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                            {

                                oPic.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                            }

                            // add to document library
                            SPDocumentLibrary documentLibrary = (SPDocumentLibrary)oWeb.Lists[ListName];
                            SPFileCollection files = documentLibrary.RootFolder.Files;
                            SPFile oDoc = files.Add(documentLibrary.RootFolder.Url + "/" + fuPublication.FileName, StreamDocument, true);
                            SPList documentLibraryAsList = oWeb.Lists[ListName];
                            SPListItem itemJustAdded = documentLibraryAsList.GetItemById(oDoc.ListItemAllFields.ID);
                            itemJustAdded["Title"] = txtTitle.Text;
                            itemJustAdded["ThumbnailUrl"] = thumbnailListUrl + thubnailJustAdded["Name"];
                            itemJustAdded["ThumbnailId"] = oPic.ListItemAllFields.ID;
                            itemJustAdded.Update();
                            if (oDoc.CheckOutType != SPFile.SPCheckOutType.None)
                            {

                                oDoc.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                            }

                            oWeb.AllowUnsafeUpdates = false;


                        }
                        using (SPWeb oWeb = oSite.OpenWeb("ar/"))
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            //add to picture library
                            SPDocumentLibrary thumbnailLibrary = (SPDocumentLibrary)oWeb.Lists[ThumbnailListName];
                            SPFileCollection thumbnailfiles = thumbnailLibrary.RootFolder.Files;
                            SPFile oPic = thumbnailfiles.Add(thumbnailLibrary.RootFolder.Url + "/" + fuThumbnail.FileName, StreamImage, true);
                            SPList thumbnailLibraryAsList = oWeb.Lists[ThumbnailListName];
                            SPListItem thubnailJustAdded = thumbnailLibraryAsList.GetItemById(oPic.ListItemAllFields.ID);
                            string thumbnailListUrl = oWeb.Url + "/" + ThumbnailListName + "/";


                            if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                            {

                                oPic.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                            }

                            // add to document library

                            SPDocumentLibrary documentLibrary = (SPDocumentLibrary)oWeb.Lists[ListName];
                            SPFileCollection files = documentLibrary.RootFolder.Files;

                            SPFile oDoc = files.Add(documentLibrary.RootFolder.Url + "/" + fuPublication.FileName, StreamDocument, true);
                            SPList documentLibraryAsList = oWeb.Lists[ListName];
                            SPListItem itemJustAdded = documentLibraryAsList.GetItemById(oDoc.ListItemAllFields.ID);
                            itemJustAdded["Title"] = txtTitleAr.Text;
                            itemJustAdded["ThumbnailUrl"] = thumbnailListUrl + thubnailJustAdded["Name"];
                            itemJustAdded["ThumbnailId"] = oPic.ListItemAllFields.ID;
                            itemJustAdded.Update();
                            if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                            {

                                oDoc.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                            }

                            oWeb.AllowUnsafeUpdates = false;


                        }
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddPublication.aspx';</script>", false);
                    }
                    txtTitle.Text = "";
                    txtTitleAr.Text = "";
                });

            }
            catch { }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
