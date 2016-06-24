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

namespace SCDR.AdminForms.EditVenue
{
    [ToolboxItemAttribute(false)]
    public partial class EditVenue : WebPart
    {
        public EditVenue()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region CustomWebPartProperty
        private const string DefaultVenueList = "CustomVenueList";
        private static string venueListName = DefaultVenueList;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultVenueList),
        WebDisplayName("Venue List Name:"),
        WebDescription("Please Enter a valid Venue List Name")]
        public string VenueListName
        {
            get { return venueListName; }
            set { venueListName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (Page.Request.QueryString["ItemID"] != null && Page.Request.QueryString["SiteName"] != null)
                {

                int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                string siteName = Page.Request.QueryString["SiteName"].ToString();
              
              
                GetVenueDetails(itemID, siteName);
                }

            }
        }

        public void GetVenueDetails(int itemID, string siteName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb(siteName))
                        {
                            SPList oList = oWeb.Lists[VenueListName];
                            SPListItem item = oList.GetItemById(itemID);
                            txtVenue.Text = item["Title"].ToString();
                            if (item["Address"] != null)
                            {
                                txtAddress.Text = item["Address"].ToString();
                            }
                            else
                            {
                                txtAddress.Text = string.Empty;
                            }
                            if (item["Description"] != null)
                            {
                                txtDescription.Text = item["Description"].ToString();
                            }
                            else
                            {
                                txtDescription.Text = string.Empty;
                            }
                            if (item["Latitude"] != null)
                            {
                                txtLatitude.Text = item["Latitude"].ToString();
                            }
                            else
                            {
                                txtLatitude.Text = string.Empty;
                            }
                            if (item["Longitude"] != null)
                            {
                                txtLongitude.Text = item["Longitude"].ToString();
                            }
                            else
                            {
                                txtLongitude.Text = string.Empty;
                            }
                            

                            SPAttachmentCollection objAttchments = item.Attachments;
                            if (objAttchments.Count > 0)
                            {
                                foreach (string fileName in item.Attachments)
                                {
                                    imgVenue.ImageUrl = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);

                                }

                            }
                            else
                            {
                                imgVenue.ImageUrl = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
                            }
                            if (item["Status"].ToString() == "Active")
                            {
                                rbActive.Checked = true;
                            }
                            else
                            {
                                rbInactive.Checked = true;
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

                            string status = string.Empty;
                            if (rbActive.Checked)
                            {
                                status = "Active";
                            }
                            else
                            {
                                status = "Inactive";
                            }
                            SPList oList = oWeb.Lists[VenueListName];
                            SPListItem item = oList.GetItemById(itemID);
                            item["Title"] = txtVenue.Text;
                            item["Address"] = txtAddress.Text;
                            item["Description"] = txtDescription.Text;
                            item["Latitude"] = txtLatitude.Text;
                            item["Longitude"] = txtLongitude.Text;
                            item["Status"] = status;
                            if (fuVenueImage.HasFile)
                            {
                                SPAttachmentCollection ocollAttachments = item.Attachments;
                                if (ocollAttachments.Count > 0)
                                {
                                    List<string> fileNames = new List<string>();

                                    foreach (string fileName in item.Attachments)
                                    {
                                        fileNames.Add(fileName);
                                    }

                                    foreach (string fileName in fileNames)
                                    {
                                        item.Attachments.Delete(fileName);
                                    }
                                }
                                foreach (HttpPostedFile postedFile in fuVenueImage.PostedFiles)
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
                                        string sMessageError = "Image Already Exists!";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessageError + "');</script>", false);

                                    }

                                }
                            }
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;

                        }
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
                    }
                });
            }
            catch
            {

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
        }
    }
}
