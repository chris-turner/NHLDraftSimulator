using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NHLDraftSimulator
{
    public class Draft
    {
        
        public DraftPick[] loadPicks(string draftYear)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            DraftPick[] draftPicks = null;
            string sql = @"select T.TeamID, T.TeamName, T.ImageFileName, DP.DraftPickID, DP.PickNum,  DP.Round, DP.PickInRound from Team as T inner join DraftPick as DP on DP.TeamID ="+ 
                "T.TeamID where DP.Year = '" +
                draftYear+ "'order by Round, PickInRound";
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                using (var rdr = command.ExecuteReader())
                {
                    var list = new List<DraftPick>();
                    while (rdr.Read())
                        list.Add(new DraftPick
                        {
                            TeamID = rdr.GetInt32(0),
                            TeamName = rdr.GetString(1),
                            ImageFileName = rdr.GetString(2),
                            DraftPickID = rdr.GetGuid(3),
                            OverallPickNumber = rdr.GetInt32(4),
                            Round = rdr.GetInt32(5),
                            PickInRound = rdr.GetInt32(6)
                        });
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
            return draftPicks;

        }

        public Player[] loadPlayers(string draftYear)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            Player[] players = null;
            string sql = @" select PlayerID,PlayerFName,PlayerLName,Position,Nationality,Ranking from Player where DraftYear = '" +
                draftYear + "'";
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                using (var rdr = command.ExecuteReader())
                {
                    var list = new List<Player>();
                    while (rdr.Read())
                        list.Add(new Player
                        {
                            PlayerID = rdr.GetInt32(0),
                            PlayerFName = rdr.GetString(1),
                            PlayerLName = rdr.GetString(2),
                            Nationality = rdr.GetString(3),
                            Position = rdr.GetString(4),
                            Ranking = rdr.GetInt32(5)
                        });
                    players = list.ToArray();
                }
            }

            return players;

        }
    }
}