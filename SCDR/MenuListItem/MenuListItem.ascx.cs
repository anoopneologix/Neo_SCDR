//--------------------------------------------------
// Project Name : SCDR
// Program Name : MenuItemList (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 29/03/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;

namespace SCDR.MenuListItem
{
    [ToolboxItemAttribute(false)]
    public partial class MenuListItem : WebPart
    {
         public MenuListItem()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Fuction for loading the Menu on Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMenuItems();
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "SiteLinks_List";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("Menu List Name:"),
        WebDescription("Please Enter a valid Menu List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        #endregion
        DataTable dtParent = new DataTable();
        // Function to call menu list items from SharePoint Custom List
        // Bind those items to the Repeaterr
        public void BindMenuItems()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            //  string listUrl = oSite.Url + "/" + ListName + "/";
                            dtParent = ConvertSPListToDataTable(oItems);
                            DataTable dtMenuList = GetParentList(dtParent);
                            repMenuItem.DataSource = dtMenuList;
                            repMenuItem.DataBind();
                        }
                    }
                });
            }
            catch(Exception ex)
            {

            }
        }

        // Function to convert SharePoint Custom List to DataTable 
        public DataTable ConvertSPListToDataTable(SPListItemCollection spItemCollection)
        {
            DataTable dtSPList = new DataTable();

            DataTable dtParentItems = new DataTable();
           
            try
            {
                dtSPList = spItemCollection.GetDataTable();
                return dtSPList;
               
            }
            catch
            {

                return (dtSPList);
            }
        }

       public DataTable GetParentList(DataTable dt)
        {
            DataTable dtMenuItem = new DataTable();
            dtMenuItem = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ParentLink"].ToString() == "")
                {
                    string liUrl = dr["linkUrl"].ToString();
                    int index = liUrl.IndexOf(",");
                    if (index > 0)
                    {
                        liUrl = liUrl.Substring(0, index);
                        dr["linkUrl"] = liUrl;
                    }
                    dtMenuItem.Rows.Add(dr.ItemArray);
                }

            }
            return dtMenuItem;

        }

        protected void repMenuItem_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string parentMenuItem = (e.Item.FindControl("hfParentItem") as HiddenField).Value;
                Repeater rptChildMenu = e.Item.FindControl("repChildMenuItem") as Repeater;
                rptChildMenu.DataSource = GetChildMenu(parentMenuItem,dtParent);
                rptChildMenu.DataBind();
            }
        }

        public DataTable GetChildMenu(string item, DataTable dt) 
        {
            DataTable dtMenuItem = new DataTable();
            dtMenuItem = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ParentLink"].ToString() == item)
                {
                    string liUrl = dr["linkUrl"].ToString();
                    int index = liUrl.IndexOf(",");
                    if (index > 0)
                    {
                        liUrl = liUrl.Substring(0, index);
                        dr["linkUrl"] = liUrl;
                    }
                  
                    dtMenuItem.Rows.Add(dr.ItemArray);
                    liUrl = "";
                    index = 0;
                }
            }
            return dtMenuItem;

        }
   
    }
}
