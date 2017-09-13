using System;
using System.Collections.Generic;
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

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }
    }

    

    public class Draft
    {
        public Guid DraftID { get; set; }
        public DraftPick ActivePick { get; set; }

    }


}