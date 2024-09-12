<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Js/numberformat.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div class="main-content-container container-fluid">

        <!-- Page Header -->
        <div class="row">
            <div class="col-lg-8 mb-1">
                <div class="page-header row no-gutters py-3">
                    <h3 class="page-title fontstyle"><%= newtest%> Management Dashboard</h3>
                </div>
            </div>
            <div class="col-lg-4 mb-1">
                <div class="main-content-container container-fluid px-4">

                    <div class="page-header row no-gutters py-4">
                        <div class="col-12 col-sm-4 d-flex align-items-center">
                            <div class="btn-group btn-group-sm btn-group-toggle d-inline-flex mb-4 mb-sm-0 mx-auto" role="group" aria-label="Page actions">
                                <a href="Index.aspx?Typ=FTD" class="<%= FTDClass%>">FTD </a>
                                <a href="Index.aspx?Typ=WTD" class="<%= WTDClass%>">WTD </a>
                                <a href="Index.aspx?Typ=MTD" class="<%= MTDClass%>">MTD </a>
                                <a href="Index.aspx?Typ=YTD" class="<%= YTDClass%>">YTD </a>
                                <a href="Index.aspx?Typ=1YER" class="<%= YRClass%>">1YR </a>


                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
        <!-- End Page Header -->
        <!-- Small Stats Blocks -->


        <div class="row">
            <div class="col-lg col-md-6 col-sm-6 mb-4">
                <div class="stats-small--1 card card-small" style="padding-top: 10px;">
                    <div class="card-body p-0 d-flex">
                        <div class="d-flex flex-column m-auto">
                            <div class="stats-small__data text-center">
                                <span class="stats-small__label text-uppercase fontstyle">Volume(C)</span>
                                <h6 class="stats-small__value count my-3">
                                    <div class="Comma"><%= Volume%></div>
                                </h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg col-md-6 col-sm-6 mb-4">
                <div class="stats-small--1 card card-small" style="padding-top: 10px;">
                    <div class="card-body p-0 d-flex">
                        <div class="d-flex flex-column m-auto">
                            <div class="stats-small__data text-center">
                                <span class="stats-small__label text-uppercase fontstyle">Revenue(L)</span>
                                <h6 class="stats-small__value count my-3">
                                    <div class="Comma"><%= Revenue%></div>
                                </h6>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg col-md-4 col-sm-6 mb-4">
                <div class="stats-small--1 card card-small" style="padding-top: 10px;">
                    <div class="card-body p-0 d-flex">
                        <div class="d-flex flex-column m-auto">
                            <div class="stats-small__data text-center">
                                <span class="stats-small__label text-uppercase fontstyle">New Accounts</span>
                                <h6 class="stats-small__value count my-3"><%= NewAcc%> </h6>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg col-md-4 col-sm-6 mb-4">
                <div class="stats-small--1 card card-small" style="padding-top: 10px;">
                    <div class="card-body p-0 d-flex">
                        <div class="d-flex flex-column m-auto">
                            <div class="stats-small__data text-center">
                                <span class="stats-small__label text-uppercase fontstyle">Active RMs</span>
                                <h6 class="stats-small__value count my-3"><%= Active_RM %></h6>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg col-md-4 col-sm-12 mb-4">
                <div class="stats-small--1 card card-small" style="padding-top: 10px;">
                    <div class="card-body p-0 d-flex">
                        <div class="d-flex flex-column m-auto">
                            <div class="stats-small__data text-center">
                                <span class="stats-small__label text-uppercase fontstyle">Active Trading Clients</span>
                                <h6 class="stats-small__value count my-3"><%= Active_Trading_Client%></h6>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <!-- End Small Stats Blocks -->
        <div class="row mb-5">
            <!-- Users Stats -->
            <div class="col-lg-4 col-md-12 col-sm-12 mb-4">

                <div class="card card-small h-100">

                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <table class="table mb-0 tbl_nrml">
                                <tbody>

                                    <tr>
                                        <td class="text-left">
                                            <h6 class="m-0 fontstyle">Brokerage</h6>
                                        </td>
                                        <td class="text-right"></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">Gross Brokrage</th>
                                        <td class="text-right fontstyle"><%= GrossBrokrage%></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">Net Brokrage</th>
                                        <td class="text-right fontstyle"><%= Revenueeq%></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">Total DP Accounts</th>
                                        <td class="text-right fontstyle"><%= DPClient%></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">Total Trading Accounts</th>
                                        <td class="text-right fontstyle"><%= TotalClient%></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">Active Clients</th>
                                        <td class="text-right fontstyle"><%= ActiveClient%></td>
                                    </tr>

                                    <tr>
                                        <th class="text-left fontstyle">To Be Dormant in 30 Days</th>
                                        <td class="text-right fontstyle"><%= DormantClient%></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>



                    </div>


                    <div class="card-footer border-top">
                        <div class="row">
                            <div class="col text-right view-report">
                                <h6 style="color: transparent; height: 10px;">View full report →</h6>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


            <div class="col-lg-8 col-md-12 col-sm-12 mb-8">
                <div class="card card-small mb-4 h-100">
                    <div class="card-header border-bottom bgnone">
                        <h6 class="m-0 fontstyle">Top 5 Client By Revenue</h6>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <table class="table mb-0 tbl_nrml">
                                <thead class="bg-light">
                                    <tr class="gridheader">
                                        <th style="text-transform: capitalize; background-color:#DDD9D4;" class="text-left fontstyle">Client</th>
                                        <th style="text-transform: capitalize;" class="text-left fontstyle">Client Code</th>
                                        <th style="text-transform: capitalize;" class="text-left fontstyle">Primary RM</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Volume(L)</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Revenue(T)</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left"><a title='<%# DataBinder.Eval(Container.DataItem, "ClientName") %>' target="_blank" href='<%=BaseUrl %><%# DataBinder.Eval(Container.DataItem, "Ftlogin") %>&LaunchUid=<%= LaunchUid %>&LIC=VandanB123896TLKM'><%# DataBinder.Eval(Container.DataItem, "ClientName") %></a></td>
                                                <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "ClientCode")%></td>
                                                <td class="text-left fontstyle"><%# DataBinder.Eval(Container.DataItem, "PRM")%></td>
                                                <td class="text-right fontstyle">
                                                    <div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Volume")%></div>
                                                </td>
                                                <td class="text-right fontstyle">
                                                    <div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Brokerage")%></div>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="card-footer border-top">
                        <div class="row">
                            <div class="col text-right view-report">
                                <a href="Clientwiserevenue.aspx">View full report →</a>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
        <div class="row mb-5">

            <div class="card card-small h-100 col-lg-12">







                <div class="card-body p-0 pb-3">
                    <div class=" ">
                        <div class="dataTables_length">
                            <table class="table mb-0 stripe row-border order-column" style="width: 100%">

                                <thead class="bg-light">
                                    <tr class="gridheader">
                                        <th class="text-left fontstyle" style="width: 20%; background-color:#DDD9D4; text-transform: capitalize;">Branch</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">New Clients Added</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Volume(L)</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Gross Revenue</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Net Revenue</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Revenue Stake</th>
                                    </tr>
                                </thead>



                                <tbody>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left fontstyle" style="font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "Zone") %> </strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "TotalClientAdded")%></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Volume")%></strong></div></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "GrossBrokrage")%></div></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Netbrokrage")%></div></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "Stake")%></strong></td>

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

        <div class="row mb-5">

            <div class="card card-small h-100 col-lg-12">







                <div class="card-body p-0 pb-3">
                    <div class=" ">
                        <div class="dataTables_length">
                            <table class="table mb-0 stripe row-border order-column" style="width: 100%">

                                <thead class="bg-light">
                                    <tr class="gridheader">
                                        <th class="text-left fontstyle" style="width: 20%; text-transform: capitalize; background-color:#DDD9D4;">RM</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">New Clients Added</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Volume(L)</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Gross Revenue</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Net Revenue</th>
                                        <th style="text-transform: capitalize;" class="text-right fontstyle">Revenue Stake</th>


                                    </tr>

                                </thead>



                                <tbody>
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left fontstyle" style="width: 20%; font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "PRM")%></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "TotalClientAdded")%></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Volume")%></strong></div></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "GrossBrokrage")%></div></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><div class="Comma"><%# DataBinder.Eval(Container.DataItem, "Netbrokrage")%></div></strong></td>
                                                <td class="text-right fontstyle" style="font-size: 13px;"><strong><%# DataBinder.Eval(Container.DataItem, "Stake")%></strong></td>


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
    </div>
    <script type="text/javascript">
        // insert commas as thousands separators 
        function addCommas(n) {
            var rx = /(\d+)(\d{3})/;
            return String(n).replace(/^\d+/, function (w) {
                while (rx.test(w)) {
                    w = w.replace(rx, '$1,$2');
                }
                return w;
            });
        }
        // return integers and decimal numbers from input
        // optionally truncates decimals- does not 'round' input
        function validDigits(n, dec) {
            n = n.replace(/[^\d\.]+/g, '');
            var ax1 = n.indexOf('.'), ax2 = -1;
            if (ax1 != -1) {
                ++ax1;
                ax2 = n.indexOf('.', ax1);
                if (ax2 > ax1) n = n.substring(0, ax2);
                if (typeof dec === 'number') n = n.substring(0, ax1 + dec);
            }
            return n;
        }
        window.onload = function () {
            var n1 = document.getElementById('number1'),
                n2 = document.getElementById('number2');
            n1.value = n2.value = '';

            n1.onkeyup = n1.onchange = n2.onkeyup = n2.onchange = function (e) {
                e = e || window.event;
                var who = e.target || e.srcElement, temp;
                if (who.id === 'number2') temp = validDigits(who.value, 2);
                else temp = validDigits(who.value);
                who.value = addCommas(temp);
            }
            n1.onblur = n2.onblur = function () {
                var temp = parseFloat(validDigits(n1.value)),
                    temp2 = parseFloat(validDigits(n2.value));
                if (temp) n1.value = addCommas(temp);
                if (temp2) n2.value = addCommas(temp2.toFixed(2));
            }
        }
    </script>

</asp:Content>

