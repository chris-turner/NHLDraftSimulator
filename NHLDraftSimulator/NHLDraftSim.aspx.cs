using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            DraftPick[] draftPicks = null;
            string sql = @"select T.TeamID, T.TeamName, DP.DraftPickID, DP.PickNum,  DP.Round, DP.PickInRound from Team as T inner join DraftPick as DP on DP.TeamID = T.TeamID where DP.Year = '" +
                Session["DraftYear"] + "'order by Round, PickInRound";
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                using (var rdr = command.ExecuteReader())
                {
                    var list = new List<DraftPick>();
                    while (rdr.Read())
                        list.Add(new DraftPick { TeamID = rdr.GetInt32(0), TeamName = rdr.GetString(1), DraftPickID = rdr.GetGuid(2),
                            OverallPickNumber = rdr.GetInt32(3), Round = rdr.GetInt32(4), PickInRound = rdr.GetInt32(5) });
                    draftPicks = list.ToArray();
                }
            }

            for (int i = 0; i < draftPicks.Length; i++)
            {
                if (i == 0)
                {
                    draftPicks[i].PreviousPick = null;
                    draftPicks[i].NextPick = draftPicks[1];
                }
                else if (i == (draftPicks.Length - 1))
                {
                    draftPicks[i].PreviousPick = draftPicks[draftPicks.Length - 2];
                    draftPicks[i].NextPick = null;
                }
                else
                {
                    draftPicks[i].PreviousPick = draftPicks[i - 1];
                    draftPicks[i].NextPick = draftPicks[i + 1];

                }
            }
                Session["draftPicks"] = draftPicks;

            }
            getCurrentPick((DraftPick[])Session["draftPicks"]);



        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        public void getCurrentPick(DraftPick[] draftPicks)
        {
            //write SQL query to find current pick
            //load 4 picks into side bar
            Guid currentPickID = Guid.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = 
                new SqlCommand("select top 1 DP.DraftPickID, DP.PickNum from DraftPick as DP where DraftPickID not in (Select DraftPickID from Player_Draft_Pick where DraftID = '" + Session["DraftID"] + "') order by PickNum", con);

            using (var rdr = cmd.ExecuteReader())
            {
                    while(rdr.Read())
                    {
                        currentPickID = rdr.GetGuid(0);
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
            }


            DraftPick pick2 = currentPick.NextPick;
            DraftPick pick3 = pick2.NextPick;
            DraftPick pick4 = pick3.NextPick;

            if (currentPick != null)
            {
                pick1roundandpick.Text = "Round " + currentPick.Round + ", Pick #" + currentPick.PickInRound;
                pick1team.Text = currentPick.TeamName;

                pick2roundandpick.Text = "Round " + pick2.Round + ", Pick #" + pick2.PickInRound;
                pick2team.Text = pick2.TeamName;

                pick3roundandpick.Text = "Round " + pick3.Round + ", Pick #" + pick3.PickInRound;
                pick3team.Text = pick3.TeamName;

                pick4roundandpick.Text = "Round " + pick4.Round + ", Pick #" + pick4.PickInRound;
                pick4team.Text = pick4.TeamName;
            }
            


        }
    }

    

    


}