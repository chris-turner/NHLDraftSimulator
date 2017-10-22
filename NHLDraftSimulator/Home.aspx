<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NHLDraftSimulator.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>NHL Draft Simulator</title>
     <link href="https://fonts.googleapis.com/css?family=Rubik" rel="stylesheet"/>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="StyleSheet1.css" rel="stylesheet"/>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="jscode.js"></script>
    

    
  </head>
  <body>
    
      <form id="form1" runat="server">
    
  <div class="header">
    <h1>NHL Draft Simulator</h1>
  </div>
  
  <div class="row">
  
       <div class="col-2 col-sm-2 col-lg-2 ">

      </div>

    <div class="col-8 col-sm-8 col-lg-8 main maincontainer">
        Welcome, choose an option below.
      <br/>
         <br/>
        
        <img src="Images/nhllogo.jpg" />
        <br/>
        <br/>
        <asp:Button runat="server" Text="Start New Draft" ID="btnNewDraft" OnClick="btnNewDraft_Click"  />
        <br/>

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