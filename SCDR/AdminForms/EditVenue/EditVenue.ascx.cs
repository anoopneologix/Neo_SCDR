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

        /// <summary>
        /// function for enabling  custom webpart property
        /// </summary>
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

        /// <summary>
        /// fires when the page loads
        /// binds deatils to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (Page.Request.QueryString["ItemID"] != null)
                {

                int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
               
              
              
                GetVenueDetails(itemID);
                }

            }
        }

        /// <summary>
        /// function for getting details of venue from sharepoint list based on Item ID and Site Langauge
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="siteName"></param>
        public void GetVenueDetails(int itemID)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {

                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[VenueListName];
                            SPListItem item = oList.GetItemById(itemID);
                            txtVenueEn.Text = item["Title"].ToString();
                            txtVenueAr.Text = item["TitleAr"].ToString();
                            if (item["Address"] != null)
                            {
                              
                                txtAddress.Text = Regex.Replace(item["Address"].ToString(), "<.*?>", string.Empty);
                            }
                            else
                            {
                                txtAddress.Text = string.Empty;
                            }
                            if (item["Description"] != null)
                            {
                                txtDescription.Text = Regex.Replace(item["Description"].ToString(), "<.*?>", string.Empty);
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

        /// <summary>
        /// fires when the button get clicked
        /// update the details to sharepoint list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVenueAr.Text != "" && txtVenueEn.Text != "")
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);

                            using (SPWeb oWeb = oSite.OpenWeb())
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
                                item["Title"] = txtVenueEn.Text;
                                item["TitleAr"] = txtVenueAr.Text;
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
                            string sMessage = "Venue updated successfully";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
                        }
                    });
                }
                else
                {
                    string sMessage = "Insufficent data. Please try again.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
                  
                }
            }
           
            catch
            {

            }

        }

        /// <summary>
        /// fires on cancel button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
        }
    }
}
