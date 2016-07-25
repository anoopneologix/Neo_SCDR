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

namespace SCDR.AdminForms.EditDepartment
{
    [ToolboxItemAttribute(false)]
    public partial class EditDepartment : WebPart
    {
        
        public EditDepartment()
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
        private const string DepartmentListName = "CustomDepartmentList";
        private static string dListName = DepartmentListName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DepartmentListName),
        WebDisplayName("Department List Name:"),
        WebDescription("Please Enter a valid Department List Name")]
        public string DlistName
        {
            get { return dListName; }
            set { dListName = value; }
        }
        #endregion

        /// <summary>
        /// function for getting details of department from sharepoint list based on Item ID and Site Langauge
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="siteName"></param>
        public void GetDepartmentDetails(int itemID)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                  {
                      using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                      {

                          using (SPWeb oWeb = oSite.OpenWeb())
                          {
                              SPList oList = oWeb.Lists[DlistName];
                              SPListItem item = oList.GetItemById(itemID);
                              txtDepartment.Text = item["Title"].ToString();
                              txtDepartmentAr.Text = item["TitleAr"].ToString();
                              SPAttachmentCollection objAttchments = item.Attachments;
                              if (objAttchments.Count > 0)
                              {
                                  foreach (string fileName in item.Attachments)
                                  {
                                      imgDepartment.ImageUrl = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);

                                  }

                              }
                              else
                              {
                                  imgDepartment.ImageUrl = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
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
        /// fires when the page loads
        /// binds deatils to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (Page.Request.QueryString["ItemID"] != null )
                {

                    int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
           

                    GetDepartmentDetails(itemID);
                }
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
                if (txtDepartment.Text != "" && txtDepartmentAr.Text != "")
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
                                SPList oList = oWeb.Lists[DlistName];
                                SPListItem item = oList.GetItemById(itemID);
                                item["Title"] = txtDepartment.Text;
                                item["TitleAr"] = txtDepartmentAr.Text;
                                item["Status"] = status;
                                if (chkYes.Checked)
                                {
                                    if (fuDepartmentIcon.HasFile)
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
                                        foreach (HttpPostedFile postedFile in fuDepartmentIcon.PostedFiles)
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
                                }
                                oWeb.AllowUnsafeUpdates = true;
                                item.Update();
                                oWeb.AllowUnsafeUpdates = false;

                            }
                            string sMessage = "Department updated successfully";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
                        }
                    });
                }
                else
                {
                    string sMessage = "Insufficient data. Please try again.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// fires on cancel button click 
        /// redirects to ManageDepartment.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
        }

        protected void txtDepartmentAr_TextChanged(object sender, EventArgs e)
        {
            if (txtDepartmentAr.Text != "")
            {
                try
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb oWeb = oSite.OpenWeb())
                            {
                                SPList oList = oWeb.Lists[DlistName];
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><Eq><FieldRef Name='TitleAr' /><Value Type='Text'>" + txtDepartmentAr.Text.Trim() + "</Value></Eq></Where>";
                                SPListItemCollection oItems = oList.GetItems(query);

                                if (oItems.Count > 0)
                                {
                                    lblDepartmentNameAr.ForeColor = System.Drawing.Color.Red;
                                    lblDepartmentNameAr.Text = "Already Exists.";
                                    txtDepartmentAr.Text = "";
                                    txtDepartmentAr.Focus();
                                }
                                else
                                {
                                    lblDepartmentNameAr.ForeColor = System.Drawing.Color.Green;
                                    lblDepartmentNameAr.Text = "Department Name Available.";
                                    txtDepartment.Focus();

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

        protected void txtDepartment_TextChanged(object sender, EventArgs e)
        {
            if (txtDepartment.Text != "")
            {
                try
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb oWeb = oSite.OpenWeb())
                            {
                                SPList oList = oWeb.Lists[DlistName];
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + txtDepartment.Text.Trim() + "</Value></Eq></Where>";
                                SPListItemCollection oItems = oList.GetItems(query);

                                if (oItems.Count > 0)
                                {
                                    lblDepartmentNameEn.ForeColor = System.Drawing.Color.Red;
                                    lblDepartmentNameEn.Text = "Already Exists.";
                                    txtDepartment.Text = "";
                                    txtDepartment.Focus();
                                }
                                else
                                {
                                    lblDepartmentNameEn.ForeColor = System.Drawing.Color.Green;
                                    lblDepartmentNameEn.Text = "Department Name Available.";


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
}
