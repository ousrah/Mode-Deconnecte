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

        public static void CreerTable(string table)
        {
            CreerTable("select * from " + table, table);
        }

            public static void CreerTable(string req,string table)
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
        }

        public static BindingSource RemplirListe(string table)
        {
            return RemplirListe("select * from " + table, table);
        }
        public static BindingSource RemplirListe(string req, string table)
        {

            CreerTable(req, table);

            BindingSource bs = new BindingSource();
            bs.DataSource = ds;
            bs.DataMember = table;

            return bs;
        }


        public static void CreerRelation(string tablePK, string tableFK, string pk, string fk )
        {
            string nomRelation = "fk_" + tableFK + "_" + tablePK;

            if (!ds.Relations.Contains(nomRelation))
            {
                DataColumn colPK = ds.Tables[tablePK].Columns[pk];
                DataColumn colFK = ds.Tables[tableFK].Columns[fk];
                DataRelation rel = new DataRelation(nomRelation, colPK, colFK);

                ds.Relations.Add(rel);

               //      rel.ChildKeyConstraint.DeleteRule = Rule.Cascade;
               //     rel.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            }


        }


        public static BindingSource RemplirListeRelation(string table, BindingSource bs, string pk, string fk)
        {
            return RemplirListeRelation("select * from " + table, table, bs, pk, fk);
        }


        public static BindingSource RemplirListeRelation(string req, string table, BindingSource bs, string pk, string fk)
        {
            CreerTable(req, table);

            CreerRelation(bs.DataMember, table, pk, fk);



               BindingSource newbs = new BindingSource();
            newbs.DataSource = bs;
            newbs.DataMember = "fk_"+table+"_"+ bs.DataMember;

            return newbs;
        }



        public static void MiseAjour(string table)
        {
            OuvrirConnection();
            com = new SqlCommand("select * from " + table,cn);
            da = new SqlDataAdapter(com);
            cb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[table]);


        }
    }
}
