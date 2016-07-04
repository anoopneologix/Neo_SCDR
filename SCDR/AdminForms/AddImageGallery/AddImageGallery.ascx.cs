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

namespace SCDR.AdminForms.AddImageGallery
{
    [ToolboxItemAttribute(false)]
    public partial class AddImageGallery : WebPart
    {
        int SpListItemId = 0;
        string subsiteName = string.Empty;
         public AddImageGallery()
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

        /// <summary>
        /// fires on the page load
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
        /// fires when the submit button get clicked
        /// items were saved to custom sharepoint list
        /// images were attached to item as list item attachments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
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
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = txtTitle.Text;
                            item["CategoryName"] = txtGroupName.Text;
                            item["Rank"] = Convert.ToInt32(txtRanking.Text);
                            if (fuThumbnailImage.HasFile)
                            {

                                foreach (HttpPostedFile postedFile in fuThumbnailImage.PostedFiles)
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
                            SpListItemId = item.ID;
                            BindThumbnailImages();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        }
                    }
                });



            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// function for binding uploaded images to pop up modal
        /// </summary>
 
        public void BindThumbnailImages()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                    {
                        SPList list = oWeb.Lists[ListName];
                        SPListItem item = list.GetItemById(SpListItemId);
                        hfListItemId.Value = SpListItemId.ToString();
                        hfSubsiteName.Value = subsiteName.ToString();
                        SPAttachmentCollection docs = item.Attachments;
                        DataTable dt = new DataTable();
                        DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                        dt.Columns.Add(dcImageUrl);
                        if (docs.Count > 0)
                        {
                            foreach (string fileName in item.Attachments)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                dt.Rows.Add(dr);
                            }
                            repThumbnail.DataSource = dt;
                            repThumbnail.DataBind();

                        }
                    }
                }
            });
        }
        /// <summary>
        /// fires when the sumit button on modal popup get clicked
        /// sets the thumbnail image
        /// update the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveThumbnail_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb(hfSubsiteName.Value.ToString()))
                    {
                        SPList list = oWeb.Lists[ListName];
                        SPListItem item = list.GetItemById(Convert.ToInt32(hfListItemId.Value));
                        item["ThumbnailUrl"] = lblUrl.Value.ToString();
                        oWeb.AllowUnsafeUpdates = true;
                        item.Update();
                        oWeb.AllowUnsafeUpdates = false;
                    }

                }
            });
            formClear();
            string sMessage = "successfully completed";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddImageGallery.aspx';</script>", false);

        }

        /// <summary>
        /// function for clearing the controls in form
        /// </summary>

        public void formClear()
        {
            txtTitle.Text = "";
            txtGroupName.Text = "";
            txtRanking.Text = "";
            lblGrpError.Text = "";
            lblRankError.Text = "";
        }
        /// <summary>
        /// fires when the textbox "txtGroupName" loses its focus
        /// function for checking the newly added category exists or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbArabic.Checked)
                {
                    subsiteName = "ar/";
                }
                else if (rbEnglish.Checked)
                {
                    subsiteName = "en/";
                }
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='CategoryName' /><Value Type='Text'>" + txtGroupName.Text.Trim() + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if(oItems.Count>0)
                            {
                                lblGrpError.ForeColor = System.Drawing.Color.Red;
                                lblGrpError.Text = "Already Exists";
                                txtGroupName.Text = "";
                                txtGroupName.Focus();
                            }
                            else
                            {
                                lblGrpError.ForeColor = System.Drawing.Color.Green;
                                lblGrpError.Text = "Category Name Available";
                                txtRanking.Focus();
                              
                            }
                        }
                    }
                });
            }
            catch
            {

            }

        }

        /// <summary>
        /// fires when the textbox "txtRanking" loses its focus
        /// function for checking the newly added rank exists or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRanking_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbArabic.Checked)
                {
                    subsiteName = "ar/";
                }
                else if (rbEnglish.Checked)
                {
                    subsiteName = "en/";
                }
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Rank' /><Value Type='Number'>" +Convert.ToInt32(txtRanking.Text) + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                lblRankError.ForeColor = System.Drawing.Color.Red;
                                lblRankError.Text = "Already Exists";
                                txtRanking.Text = "";
                                txtRanking.Focus();
                            }
                            else
                            {
                                lblRankError.ForeColor = System.Drawing.Color.Green;
                                lblRankError.Text = "Available";
                                txtTitle.Focus();

                            }
                        }
                    }
                });
            }
            catch
            {

            }

        }
        /// <summary>
        /// for clearing the form while choosing language
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            formClear();
        }
        /// <summary>
        /// for clearing the form while choosing language
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            formClear();
        }

       
    }
}
