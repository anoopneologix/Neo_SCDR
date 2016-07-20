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

namespace SCDR.AdminForms.ViewDepartment
{
    [ToolboxItemAttribute(false)]
    public partial class ViewDepartment : WebPart
    {
        public ViewDepartment()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
       
        /// <summary>
       /// for enabling custom properties for webpart
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
        /// fires when the page loads
        /// binds the department details to gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

                BindDepartment();

            }
        }
       
        /// <summary>
        /// function for binding department details to gridview
        /// department details will selected on the basis of language selected
        /// </summary>
        public void BindDepartment()
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
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtDepartment = ConvertSPListToDataTable(oItems);
                            gdvDepartment.DataSource = dtDepartment;
                            gdvDepartment.DataBind();
                        }
                    }
                });
            }
            catch
            {

            }

        }

        /// <summary>
        /// Function to convert SharePoint List to DataTable 
        /// </summary>
        /// <param name="spItemCollection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// fires when the rows on gridview get clicked
        /// for edit and delete venue
        /// if edit, then the user will redirect to a new page EditDepartment.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvDepartment_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                 

                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        SPList oList = oWeb.Lists[DlistName];
                        int listItemId = Convert.ToInt32(e.CommandArgument);
                        if (e.CommandName == "delme")
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPListItem itemToDelete = oList.GetItemById(listItemId);
                            itemToDelete.Delete();
                            oWeb.AllowUnsafeUpdates = false;
                            BindDepartment();
                        }
                        else if (e.CommandName == "editme")
                        {
                            Page.Response.Redirect("EditDepartment.aspx?ItemID=" + listItemId);
                        }

                    }
                }
            });
        }

        /// <summary>
        /// fires when the linkbutton 'LinkButton1' gets clicked
        /// page redirect to AddDepartment.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AddDepartment.aspx");
        }

     
    }
}
