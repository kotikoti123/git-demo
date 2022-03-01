using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ado.net
{
    class DataAccess
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public SqlConnection GetConnection()
        {
            con = new SqlConnection(
                "Data Source=DESKTOP-59GIMJ1\\MSSQLSERVER01;Initial Catalog=Northwind;Integrated Security=true");
            con.Open();
            return con;
        }
        public void DisplayRegion()
        {
            con = GetConnection();
            SqlDataAdapter da;
            da = new SqlDataAdapter("select * from Region", con);
            DataSet ds = new DataSet();//Collection of tables
            da.Fill(ds, "MyRegion");
            //read table from dataset
            DataTable dt;
            dt = ds.Tables["MyRegion"];
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(row[col]);
                    Console.Write(" ");
                }
                Console.WriteLine(" ");
            }

            //adding shipper table to dataset
            /*    Console.WriteLine("----------------");
                da = new SqlDataAdapter("select * from Shippers", con);
                da.Fill(ds, "MyShipper");
                //read table from dataset
                DataTable dt1;
                dt1 = ds.Tables["MyShipper"];
                foreach (DataRow row in dt1.Rows)
                {
                   foreach(DataColumn col in dt1.Columns)
                    {
                        Console.WriteLine(row[col]);
                        Console.Write(" ");
                    }
                    Console.Write(" ");
                }*/
            //Insert new roe in region table in dataset
            // Insert ,update,delete operation

            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            da.Fill(ds);
            //inserting or add new roe to region table
            //creating a new row in MyRegion table in dataset
            DataRow row1 = ds.Tables["MyRegion"].NewRow();
            row1["RegionID"] = 10;
            row1["RegionDescription"] = "NW";
            //adding row to datatable in dataset
            ds.Tables["MyRegion"].Rows.Add(row1);
            da.Update(ds, "MyRegion");
            Console.WriteLine("------------");
            dt = ds.Tables["MyRegion"];

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(row[col]);
                    Console.Write(" ");
                }
                Console.WriteLine(" ");



            }
        }
    }
    
    class DisconnectedArchitecture
    {
        static void Main()
        {
            DataAccess dataAccess = new DataAccess();
            dataAccess.DisplayRegion();
        }
    }
}
