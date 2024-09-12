<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Fangeble.aspx.cs" Inherits="Fangeble" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="main-content-container container-fluid">
            <!-- Page Header -->


<div class="row">
 <div class="col-lg-10 mb-1">

            <div class="page-header row no-gutters py-3">
				<h3 class="page-title fontstyle">Margin Limit</h3><span class="page-subtitle" style="vertical-align:text-bottom;">&nbsp;(Amt in ₹)</span> 
            </div>
			


</div>
 <div class="col-lg-2 mb-1">

 
</div>
</div>

           
			<!-- Sales Report -->
			


            <div class="row mt-4">


 <div class="col-lg-6 col-md-12 col-sm-12 mb-4">
                <div class="card card-small h-100">
                  <div class="card-header border-bottom">
                    <h6 class="m-0">Summary</h6>
                  </div>
                  <div class="card-body p-0">
                    <ul class="list-group list-group-small list-group-flush">
                      <li class="list-group-item d-flex px-3" style="height:55px;">
                        <span class="text-semibold text-fiord-blue fontstyle">Current Balance</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray"><div class="plusmin"><%= CurrentBalance %></div></span>
                      </li>
                       <li class="list-group-item d-flex px-3" style="height:55px;">
                        <span class="text-semibold text-fiord-blue fontstyle">Utilization</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray" id="number2"><div class="plusmin"><%= Utilization %></div></span>
                      </li>
                      <li class="list-group-item d-flex px-3" style="height:55px;">
                        <span class="text-semibold text-fiord-blue fontstyle">Total Free</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray"><div class="Comma"><%= TotalFree %></div></span>
                      </li>
                                           <li class="list-group-item d-flex px-3"  style="height:55px;">
                                   <span class="text-semibold text-fiord-blue fontstyle">Free Limit</span>
                                    <span class="ml-auto text-right text-semibold text-reagent-gray">
                                    <asp:TextBox runat="server"  ID="TextBox1" autocomplete="off" AutoCompleteType="Disabled" class="form-control" style="text-align:right;" placeholder="Free Limit" Width="210px" ></asp:TextBox>
                                   </span>
                                </li>
                    </ul>
                  </div>
                </div>
              </div>



           				<div class="col-lg-6 mb-4">
                <!-- Sliders & Progress Bars -->
                <div class="card card-small mb-4">


                  <div class="card-header border-bottom">
                   <h6 class="m-0">Allocation</h6>
                  </div>

                  <ul class="list-group list-group-flush">


                    <li class="list-group-item d-flex px-3" style="height:55px;">
                       <span class="text-semibold text-fiord-blue fontstyle">Type</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray w-50">
                        
                        <asp:DropDownList runat="server" ID="Exchange"  class="custom-select w-75" AutoPostBack="true"  >
                            <asp:ListItem runat="server" Text="Up Stream" Value="Up"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Down Stream" Value="down"></asp:ListItem>
                        </asp:DropDownList>
                        
                       
                        </span>
                    </li>

                   


                       <li class="list-group-item d-flex px-3"  style="height:55px;">
    <span class="text-semibold text-fiord-blue fontstyle">Amount</span>
     <span class="ml-auto text-right text-semibold text-reagent-gray">
     <asp:TextBox runat="server"  ID="Amt" class="form-control" autocomplete="off" AutoCompleteType="Disabled" style="text-align:right;" placeholder="Amount" Width="210px" ></asp:TextBox>
    </span>
 </li>

                    

                    <li class="list-group-item d-flex px-3"  style="height:55px;">
                       <span class="text-semibold text-fiord-blue fontstyle">Remark</span>
                        <span class="ml-auto text-right text-semibold text-reagent-gray">
                        <asp:TextBox runat="server" id="Remark" class="form-control" autocomplete="off" AutoCompleteType="Disabled" style="text-align:right;" placeholder="Remark" Width="210px" ></asp:TextBox>
                       </span>
                    </li>

                  <li class="list-group-item d-flex px-3">
                  <span class="ml-auto text-right text-semibold text-reagent-gray">
                  <asp:Button runat="server" ID="Submit" onclick="Submit_Click"
                          class="mb-2 btn button_brand mr-2"  Text="Submit"  Width="200px" 
                          />
                          <asp:Label runat="server" ID="Lab" Text=""  class="text-semibold text-fiord-blue" Visible="false"></asp:Label>
                  </span>
                    </li>
                  </ul>
                </div>

                </div>



            </div>

            
			<div class="card card-small mb-4">			
				<div class="card-body p-0 pb-3">
						<div class=" ">
							<div class="dataTables_length">
                           <table id="example" class="table mb-0 stripe row-border order-column" style="width:100%">
					  
                      <thead class="bg-light">
							<tr class="gridheader">
							  <th style="text-transform:capitalize;" scope="col" class="border-0 txt_left fontstyle">Datetime</th>
							  <th style="text-transform:capitalize;" scope="col" class="border-0 txt_left fontstyle">Type</th>
							  <th style="text-transform:capitalize;" scope="col" class="border-0 txt_left fontstyle">Amount</th>
							  <th style="text-transform:capitalize;" scope="col" class="border-0 txt_left fontstyle">Remark</th>
							</tr>
						  </thead>

                    
                     

						  <tbody>

          <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
							<tr>
							  <th class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Date")%></th>
							  <th class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Type")%></th>
							  <th class="txt_Right fontstyle"><%# DataBinder.Eval(Container.DataItem, "Amount")%></th>
							  <th class="txt_left fontstyle"><%# DataBinder.Eval(Container.DataItem, "Remark")%></th>
							 
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


      
		  
 <script type="text/C#">


     function ConvertCSV(nStr) {
         let x = String(nStr).split('.');
         let x1 = x[0];
         let x2 = x[1] || '0';

         let lastThree = x1.substring(x1.length - 3);
         let otherNumbers = x1.substring(0, x1.length - 3);

         if (otherNumbers !== '' && otherNumbers !== '-') {
             lastThree = ',' + lastThree;
         }

         let res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree;
         return res + '.' + x2;

     }


     function MakePosNeg() {
         var TDs = document.querySelectorAll('.plusmin');

         $(".plusmin").each(function (c, obj) {
             $(obj).text(ConvertCSV(parseFloat($(obj).text()).toFixed(2)));
         });

         for (var i = 0; i < TDs.length; i++) {
             var temp = TDs[i];
             if (temp.firstChild.nodeValue.indexOf('-') == 0) { temp.className = "negative"; }
             else { temp.className = "positive"; }
         }



     }

     onload = MakePosNeg()
 </script>

<script type="text/javascript">
    function openInNewTab() {
        window.document.forms[0].target = '_blank';
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
    }
</script>

</asp:Content>

