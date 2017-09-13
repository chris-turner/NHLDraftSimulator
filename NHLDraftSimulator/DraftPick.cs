using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHLDraftSimulator
{
    public class DraftPick
    {
        public int OverallPickNumber { get; set; }

        public string Round { get; set; }

        public int PickInRound { get; set; }

        public DraftPick NextPick { get; set; }

        public DraftPick PreviousPick { get; set; }

        public string Team { get; set; }

        public Guid DraftID { get; set; }

    }
}