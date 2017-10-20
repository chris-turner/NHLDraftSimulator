using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHLDraftSimulator
{
    public class DraftPick
    {
        public int OverallPickNumber { get; set; }

        public int Round { get; set; }

        public int PickInRound { get; set; }

        public DraftPick NextPick { get; set; }

        public DraftPick PreviousPick { get; set; }

        public string TeamName { get; set; }

        public int TeamID { get; set; }

        public string ImageFileName { get; set; }

        public int DraftPickID { get; set; }

        public bool IsUserTeam { get; set; }

        public int PlayerID { get; set; }

        public string PlayerName { get; set; }

    }

}