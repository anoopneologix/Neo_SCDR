using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;

namespace SCDR.SubscribeAr
{
    [ToolboxItemAttribute(false)]
    public partial class SubscribeAr : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SubscribeAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region CustomWebPartProperty
        private const string DefaultListName = "SubribeSite";
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            bool checkFlag = false;
            if (txtemail.Text.ToString() != string.Empty)
            {
                try
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb oWeb = oSite.OpenWeb())
                            {
                                SPList list = oWeb.Lists["SubcribeSite"];
                                foreach (SPListItem checkItem in list.Items)
                                {
                                    if (String.Compare(checkItem["Title"].ToString(), txtemail.Text.ToString(), true) == 0)
                                    {
                                        checkFlag = true;
                                        txtemail.ForeColor = Color.Red;
                                        txtemail.Text = "المشترك بالفعل";
                                        break;
                                    }
                                   
                                }

                                if(!checkFlag)
                                {
                                    SPListItem item = list.Items.Add();
                                    item["Title"] = txtemail.Text;
                                    oWeb.AllowUnsafeUpdates = true;
                                    item.Update();
                                    oWeb.AllowUnsafeUpdates = false;
                                    txtemail.ForeColor = Color.Green;
                                    txtemail.Text = "المشترك بنجاح";
                                    foreach (SPWorkflowAssociation workflow in list.WorkflowAssociations)
                                    {
                                        if (workflow.Name == "New Subscribe")
                                        {
                                            oWeb.AllowUnsafeUpdates = true;
                                            //SPSecurity.RunWithElevatedPrivileges(delegate()
                                            //{
                                            oSite.WorkflowManager.StartWorkflow(item, workflow, workflow.AssociationData);
                                            //});

                                        }
                                    }
                                }

                            }
                        }
                    });

                }
                catch (Exception ex)
                { }
            }
                
            else 
            {
                txtemail.ForeColor = Color.Red;
                txtemail.Text = "يرجى إدخال عنوان البريد الإلكتروني";
            }
        }
    }
}
