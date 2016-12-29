//This page is for system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
namespace SQL_LAB
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string strsql = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Andy/Desktop/SQL/SQL_LAB/LAB.mdb";
        OleDbConnection icn = new OleDbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool if_search = false;
            icn.ConnectionString = strsql;
            string space = "-------------------------";
            string information = "";
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand mycmd = new OleDbCommand(@"select * from student", icn);
            OleDbDataReader reader = mycmd.ExecuteReader();
            while(reader.Read())
            {
                if_search = true;
                information = information + reader["ID"] + space + reader["PASSWORD"] + "<br>";
            }
            if (if_search)
                Label1.Text = information;
            else
                Label1.Text = "No Data";
            icn.Close();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            if (TextBox1.Text == "" || TextBox2.Text == "")
                Label2.Text = "Some blank please check";
            else
            {
                try
                {
                    string ID = TextBox1.Text;
                    string new_passwd = TextBox2.Text;
                    string cmd = @"UPDATE student SET [PASSWORD] = '" + new_passwd + "' WHERE [ID] = '" + ID + "'";
                    OleDbCommand mycmd = new OleDbCommand(cmd, icn);
                    mycmd.ExecuteNonQuery();
                    icn.Close();
                    Label2.Text = "Change success";
                }
                catch(Exception)
                {
                    Label2.Text = "Change error";
                }
                
            }
        }
    }
}