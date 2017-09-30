using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NHLDraftSimulator
{
    public class DraftUser
    {
        public static void addNewUser(HttpResponse Response, HttpCookie cookie)
        {
            Guid UserID = Guid.NewGuid();
            cookie = new HttpCookie("UserID");
            cookie.Values.Add("userID", UserID.ToString());
            cookie.Expires = DateTime.Now.AddYears(2111);
            Response.Cookies.Add(cookie);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into [User] (UserID) values ('" + UserID + "')", con);
            cmd.ExecuteReader();
            con.Close();
        }
    }
}