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

namespace SCDR.AdminForms.AddVideoGallery
{
    [ToolboxItemAttribute(false)]
    public partial class AddVideoGallery : WebPart
    {
        string subsiteName = string.Empty;
        public AddVideoGallery()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomVideoGallery";
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
                            item["Title"] = txtVideoUrl.Text.Substring(0, 8);
                            item["VideoUrl"] = txtVideoUrl.Text;
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                           
                        }
                    }
                });

                formClear();
                string sMessage = "successfully completed";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddVideoGallery.aspx';</script>", false);


            }
            catch (Exception ex)
            {

            }

        }
        //function for clearing the controls in form
        public void formClear()
        {
            txtVideoUrl.Text = "";
           

        }
    }
}
