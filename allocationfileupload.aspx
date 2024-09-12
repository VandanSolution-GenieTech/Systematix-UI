<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="allocationfileupload.aspx.cs" Inherits="allocationfileupload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
     <div class="col-lg-12 mb-1">
         <div class="page-header row no-gutters py-4">
             <h3 class="page-title">File Upload</h3>
         </div>
     </div>
         <div class="col-3"></div>
             <div class="col-lg-6">
    <div class="card card-small mb-4">

        <div class="card-header border-bottom gridheader">
            <h6 class="m-0">Export Allocation</h6>
        </div>
        <ul class="list-group list-group-flush">

            <div class="row">
                
                <div class="col-6">

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
                            <asp:FileUpload runat="server" Style="margin-left: 10px;" ID="FileUploadcash"
                                 /></span>
                    </li>
                </div>

                <div class="col-6">
                    <li class="list-group-item d-flex px-3">
                        <span class="text-semibold text-fiord-blue"></span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                            <asp:FileUpload runat="server" Style="margin-left: 1px;" ID="FileUploadFO"
                                 /></span>
                    </li>
                </div>
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
        </ul>
    </div>

</div>
 </div>
    
    
</asp:Content>

