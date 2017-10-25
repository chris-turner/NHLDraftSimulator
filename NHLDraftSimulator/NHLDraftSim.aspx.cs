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
                Session["CurrentPick"] = currentPick;
                checkIfUserPick(currentPick);


            }




        }

        public void checkIfUserPick(DraftPick currentPick)
        {
            if (!currentPick.IsUserTeam)
            {
                btnDraft.Visible = false;
                Timer.Interval = 2000;

            }
            else
            {
                Timer.Interval = 60000;
                btnDraft.Visible = true;
            }

        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            availablePlayers.ScrollBars = ScrollBars.Auto;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select PlayerID, P.PlayerFName + ' ' + P.PlayerLName as 'Name', P.Position, P.Nationality, P.Ranking" +
                " from Player as P where P.DraftYear = (select Draft.DraftYear from Draft where Draft.DraftID = '" + Session["DraftID"] + "' ) and P.PlayerID not in" +
                "(Select PlayerTakenID from Player_Draft_Pick where DraftID = '" + Session["DraftID"].ToString().Trim() + "' ) order by P.Ranking", con);
            PlayerSelectionGridView.DataSource = cmd.ExecuteReader();
            PlayerSelectionGridView.DataBind();
            con.Close();
            availablePlayers.Visible = true;

        }

        public void autoPick(DraftPick currentPick)
        {
            if (currentPick.PlayerName == null)
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
                    btnDraft.Visible = false;
                    currentPickSelection.Visible = true;
                    currentPickSelection.Text = currentPick.TeamName + " select " + pickName;
                    currentPick.PlayerID = playerID;
                    currentPick.PlayerName = pickName;
                    Session["CurrentPick"] = currentPick;
                }
            }

        }


        public DraftPick getCurrentPick(DraftPick[] draftPicks)
        {
            int currentPickID = 0;
            int UserTeamID = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select top 1 DP.DraftPickID, DP.PickNum from DraftPick as DP where DraftPickID not in (Select DraftPickID from Player_Draft_Pick where DraftID = '"
                + Session["DraftID"].ToString().Trim() + "') order by PickNum", con);

            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    currentPickID = rdr.GetInt32(0);
                }
            }
            con.Close();
            con.Open();
            cmd =
                new SqlCommand("select TeamID, UserID, DraftID from User_Team_Draft where UserID = '" + Session["UserID"] + "' and DraftID = '" +
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
            if (currentPick != null)
            {
                TeamLogo.ImageUrl = "/Images/" + currentPick.ImageFileName + ".gif";
                teamonclock.Text = currentPick.TeamName + " are on the clock.";
                populateSidebar(currentPick);
            }

                return currentPick;


        }

        public void advancePick(DraftPick currentPick)
        {
            //figure out what to do with populating picks on sidebar
            currentPickSelection.Text = "";
            currentPick = currentPick.NextPick;
            TeamLogo.ImageUrl = "/Images/" + currentPick.ImageFileName + ".gif";
            teamonclock.Text = currentPick.TeamName + " are on the clock.";
            Session["CurrentPick"] = currentPick;
            checkIfUserPick(currentPick);

        }


        protected void choosePlayer_Click(object sender, EventArgs e)
        {
            availablePlayers.ScrollBars = ScrollBars.None;
            DraftPick currentPick = (DraftPick)Session["CurrentPick"];
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
            PlayerSelectionGridView.DataSource = null;
            PlayerSelectionGridView.DataBind();
            btnDraft.Visible = false;
            Timer.Interval = 4500;


        }
        

        protected void viewFullResults_Click(object sender, EventArgs e)
        {
            Timer.Enabled = false;
            btnDraft.Visible = false;
            btnResume.Visible = true;
            availablePlayers.Visible = false;
            Response.Write("<script>");
            Response.Write("window.open('DraftResults.aspx','_blank')");
            Response.Write("</script>");

        }

        protected void Timer_Tick(object sender, EventArgs e)
        {
            DraftPick currentPick = (DraftPick)Session["CurrentPick"];
            
            

            if (currentPick.PlayerName == null || currentPick.NextPick != null)
            {
                populateSidebar(currentPick);
                if (currentPick.PlayerName == null)
                {
                    autoPick(currentPick);
                    Timer.Interval = 4500;

                }
                else
                {
                    advancePick(currentPick);
                }
            }
            else
            {
                pick1playerName.Text = currentPick.PlayerName;
                teamonclock.Text = "The draft has ended.";
                Timer.Enabled = false;
            }
            
        }
        public void populateSidebar(DraftPick currentPick)
        {
            DraftPick pick2 = null;
            DraftPick pick3 = null;
            DraftPick pick4 = null;
            if (currentPick.NextPick != null)
            {
                pick2 = currentPick.NextPick;
                if (pick2.NextPick != null)
                {
                    pick3 = pick2.NextPick;

                    if (pick3.NextPick != null)
                    {
                        pick4 = pick3.NextPick;
                    }

                    else
                    {
                        pick4roundandpick.Text = "";
                        pick4team.Text = "";
                        pick4playerName.Text = "";
                    }
                }
                else
                {
                    pick3roundandpick.Text = "";
                    pick3team.Text = "";
                    pick3playerName.Text = "";

                    pick4roundandpick.Text = "";
                    pick4team.Text = "";
                    pick4playerName.Text = "";
                }

            }
            else
            {
                pick2roundandpick.Text = "";
                pick2team.Text = "";
                pick2playerName.Text = "";

                pick3roundandpick.Text = "";
                pick3team.Text = "";
                pick3playerName.Text = "";

                pick4roundandpick.Text = "";
                pick4team.Text = "";
                pick4playerName.Text = "";
            }


            if (currentPick != null)
            {
                pick1roundandpick.Text = "Round " + currentPick.Round + ", Pick #" + currentPick.PickInRound;
                pick1team.Text = currentPick.TeamName;
                pick1playerName.Text = currentPick.PlayerName;

                if (pick2 != null)
                {
                    pick2roundandpick.Text = "Round " + pick2.Round + ", Pick #" + pick2.PickInRound;
                    pick2team.Text = pick2.TeamName;
                }

                if (pick3 != null)
                {
                    pick3roundandpick.Text = "Round " + pick3.Round + ", Pick #" + pick3.PickInRound;
                    pick3team.Text = pick3.TeamName;
                }

                if (pick4 != null)
                {
                    pick4roundandpick.Text = "Round " + pick4.Round + ", Pick #" + pick4.PickInRound;
                    pick4team.Text = pick4.TeamName;
                }
           

            }
        }

       

        protected void btnResume_Click(object sender, EventArgs e)
        {
            btnResume.Visible = false;
            Timer.Enabled = true;
            checkIfUserPick((DraftPick)Session["CurrentPick"]);
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            
            DraftPick currentPick = null;
            bool isUserPick = false;
            while (!isUserPick)
            {
               
                currentPick = (DraftPick)Session["CurrentPick"];
                if (currentPick != null)
                {
                    if (currentPick.IsUserTeam && currentPick.PlayerName == null)
                    {
                        isUserPick = true;
                    }
                    else
                    {
                        autoPick(currentPick);
                        if (currentPick.NextPick != null)
                        {
                            advancePick(currentPick);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }


            }
            currentPick = (DraftPick)Session["CurrentPick"];

            checkIfUserPick(currentPick);
            currentPick = (DraftPick)Session["CurrentPick"];
            populateSidebar(currentPick);

        }

        protected void home_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }

    

    
   
    


}