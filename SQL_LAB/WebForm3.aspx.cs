//This page is for normal user
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
    public partial class WebForm3 : System.Web.UI.Page
    {
        string strsql = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Andy/Desktop/SQL/SQL_LAB/LAB.mdb";
        OleDbConnection icn = new OleDbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string space = "----------";
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand cmd = new OleDbCommand(@"select * from classroom", icn);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Label2.Text = reader.GetString(0) + space + reader.GetString(1) + space + reader.GetString(2) + space + reader.GetString(3) + space + reader.GetString(4);
            reader.Read();
            Label3.Text = reader.GetString(0) + space + reader.GetString(1) + space + reader.GetString(2) + space + reader.GetString(3) + space + reader.GetString(4);
            reader.Read();
            Label4.Text = reader.GetString(0) + space + reader.GetString(1) + space + reader.GetString(2) + space + reader.GetString(3) + space + reader.GetString(4);
            icn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            bool if_search = false;
            string space = "---------------";
            string search = "";
            icn.ConnectionString = strsql;
            if (icn.State == ConnectionState.Open)
                icn.Close();
            icn.Open();
            OleDbCommand cmd = new OleDbCommand(@"select * from borrow", icn);
            OleDbDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                if_search = true;
                search = search + reader.GetString(1) + space + reader.GetString(2) + "<br>";
            }
            if (if_search == false)
                Label5.Text = "No borrow now";
            else
                Label5.Text = search;
            icn.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            bool borrow = false;
            if (TextBox1.Text == "" ||
                TextBox2.Text == "" ||
                TextBox3.Text == "" ||
                TextBox4.Text == "" ||
                TextBox5.Text == "")
            {
                Label6.Text = "Something have not type please check";
            }
            else
                borrow = true;
            if(borrow)
            {
                try
                {
                    string USER_NAME = TextBox1.Text;
                    string CID = TextBox2.Text;
                    string TIME = TextBox3.Text;
                    string USE_FOR = TextBox4.Text;
                    string OTHER = TextBox5.Text;
                    icn.ConnectionString = strsql;
                    if (icn.State == ConnectionState.Open)
                        icn.Close();
                    icn.Open();
                    string cmd = "INSERT INTO borrow ([USER_NAME],[CID],[TIME],[USE_FOR],[OTHER]) VALUES(?,?,?,?,?)";
                    OleDbCommand mycmd = new OleDbCommand(cmd, icn);
                    mycmd.Parameters.Add("@USER_NAME", OleDbType.Char).Value = TextBox1.Text;
                    mycmd.Parameters.Add("@CID", OleDbType.Char).Value = TextBox2.Text;
                    mycmd.Parameters.Add("@TIME", OleDbType.Char).Value = TextBox3.Text; ;
                    mycmd.Parameters.Add("@USE_FOR", OleDbType.Char).Value = TextBox4.Text;
                    mycmd.Parameters.Add("@OTHER", OleDbType.Char).Value = TextBox5.Text;
                    mycmd.ExecuteNonQuery();
                    icn.Close();
                    Label6.Text = "Borrow success!";
                }
                catch(Exception)
                {
                    Label6.Text = "Error happened!";
                }
               
            }
        }
    }
}