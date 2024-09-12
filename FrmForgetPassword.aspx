<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmForgetPassword.aspx.cs" Inherits="FrmForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fuel</title>
	<meta charset="UTF-8">
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon[37].ico" />
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="images/icons/favicon[37].ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">s
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="css/util.css">
	<link rel="stylesheet" type="text/css" href="css/main.css">
<!--===============================================================================================-->
	<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="">
<link href="https://fonts.googleapis.com/css2?family=Public+Sans&display=swap" rel="stylesheet">
</head>
<body>

	<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<div class="login100-form-title" style="background-color:#1e1250;">
                    <img src="images/logo.png"  style="margin-top: 10px;  height:103px; width:238px; margin-bottom: 10px;" alt="Logo"/>
				</div>

				<form  class="login100-form validate-form" runat="server" id="Form2">
                   
                    <h1 class="login_hd">
                        
                        <span><strong>Reset Password</strong></span>
                        <span runat="server" id="logoutlab" visible="false" style="font-family:'Public Sans', sans-serif; color:Red">Please change your default password before logging in</span>
                    </h1>

					<div class="wrap-input100 validate-input m-b-10" data-validate="Username is required">
						<asp:TextBox runat="server" ID="User" class="input100" placeholder="Please enter User Id"></asp:TextBox>
						<span class="focus-input100"></span> 
					</div>

                   <div class="container-login100-form-btn m-b-30">
						<asp:Button runat="server" ID="Button1" style="background-color:#1e1250; height:40px; "  
                            class="login100-form-btn" Text="Send OTP" onclick="Button1_Click" 
                            />
                            <asp:Label runat="server" ID="Mobno" Visible="false" style="font-size:small; padding-left:10px; padding-top:10px;" ></asp:Label>
					</div>



                    <div class="wrap-input100 m-b-18">
						<asp:TextBox runat="server" ID="TextBox2" class="input100" placeholder="Enter OTP"></asp:TextBox>
						<span class="focus-input100"></span> 
					</div>

					<div class="wrap-input100 m-b-18">
						<asp:TextBox runat="server" ID="Password" type="password"  class="input100" placeholder="Enter New password"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 m-b-18">
						<asp:TextBox runat="server" ID="TextBox1" type="password"  class="input100" placeholder="Reconfirm New Password"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>                
                

					<div class="container-login100-form-btn">
						<asp:Button runat="server" ID="Log" style="background-color:#1e1250;" class="login100-form-btn" Text="Reset" onclick="Log_Click" 
                            />

                    <div class="flp col-md-2 col-sm-5 col-xs-3 pull-right">
                    <asp:Button runat="server" ID="Button2" style="background-color:#1e1250;" 
                            class="login100-form-btn" Text="Cancel" onclick="Button2_Click"/>
					
                    </div>
					</div>

				</form>
<%--                    <table width="100%" style="background-color:#34b350;">
                    <tr>
                    <td style="text-align:left;padding:0px 10px 0px 10px;">
                    <a style="color:White" href="assets/Disclaimer.pdf"><h6><strong>Disclaimer</strong></h6></a>
                    </td>
                    <td style="text-align:center;padding:0px 10px 0px 10px;">
                    <a style="color:White" href="assets/PrivacyPolicy.pdf"><strong><h6>Privacy Policy</strong></h6></a>
                    </td>
                   <td style="text-align:right;padding:0px 10px 0px 10px;">
                    <a style="color:White" href="assets/TermsofUse.pdf"><strong><h6>Terms Of Use</strong></h6></a>
                    </td>
                    </tr>
                    
                    </table>
--%>

			</div>
		</div>
	</div>
<!--===============================================================================================-->
	<script src="vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/bootstrap/js/popper.js"></script>
	<script src="vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/daterangepicker/moment.min.js"></script>
	<script src="vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="js/main.js"></script>
</body>
</html>
