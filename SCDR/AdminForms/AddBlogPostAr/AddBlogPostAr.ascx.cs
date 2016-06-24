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

namespace SCDR.AdminForms.AddBlogPostAr
{
    [ToolboxItemAttribute(false)]
    public partial class AddBlogPostAr : WebPart
    {
        string subsiteName = string.Empty;
        public AddBlogPostAr()
        {
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "ar_Blog";
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
        private const string DefaultBlogImageLibraryName = "BlogImagesLibrary";
        private static string blogListName = DefaultBlogImageLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultBlogImageLibraryName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
        public string BlogListName
        {
            get { return blogListName; }
            set { blogListName = value; }
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

                        subsiteName = "ar/";
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            string mediaContent = string.Empty;
                            if (txtVideoUrl.Text != "")
                            {
                                mediaContent = txtVideoUrl.Text + "<br/>";
                            }
                            if (fuBlogImage.HasFile)
                            {
                                oWeb.AllowUnsafeUpdates = true;
                                Guid newGuid = Guid.NewGuid();
                                oWeb.AllowUnsafeUpdates = true;
                                SPDocumentLibrary documentLibrary = (SPDocumentLibrary)oWeb.Lists[BlogListName];
                                SPFileCollection files = documentLibrary.RootFolder.Files;
                                Stream StreamImage = null;

                                StreamImage = fuBlogImage.PostedFile.InputStream;
                                SPFile oPic = files.Add(documentLibrary.RootFolder.Url + "/" + newGuid + System.IO.Path.GetExtension(fuBlogImage.FileName), StreamImage, true);
                                SPList documentLibraryAsList = oWeb.Lists[BlogListName];
                                SPListItem itemJustAdded = documentLibraryAsList.GetItemById(oPic.ListItemAllFields.ID);
                                string imgUrl = oWeb.Url + "/" + BlogListName + "/" + itemJustAdded["Name"];
                                mediaContent = "<img src='" + imgUrl + "' alt=''><br/>";
                                if (oPic.CheckOutType != SPFile.SPCheckOutType.None)
                                {

                                    oPic.CheckIn("File uploaded Programmatically !", SPCheckinType.OverwriteCheckIn);
                                }

                                oWeb.AllowUnsafeUpdates = false;
                            }
                            //blog
                            SPList list = oWeb.Lists[ListName];
                            SPListItem lstItm = SPUtility.CreateNewDiscussion(list.Items, txtSubject.Text);

                            lstItm[SPBuiltInFieldId.Body] = mediaContent + hfBody.Value;

                            oWeb.AllowUnsafeUpdates = true;
                            lstItm.Update();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        formClear();
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='../../SitePages/blog.aspx';</script>", false);

                    }
                });
            }
            catch
            {

            }

        }
        public void formClear()
        {
            txtSubject.Text = "";
            txtVideoUrl.Text = "";
            rbVideo.Checked = false;
            rbImage.Checked = false;
            rbNone.Checked = true;
        }
    }
}
