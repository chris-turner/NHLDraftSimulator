<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NHLDraftSim.aspx.cs" Inherits="NHLDraftSimulator.NHLDraftSim"  %>

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
	  
	  
	  
  
  
  <div class="row container-fluid">

      <div class="header">
    <h1 id="pageheader" runat="server">NHL Draft Simulator</h1>
  </div>
      <div class="col-2 col-sm-2 col-lg-2 ">

      </div>

   <div class="col-8 col-sm-8 col-lg-8 maincontainer" >
       
     <asp:Label ID="teamonclock" runat="server" Text="">TeamName are on the clock.</asp:Label>
      <br/>
        <asp:Image ID="TeamLogo" runat="server" ImageUrl="/Images/nhllogo.jpg" />
        <br/>

       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer" runat="server" OnTick="Timer_Tick" Interval="1000">
       </asp:Timer>
        <br/>
        <label id="timerLabel" runat="server" Visible="False">0:00</label>
      <br/>
        <br/>
        <asp:Label ID="currentPickSelection" runat="server" Visible="false"></asp:Label>
      <br/>
        <asp:Button ID="btnDraft" runat="server" Text="Draft" OnClick="btnDraft_Click" />
        <br/>
        <asp:Button ID="btnResume" runat="server" Text="Resume" Visible="false" OnClick="btnResume_Click" />
         <asp:Panel ID="availablePlayers"  runat="server" Height="464px" Visible="false" ScrollBars="Vertical">
              <asp:GridView class="playergrid" ID="PlayerSelectionGridView" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" ShowHeader="True" GridLines="none"  >

            <Columns>
                    <asp:BoundField DataField="Ranking"  SortExpression="Ranking" >  <HeaderStyle Width="200px" /> </asp:BoundField>

                 </Columns>
                  <Columns>
                <asp:BoundField DataField="Position"  SortExpression="Position" />
                       </Columns>
                  <Columns>
                <asp:BoundField DataField="Name"  SortExpression="PlayerLName" />
                       </Columns>
                  <Columns>
                <asp:BoundField DataField="Nationality"  SortExpression="Nationality" />
                       </Columns>
                  <Columns>
                <asp:TemplateField >
                <ItemTemplate >
                        <asp:LinkButton id="choosePlayer" class="btn btn-primary btn-lg" runat="server" OnClick="choosePlayer_Click" CommandArgument= '<%# Eval("PlayerID") %>'>Select</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
               </Columns>

        </asp:GridView>

           </asp:Panel>
   
   </div>

   
    <div class="col-2 col-sm-2 col-lg-2 right">
        
        
      <asp:Label ID="pick1roundandpick" runat="server" Text="Pick1"> </asp:Label><br/>
      <asp:Label ID="pick1team" runat="server" Text="Pick1"> </asp:Label><br/>
        <asp:Label ID="pick1playerName" runat="server" Text=""> </asp:Label><br/>

      
      <hr/>
      
      <asp:Label ID="pick2roundandpick" runat="server" Text="Pick2"> </asp:Label><br/>
      <asp:Label ID="pick2team" runat="server" Text="Pick2"> </asp:Label><br/>
        <asp:Label ID="pick2playerName" runat="server" Text=""> </asp:Label><br/>
      
      <hr/>
      
      <asp:Label ID="pick3roundandpick" runat="server" Text="Pick3"> </asp:Label><br/>
      <asp:Label ID="pick3team" runat="server" Text="Pick3"> </asp:Label><br/>
         <asp:Label ID="pick3playerName" runat="server" Text=""> </asp:Label><br/>

      
      <hr/>
      
      <asp:Label ID="pick4roundandpick" runat="server" Text="Pick4"> </asp:Label><br/>
      <asp:Label ID="pick4team" runat="server" Text="Pick4"> </asp:Label><br/>
        <asp:Label ID="pick4playerName" runat="server" Text=""> </asp:Label><br/>
        <hr/>
        <asp:LinkButton id="viewFullResults" class="btn btn-primary btn-md" runat="server" OnClick="viewFullResults_Click">View Full Draft Results</asp:LinkButton>
        <hr/>
        <asp:LinkButton id="simToUserPickbtn" class="btn btn-primary btn-md" runat="server" OnClick="btnSim_Click">Sim to User Pick</asp:LinkButton>
        <hr/>
      
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