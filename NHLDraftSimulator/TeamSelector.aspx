<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamSelector.aspx.cs" Inherits="NHLDraftSimulator.TeamSelector" EnableEventValidation="false" %>

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
                     <div class="col-2 col-sm-2 col-lg-2 ">

      </div>
    <div class="col-8 col-sm-8 col-lg-8 main maincontainer">
        Choose your team
      <br/>


        <asp:ListView ID="TeamListView" runat="server" HorizontalAlign="center">

            <ItemTemplate >

                        <asp:ImageButton ID="teamImage" runat="server" ImageUrl= '<%# "Images\\"+Eval("ImageFileName")+".gif" %>' 
                            OnClick="teamImage_onclick" CommandArgument='<%# Eval("TeamID") %>' height="75px" Width="100px" />

                      </ItemTemplate>
        </asp:ListView>
    </div>


    <div class="col-2 col-sm-2 col-lg-2 right">
      
    </div>
    

</div>  
      </form>
</body>
</html>

