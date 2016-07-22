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
                       
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPDocumentLibrary documentLibrary = (SPDocumentLibrary)oWeb.Lists[ListName];
                            SPFileCollection files = documentLibrary.RootFolder.Files;
                            Stream StreamImage = null;
                            if (fuPublication.HasFile)
                            {
                                StreamImage = fuPublication.PostedFile.InputStream;
                            }
                            SPFile oPic = files.Add(documentLibrary.RootFolder.Url + "/" + fuPublication.FileName, StreamImage, true);
                            SPList documentLibraryAsList = oWeb.Lists[ListName];
                            SPListItem itemJustAdded = documentLibraryAsList.GetItemById(oPic.ListItemAllFields.ID);
                            itemJustAdded["Title"] = txtTitle.Text;
                            itemJustAdded["TitleAr"] = txtTitleAr.Text;
                            itemJustAdded.Update();
                            if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                            {

                                oPic.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
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
