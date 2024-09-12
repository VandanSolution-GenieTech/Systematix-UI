<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="noncash.aspx.cs" Inherits="noncash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12 mb-1">
            <div class="page-header row no-gutters py-4">
                <h3 class="page-title fontstyle">Non Cash</h3>
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
                    <h6 class="m-0">Non Cash</h6>
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue fontstyle">Client Code</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="clientcode" class="form-control" placeholder="ClientCode" Style="max-width: 200px; width: 200px;"></asp:TextBox>
                            </div>
                        </span>
                    </li>
                   <%-- <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue fontstyle">Exchange</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="Exchange" class="form-control" placeholder="Exchange" Style="max-width: 200px; width: 200px;"></asp:TextBox>
                            </div>
                        </span>
                    </li>--%>
                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue fontstyle">Amount</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:TextBox runat="server" ID="Amount" onkeypress="return isNumberOrDecimal(event)" class="form-control" placeholder="Amount" Style="max-width: 200px; width: 200px;"></asp:TextBox>
                            </div>
                        </span>
                    </li>
                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue fontstyle">Non Cash File</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div1" class=" input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                            <asp:FileUpload runat="server" ID="Non_cash" Style="margin-left: 150px;"  />
                                </div></span>
                    </li>
                    <div class="col-3 alertstyle">
                        <div class="message_alert" id="alertcontainer"></div>
                    </div>
                    <li class="list-group-item d-flex px-3">


                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <div id="Div3" class="input-group input-group-sm my-auto ml-auto mr-auto ml-sm-auto mr-sm-0" style="max-width: 350px;">
                                <asp:Button runat="server" ID="Button2" class="mb-2 btn button_brand mr-2"
                                    Text="Export" Onclick="Export_Click"  />&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="Submit" class="mb-2 btn button_brand mr-2"
                                    Text="Upload" OnClick="Non_cashbtn" />&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="Button1" class="mb-2 btn button_brand mr-2"
                                    Text="Submit" OnClick="Submit_Click" />
                                <asp:Label runat="server" ID="Lab" Text="" class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                            </div>
                        </span>

                    </li>

                </ul>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function isNumberOrDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>

