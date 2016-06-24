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
using System.Collections.Generic;
using System.Linq;

namespace SCDR.AdminForms.EditImageGallery
{
    [ToolboxItemAttribute(false)]
    public partial class EditImageGallery : WebPart
    {
        List<string> thumbnailUrl = new List<string>();
        List<string> fileNames = new List<string>();
        List<string> attachmenUrl = new List<string>();
        List<string> oattachmenUrl = new List<string>();
        int SpListItemId = 0;
        string subsiteName = string.Empty;
        public EditImageGallery()
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
                divDetails.Visible = false;
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
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtImageGallery = ConvertSPListToDataTable(oItems);
                            ddlCategoryName.DataSource = dtImageGallery;
                            ddlCategoryName.Items.Clear();
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

        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            if(rbArabic.Checked)
            {
            divDetails.Visible = false;
            BindCategory();
            }
        }

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
             if(rbEnglish.Checked)
             {
            
            divDetails.Visible = false;
            BindCategory();
             }
             
        }

        protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoryName.SelectedIndex > 0)
            {
                BindDetails();
                divDetails.Visible = true;
            }
        }

        public void BindDetails()
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
                            SPList oList = oWeb.Lists[ListName];
                            SPListItem item = oList.GetItemById(Convert.ToInt32(ddlCategoryName.SelectedValue));
                            txtTitle.Text = item["Title"].ToString();
                            txtRanking.Text = item["Rank"].ToString();
                            hfListItemId.Value = ddlCategoryName.SelectedValue;
                            string imgUrl = item["ThumbnailUrl"].ToString();
                            int index = imgUrl.IndexOf(",");
                            if (index > 0)
                            {
                                imgUrl = imgUrl.Substring(0, index);
                                thumbnailUrl.Add(imgUrl);
                            }
                            SPAttachmentCollection objAttchments = item.Attachments;
                            DataTable dt = new DataTable();
                            DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                            dt.Columns.Add(dcImageUrl);
                            if (objAttchments.Count > 0)
                            {
                                foreach (string fileName in item.Attachments)
                                {
                                  
                                        DataRow dr = dt.NewRow();
                                        dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                        dt.Rows.Add(dr);
                                    
                                }
                                gdvAttachments.DataSource = dt;
                                gdvAttachments.DataBind();

                            }
                        }
                    }
                });
            }
            catch
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
                            SPListItem item = list.GetItemById(Convert.ToInt32(ddlCategoryName.SelectedValue));
                            item["Title"] = txtTitle.Text;
                            item["Rank"] = txtRanking.Text;
                            string oimgUrl = item["ThumbnailUrl"].ToString();
                            int index = oimgUrl.IndexOf(",");
                            if (index > 0)
                            {
                                oimgUrl = oimgUrl.Substring(0, index);
                                thumbnailUrl.Add(oimgUrl);
                            }
                           
                            if (fuThumbnailImage.HasFile)
                            {

                                foreach (HttpPostedFile postedFile in fuThumbnailImage.PostedFiles)
                                {
                                    try
                                    {
                                        Stream fs = postedFile.InputStream;
                                        byte[] fileContents = new byte[fs.Length];
                                        fs.Read(fileContents, 0, (int)fs.Length);
                                        fs.Close();
                                        SPAttachmentCollection attach = item.Attachments;
                                        string fileName = Path.GetFileName(postedFile.FileName);
                                        attach.Add(fileName, fileContents);
                                    }
                                    catch
                                    {
                                        string sMessage = "Image Already Exists!";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                    }

                                }
                            }
                         
                            // Select the checkboxes from the GridView control
                            for (int i = 0; i < gdvAttachments.Rows.Count; i++)
                            {
                                GridViewRow row = gdvAttachments.Rows[i];
                                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                                Image myImage = row.FindControl("imgAttachments") as Image;
                                if (isChecked)
                                {
                                    // Column 2 is the name column
                                    string imgUrl = myImage.ImageUrl.ToString();
                                    fileNames.Add(imgUrl.Substring(imgUrl.LastIndexOf("/") + 1));
                                 
                                }
                            }
                            foreach (string fileName in fileNames)
                            {

                                    item.Attachments.Delete(fileName);
                               
                            }  
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                            if (rbYes.Checked)
                            {
                                SpListItemId = item.ID;
                                BindThumbnailImages();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                            else if(rbNo.Checked)
                            {
                                SpListItemId = item.ID;
                                SPList olist = oWeb.Lists[ListName];
                                SPListItem oitem = olist.GetItemById(SpListItemId);
                                SPAttachmentCollection ocollAttachments = oitem.Attachments;
                                if (ocollAttachments.Count > 0)
                                {
                                    foreach (string fileName in oitem.Attachments)
                                    {
                                        oattachmenUrl.Add(SPUrlUtility.CombineUrl(oitem.Attachments.UrlPrefix, fileName));
                                    }
                                    bool val = thumbnailUrl.Intersect(oattachmenUrl).Any();
                                    if (val == false)
                                    {
                                        UpdateThumbnailImages();
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                           
                                    }
                                    else
                                    {
                                        formClear();
                                        string sMessage = "successfully completed";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                    }

                                }
                                else
                                {
                                    oWeb.AllowUnsafeUpdates = true;
                                    oitem.Delete();
                                    oWeb.AllowUnsafeUpdates = false;
                                    formClear();
                                    string sMessage = "successfully completed";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                }

                              
                              
                            }

                        }
                    }
                });



            }
            catch (Exception ex)
            {

            }

        }

        //function for binding uploaded images to pop up modal
        public void UpdateThumbnailImages()
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
        //function for binding uploaded images to pop up modal
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

        }
  
      //function for clearing the controls in form
        public void formClear()
        {
            ddlCategoryName.Items.Clear();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select Category--", "0"));
                   
           ddlCategoryName.SelectedIndex = 0;
            rbArabic.Checked = false;
            rbEnglish.Checked = false;
            divDetails.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
        }

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
                            query.Query = @"<Where><Eq><FieldRef Name='Rank' /><Value Type='Number'>" + Convert.ToInt32(txtRanking.Text) + "</Value></Eq></Where>";
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

                            }
                        }
                    }
                });
            }
            catch
            {

            }

        }
    }
}
