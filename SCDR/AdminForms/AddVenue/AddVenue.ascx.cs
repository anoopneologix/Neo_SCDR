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

namespace SCDR.AdminForms.AddVenue
{
    [ToolboxItemAttribute(false)]
    public partial class AddVenue : WebPart
    {
        string subsiteName = string.Empty;
        public AddVenue()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomVenueList";
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
                        byte[] fileContents = new byte[16 * 1024];
                        string fileName = string.Empty;
                        string status=string.Empty;
                        if(rbActive.Checked)
                        {
                            status="Active";
                        }
                        else if(rbInactive.Checked)
                        {
                            status="Inactive";
                        }
                        if (fuVenueIcon.HasFile)
                        {

                            foreach (HttpPostedFile postedFile in fuVenueIcon.PostedFiles)
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
                            item["Title"] = txtVenueAr.Text;
                            item["Address"] = txtAddress.Text;
                            item["Description"] = txtDescription.Text;
                            item["Latitude"] = txtLatitude.Text;
                            item["Longitude"] = txtLongitude.Text;
                            item["Status"] = status;
                            SPAttachmentCollection attach = item.Attachments;
                            attach.Add(fileName, fileContents);
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        using (SPWeb oWeb = oSite.OpenWeb("en/"))
                        {
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = txtVenueEn.Text;
                            item["Address"] = txtAddress.Text;
                            item["Description"] = txtDescription.Text;
                            item["Latitude"] = txtLatitude.Text;
                            item["Longitude"] = txtLongitude.Text;
                            item["Status"] = status;
                            SPAttachmentCollection attach = item.Attachments;
                            attach.Add(fileName, fileContents);
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                        }
                        string sMessage = "Venue added successfully";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
                        formClear();
                    }
                });



            }
            catch (Exception ex)
            {

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
            string sMessage = "operation cancelled";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ManageVenue.aspx';</script>", false);
        }

        void formClear()
        {
            txtVenueEn.Text = "";
            txtVenueAr.Text = "";
            txtAddress.Text = "";
            txtDescription.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            rbActive.Checked = true;
        }
    }
}
