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
            OuvrirConnection();

            com.Connection = cn;
            com.CommandText = req;
            da.SelectCommand = com;

            if (ds.Tables.Contains(table))
            {
                ds.EnforceConstraints = false;
                ds.Tables[table].Clear();

            }

            da.Fill(ds, table);
            ds.EnforceConstraints = true;


            BindingSource bs = new BindingSource();
            bs.DataSource = ds;
            bs.DataMember = table;

            return bs;
        }

        public static BindingSource RemplirListeRelation(string table, BindingSource bs, string pk, string fk)
        {
            return RemplirListeRelation("select * from " + table, table, bs, pk, fk);
        }


        public static BindingSource RemplirListeRelation(string req, string table, BindingSource bs, string pk, string fk)
        {
            OuvrirConnection();

            com.Connection = cn;
            com.CommandText = req;
            da.SelectCommand = com;


            if (ds.Tables.Contains(table))
            {
                ds.EnforceConstraints = false;
                ds.Tables[table].Clear();
            }

            da.Fill(ds, table);
            ds.EnforceConstraints = true;

            string nomRelation = "fk_" + table + "_" + bs.DataMember;

            if (!ds.Relations.Contains(nomRelation))
            {
                DataColumn colPK = ds.Tables[bs.DataMember].Columns[pk];
                DataColumn colFK = ds.Tables[table].Columns[fk];
                DataRelation rel = new DataRelation(nomRelation, colPK, colFK);
                ds.Relations.Add(rel);
            }




            BindingSource newbs = new BindingSource();
            newbs.DataSource = bs;
            newbs.DataMember = nomRelation;

            return newbs;
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
