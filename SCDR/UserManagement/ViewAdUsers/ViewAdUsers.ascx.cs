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
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;


namespace SCDR.UserManagement.ViewAdUsers
{
    [ToolboxItemAttribute(false)]
    public partial class ViewAdUsers : WebPart
    {
      //  private string sDomain = "extscdr.gov.ae";
      ////  private string sDefaultOU = "OU=Test Users,OU=Test,DC=test,DC=com";
      //  private string sDefaultRootOU = "DC=extscdr,DC=gov,DC=ae";
      //  private string sServiceUser = @"EXTSCDR\Administrator";
      //  private string sServicePassword = "P@ssw0rd";
        public ViewAdUsers()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        /// <summary>
        /// function for enabiling custom webpart properties
        /// </summary>
        #region CustomWebPartProperty
        private const string DefaultListName = "ADUserDetailsList";
        private static string listName = DefaultListName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultListName),
        WebDisplayName("AD users List Name:"),
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

                BindADUserDetails();

            }
        }

        /// <summary>
        /// function for binding the venue to gridview based on langauage
        /// </summary>
        public void BindADUserDetails()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                      
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            DataTable dtVenue = ConvertSPListToDataTable(oItems);
                            gdvAdUsers.DataSource = dtVenue;
                            gdvAdUsers.DataBind();
                            /**/
                        }
                    }
                });
            }
            catch
            {

            }

        }

        /// <summary>
        ///  Function to convert SharePoint List to DataTable 
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

        protected void gdvAdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        SPList oList = oWeb.Lists[ListName];
                        int listItemId = Convert.ToInt32(e.CommandArgument);
                        if (e.CommandName == "rejectme")
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPListItem itemToUpdate = oList.GetItemById(listItemId);
                            itemToUpdate["Status"] = "Rejected";
                            itemToUpdate.Update();
                            oWeb.AllowUnsafeUpdates = false;
                           
                        }
                        else if (e.CommandName == "approveme")
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPListItem itemToUpdate = oList.GetItemById(listItemId);
                            string userName = itemToUpdate["Title"].ToString();
                            string userPassword = itemToUpdate["Password"].ToString();
                            string userFirstName = itemToUpdate["FirstName"].ToString();
                            string userLastName = itemToUpdate["LastName"].ToString();
                            string userPhoneNumber = itemToUpdate["PhoneNumber"].ToString();
                            string userEmailID = itemToUpdate["EmailId"].ToString();
                            string userGroupName = itemToUpdate["GroupName"].ToString();
                            bool  returnvalue = CreateUserAccount(userName, userPassword,userFirstName,userLastName,userEmailID,userPhoneNumber);
                            if (returnvalue ==true)
                            {
                                itemToUpdate["Status"] = "Approved";
                                itemToUpdate.Update();
                            }
                            oWeb.AllowUnsafeUpdates = false;
                        }

                    }
                }
            });
        }

        public bool CreateUserAccount(string userName, string userPassword, string userFirstName, string userLastName, string userEmailID, string userPhoneNumber)
        {
            try
            {
                string ldapPath = "extscdr.gov.ae/CN=Users,DC=EXTSCDR,DC=GOV,DC=AE";
                string oGUID = string.Empty;
                string connectionPrefix = "LDAP://" + ldapPath;
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, @"extscdr1\scdradmin", @"P@ss123", AuthenticationTypes.Secure);
                DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "user");
                newUser.Properties["samAccountName"].Value = userName;
                newUser.CommitChanges();
                
                newUser.Properties["givenName"].Value = userFirstName;
                newUser.CommitChanges();
                newUser.Properties["sn"].Value = userLastName;
                newUser.CommitChanges();
                newUser.Properties["displayName"].Value =userFirstName+ " " + userLastName;
                newUser.CommitChanges();
                newUser.Properties["mail"].Value = userEmailID;
                newUser.CommitChanges();
                newUser.Properties["telephoneNumber"].Value = userPhoneNumber;
                newUser.CommitChanges();
                newUser.Properties["userPrincipalName"].Value = userName + "@extscdr.gov.ae";
                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();
                newUser.Invoke("SetPassword", new object[] { userPassword });
                newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();
                return true;
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingwith --> E.Message.ToString();
                return false;

            }

        }
       
        

    }
}
