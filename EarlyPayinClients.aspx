<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EarlyPayinClients.aspx.cs" Inherits="EarlyPayinClients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12 mb-1">
            <div class="page-header row no-gutters py-4">
                <h3 class="page-title">Early PayIn Client</h3>
            </div>
        </div>

    </div>


    <div class="row">
        <div class="col-lg-3 mb-4">
        </div>
        <div class="col-lg-6 mb-4">
            <!-- Sliders & Progress Bars -->
            <div class="card card-small mb-4">
                <div class="card-header border-bottom">
                    <h6 class="m-0">Early PayIn Client</h6>
                </div>
                <ul class="list-group list-group-flush">


                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue">ClientCode</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="clientcode" class="form-control" placeholder="" Style="max-width: 200px; width: 200px;"></asp:TextBox>
                            </div>
                        </span>
                    </li>

                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue">File</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:FileUpload runat="server" Style="margin-left: 10px;" ID="FileUpload"/>
                            </div>
                        </span>
                    </li>

           
                    <div class="col-3 alertstyle">
                        <div class="message_alert" id="alertcontainer"></div>
                    </div>
                    <li class="list-group-item d-flex px-3">
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand " Text="Submit" OnClick="Submit_Click" />
                            <asp:Button runat="server" ID="Upload" class="mb-2 btn button_brand " Text="Upload" OnClick="Upload_Click" />
                            <asp:Label runat="server" ID="Lab" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                            <%--                        <button class="mb-2 btn btn-sm btn-outline-success mr-1" type="button">Submit</button>--%>
                        </span>
                    </li>



                </ul>
            </div>

        </div>
    </div>

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
