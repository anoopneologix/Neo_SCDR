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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
                GetDepartmentDetails(itemID);
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
                            int itemID = Convert.ToInt32(Page.Request.QueryString["ItemID"]);
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
                            item["Status"] = status;
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
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                           
                        }
                        string sMessage = "successfully completed";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
        }
    }
}
