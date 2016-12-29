//This page is for office
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
    public partial class WebForm4 : System.Web.UI.Page
    {
        string old_passwd = "";
        string strsql = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Andy/Desktop/SQL/SQL_LAB/LAB.mdb";
        OleDbConnection icn = new OleDbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand cmd = new OleDbCommand(@"select * from student'", icn);
            OleDbDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                if(reader["ID"].ToString() == "office")
                {
                    old_passwd = reader["PASSWORD"].ToString();
                }
            }
            icn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool if_search = false;
            icn.ConnectionString = strsql;
            string space = "-------------------------";
            string search = "";
            string num = "";
            string user_name = "";
            string cid = "";
            string time = "";
            string use_for = "";
            string other = "";
            string return_date = "";
            string office = "";
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand cmd = new OleDbCommand(@"select * from borrow", icn);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if_search = true;
                if (reader["NUMBER"].ToString() == "")
                    num = " ";
                else
                    num = reader["NUMBER"].ToString();
                if (reader["USER_NAME"].ToString() == "")
                    user_name = " ";
                else
                    user_name = reader["USER_NAME"].ToString();
                if (reader["CID"].ToString() == "")
                    cid = " ";
                else
                    cid = reader["CID"].ToString();
                if (reader["TIME"].ToString() == "")
                    time = " ";
                else
                    time = reader["TIME"].ToString();
                if (reader["USE_FOR"].ToString() == "")
                    use_for = " ";
                else
                    use_for = reader["USE_FOR"].ToString();
                if (reader["OTHER"].ToString() == "")
                    other = " ";
                else
                    other = reader["OTHER"].ToString();
                if (reader["RETURN_DATE"].ToString() == "")
                    return_date = "Not return";
                else
                    return_date = reader["RETURN_DATE"].ToString();
                if (reader["OFFICE"].ToString() == "")
                    office = "NONE";
                else
                    office = reader["OFFICE"].ToString();
                search = search + num + space + user_name + space + cid + space + time + space + use_for + space + other + space + return_date + space + office + "<br>";
            }
            if (if_search)
                Label1.Text = search;
            else
                Label1.Text = "No borrow information now";
            icn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "")
                Label2.Text = "Some blank please check";
            else
            {
                try
                {
                    string number = TextBox1.Text;
                    string return_date = TextBox2.Text;
                    string office = TextBox3.Text;
                    string cmd1 = @"UPDATE borrow SET [RETURN_DATE] = '" + return_date + "' WHERE [NUMBER] = " + number;
                    string cmd2 = @"UPDATE borrow SET OFFICE = '" + office + "' WHERE [NUMBER] = " + number;
                    OleDbCommand mycmd1 = new OleDbCommand(cmd1, icn);
                    OleDbCommand mycmd2 = new OleDbCommand(cmd2, icn);
                    mycmd1.ExecuteNonQuery();
                    mycmd2.ExecuteNonQuery();
                    icn.Close();
                    Label2.Text = "Update success!";
                }
                catch(Exception)
                {
                    Label2.Text = "Some error happened,please retype";
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            icn.ConnectionString = strsql;
            if (TextBox4.Text != old_passwd)
                Label3.Text = "Old password error";
            else if (TextBox5.Text != TextBox6.Text)
                Label3.Text = "New password error";
            else
            {
                try
                {
                    if (icn.State == ConnectionState.Open)
                        icn.Close();
                    icn.Open();
                    string new_password = TextBox6.Text;
                    string cmd = @"UPDATE student SET [PASSWORD] = '" + new_password + "' WHERE [ID] = 'office'";
                    OleDbCommand mycmd = new OleDbCommand(cmd, icn);
                    mycmd.ExecuteNonQuery();
                    icn.Close();
                    Label3.Text = "Update success";
                }
                catch(Exception)
                {
                    Label3.Text = "Update error";
                }
            }
        }
    }
}