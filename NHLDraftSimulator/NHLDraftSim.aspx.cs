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
                

            }
            //getCurrentPick();



        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        public void getCurrentPick(List<DraftPick> draftPicks, Guid DraftID)
        {
            //write SQL query to find current pick
            //load 4 picks into side bar
            pick1roundandpick.Text = "Round " + draftPicks[0].Round + ", Pick #" + draftPicks[0].PickInRound;
            pick1team.Text = draftPicks[0].TeamName;

            pick2roundandpick.Text = "Round " + draftPicks[1].Round + ", Pick #" + draftPicks[1].PickInRound;
            pick2team.Text = draftPicks[1].TeamName;

            pick3roundandpick.Text = "Round " + draftPicks[2].Round + ", Pick #" + draftPicks[2].PickInRound;
            pick3team.Text = draftPicks[2].TeamName;

            pick4roundandpick.Text = "Round " + draftPicks[3].Round + ", Pick #" + draftPicks[3].PickInRound;
            pick4team.Text = draftPicks[3].TeamName;


        }
    }

    

    


}