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

        public Guid DraftPickID { get; set; }

    }
}