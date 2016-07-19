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
                          //  bool returnvalue = CreateUserAccount(userName, userPassword);
                            bool  returnvalue = CreateUserAccount(userName, userPassword);
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
   
        public bool CreateUserAccount(string userName,string userPassword)
        {
            try
            {
                string ldapPath = "extscdr.gov.ae";
                string oGUID = string.Empty;
                string connectionPrefix = "LDAP://" + ldapPath;
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, @"extscdr1\scdradmin", @"P@ss123", AuthenticationTypes.Secure);
                DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "user");
                newUser.Properties["samAccountName"].Value = userName;
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
       
        #region Alternate Method
        // /// <summary>
       // /// Creates a new user on Active Directory
       // /// </summary>
       // /// <param name="sOU">The OU location you want to save your user</param>
       // /// <param name="sUserName">The username of the new user</param>
       // /// <param name="sPassword">The password of the new user</param>
       // /// <param name="sGivenName">The given name of the new user</param>
       // /// <param name="sSurname">The surname of the new user</param>
       // /// <returns>returns the UserPrincipal object</returns>
       //public UserPrincipal CreateNewUser(string sUserName, string sPassword, string sGivenName, string sSurname)
       // {
       //     if (!IsUserExisiting(sUserName))
       //     {
       //         PrincipalContext oPrincipalContext = GetPrincipalContext();

       //         UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext, sUserName, sPassword, true /*Enabled or not*/);

       //         //User Log on Name
       //         oUserPrincipal.UserPrincipalName = sUserName;
       //         oUserPrincipal.GivenName = sGivenName;
       //         oUserPrincipal.Surname = sSurname;
       //         oUserPrincipal.Save();

       //         return oUserPrincipal;
       //     }
       //     else
       //     {
       //         return GetUser(sUserName);
       //     }
       // }

       // /// <summary>
       // /// Gets the base principal context
       // /// </summary>
       // /// <returns>Retruns the PrincipalContext object</returns>
       // public PrincipalContext GetPrincipalContext()
       // {
       //     PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain,sServiceUser, sServicePassword);
       //     return oPrincipalContext;
       // }

       // /// <summary>
       // /// Checks if user exsists on AD
       // /// </summary>
       // /// <param name="sUserName">The username to check</param>
       // /// <returns>Returns true if username Exists</returns>
       // public bool IsUserExisiting(string sUserName)
       // {
       //     if (GetUser(sUserName) == null)
       //     {
       //         return false;
       //     }
       //     else
       //     {
       //         return true;
       //     }
       // }

       // /// <summary>
       // /// Gets a certain user on Active Directory
       // /// </summary>
       // /// <param name="sUserName">The username to get</param>
       // /// <returns>Returns the UserPrincipal Object</returns>
       // public UserPrincipal GetUser(string sUserName)
       // {
       //     PrincipalContext oPrincipalContext = GetPrincipalContext();

       //     UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
       //     return oUserPrincipal;
       // }

       // /// <summary>
       // /// Sets the user password
       // /// </summary>
       // /// <param name="sUserName">The username to set</param>
       // /// <param name="sNewPassword">The new password to use</param>
       // /// <param name="sMessage">Any output messages</param>
       // public void SetUserPassword(string sUserName, string sNewPassword, out string sMessage)
       // {
       //     try
       //     {
       //         UserPrincipal oUserPrincipal = GetUser(sUserName);
       //         oUserPrincipal.SetPassword(sNewPassword);
       //         sMessage = "";
       //     }
       //     catch (Exception ex)
       //     {
       //         sMessage = ex.Message;
       //     }

        // }
        #endregion

    }
}
