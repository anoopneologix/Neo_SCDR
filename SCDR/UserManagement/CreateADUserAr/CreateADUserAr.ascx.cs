using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace SCDR.UserManagement.CreateAdUserAr
{
    [ToolboxItemAttribute(false)]
    public partial class CreateAdUserAr : WebPart
    {
        public CreateAdUserAr()
        {
        }

        /// <summary>
        /// function for enabling custom webpart properties in sharepoint
        /// </summary>
        #region CustomWebPartProperty
        private const string DefaultListName = "ADUserDetailsList";
        private static string listName = DefaultListName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultListName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultGroupName = "Users";
        private static string groupName = DefaultGroupName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultGroupName),
        WebDisplayName("Gropu Name:"),
        WebDescription("Please Enter a valid Group Name")]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + txtUsername.Text.Trim() + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                lblUserError.ForeColor = System.Drawing.Color.Red;
                                lblUserError.Text = "موجود بالفعل";
                                txtUsername.Text = "";
                                txtUsername.Focus();
                            }
                            else
                            {
                                lblUserError.ForeColor = System.Drawing.Color.Green;
                                lblUserError.Text = "اسم المستخدم متاح";
                                txtPassword.Focus();

                            }
                        }
                    }
                });
            }
            catch
            {

            }

        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='EmailId' /><Value Type='Text'>" + txtEmail.Text.Trim() + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                lblEmailError.ForeColor = System.Drawing.Color.Red;
                                lblEmailError.Text = "موجود بالفعل";
                                txtEmail.Text = "";
                                txtEmail.Focus();
                            }
                            else
                            {
                                lblEmailError.ForeColor = System.Drawing.Color.Green;
                                lblEmailError.Text = "عنوان البريد الإلكتروني متاح";
                                txtUsername.Focus();

                            }
                        }
                    }
                });
            }
            catch
            {

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    //string getNotification = string.Empty;
                    //if (chkGetNotification.Checked)
                    //{
                    //    getNotification = "True";
                    //}
                    //else
                    //{
                    //    getNotification = "False";
                    //}
                    using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = txtUsername.Text;
                            item["Password"] = txtPassword.Text;
                            item["FirstName"] = txtFirstName.Text;
                            item["LastName"] = txtLastName.Text;
                            item["EmailId"] = txtEmail.Text;
                            item["PhoneNumber"] = txtPhoneNumber.Text;
                            item["GroupName"] = GroupName;
                            item["Status"] = "Pending";
                            item["GetNotification"] = false;
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;

                            //Guid workflowId = GetRelatedWorkFlowId(Web);

                            foreach (SPWorkflowAssociation workflow in list.WorkflowAssociations)
                            {
                                if (workflow.Name == "NewExtUser")
                                {
                                        oWeb.AllowUnsafeUpdates = true;
                                        //SPSecurity.RunWithElevatedPrivileges(delegate()
                                        //{
                                            oSite.WorkflowManager.StartWorkflow(item, workflow, workflow.AssociationData);
                                        //});
                                    
                                }
                            }
                            Page.Response.Redirect("/sites/SCDR/ar/SitePages/RegSuccess.aspx");
                            //string sMessage = "تم التسجيل بنجاح";
                            ////   ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('" + sMessage + "');", false);
                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>var ourLocation = document.URL;alert('" + sMessage + "');window.location.href=ourLocation;</script>", false);
                        }
                       
                    }
                });
            }
            catch
            {
                /**/
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            
            Page.Response.Redirect("/sites/SCDR/ar/SitePages/Home.aspx");
        }
        void formClear()
        {
            txtCpassword.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtPhoneNumber.Text = "";
            txtUsername.Text = "";
            //chkGetNotification.Checked = false;
        }
    }
}


