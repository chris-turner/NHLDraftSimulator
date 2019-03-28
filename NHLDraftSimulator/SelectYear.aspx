<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectYear.aspx.cs" Inherits="NHLDraftSimulator.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>NHL Draft Simulator</title>
    <link href="https://fonts.googleapis.com/css?family=Rubik" rel="stylesheet"/>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="StyleSheet1.css" rel="stylesheet"/>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="jscode.js"></script>
    

    
  </head>
  <body>
    
      <form id="form1" runat="server">
    
  <div class="centerDiv">
    <h1>NHL Draft Simulator</h1>
  </div>
           <div class="row">
                <div class="col-2 col-sm-2 col-lg-2 ">

      </div>
    <div class="col-8 col-sm-8 col-lg-8 main maincontainer">
        Select A Draft Year
      <br/>
        
        <img src="Images/nhllogo.jpg" />
         <br/>
         <br/>
        <asp:DropDownList ID="ddlYears" runat="server" Width ="140px"></asp:DropDownList>
        <br/>
        <br/>
        Name Your Draft
         <br/>
      <asp:TextBox ID="draftNameTxtBox" runat="server" MaxLength="12" Width ="140px"></asp:TextBox>
        <br/>
         <br/>
         <br/>
        <asp:Button runat="server" Text="Continue" ID="btnContinue" OnClick="btnContinue_Click"  />
    </div>
          
    <div class="col-2 col-sm-2 col-lg-2 right">
     
    </div>

</div>  
      </form>
      <footer class="footer">
      <div class="container">
        <span class="text-muted">Chris Turner 2017 - still a work in progress - <a href="comingsoon.html">coming soon</a> - player info provided by TheDraftAnalyst.com</span>
      </div>
    </footer>
</body>
</html>

