//This page is home page
//The path of data base file should be changed to its location
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace SQL_LAB
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strsql = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Andy/Desktop/SQL/SQL_LAB/LAB.mdb";
        OleDbConnection icn = new OleDbConnection();
        private Dictionary<string, string> currentUser = null;
        private List<Dictionary<string, string>> user_list = new List<Dictionary<string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand cmd = new OleDbCommand(@"select * from student", icn);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ID"].ToString() != "")
                {
                    user_list.Add(new Dictionary<string, string>
                    {
                        {"ID",reader["ID"].ToString()},
                        {"SNAME",reader["SNAME"].ToString()},
                        {"PASSWORD",reader["PASSWORD"].ToString()}
                    }
                    );
                }
            }
            icn.Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool login = false;
            foreach (var item in user_list)
            {
                if(TextBox1.Text == item["ID"] && TextBox2.Text == item["PASSWORD"])
                {
                    Button1.Enabled = false;
                    Button2.Enabled = false;
                    currentUser = item;
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    login = true;
                }
                else
                {
                    Label1.Text = "login failed";
                }
            }
            if (login)
            {
                Label1.Text = "login......";
                if (currentUser["ID"] == "office")
                    Response.Redirect("WebForm4.aspx");
                else if (currentUser["ID"] == "system")
                    Response.Redirect("WebForm5.aspx");
                else
                    Response.Redirect("WebForm3.aspx");
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }
    }
}