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

namespace SCDR.AdminForms.AddNewAtSCDR
{
    [ToolboxItemAttribute(false)]
    public partial class AddNewAtSCDR : WebPart
    {
        public AddNewAtSCDR()
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
        private const string DefaultLibraryName = "NewAtSCDR_List";
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
        /// <summary>
        /// fires when the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

            }
        }
        /// <summary>
        /// fires when the submit buton on english panel get clicked
        /// saves the details to custom sharepoint list in english subsite
        /// image saved ad item attachment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string subsiteName = "en/";
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                       
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = hfNewDescription.Value.ToString().Substring(0, 50);
                            item["EventDescription"] = hfNewDescription.Value.ToString();
                            if (fuEnNewImages.HasFile)
                            {

                                foreach (HttpPostedFile postedFile in fuEnNewImages.PostedFiles)
                                {

                                    Stream fs = postedFile.InputStream;
                                    byte[] fileContents = new byte[fs.Length];
                                    fs.Read(fileContents, 0, (int)fs.Length);
                                    fs.Close();
                                    SPAttachmentCollection attach = item.Attachments;
                                    string fileName = Path.GetFileName(postedFile.FileName);
                                    attach.Add(fileName, fileContents);

                                }
                            }
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddNewAtSCDR.aspx';</script>", false);
                    }
                    formClear();
                });

            }
            catch { }

        }
        /// <summary>
        /// fires when the submit buton on arabic panel get clicked
        /// saves the details to custom sharepoint list in arabic subsite
        /// image saved ad item attachment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArSubmit_Click(object sender, EventArgs e)
        {
            string subsiteName = "ar/";
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = hfNewDescription.Value.ToString().Substring(0, 50);
                            item["EventDescription"] = hfArNewDescription.Value.ToString();
                            if (fuArNewImages.HasFile)
                            {

                                foreach (HttpPostedFile postedFile in fuArNewImages.PostedFiles)
                                {

                                    Stream fs = postedFile.InputStream;
                                    byte[] fileContents = new byte[fs.Length];
                                    fs.Read(fileContents, 0, (int)fs.Length);
                                    fs.Close();
                                    SPAttachmentCollection attach = item.Attachments;
                                    string fileName = Path.GetFileName(postedFile.FileName);
                                    attach.Add(fileName, fileContents);

                                }
                            }
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddNewAtSCDR.aspx';</script>", false);
                    }
                    formClear();
                });

            }
            catch { }


        }
        /// <summary>
        /// function for clearing the form after succesfull commit
        /// </summary>
        void formClear()
        {
            txtArDescription.Text = "";
            txtDescription.Text = "";
        }
    }
}
