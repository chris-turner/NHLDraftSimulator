<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NHLDraftSim.aspx.cs" Inherits="NHLDraftSimulator.NHLDraftSim" %>

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
    
  <div class="header">
    <h1>NHL Draft Simulator</h1>
  </div>
  
  <div class="row">
    <div class="col-12 col-sm-2 col-lg-2 left">
        
        
      <asp:Label ID="pick1roundandpick" runat="server" Text="Pick1">
      </asp:Label><br/>
      <asp:Label ID="pick1team" runat="server" Text="Pick1">
      </asp:Label><br/>
      
      <hr/>
      
      <asp:Label ID="pick2roundandpick" runat="server" Text="Pick2">
      </asp:Label><br/>
      <asp:Label ID="pick2team" runat="server" Text="Pick2">
      </asp:Label><br/>
      
      <hr/>
      
      <asp:Label ID="pick3roundandpick" runat="server" Text="Pick3">
      </asp:Label><br/>
      <asp:Label ID="pick3team" runat="server" Text="Pick3">
      </asp:Label><br/>
      
      <hr/>
      
      <asp:Label ID="pick4roundandpick" runat="server" Text="Pick4">
      </asp:Label><br/>
      <asp:Label ID="pick4team" runat="server" Text="Pick4">
      </asp:Label><br/>
      
      <hr/>
    </div>


    <div class="col-12 col-sm-8 col-lg-8 main maincontainer">
        TeamName are on the clock.
      <br/>
        
        <img src="Images/nhllogo.jpg" />
        <br/>
        <br/>
        <asp:Button runat="server" Text="Draft" OnClick="Unnamed1_Click" />
        <br/>
         <asp:Panel ID="Panel1" runat="server" Height="464px" Visible="false">


            test</asp:Panel>

    </div>

     
    

</div>  
      </form>
</body>
</html>
