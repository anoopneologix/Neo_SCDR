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

namespace SCDR.AdminForms.DeleteCalendarEvents
{
    [ToolboxItemAttribute(false)]
    public partial class DeleteCalendarEvents : WebPart
    {
        string subsiteName = string.Empty;
        public DeleteCalendarEvents()
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
        private const string DefaultLibraryName = "CustomCalendarList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Calendar List Name:"),
        WebDescription("Please Enter a valid Calendar List Name")]
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

        protected void txtEventDate_TextChanged(object sender, EventArgs e)
        {
            if (txtEventDate.Text != "")
            {
                string txt = txtEventDate.Text;
                BindEvents(txt);
            }

        }
        public void BindEvents(string txt)
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
                            SPQuery query = new SPQuery();
                            DateTime sDate = DateTime.Parse(txt);
                            string dtsDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(sDate.Date);
                            query.Query = @"<Where><Eq><FieldRef Name='EventDate'/><Value IncludeTimeValue='FALSE' Type='DateTime'>" + dtsDate + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems != null)
                            {
                                if (oItems.Count > 0)
                                {
                                    ddlEventName.DataSource = oItems;
                                    ddlEventName.DataValueField = "ID"; // List field holding value 
                                    ddlEventName.DataTextField = "Title"; // List field holding name to be displayed on page 
                                    ddlEventName.DataBind();
                                    ddlEventName.Items.Insert(0, new ListItem("--Select Event--", "0"));
                                    divButton.Visible = true;
                                }
                                else
                                {
                                    formClear();
                                    string sMessage = "No events were found!";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                                }
                            }

                        }
                    }
                });
            }
            catch
            {

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
                            SPListItem item = list.GetItemById(Convert.ToInt32(ddlEventName.SelectedValue));
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
        public void formClear()
        {
            ddlEventName.Items.Clear();
            ddlEventName.Items.Insert(0, new ListItem("--Select Event--", "0"));
            ddlEventName.SelectedIndex = 0;
            txtEventDate.Text = "";
            rbArabic.Checked = false;
            rbEnglish.Checked = false;
            divButton.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
        }

    }
}
