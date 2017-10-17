using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHLDraftSimulator
{
    public partial class NHLDraftSim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Draft draft = new Draft();
                Session["players"] = draft.loadPlayers(Session["DraftYear"].ToString().Trim());
                Session["draftPicks"] = draft.loadPicks(Session["DraftYear"].ToString().Trim());
                DraftPick currentPick = getCurrentPick((DraftPick[])Session["draftPicks"]);
                checkIfUserPick(currentPick);
                

            }
          

            

        }

        public void checkIfUserPick(DraftPick currentPick)
        {
            if (!currentPick.IsUserTeam)
            {
                btnDraft.Visible = false;
                //System.Timers.Timer t = new System.Timers.Timer(100);
                //t.Start();
                
            }
            else
            {
                btnDraft.Visible = true;
            }

        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select PlayerID, P.PlayerFName, P.PlayerLName, P.Position, P.Nationality, P.Ranking"+
                " from Player as P where P.DraftYear = (select Draft.DraftYear from Draft where Draft.DraftID = '"+Session["DraftID"]+"' ) and P.PlayerID not in"+
                "(Select PlayerTakenID from Player_Draft_Pick where DraftID = '" + Session["DraftID"].ToString().Trim() + "' ) order by P.Ranking", con);
            PlayerSelectionGridView.DataSource = cmd.ExecuteReader();
            PlayerSelectionGridView.DataBind();
            con.Close();

            availablePlayers.Visible = true;

        }

        public void autoPick(DraftPick currentPick)
        {
            int playerID = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            //eventually write a more complex algorithm that takes Team needs / tendencies/ etc. into account. 
            SqlCommand cmd = new SqlCommand("select top 1 PlayerID, P.Ranking" +
                " from Player as P where P.DraftYear = (select Draft.DraftYear from Draft where Draft.DraftID = '" + Session["DraftID"].ToString().Trim() + "' ) and P.PlayerID not in" +
                "(Select PlayerTakenID from Player_Draft_Pick where DraftID = '" + Session["DraftID"].ToString().Trim() + "') order by P.Ranking", con);
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    playerID = rdr.GetInt32(0);
                }
            }
            con.Close();
            if (playerID != 0)
            {
                con.Open();
                cmd = new SqlCommand("insert into Player_Draft_Pick (PlayerTakenID, DraftID, DraftPickID) values (" + playerID + ",'" + Session["DraftID"] + "','" + currentPick.DraftPickID + "')", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Player[] players = (Player[])Session["players"];
                string pickName = "";
                foreach (Player player in players)
                {
                    if (player.PlayerID == playerID)
                    {
                        pickName = player.PlayerFName + " " + player.PlayerLName;
                    }
                }

                currentPickSelection.Visible = true;
                currentPickSelection.Text = currentPick.TeamName + " select " + pickName;
                currentPick.PlayerID = playerID;
                currentPick.PlayerName = pickName;
                //Thread.Sleep(5000);
                advancePick(currentPick);
            }

        }


        public DraftPick getCurrentPick(DraftPick[] draftPicks)
        {
            Guid currentPickID = Guid.Empty;
            int UserTeamID = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = 
                new SqlCommand("select top 1 DP.DraftPickID, DP.PickNum from DraftPick as DP where DraftPickID not in (Select DraftPickID from Player_Draft_Pick where DraftID = '" 
                + Session["DraftID"].ToString().Trim() + "') order by PickNum", con);

            using (var rdr = cmd.ExecuteReader())
            {
                    while(rdr.Read())
                    {
                        currentPickID = rdr.GetGuid(0);
                    }
            }
            con.Close();
            con.Open();
            cmd =
                new SqlCommand("select TeamID, UserID, DraftID from User_Team_Draft where UserID = '"+Session["UserID"]+"' and DraftID = '"+
                Session["DraftID"].ToString().Trim() + "'", con);

            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    UserTeamID = rdr.GetInt32(0);
                }
            }
            con.Close();

            DraftPick currentPick = null;
            foreach (DraftPick pick in draftPicks)
            {
                if (pick.DraftPickID.Equals(currentPickID))
                {
                    currentPick = pick;
                }

                if (pick.TeamID.Equals(UserTeamID))
                {
                    pick.IsUserTeam = true;
                }
                else
                {
                    pick.IsUserTeam = false;
                }
            }
            Session["CurrentPick"] = currentPick;
            DraftPick pick2 = currentPick.NextPick;
            DraftPick pick3 = pick2.NextPick;
            DraftPick pick4 = pick3.NextPick;

            if (currentPick != null)
            {
                TeamLogo.ImageUrl = "/Images/"+currentPick.ImageFileName+".gif";
                teamonclock.Text = currentPick.TeamName + " are on the clock.";

                pick1roundandpick.Text = "Round " + currentPick.Round + ", Pick #" + currentPick.PickInRound;
                pick1team.Text = currentPick.TeamName;

                pick2roundandpick.Text = "Round " + pick2.Round + ", Pick #" + pick2.PickInRound;
                pick2team.Text = pick2.TeamName;

                pick3roundandpick.Text = "Round " + pick3.Round + ", Pick #" + pick3.PickInRound;
                pick3team.Text = pick3.TeamName;

                pick4roundandpick.Text = "Round " + pick4.Round + ", Pick #" + pick4.PickInRound;
                pick4team.Text = pick4.TeamName;

                
            }
            return currentPick;


        }

        public void advancePick(DraftPick currentPick)
        {
            //figure out what to do with populating picks on sidebar
            currentPick = currentPick.NextPick;
            TeamLogo.ImageUrl = "/Images/" + currentPick.ImageFileName + ".gif";
            teamonclock.Text = currentPick.TeamName + " are on the clock.";
            Session["currentPick"] = currentPick;
            checkIfUserPick(currentPick);

        }


        protected void choosePlayer_Click(object sender, EventArgs e)
        {
            DraftPick currentPick = (DraftPick)Session["currentPick"];
            LinkButton btn = (LinkButton)(sender);
            int playerID = Convert.ToInt32(btn.CommandArgument);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Player_Draft_Pick (PlayerTakenID, DraftID, DraftPickID) values (" + playerID + ",'" + Session["DraftID"] + "','" + currentPick.DraftPickID + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Player[] players = (Player[])Session["players"];
            string pickName = "";
            foreach (Player player in players)
            {
                if (player.PlayerID == playerID)
                {
                    pickName = player.PlayerFName + " " + player.PlayerLName;
                }
            }

            currentPickSelection.Visible = true;
            currentPickSelection.Text = currentPick.TeamName + " select " + pickName;
            currentPick.PlayerID = playerID;
            currentPick.PlayerName = pickName;
            //Thread.Sleep(5000);
            PlayerSelectionGridView.DataSource = null;
            PlayerSelectionGridView.DataBind();
            advancePick(currentPick);
        }

        /*
        method for when timer expires or user chooses a player

        make pick
        display pick for 3 seconds
        advance pick (currentpick)
    */
        

        protected void viewFullResults_Click(object sender, EventArgs e)
        {

        }
    }

    
   
    


}