//This page is for register
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
    public partial class WebForm2 : System.Web.UI.Page
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool reg = false;
            if (TextBox1.Text == "" ||
                TextBox2.Text == "" ||
                TextBox3.Text == "" ||
                TextBox4.Text == "" ||
                TextBox5.Text == "" ||
                TextBox6.Text == "" ||
                TextBox7.Text == "")
            {
                Label1.Text = "Something have not type please check";
            }
            else if (TextBox6.Text != TextBox7.Text)
                Label1.Text = "Please retype password";
            else
                reg = true;
            if (reg == true)
            {
                try
                {
                    string ID = TextBox1.Text;
                    string NAME = TextBox2.Text;
                    string LAB_NAME = TextBox3.Text;
                    string EMAIL = TextBox4.Text;
                    string PHONE = TextBox5.Text;
                    string PASSWORD = TextBox6.Text;
                    if (icn.State == ConnectionState.Open)
                        icn.Close();
                    icn.Open();
                    string cmd = "INSERT INTO student (ID,SNAME,LAB_NAME,EMAIL,PHONE,[PASSWORD]) VALUES(?,?,?,?,?,?)";
                    OleDbCommand mycmd = new OleDbCommand(cmd, icn);
                    mycmd.Parameters.Add("@ID", OleDbType.Char).Value = TextBox1.Text;
                    mycmd.Parameters.Add("@SNAME", OleDbType.Char).Value = TextBox2.Text;
                    mycmd.Parameters.Add("@LAB_NAME", OleDbType.Char).Value = TextBox3.Text; ;
                    mycmd.Parameters.Add("@EMAIL", OleDbType.Char).Value = TextBox4.Text;
                    mycmd.Parameters.Add("@PHONE", OleDbType.Char).Value = TextBox5.Text;
                    mycmd.Parameters.Add("@PASSWORD", OleDbType.Char).Value = TextBox6.Text;
                    mycmd.ExecuteNonQuery();
                    icn.Close();
                    Label1.Text = "account created!";
                }
                catch(Exception)
                {
                    Label1.Text = "Register error,maybe you use the same ID";
                }
            }
            
        }
    }
}