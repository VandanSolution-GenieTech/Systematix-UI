<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HDFC.aspx.cs" Inherits="HDFC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content-container container-fluid">


        <div class="row">
            <div class="col-lg-11 mb-1">

                <div class="page-header row no-gutters py-3">
                    <h3 class="page-title fontstyle">ICICI Transaction</h3>
                    <span class="page-subtitle" style="vertical-align: text-bottom;">&nbsp;</span>
                </div>

            </div>
            <div class="col-lg-1 mb-1" style="margin-left: -20px;">
                <div class="page-header row no-gutters py-3">

                    <asp:Button runat="server" ID="export" Text="Download" OnClick="export_Click"
                        class="mb-2 btn button_brand mr-2" />


                </div>
            </div>
        </div>
        <!-- Sales Report -->
        <div class="card card-small h-100 mb-4">


            <div class="card-header border-bottom bgnone" style="padding-bottom: 0px;">

                <div class="row">
                    <div class="col-12 col-sm-9">


                        <label>
                            <div id="" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="FDate" type="text" autocomplete="off" AutoCompleteType="Disabled" class="input-sm form-control datepicker1" placeholder="Start Date"></asp:TextBox>
                            </div>
                        </label>
                        <label>
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="TDate" type="text" autocomplete="off" AutoCompleteType="Disabled" class="input-sm form-control datepicker1" placeholder="End Date"></asp:TextBox>
                            </div>
                        </label>
                         <label>
							<div id="" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
							  <asp:DropDownList runat="server" ID="Select" class="custom-select js-example-basic-single" style="max-width: 350px; width: 200px;"></asp:DropDownList>
							</div>
                                     </label>
                        <label>
                            <span class="ml-auto text-right text-semibold text-reagent-gray">
                                <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                    <asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand mr-2" Text="Submit" onclick="Submit_Click"/>
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

                            <thead class="bg-light">
                                <tr>
                                    <th style="text-transform: capitalize;" scope="col" class="sorting_asc txt_left">S. No.</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Bank Name</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Transaction Date</th>
                                    <th style="text-transform: capitalize;" class="text-center" scope="col">Account</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Beneficiary Name</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Branch</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Amount</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Mode</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Dr/Cr</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Description</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Remitter Account</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Remitter IFSC</th>
                                   <%-- <th style="text-transform: capitalize;" class="text-center" scope="col">Remitter Virtual Code</th>--%>
                                </tr>
                            </thead>


                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "BankName") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "valuedate", "{0:yyyy-MM-dd}") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "accountnumber") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "remittername") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "branchname") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "transactionamount") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "indicator") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "mode") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "transactionparticular") + " " + DataBinder.Eval(Container.DataItem, "transactionparticular2") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "remitteraccount") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "ifsc") %></td>
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
