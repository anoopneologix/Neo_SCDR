<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrentUserDetails.ascx.cs" Inherits="SCDR.CurrentUserDetails.CurrentUserDetails" %>

                      <div class="login">
                                    <div class="login_btn">
                                        <i class="fa"></i> 
                                        <span class="login_form_outer"> 
											
												<span class="col-md-12 col-sm-12 col-xs-12 tab-frm signin-frm" style="display: block">
													<div>
															<div class="form-group custom-frm-group">
																<label id="lblUserName" runat="server" for="email">UserName</label> 
																<span class="email">
                                                                    <asp:TextBox ID="txtUsername" ReadOnly="true" class="form-control frm-email" runat="server"></asp:TextBox>
																	</span>
                                    </div>
                                    
                                    <div class="form-group custom-frm-group">
																<label id="lblEmail" runat="server" for="email">Email Address:</label> 
																<span class="email">
																	 <asp:TextBox ID="txtEmail" ReadOnly="true" class="form-control frm-email" runat="server"></asp:TextBox>
																</span>
                                    </div>

                                                            <asp:Button ID="btnSettings" class="btn btn-default btn-theme btn-submit" runat="server" Text="" />
                                    <asp:Button ID="btnSignOut" class="btn btn-primary btn-signup" runat="server" Text="" OnClick="btnSignOut_Click" />
                                  
                                    </div>
                                    </span>

                                
                          

                    


                </span>
        </div>
        </div>