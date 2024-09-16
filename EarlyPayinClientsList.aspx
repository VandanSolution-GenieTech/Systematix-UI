<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EarlyPayinClientsList.aspx.cs" Inherits="EarlyPayinClientsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content-container container-fluid">


        <div class="row">
            <div class="col-lg-8 mb-1">

                <div class="page-header row no-gutters py-3">
                    <h3 class="page-title fontstyle">Early Payin Clients</h3>
                    <span class="page-subtitle" style="vertical-align: text-bottom;"></span>
                </div>

            </div>

            <div class="col-lg-2 mb-1">
                <div class="page-header row no-gutters py-3">
                  <asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand mr-2" Text="Add" PostBackUrl="~/EarlyPayinClients.aspx"/>
                </div>
            </div>

        </div>
        <!-- Sales Report -->
        <div class="card card-small col-lg-10 offset-1 h-100 mb-4">
            <%--            <div class="card-header border-bottom bgnone" style="padding-bottom: 0px;">
            </div>--%>
            <div class="card-body p-0 pb-3">
                <div class=" ">
                    <div class="dataTables_length">
                        <table id="example" class="table mb-0 stripe row-border order-column" style="width: 100%">

                            <thead class="bg-light">
                                <tr>
                                    <th style="text-transform: capitalize;" scope="col" class="sorting_asc txt_left">S. No.</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Client Code</th>
                                    <th style="text-transform: capitalize;" class="txt_left" scope="col">Added Date</th>
                                    <%-- <th style="text-transform: capitalize;" class="text-center" scope="col">Remitter Virtual Code</th>--%>
                                </tr>
                            </thead>


                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "clientcode") %></td>
                                            <td scope="col" class="txt_left"><%# DataBinder.Eval(Container.DataItem, "addeddate") %></td>
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

