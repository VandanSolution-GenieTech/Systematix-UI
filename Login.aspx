<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Systematix</title>
	<style>
		.login100-form-btn:hover{
			background-color:#FF7553 ;
		}
	</style>
	<meta charset="UTF-8">
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon[37].ico" />
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->	
	<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">
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
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Public+Sans&display=swap" rel="stylesheet">
	
</head>
<body>
	<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<div class="login100-form-title" style="background-color:#1e1250;">
                    <img src="images/logo.png" style="margin-top: 10px;  height:90px; width:238px; margin-bottom: 10px;" alt="Logo"/>
				</div>

				<form  class="login100-form validate-form" runat="server" id="Form2">
                   
                    <h1 class="login_hd" style="font-family:'Public Sans', sans-serif;">
                        Welcome to <strong style="color:#f7a600;"><br />Systematix Login </strong><br />
                        <span runat="server" id="logoutlab" visible="false" style="font-family: 'Public Sans', sans-serif; color:Red">You have been successfully logged out. <br /> Please login again.</span>
						 <span runat="server" id="Timeout1" visible="false" style="font-family: 'Public Sans', sans-serif; color:Red">You have been auto logged out due to Session Expirey.<br /> Please login again.</span>
                    </h1>

					<div class="wrap-input100 validate-input m-b-26" data-validate="Username is required" style="font-family: 'Public Sans', sans-serif;">
						<label class="">User Id</label>
						<asp:TextBox runat="server" ID="User" class="input100" placeholder="Enter Your Id"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input m-b-18" data-validate = "Password is required" style="font-family: 'Public Sans', sans-serif;">
						<label class="">Password</label>
						<asp:TextBox runat="server" ID="Password" type="password"  class="input100" placeholder="Enter Password"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>

 

					<div class="container-login100-form-btn" style="z-index: 100 !important; padding-left: 22%;">


						<asp:Button runat="server" ID="Log" style="background-color:#1e1250; color:white;" class="login100-form-btn " Text="Login" 
                            onclick="Log_Click" />
<%--                                                <asp:Button Visible="false" runat="server" ID="Button1"  class="login100-form-btn" 
                            Text="Test" onclick="Button1_Click" 
                            />--%>
                           
<%--                    <div class="flp col-md-2 col-sm-5 col-xs-3 pull-right">
                    <a href="FrmForgetPassword.aspx" >Forgot Password</a>						
                    </div>--%>

                    <a href="FrmForgetPassword.aspx" style="color:#1e1250; font-family: 'Public Sans', sans-serif; padding-top:10px; padding-left:10px;" ><b><u>Forgot / Reset Password</u></b></a>						

                           </div>


				</form>
                    <table width="100%" style="background-color:#1e1250; height:30px;">
                    <tr>

                    <td style="text-align:center;padding:0px 10px 0px 10px;">
						<a style="color:white;font-family:'Public Sans', sans-serif;" href="#" target="_blank"><strong><h6>Terms and Conditions</h6></strong></a>
                    </td>

                    </tr>
                    
                    </table>


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
