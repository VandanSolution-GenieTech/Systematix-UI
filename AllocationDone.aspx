<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AllocationDone.aspx.cs" Inherits="AllocationDone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="main-content-container container-fluid">

    <!-- Page Header -->

    <div class="row">
        <div class="col-lg-10 mb-1">

            <div class="page-header row no-gutters py-3">
                <h3 class="page-title fontstyle">Allocation Done</h3>
                <span class="page-subtitle " style="vertical-align: text-bottom;">&nbsp;(Amt in ₹)</span>
            </div>


        </div>
        <div class="col-lg-2 mb-1">
            <div class="page-header row no-gutters py-3">



                <asp:Button runat="server" ID="export" Text="Excel" 
                    class="mb-2 btn button_brand mr-2" OnClick="export_Click" />
             
            </div>
        </div>
    </div>
    <div class="card card-small h-100 mb-4">
        		<div class="card-header border-bottom bgnone" style="padding-bottom:0px;">

      <div class="row">
				<div class="col-12 col-sm-9">

              <label>
						<div id="Div2" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
						<asp:DropDownList runat="server" ID="Client" AutoPostBack="true"  class="custom-select" 
          ></asp:DropDownList>
                  </div>
					</label>
				
					



          <label>
<span class="ml-auto text-right text-semibold text-reagent-gray">
<div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
<asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand mr-2" 
                  Text="Submit" onclick="Submit_Click"/>

               


                  </div>
</span>
   
  </label>

				</div>


                
 


			</div>
		
			
		</div>
        <div class="card-body p-0 pb-3">
            <div class=" ">
                <div class="dataTables_length">
                    <table id="example" class="table mb-0 stripe row-border order-column" style="width: 100%">

                        <thead class="bg-light" >
                            <tr class="gridheader ">
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col ">Id</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Datetime</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Client Code</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Client Name</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Amount</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Segment</th>
                                <th style="text-transform: capitalize;" class="txt_left fontstyle " scope="col">Before Amount</th>
                                
                            </tr>

                        </thead>
                        

                        <tbody>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Id") %></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "dt")%></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "ClientCode") %></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Clientname") %></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Amt") %></div></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Segment")%></td>
                                        <td style="text-transform: capitalize;" scope="col" class="txt_left fontstyle"><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "BeforeAmount") %></div></td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>


                        </tbody>



                    </table>
                </div>
            </div>
        </div>






    </div>
</div>
</asp:Content>

