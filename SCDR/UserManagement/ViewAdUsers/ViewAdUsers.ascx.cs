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
using Microsoft.SharePoint.Workflow;
using System.Web.Security;



namespace SCDR.UserManagement.ViewAdUsers
{
    [ToolboxItemAttribute(false)]
    public partial class ViewAdUsers : WebPart
    {
     
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

                            foreach (SPWorkflowAssociation workflow in oList.WorkflowAssociations)
                            {
                                if (workflow.Name == "AppRejExtUser")
                                {
                                    oWeb.AllowUnsafeUpdates = true;
                                    //SPSecurity.RunWithElevatedPrivileges(delegate()
                                    //{
                                    oSite.WorkflowManager.StartWorkflow(itemToUpdate, workflow, workflow.AssociationData);
                                    //});

                                }
                            }
                           
                        }
                        else if (e.CommandName == "approveme")
                        {
                           
                            SPListItem itemToUpdate = oList.GetItemById(listItemId);
                            string itemStatus = itemToUpdate["Status"].ToString();
                            if (itemStatus != "Approved")
                            {
                                oWeb.AllowUnsafeUpdates = true;
                                string userName = itemToUpdate["Title"].ToString();
                                string userPassword = itemToUpdate["Password"].ToString();
                                string userFirstName = itemToUpdate["FirstName"].ToString();
                                string userLastName = itemToUpdate["LastName"].ToString();
                                string userPhoneNumber = itemToUpdate["PhoneNumber"].ToString();
                                string userEmailID = itemToUpdate["EmailId"].ToString();
                                string userGroupName = itemToUpdate["GroupName"].ToString();
                                bool returnvalue = CreateUserAccount(userName, userPassword, userFirstName, userLastName, userEmailID, userPhoneNumber);
                                if (returnvalue == true)
                                {
                                    itemToUpdate["Status"] = "Approved";
                                    itemToUpdate.Update();
                                    AddUserToAGroup(userName, "SCDR Wearhouse Users", userFirstName, userLastName, userEmailID);
                                }
                                oWeb.AllowUnsafeUpdates = false;
                                string sMessage = "successfully completed";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='ViewADUsers.aspx';</script>", false);
                                BindADUserDetails();
                                foreach (SPWorkflowAssociation workflow in oList.WorkflowAssociations)
                                {
                                    if (workflow.Name == "AppRejExtUser")
                                    {
                                        oWeb.AllowUnsafeUpdates = true;
                                        //SPSecurity.RunWithElevatedPrivileges(delegate()
                                        //{
                                        oSite.WorkflowManager.StartWorkflow(itemToUpdate, workflow, workflow.AssociationData);
                                        //});

                                    }
                                }
                            }
                            else
                            {
                                string sMessage = "Already approved";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                     
                            }
                        }

                    }
                }
            });
        }

        /// <summary>
        /// Add a user to a Sharepoint group
        /// </summary>
        /// <param name="userLoginName">Login name of the user to add</param>
        /// <param name="userGroupName">Group name to add</param>
        private void AddUserToAGroup(string userLoginName, string userGroupName,string firstName, string lastName, string emailAdd)
        {
            //Executes this method with Full Control rights even if the user does not otherwise have Full Control
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                //Don't use context to create the spSite object since it won't create the object with elevated privileges but with the privileges of the user who execute the this code, which may casues an exception
                using (SPSite spSite = new SPSite(Page.Request.Url.ToString()))
                {
                    using (SPWeb spWeb = spSite.OpenWeb())
                    {
                        try
                        {

                            spWeb.AllowUnsafeUpdates = true;
                            string userName = "i:0#.f|FBAMembershipProvider|" + userLoginName; // also try with full email address as per your configuration
                            spWeb.SiteUsers.Add(userName, emailAdd, firstName + " " + lastName, "");

                            SPUser user = spWeb.SiteUsers[userName];
if (user != null)
{
    spWeb.Groups[userGroupName].AddUser(user);
    spWeb.Update();
    spWeb.AllowUnsafeUpdates = false;
}
                            //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)
                           

                            //SPUser spUser = spWeb.EnsureUser(userLoginName);

                            //if (spUser != null)
                            //{
                            //    SPGroup spGroup = spWeb.Groups[userGroupName];

                            //    if (spGroup != null)
                            //        spGroup.AddUser(spUser);
                            //}
                        }
                        catch (Exception ex)
                        {
                            //Error handling logic should go here
                        }
                        finally
                        {
                            spWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }

                string blogUrl = "http://" + Page.Request.Url.Authority + "/sites/blog_en";
                //SPSite blogRoot = new SPSite(SPContext.Current.Site.Url);
                using (SPSite spSite = new SPSite(blogUrl))
                {
                    using (SPWeb spWeb = spSite.OpenWeb())
                    {
                        try
                        {
                            //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)
                            spWeb.AllowUnsafeUpdates = true;

                            string userName = "i:0#.f|FBAMembershipProvider|" + userLoginName; // also try with full email address as per your configuration
                            spWeb.SiteUsers.Add(userName, emailAdd, firstName + " " + lastName, "");

                            SPUser user = spWeb.SiteUsers[userName];
                            if (user != null)
                            {
                                spWeb.Groups["SCDR English Blog Members"].AddUser(user);
                                spWeb.Update();
                                spWeb.AllowUnsafeUpdates = false;
                            }
                            //SPUser spUser = spWeb.EnsureUser(userLoginName);

                            //if (spUser != null)
                            //{
                            //    SPGroup spGroup = spWeb.Groups["SCDR English Blog Members"];

                            //    if (spGroup != null)
                            //        spGroup.AddUser(spUser);
                            //}
                        }
                        catch (Exception ex)
                        {
                            //Error handling logic should go here
                        }
                        finally
                        {
                            spWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }
                blogUrl = "http://" + Page.Request.Url.Authority + "/sites/blog_ar";
                using (SPSite spSite = new SPSite(blogUrl))
                {
                    using (SPWeb spWeb = spSite.OpenWeb())
                    {
                        try
                        {
                            //Allow updating of some sharepoint lists, (here spUsers, spGroups etc...)
                            spWeb.AllowUnsafeUpdates = true;

                            string userName = "i:0#.f|FBAMembershipProvider|" + userLoginName; // also try with full email address as per your configuration
                            spWeb.SiteUsers.Add(userName, emailAdd, firstName + " " + lastName, "");

                            SPUser user = spWeb.SiteUsers[userName];
                            if (user != null)
                            {
                                spWeb.Groups["أعضاء مدونة عربية"].AddUser(user);
                                spWeb.Update();
                                spWeb.AllowUnsafeUpdates = false;
                            }
                            //SPUser spUser = spWeb.EnsureUser(userLoginName);

                            //if (spUser != null)
                            //{
                            //    SPGroup spGroup = spWeb.Groups["أعضاء مدونة عربية"];

                            //    if (spGroup != null)
                            //        spGroup.AddUser(spUser);
                            //}
                        }
                        catch (Exception ex)
                        {
                            //Error handling logic should go here
                        }
                        finally
                        {
                            spWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }

        public bool CreateUserAccount(string userName, string userPassword, string userFirstName, string userLastName, string userEmailID, string userPhoneNumber)
        {
            MembershipCreateStatus createStatus;
            MembershipUser user = Membership.CreateUser(userName, userPassword, userEmailID, userFirstName, userLastName, true, out createStatus);
            user.IsApproved = true;
            Membership.UpdateUser(user);  
            switch (createStatus)
           {
                    //This Case Occured whenver User Created Successfully in database
                case MembershipCreateStatus.Success:
                   return true;

                default:
                   return false;
            }

        }
        //public bool CreateUserAccount(string userName, string userPassword, string userFirstName, string userLastName, string userEmailID, string userPhoneNumber)
        //{
        //    try
        //    {
        //        string ldapPath = "ext-scdr.scdr.gov.ae/CN=Users,DC=EXT-SCDR,DC=SCDR,DC=GOV,DC=AE";
        //       string oGUID = string.Empty;
        //        string connectionPrefix = "LDAP://" + ldapPath;
        //        DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, @"ext-scdr\scdradmin", @"Pass123", AuthenticationTypes.Secure);
        //        DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "user");
        //        newUser.Properties["samAccountName"].Value = userName;
        //        newUser.Properties["givenName"].Value = userFirstName;
        //        newUser.Properties["sn"].Value = userLastName;
        //        newUser.Properties["displayName"].Value =userFirstName+ " " + userLastName;
        //        newUser.Properties["mail"].Value = userEmailID;
        //        newUser.Properties["telephoneNumber"].Value = userPhoneNumber;
        //        newUser.Properties["userPrincipalName"].Value = userName + "@extscdr.gov.ae";
        //        //newUser.Properties["unicodePwd"].Value = userPassword;
        //        newUser.CommitChanges();
        //        //oGUID = newUser.Guid.ToString();
        //        newUser.Close();
        //        PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "ext-scdr", "DC=EXT-SCDR,DC=SCDR,DC=GOV,DC=AE", @"ext-scdr\Administrator", @"P@ssw0rd");
        //        UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, userName);
        //        if (user == null) 
        //            return false;
        //        user.SetPassword(userPassword);
        //        //ldapPath = "ext-scdr.scdr.gov.ae/CN=" + userName + "OU=Users,DC=EXT-SCDR,DC=SCDR,DC=GOV,DC=AE";
        //        //connectionPrefix = "LDAP://" + ldapPath;
        //        ////dirEntry = new DirectoryEntry(connectionPrefix, @"ext-scdr\scdradmin", @"Pass123", AuthenticationTypes.Secure);
        //        //newUser = new DirectoryEntry(connectionPrefix, @"ext-scdr\Administrator", @"P@ssw0rd", AuthenticationTypes.Secure);
        //        //newUser.Invoke("SetPassword", new object[] { userPassword });
        //        //newUser.Properties["LockOutTime"].Value = 0;
        //        ////newUser.Properties["userAccountControl"][0] = 0x0200;
        //        //newUser.CommitChanges();
        //        ////dirEntry.CommitChanges();
        //        ////dirEntry.Close();
        //        //newUser.Close();
        //        user.Enabled = true;
        //        user.PasswordNeverExpires = true;
        //        user.Save();
        //        dirEntry.Close();
        //        return true;
        //    }
        //    catch (System.DirectoryServices.DirectoryServicesCOMException E)
        //    {
        //        //DoSomethingwith --> E.Message.ToString();
        //        return false;

        //    }

        //}
       
        

    }
}
