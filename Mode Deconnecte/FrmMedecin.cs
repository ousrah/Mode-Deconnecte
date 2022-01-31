using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace Mode_Deconnecte
{
    public partial class FrmMedecin : Form
    {
        SqlCommand com = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        BindingSource bs = new BindingSource();
        SqlCommandBuilder cb;


        public FrmMedecin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            db.OuvrirConnection();    
            // cn.ConnectionString = @"data source=.\sqlexpress2008;initial catalog=cabinetMedecin;integrated security=true";

            
            com.Connection = db.cn;
            com.CommandText = "select * from medecin";
            da = new SqlDataAdapter(com);
            cb = new SqlCommandBuilder(da);


            da.Fill(ds, "Medecin");
            bs.DataSource = ds;
            bs.DataMember = "Medecin";

            listBox1.DisplayMember = "nom";
            listBox1.ValueMember = "id";
            listBox1.DataSource = bs;
            txtNom.DataBindings.Add("Text", bs, "nom");
            txtPrenom.DataBindings.Add("Text", bs, "prenom");
            txtTel.DataBindings.Add("Text", bs, "telephone");
            txtEmail.DataBindings.Add("Text", bs, "email");
 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.Filter = "nom like '%" + txtRecherche.Text + "%'";
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            bs.AddNew();
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            da.Update(ds.Tables["Medecin"]);

        }
    }
}
