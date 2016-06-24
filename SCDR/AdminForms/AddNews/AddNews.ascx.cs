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
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace SCDR.AdminForms.AddNews
{
    [ToolboxItemAttribute(false)]
    public partial class AddNews : WebPart
    {
        
         int SpListItemId = 0;
         int SpListItemIdAr = 0;
        public AddNews()
        {
           
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

          // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomNewsList";
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
          
            
        }

        //function for add items to list
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
             try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
              {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {
                    
                      Guid newsId = Guid.NewGuid();
                      string directoryPath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}/", "TempfileUpload"));
                      if (!Directory.Exists(directoryPath))
                      {
                          Directory.CreateDirectory(directoryPath);
                      }
                      if (fuThumbnailImage.HasFile)
                      {

                          foreach (HttpPostedFile postedFile in fuThumbnailImage.PostedFiles)
                          {
                              string fileName = Path.GetFileName(fuThumbnailImage.PostedFile.FileName);
                              fuThumbnailImage.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/TempfileUpload/") + fileName);
                          }
                      }
                      string[] filesPath = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/SampleFiles/"));
                      List<ListItem> files = new List<ListItem>();
                      foreach (string path in filesPath)
                      {
                          files.Add(new ListItem(Path.GetFileName(path)));
                      }
                      using (SPWeb oWeb = oSite.OpenWeb("en/"))
                      {
                          SPList list = oWeb.Lists[ListName];
                          SPListItem item = list.Items.Add();
                          item["NewsID"] = newsId.ToString();
                          item["Title"] = txtNewsHeading.Text;
                          item["Date"] = txtNewsdate.Text;
                          item["Location"] = txtNewsLocation.Text;
                          item["Description"] = hfNewsDescription.Value.ToString();
                          if(fuThumbnailImage.HasFile)
                          {
                              
                         foreach (HttpPostedFile postedFile in fuThumbnailImage.PostedFiles)
                          {

                              Stream fs = postedFile.InputStream;
                              byte[] fileContents = new byte[fs.Length];
                              fs.Read(fileContents, 0, (int)fs.Length);
                              fs.Close();
                              SPAttachmentCollection attach = item.Attachments;
                              string fileName = Path.GetFileName(postedFile.FileName);
                              attach.Add(fileName, fileContents);

                          }
                          }
                          
                          oWeb.AllowUnsafeUpdates = true;
                          item.Update();
                          oWeb.AllowUnsafeUpdates = false;
                          SpListItemId = item.ID;
                      }
                      using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                      {
                          SPList listAr = oWebAr.Lists[ListName];
                          SPListItem itemAr = listAr.Items.Add();
                          itemAr["NewsID"] = newsId.ToString();
                          itemAr["Title"] = txtNewsHeadingAr.Text;
                          itemAr["Date"] = txtNewsDateAr.Text;
                          itemAr["Location"] = txtNewsLocationAr.Text;
                          itemAr["Description"] = hfNewsDescriptionAr.Value.ToString();
                          if (fuThumbnailImage.HasFile)
                          {
                            
                         foreach (HttpPostedFile postedFileAr in fuThumbnailImage.PostedFiles)
                              {

                                  Stream fsAr = postedFileAr.InputStream;
                                  byte[] fileContentsAr = new byte[fsAr.Length];
                                  fsAr.Read(fileContentsAr, 0, (int)fsAr.Length);
                                  fsAr.Close();
                                  SPAttachmentCollection attachAr = itemAr.Attachments;
                                  string fileNameAr = Path.GetFileName(postedFileAr.FileName);
                                  attachAr.Add(fileNameAr, fileContentsAr);
                              }
                          }
                          oWebAr.AllowUnsafeUpdates = true;
                          itemAr.Update();
                          oWebAr.AllowUnsafeUpdates = false;
                          SpListItemIdAr = itemAr.ID;
                      }
                          if (rbYes.Checked)
                          {
                              BindThumbnailImages();
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                          }
                          else if (rbNo.Checked)
                          {
                              using (SPWeb oWeb = oSite.OpenWeb("en/"))
                              {
                                  SPList list = oWeb.Lists[ListName];
                                  SPListItem item = list.GetItemById(SpListItemId);
                                  string thumbnailUrl = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
                                  item["ThumbnailUrl"] = thumbnailUrl;
                                  oWeb.AllowUnsafeUpdates = true;
                                  item.Update();
                                  oWeb.AllowUnsafeUpdates = false;
                              }
                              using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                              {
                                  SPList listAr = oWebAr.Lists[ListName];
                                  SPListItem itemAr = listAr.GetItemById(SpListItemIdAr);
                                  string thumbnailUrl = oWebAr.Url + "/_layouts/15/SCDR/images/default.png";
                                  itemAr["ThumbnailUrl"] = thumbnailUrl;
                                  oWebAr.AllowUnsafeUpdates = true;
                                  itemAr.Update();
                                  oWebAr.AllowUnsafeUpdates = false;
                              }
                                      formClear();
                                      string sMessage = "successfully completed";
                                      ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                          }
                      

                  }
              });



            }
            catch (Exception ex)
            {
                
            }

        }

        //function for update list item (Thumbnail Url)
        protected void btnSaveThumbnail_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                 {
                     using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                     {
                         using (SPWeb oWeb = oSite.OpenWeb("en/"))
                         {
                             SPList list = oWeb.Lists[ListName];
                             SPListItem item = list.GetItemById(Convert.ToInt32(hfListItemId.Value));
                             item["ThumbnailUrl"] = lblUrl.Value.ToString();
                             oWeb.AllowUnsafeUpdates = true;
                             item.Update();
                             oWeb.AllowUnsafeUpdates = false;
                         }
                         using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                         {
                             SPList listAr = oWebAr.Lists[ListName];
                             SPListItem itemAr = listAr.GetItemById(Convert.ToInt32(hfListItemIdAr.Value));
                             string engThumbnailUrl = lblUrl.Value.ToString();
                             StringBuilder arThumbnailUrl = new StringBuilder(engThumbnailUrl);
                             arThumbnailUrl.Replace("/en/", "/ar/");
                             itemAr["ThumbnailUrl"] = arThumbnailUrl;
                             oWebAr.AllowUnsafeUpdates = true;
                             itemAr.Update();
                             oWebAr.AllowUnsafeUpdates = false;
                         }
                         formClear();
                         string sMessage = "successfully completed";
                         ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);

                     }
                 });
            }
            catch { }

        }

         //function for binding uploaded images to pop up modal
        public void BindThumbnailImages()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
             {
                 using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                 {
                     
                     using (SPWeb oWeb = oSite.OpenWeb("en/"))
                     {
                         SPList list = oWeb.Lists[ListName];
                         SPListItem item = list.GetItemById(SpListItemId);
                         hfListItemId.Value = SpListItemId.ToString();
                         SPAttachmentCollection docs = item.Attachments;
                         DataTable dt = new DataTable();
                         DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                         dt.Columns.Add(dcImageUrl);
                         if (docs.Count > 0)
                         {
                             foreach (string fileName in item.Attachments)
                             {
                                 DataRow dr = dt.NewRow();
                                 dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                 dt.Rows.Add(dr);
                             }
                             repThumbnail.DataSource = dt;
                             repThumbnail.DataBind();

                         }
                     }
                     using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                     {
                         SPList listAr = oWebAr.Lists[ListName];
                         SPListItem itemAr = listAr.GetItemById(SpListItemIdAr);
                         hfListItemIdAr.Value = SpListItemIdAr.ToString();
                         SPAttachmentCollection docs = itemAr.Attachments;
                         DataTable dt = new DataTable();
                         DataColumn dcImageUrlAr = new DataColumn("ImageUrlAr", typeof(string));
                         dt.Columns.Add(dcImageUrlAr);
                         if (docs.Count > 0)
                         {
                             foreach (string fileName in itemAr.Attachments)
                             {
                                 DataRow dr = dt.NewRow();
                                 dr["ImageUrlAr"] = SPUrlUtility.CombineUrl(itemAr.Attachments.UrlPrefix, fileName);
                                 dt.Rows.Add(dr);
                             }

                             repthumbnailAr.DataSource = dt;
                             repthumbnailAr.DataBind();
                         }
                     }
                 
                 }
             });
        }

         //function for clearing the controls in form
        public void formClear()
        {
            txtNewsHeading.Text = "";
            txtNewsdate.Text = "";
            txtNewsLocation.Text = "";
            txtNewsDescription.Text = "";
            rbYes.Checked = true;
            txtNewsLocationAr.Text = "";
            txtNewsHeadingAr.Text = "";
            txtNewsDescriptionAr.Text = "";
            txtNewsDateAr.Text = "";
        }
    }
}
