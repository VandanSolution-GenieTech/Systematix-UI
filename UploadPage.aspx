<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadPage.aspx.cs" Inherits="UploadPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    

    <%-- start --%>


    <div class="row">
        <div class="col-lg-10 mb-1">
            <div class="page-header row no-gutters py-4">
                <h3 class="page-title fontstyle"><%= Titleprefix %> RM-SRM Mapping</h3>
            </div>
            
        </div>
        <div class="col-lg-2 mb-1">
                <div class="page-header row no-gutters py-3">

                    <asp:Button runat="server" ID="txtAdd" Visible="false" Text="Upload New Mapping" OnClick="Add"
                        class="mb-2 btn button_brand mr-2" Style="scroll-behavior: smooth;"/>
                </div>
            </div>
    </div>
    <div class="row" Id="Rmsrm" visible="false" runat="server">
        <div class="col-lg-3 mb-4">
        </div>
        <div class="col-lg-6 mb-4">
            <!-- Sliders & Progress Bars -->
            <div class="card card-small mb-4">
                <div class="card-header border-bottom">
                    <h6 class="m-0">Upload Mapping</h6>
                </div>
                <ul class="list-group list-group-flush">


                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue fontstyle">Browse WS Format File</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <%--<asp:FileUpload ID="fupload" runat="server" />--%>


                                <asp:FileUpload ID="fupload" runat="server" />


                            </div>
                        </span>
                    </li>

                    

                    <div class="col-3 alertstyle">
                        <div class="message_alert" id="alertcontainer"></div>
                    </div>
                    <li class="list-group-item d-flex px-3">

                          <span class="ml-auto text-right text-semibold text-reagent-gray">
                  <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                        <asp:Button runat="server" ID="btnUpload" class="mb-2 btn button_brand mr-2" 
                                    Text="Upload" onclick="btnUpload_Click"/>
                        <asp:Button runat="server" ID="Closebtn" class="mb-2 btn button_brand mr-2" 
                                    Text="Close" onclick="Close_Click"/>
                        <asp:Label runat="server" ID="lblmsg" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                  </div>
                          </span>
                       
                    </li>



                </ul>
            </div>

        </div>

    </div>

      <div class="card card-small h-100 mb-4" runat="server" id="Grid" >

				
					<div class="card-header border-bottom bgnone">
					<div class="card-body p-0 pb-3">
						<div class=" ">
							<div class="dataTables_length">	
                           <table id="example" class="table mb-0 stripe row-border order-column" style="width:100%">
                            <thead class="bg-light">
						        <tr class="gridheader">
								  <th  style="text-transform:capitalize;" class="text-left fontstyle">Service RM Name</th>
								  <th  style="text-transform:capitalize;" class="text-left fontstyle">Service RM Email</th>
								  <th  style="text-transform:capitalize;" class="text-left fontstyle">RM Name</th>
								  <th  style="text-transform:capitalize;" class="text-left fontstyle">RM Email Id</th>
								  
								</tr>
							  </thead>
							  <tbody>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
								  <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
   <ContentTemplate>
          <asp:Repeater ID="Repeater2" runat="server">
          <ItemTemplate>
								<tr>
								  <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "SRMname")%></td>
								  <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "SRMemail")%></td>
							      <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "RMname")%></td>
								  <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "RMemail")%></td>
	
								</tr>
         </ItemTemplate>
          </asp:Repeater>								
   </ContentTemplate>
									  </asp:UpdatePanel>

							  </tbody>
							</table>
                            </div>
						</div>
				    </div>
                        </div>
          </div>
    



    	<script>
            $("#submitPrice").hide();
            $("#edit").click(function () {
                $("#submitPrice").show();
                $("#edit").hide();
            });

            $("#btnSubmit").click(function () {
                $("#submitPrice").hide();
                $("#edit").hide();
            });

        </script>


</asp:Content>

