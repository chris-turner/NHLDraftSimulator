<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NHLDraftSim.aspx.cs" Inherits="NHLDraftSimulator.NHLDraftSim"  %>

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
	  
	  
	  
  
  
  <div class="row container-fluid">

      <div class="header">
    <h1>NHL Draft Simulator</h1>
  </div>
      <div class="col-2 col-sm-2 col-lg-2 ">

      </div>

   <div class="col-8 col-sm-8 col-lg-8 maincontainer" >
       
     <asp:Label ID="teamonclock" runat="server" Text="">TeamName are on the clock.</asp:Label>
      <br/>
        <asp:Image ID="TeamLogo" runat="server" ImageUrl="/Images/nhllogo.jpg" />
        <br/>

        <br/>
        <asp:Label ID="currentPickSelection" runat="server" Visible="false"></asp:Label>
      <br/>
        <asp:Button ID="btnDraft" runat="server" Text="Draft" OnClick="btnDraft_Click" />
        <br/>
         <asp:Panel ID="availablePlayers"  runat="server" Height="464px" Visible="false">
              <asp:GridView ID="PlayerSelectionGridView" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeader="false" GridLines="none" >

            <Columns>
                    <asp:BoundField DataField="Ranking"  SortExpression="Ranking" />
                 </Columns>
                  <Columns>
                <asp:BoundField DataField="Position"  SortExpression="Position" />
                       </Columns>
                   <Columns>
                <asp:BoundField DataField="PlayerFName"  SortExpression="PlayerFName" />
                       </Columns>
                  <Columns>
                <asp:BoundField DataField="PlayerLName"  SortExpression="PlayerLName" />
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
        
        
      <asp:Label ID="pick1roundandpick" runat="server" Text="Pick1">
      </asp:Label><br/>
      <asp:Label ID="pick1team" runat="server" Text="Pick1">
      </asp:Label><br/>
        <asp:Label ID="pick1playerName" runat="server" Text="">
      </asp:Label><br/>

      
      <hr/>
      
      <asp:Label ID="pick2roundandpick" runat="server" Text="Pick2">
      </asp:Label><br/>
      <asp:Label ID="pick2team" runat="server" Text="Pick2">
      </asp:Label><br/>
        <asp:Label ID="pick2playerName" runat="server" Text="">
      </asp:Label><br/>
      
      <hr/>
      
      <asp:Label ID="pick3roundandpick" runat="server" Text="Pick3">
      </asp:Label><br/>
      <asp:Label ID="pick3team" runat="server" Text="Pick3">
      </asp:Label><br/>
         <asp:Label ID="pick3playerName" runat="server" Text="">
      </asp:Label><br/>

      
      <hr/>
      
      <asp:Label ID="pick4roundandpick" runat="server" Text="Pick4">
      </asp:Label><br/>
      <asp:Label ID="pick4team" runat="server" Text="Pick4">
      </asp:Label><br/>
        <asp:Label ID="pick4playerName" runat="server" Text="">
      </asp:Label><br/>
        <hr/>
        <asp:LinkButton id="viewFullResults" class="btn btn-primary btn-md" runat="server" OnClick="viewFullResults_Click">View Full Draft Results</asp:LinkButton>
        <hr/>
    
      
    </div>
  
  
  </div>

	  
      </form>
</body>
</html>