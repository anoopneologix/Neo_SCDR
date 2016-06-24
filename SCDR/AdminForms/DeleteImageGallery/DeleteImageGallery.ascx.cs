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

namespace SCDR.AdminForms.DeleteImageGallery
{
    [ToolboxItemAttribute(false)]
    public partial class DeleteImageGallery : WebPart
    {
        string subsiteName = string.Empty;
        public DeleteImageGallery()
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
                divButton.Visible = false;
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
                        divButton.Visible = true;
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


        protected void btnDelete_Click(object sender, EventArgs e)
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
                            oWeb.AllowUnsafeUpdates = true;
                            item.Delete();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        formClear();
                        string sMessage = "successfully deleted";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                    }
                });
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
        }

              //function for clearing the controls in form
        public void formClear()
        {
            ddlCategoryName.Items.Clear();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select Category--", "0"));

            ddlCategoryName.SelectedIndex = 0;
            rbArabic.Checked = false;
            rbEnglish.Checked = false;
            divButton.Visible = false;
        }

        protected void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnglish.Checked)
            {
              BindCategory();
            }
        }

        protected void rbArabic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArabic.Checked)
            {
                BindCategory();
            }
             
        }
    }
}
