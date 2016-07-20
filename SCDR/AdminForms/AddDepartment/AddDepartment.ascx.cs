﻿using System;
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

namespace SCDR.AdminForms.AddDepartment
{
    [ToolboxItemAttribute(false)]
    public partial class AddDepartment : WebPart
    {
        public AddDepartment()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        /// <summary>
        /// for adding custom webpart properties
        /// </summary>
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomDepartmentList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Picture Library Name:"),
        WebDescription("Please Enter a valid Department Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        #endregion

        /// <summary>
        /// fires on page load
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
        /// stores the newly added department to shatepoint custom list
        /// English department name and details stores to the list created in English website
        /// arabic department name and details stores to the list created in Arabic website
        /// on succesfull submit, page redirects to ManageDepartment.aspx
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
                       byte[] fileContents = new byte[16 * 1024];
                       string fileName = string.Empty;
                       string status = string.Empty;
                       if (rbActive.Checked)
                       {
                           status = "Active";
                       }
                       else if (rbInactive.Checked)
                       {
                           status = "Inactive";
                       }
                       if (fuDepartmentIcon.HasFile)
                       {

                           foreach (HttpPostedFile postedFile in fuDepartmentIcon.PostedFiles)
                           {

                               Stream fs = postedFile.InputStream;
                               fileContents = new byte[fs.Length];
                               fs.Read(fileContents, 0, (int)fs.Length);
                               fs.Close();
                               fileName = Path.GetFileName(postedFile.FileName);

                           }
                       }
                       using (SPWeb oWeb = oSite.OpenWeb("ar/"))
                       {
                           SPList list = oWeb.Lists[ListName];
                           SPListItem item = list.Items.Add();
                           item["Title"] = txtDepartmentAr.Text;
                           item["Status"] = status;
                           if (fileName!="")
                           {
                               SPAttachmentCollection attach = item.Attachments;
                               attach.Add(fileName, fileContents);
                           }
                           oWeb.AllowUnsafeUpdates = true;
                           item.Update();
                           oWeb.AllowUnsafeUpdates = false;
                       }
                       using (SPWeb oWeb = oSite.OpenWeb("en/"))
                       {
                           SPList list = oWeb.Lists[ListName];
                           SPListItem item = list.Items.Add();
                           item["Title"] = txtDepartment.Text;
                           item["Status"] = status;
                           if (fileName != "")
                           {
                               SPAttachmentCollection attach = item.Attachments;
                               attach.Add(fileName, fileContents);
                           }
                           oWeb.AllowUnsafeUpdates = true;
                           item.Update();
                           oWeb.AllowUnsafeUpdates = false;

                       }
                       formClear();
                       string sMessage = "Department added successfully";
                       ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
                   }
               });

            }
            catch
            { }

        }

        /// <summary>
        /// fires when the cancel button get clicked
        /// clear the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageDepartment.aspx';</script>", false);
                 
        }

        /// <summary>
        /// function for clearing the form after succesfull submit
        /// </summary>
        void formClear()
        {
            txtDepartment.Text = "";
            rbActive.Checked = true;
        }

        protected void txtDepartmentAr_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDepartment_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
