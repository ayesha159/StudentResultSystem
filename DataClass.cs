using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

/// <summary>
/// Summary description for AJ_DataClass
/// </summary>
public class DataClass
{
    string strcon = @"Data Source=DESKTOP-02QTVP4;Initial Catalog=ProjectB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
    public DataClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void GetUserInfo(string _id, ref string _Name, ref string _RoleType)
    {
        string myConnectionString = strcon;
        string tTable = string.Empty;
        SqlConnection con = new SqlConnection(myConnectionString);
        DataSet ds = new DataSet();

        if (con.State == ConnectionState.Closed) con.Open();
        SqlCommand com = new SqlCommand("Select * from tblReg where id = " + _id + "'", con);
        int i = com.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = com;
        da.TableMappings.Add("Table", "_Table");
        if (con.State == ConnectionState.Open)
            con.Close();
        da.Fill(ds);

       

    }
    
    public void popGrid(DataGridView gv,string qry) 
    {
        DataSet ds = new DataSet();
        ds = GetRecords("table", qry);
            gv.DataSource = ds.Tables[0];
    }

    public void popCmb(ComboBox cmb,string qry, string vm,string dm)  
    {
      DataSet ds = new DataSet ();

       ds =  GetRecords("tbl",qry);
       cmb.DataSource = ds.Tables[0];  
            cmb.DisplayMember = dm;
             cmb.ValueMember = vm;
        
        
    }

   
        public DataSet GetRecords(string _Table, string Qry)
    {
        string myConnectionString = strcon;
        string tTable = string.Empty;
        SqlConnection con = new SqlConnection(myConnectionString);
        DataSet ds = new DataSet();

        if (con.State == ConnectionState.Closed) con.Open();
        SqlCommand com = new SqlCommand(Qry, con);
        int i = com.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = com;
        da.TableMappings.Add("Table", _Table);
        if (con.State == ConnectionState.Open)
            con.Close();
        da.Fill(ds);
        return ds;
    }

    public int TRec(string Qry)
    {
        string myConnectionString = strcon;
        string tTable = string.Empty;
        SqlConnection con = new SqlConnection(myConnectionString);
        DataSet ds = new DataSet();

        if (con.State == ConnectionState.Closed) con.Open();
        SqlCommand com = new SqlCommand(Qry, con);
        int i = com.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = com;
        da.TableMappings.Add("Table", "_Table");
        if (con.State == ConnectionState.Open)
            con.Close();
        da.Fill(ds);
        return ds.Tables[0].Rows.Count;

    }

    public string InsertIntoDatabase(string _Table, string _Fields, string _Values)
    {
        try
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO " + _Table + " (" + _Fields + ") VALUES (" + _Values + ")";
            cmd.CommandType = CommandType.Text;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.ExecuteScalar();
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            return "Record(s) Added Succfully";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    public string DeleteFromDatabase(string Qry)
    {
        try
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = Qry;
            cmd.CommandType = CommandType.Text;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
                con.Close();
            return "Delete Record(s) Succfully";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    public string UpdateDatabase(string Qry)
    {
        try
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = Qry;
            cmd.CommandType = CommandType.Text;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
                con.Close();
            return "Update Record(s) Succfully";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

}

