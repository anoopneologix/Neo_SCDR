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
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
namespace SCDR.AdminForms.EditNewsContent
{
    [ToolboxItemAttribute(false)]
    public partial class EditNewsContent : WebPart
    {
        public EditNewsContent()
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
        private const string DefaultLibraryName = "CustomNewsList";
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
                if (Page.Request.QueryString["NewsID"] != null && Page.Request.QueryString["SiteName"] != null)
                {

                    int itemID = Convert.ToInt32(Page.Request.QueryString["NewsID"]);
                    string siteName = Page.Request.QueryString["SiteName"].ToString();


                    GetNewsDetails(itemID, siteName);
                }

            }
        }

        public void GetNewsDetails(int itemID, string siteName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb(siteName))
                        {
                            string liDescription = string.Empty;
                            SPList oList = oWeb.Lists[ListName];
                            SPListItem item = oList.GetItemById(itemID);
                            txtNewsHeading.Text = item["Title"].ToString();
                            txtNewsLocation.Text = item["Location"].ToString();
                            DateTime dtNewsDate = Convert.ToDateTime(item["Date"]);
                            string newsDate = dtNewsDate.ToString("dd/MMM/yyyy");
                            txtNewsdate.Text = newsDate;
                            liDescription = item["Description"].ToString();
                            hfNewsDescription.Value = Regex.Replace(liDescription, "<.*?>", string.Empty);
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
                            else
                            {
                               
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
                       
                        int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                        string siteName = Page.Request.QueryString["SiteName"].ToString();
                        using (SPWeb oWeb = oSite.OpenWeb(siteName))
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItem item = oList.GetItemById(itemID);
                            item["Title"] = txtNewsHeading.Text;
                            item["Location"] = txtNewsLocation.Text;
                            item["Description"] = hfNewsDescription.Value;
                            item["Date"] = txtNewsdate.Text;
                           if(chkYes.Checked)
                           {
                               if (fuNewsImage.HasFile)
                               {

                                   foreach (HttpPostedFile postedFile in fuNewsImage.PostedFiles)
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
                               List<string> deletedFilenames = GetDeletedFileNames();
                               foreach (string fileName in deletedFilenames)
                               {

                                   item.Attachments.Delete(fileName);

                               }
                               oWeb.AllowUnsafeUpdates = true;
                               item.Update();
                               oWeb.AllowUnsafeUpdates = false;
                               
                               BindThumbnailImages();
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                           }
                           else if (chkNo.Checked)
                           {
                            /*   int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                               string siteName = Page.Request.QueryString["SiteName"].ToString();
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
                                       ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='EditImageGallery.aspx';</script>", false);

                                   }

                               }
                               else
                               {
                                   oWeb.AllowUnsafeUpdates = true;
                                   oitem.Delete();
                                   oWeb.AllowUnsafeUpdates = false;
                                   formClear();
                                   string sMessage = "successfully completed";
                                   ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='EditImageGallery.aspx';</script>", false);

                               }*/



                           }
                           
                          
                           oWeb.AllowUnsafeUpdates = true;
                           item.Update();
                           oWeb.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch
            {

            }
        }

        public List<string> GetDeletedFileNames()
        {
            List<string> fileNames = new List<string>();
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
            return fileNames;
        }

        //function for binding uploaded images to pop up modal
        public void BindThumbnailImages()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                    string siteName = Page.Request.QueryString["SiteName"].ToString();
                    using (SPWeb oWeb = oSite.OpenWeb(siteName))
                    {
                        SPList list = oWeb.Lists[ListName];
                        SPListItem item = list.GetItemById(itemID);
                        hfListItemId.Value = itemID.ToString();
                        hfSubsiteName.Value = siteName.ToString();
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
            
            string sMessage = "successfully completed";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);


        }
    }
}
