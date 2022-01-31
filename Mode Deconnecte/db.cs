using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
namespace Mode_Deconnecte
{
    static class db
    {
        public static SqlConnection cn = new SqlConnection();
        public static DataSet ds = new DataSet();


        public static SqlCommand com = new SqlCommand();
        public static SqlDataAdapter da = new SqlDataAdapter();

        public static SqlCommandBuilder cb = new SqlCommandBuilder();
        public static void OuvrirConnection()
        {
            if (cn.State != ConnectionState.Open)
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["CabinetMedecinConnectionString"].ConnectionString;
                cn.Open();
            }
        }

        public static void FermerConnection()
        {
            if (cn.State != ConnectionState.Closed)
                          cn.Close();
         
        }

        public static BindingSource RemplirListe(string table)
        {
            return RemplirListe("select * from " + table, table);
        }
        public static BindingSource RemplirListe(string req, string table)
        {
            if (cn.State != ConnectionState.Open)
                OuvrirConnection();

            com.Connection = cn;
            com.CommandText = req;
            da.SelectCommand = com;

            if (ds.Tables.Contains(table))
                ds.Tables[table].Clear();
            
            da.Fill(ds, table);
            


            BindingSource bs = new BindingSource();
            bs.DataSource = ds;
            bs.DataMember = table;

            return bs;
        }

        public static void MiseAjour(string table)
        {
            OuvrirConnection();
            com.CommandText = "select * from " + table;
            com.Connection = cn;
            da.SelectCommand = com;
            cb.DataAdapter = da;
            da.Update(ds.Tables[table]);


        }
    }
}
