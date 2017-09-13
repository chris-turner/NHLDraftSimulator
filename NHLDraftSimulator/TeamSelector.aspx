<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamSelector.aspx.cs" Inherits="NHLDraftSimulator.TeamSelector" %>

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
      
    </div>


    <div class="col-12 col-sm-8 col-lg-8 main maincontainer">
        Choose your team
      <br/>
        <asp:GridView ID="TeamImageGridView" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeader="false" GridLines="None" >

            <Columns>
                    <asp:BoundField DataField="TeamName"  SortExpression="TeamName" />
                    <asp:TemplateField >
                      <ItemTemplate >

                        <asp:Image ID="Image1" runat="server" ImageUrl= '<%# "Images\\"+Eval("ImageFileName")+".gif" %>' height="120px" Width="150px" />

                      </ItemTemplate>
                        
                        
                    </asp:TemplateField>
                <asp:TemplateField >
                <ItemTemplate >
                        <asp:LinkButton id="teambutton" class="btn btn-primary btn-lg" runat="server" OnClick="btnChoose_onclick" CommandArgument= '<%# Eval("TeamID") %>'>Select</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

        </asp:GridView>
        
        <br/>
        <asp:Button runat="server" Text="Draft" />
        <br/>

    </div>

     
    

</div>  
      </form>
</body>
</html>

