<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmFtHoldingUpload.aspx.cs" Inherits="frmFtHoldingUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-lg-12 mb-1">
            <div class="page-header row no-gutters py-4">
                <h3 class="page-title">Data Export</h3>
            </div>
        </div>

    </div>


    <div class="row">
        <div class="col-lg-1 mb-3">
        </div>
        <div class="col-lg-10 mb-6">
            <div class="row" id="ExportBOD" runat="server">
                <div class="col-lg-6" >
                    <div class="card card-small mb-4">

                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0">Export BOD Holdings - Cash</h6>
                        </div>
                        <ul class="list-group list-group-flush">


                            <li class="list-group-item d-flex px-3">
                                <span class="text-semibold text-fiord-blue fontstyle">Holdings File</span>
                                <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <asp:FileUpload runat="server" Style="margin-left: 50px;" ID="CA" OnDataBinding="CA_DataBinding"
                                        OnDisposed="CA_Disposed" /></span>
                            </li>
                            <div class="col-3 alertstyle">
                                <div class="message_alert" id="alertcontainer"></div>
                            </div>
                            <li class="list-group-item d-flex px-3">

                                <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 700px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand "
                                     Text="Export Holding" OnClick="Submit_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <%-- <asp:Button runat="server" ID="Submit5" class="mb-2 btn button_brand " 
                                    Text="Export BTST Holding" onclick="Submit_Click6"/>--%>
                                        <asp:Button runat="server" ID="Button1" class="mb-2 btn button_brand "
                                            Text="Export eProtector Holding" OnClick="E_protector" />
                                        <asp:Label runat="server" ID="lblmsg" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                    </div>
                                </span>


                            </li>
                    </div>

                </div>
                <div class="col-lg-6">
                    <div class="card card-small mb-4">
                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0">Export BOD - Others</h6>
                        </div>




                        <li class="list-group-item d-flex px-3" style="height: 50px;">
                            <%-- <span class="text-semibold text-fiord-blue">PS03 Input File</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                        <asp:FileUpload runat="server" style="margin-left:30px;" ID="FileUpload1" ondatabinding="CA_DataBinding" 
                            ondisposed="CA_Disposed"/></span>--%>
                        
                        </li>

                        <li class="list-group-item d-flex px-3">

                            <span class="ml-auto text-right text-semibold text-reagent-gray">
                                <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button runat="server" ID="Button5" class="mb-2 btn button_brand "
                          Text="Dump for ODIN" OnClick="Submit_Click12" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button runat="server" ID="Button6" class="mb-2 btn button_brand mr-2"
                          Text="Export FNO Position" OnClick="Submit_Click2" />
                                    <%--<asp:Button runat="server" ID="Button6" class="mb-2 btn button_brand mr-2" 
                                    Text="Export PS03" onclick="Submit_Clickpo3"/>--%>
                                    <asp:Label runat="server" ID="Label1" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                </div>
                            </span>


                        </li>


                        </li>
                    
                    </div>
                </div>
            </div>
            <!-- Sliders & Progress Bars -->
            <div class="row">
                <div class="col-lg-6">
                    <div class="card card-small mb-4">
                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0 ">Export Client Wise DP Holdings</h6>
                        </div>
                        <div class="card-header border-bottom">
                            <label>
                                <div id="" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                    <asp:Label runat="server" Class="fontstyle" Style="margin-top: 10px;">All Clients DP Holding As of &nbsp;&nbsp;</asp:Label>
                                    <asp:TextBox runat="server" ID="WDate" type="text" class="input-sm form-control datepicker1" Style="width: 100px;" placeholder="Start Date"></asp:TextBox>
                                </div>
                            </label>


                        </div>
                        <li class="list-group-item d-flex px-3">

                            <span class="ml-auto text-right text-semibold text-reagent-gray">
                                <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">

                                    <asp:Button runat="server" ID="Button3" class="mb-2 btn button_brand mr-2"
                                        Text="Export DP Holdings" OnClick="Submit_Click3" />
                                    <asp:Label runat="server" ID="Label3" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                </div>
                            </span>


                        </li>
                    </div>

                </div>
                <div class="col-lg-6">
                    <div class="card card-small mb-4">
                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0 ">Export Off-Market DP Trades</h6>
                        </div>
                        <div class="card-header border-bottom">
                            <label>
                                <asp:DropDownList runat="server" ID="DropDownList2" Width="150px" Height="30px"
                                    class="input-sm form-control" AutoPostBack="true">
                                    <asp:ListItem Value="mod">Modified Date</asp:ListItem>
                                    <asp:ListItem Value="trf">Transfer Date</asp:ListItem>

                                </asp:DropDownList>
                            </label>
                            <label>
                                <div id="" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="width: 100px; max-width: 250px;">
                                    <asp:TextBox runat="server" ID="FDate" type="text" class="input-sm form-control datepicker1" placeholder="Start Date"></asp:TextBox>
                                </div>
                            </label>
                            <label>
                                <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="width: 100px; max-width: 250px;">
                                    <asp:TextBox runat="server" ID="TDate" type="text" class="input-sm form-control datepicker1" placeholder="End Date"></asp:TextBox>
                                </div>
                            </label>
                            <label>
                                <asp:DropDownList runat="server" ID="DropDownList1" Width="90px" Height="30px"
                                    class="input-sm form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="Exchange_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </div>
                        <li class="list-group-item d-flex px-3">

                            <span class="ml-auto text-right text-semibold text-reagent-gray">
                                <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 750px;">
                                    <asp:Button runat="server" ID="Button4" class="mb-2 btn button_brand mr-2" Style="margin-right: 100px;"
                                        Text="Export Audit Trail" OnClick="Submit_Click5" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                      
                    <asp:Button runat="server" ID="Button2" class="mb-2 btn button_brand mr-2"
                        Text="Export WS Format" OnClick="Submit_Click4" />

                                    <asp:Button runat="server" ID="DPtrade" Visible="false" class="mb-2 btn button_brand mr-2"
                                        Text="Export DP Trade" OnClick="Export_Click" />
                                    <asp:Label runat="server" ID="Label2" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                </div>
                            </span>


                        </li>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="card card-small mb-4">

                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0">Export Adhoc Limit</h6>
                        </div>
                        <ul class="list-group list-group-flush">


                            <li class="list-group-item d-flex px-3">
                                <span class="text-semibold text-fiord-blue fontstyle">MF Apporoved Script</span>
                                <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <asp:FileUpload runat="server" Style="margin-left: 50px; height: 45px;" ID="FileUploadMF" OnDataBinding="CA_DataBinding"
                                        OnDisposed="CA_Disposed" /></span>
                            </li>
                            <div class="col-3 alertstyle">
                                <div class="message_alert" id="alertcontainer"></div>
                            </div>
                            <li class="list-group-item d-flex px-3">

                                <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 700px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button runat="server" ID="Button7" class="mb-2 btn button_brand "
                                 Text="Export Adhoc Limit" OnClick="Adhoc_Limit" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <%-- <asp:Button runat="server" ID="Button10" class="mb-2 btn button_brand "
                                Text="Export Adhoc Limit" OnClick="mf_export" />--%>
                                    </div>
                                </span>


                            </li>
                    </div>

                </div>
                <div class="col-lg-6">
                    <div class="card card-small mb-4">

                        <div class="card-header border-bottom gridheader">
                            <h6 class="m-0">Export Allocation</h6>
                        </div>
                        <ul class="list-group list-group-flush">

                            <div class="row">
                                <div class="col-6" style="height: 10px;">

                                    <li class="list-group-item d-flex px-3">
                                        <span class="text-semibold text-fiord-blue fontstyle">Cash Allocation</span>

                                    </li>
                                </div>
                                <div class="col-6">
                                    <li class="list-group-item d-flex px-3">
                                        <span class="text-semibold text-fiord-blue fontstyle">FO Allocation</span>

                                    </li>
                                </div>
                                <div class="col-6">

                                    <li class="list-group-item d-flex px-3">
                                        <span class="text-semibold text-fiord-blue"></span>
                                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                                            <asp:FileUpload runat="server" Style="margin-left: 10px;" ID="FileUploadcash" OnDataBinding="CA_DataBinding"
                                                OnDisposed="CA_Disposed" /></span>
                                    </li>
                                </div>
                                <div class="col-6">
                                    <li class="list-group-item d-flex px-3">
                                        <span class="text-semibold text-fiord-blue"></span>
                                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                                            <asp:FileUpload runat="server" Style="margin-left: 1px;" ID="FileUploadFO" OnDataBinding="CA_DataBinding"
                                                OnDisposed="CA_Disposed" /></span>
                                    </li>
                                </div>
                            </div>
                            <div class="col-3 alertstyle">
                                <div class="message_alert" id="alertcontainer"></div>
                            </div>
                            <li class="list-group-item d-flex px-3">

                                <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 700px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                         <asp:Button runat="server" ID="Button8" class="mb-2 btn button_brand " Text="Cash Allocation" OnClick="cash_allocation" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                         <asp:Button runat="server" ID="Button9" class="mb-2 btn button_brand " Text="FO Allocation" OnClick="Fo_allocation" />
                                         <asp:Label runat="server" ID="Label5" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                    </div>
                                </span>


                            </li>
                    </div>

                </div>
                <div class="col-lg-6" runat="server" visible="false">
                    <div class="card card-small mb-4">
                        <div class="card-header border-bottom">
                            <h6 class="m-0">Export ORM</h6>
                        </div>
                        <div class="card-header border-bottom">
                            <label>
                                <div id="" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                    <asp:Label runat="server" Class="fontstyle">ORM Report As of &nbsp;&nbsp;</asp:Label>
                                    <asp:TextBox runat="server" ID="ORMTXT" type="text" class="input-sm form-control datepicker1" Style="width: 100px;" placeholder="Start Date"></asp:TextBox>
                                </div>
                            </label>


                        </div>
                        <li class="list-group-item d-flex px-3">

                            <span class="ml-auto text-right text-semibold text-reagent-gray">
                                <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">

                                    <asp:Button runat="server" ID="ORMTXTBTN1" class="mb-2 btn button_brand mr-2"
                                        Text="Export ORM TXT" OnClick="ORMTXT_Click" />
                                    <asp:Button runat="server" ID="ORMTXTBTN2" class="mb-2 btn button_brand mr-2"
                                        Text="Export ORM EXCEL" OnClick="ORMEXCEL_Click" />
                                    <asp:Label runat="server" ID="Label4" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                                </div>
                            </span>


                        </li>
                    </div>

                </div>

            </div>

        </div>







        </ul>
    </div>


    </div>
    

    <%-- start repeater --%>














    <style>
        .message_alert {
            width: 25%;
            position: fixed;
            top: 0;
            z-index: 100000;
            padding: 10px;
            font-size: 15px;
        }

        .alertstyle {
            left: 100%;
            position: absolute;
        }
    </style>



    <script type="text/javascript">
        //Bootstrap Notification
        function ShowMessage(message, messagetype) {
            var css;
            switch (messagetype) {
                case 'Success':
                    css = 'alert alert-success'
                    break;
                case 'Error':
                    css = 'alert alert-danger'
                    break;
                case 'Warning':
                    css = 'alert alert-dark'
                    break;
                default:
                    css = 'alert alert-info'
            }
            $('#alertcontainer').append('<div id="alert_div" style="margin:0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"class= "alert fade in' + css + '" ><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong><span>' + message + '</span></div > ');

            setTimeout(function () {
                $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 100);
        }
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

    <script>
        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x[1];
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
                x1 = x1
            }
            return x1 + '.' + x2;
        }

        $(function () {
            $(".Comma").each(function (c, obj) {
                $(obj).text(addCommas(parseFloat($(obj).text()).toFixed(2)));
            });
        });
    </script>


</asp:Content>

