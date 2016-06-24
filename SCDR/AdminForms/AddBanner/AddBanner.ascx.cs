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

namespace SCDR.AdminForms.AddBanner
{
    [ToolboxItemAttribute(false)]
    public partial class AddBanner : WebPart
    {
        public AddBanner()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "BannerImage_Library";
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
                
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string subsiteName = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                  {

                      using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                      {
                          if (rbArabic.Checked)
                          {
                              subsiteName = "ar/";
                          }
                          else if (rbEnglish.Checked)
                          {
                              subsiteName = "en/";
                          }

                          using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                          {
                              oWeb.AllowUnsafeUpdates = true;
                              SPDocumentLibrary documentLibrary = (SPDocumentLibrary)oWeb.Lists[ListName];
                              SPFileCollection files = documentLibrary.RootFolder.Files;
                              Stream StreamImage = null;
                              if (fuBannerImage.HasFile)
                              {
                                  StreamImage = fuBannerImage.PostedFile.InputStream;
                              }
                              SPFile oPic = files.Add(documentLibrary.RootFolder.Url + "/" + fuBannerImage.FileName, StreamImage,true) ;
                              SPList documentLibraryAsList = oWeb.Lists[ListName];
                              SPListItem itemJustAdded = documentLibraryAsList.GetItemById(oPic.ListItemAllFields.ID);
                              itemJustAdded["Description"] = txtBannerDescription.Text;
                              itemJustAdded["Person"] = txtBannerPerson.Text;
                             itemJustAdded.Update();
                              if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                              {
                                
                                  oPic.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                              }
                              
                              oWeb.AllowUnsafeUpdates = false;

                         
                          }
                          string sMessage = "successfully completed";
                          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddBanner.aspx';</script>", false);
                      }
                      formClear();
                  });

            }
            catch { }
        }

        public void formClear()
        {
            txtBannerDescription.Text = "";
            txtBannerPerson.Text = "";
        }

    }
}
