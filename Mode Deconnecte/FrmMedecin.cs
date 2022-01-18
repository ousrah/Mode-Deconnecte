using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Mode_Deconnecte
{
    public partial class FrmMedecin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        BindingSource bs = new BindingSource();
        public FrmMedecin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                  cn.ConnectionString = @"data source=.\sqlexpress2008;initial catalog=cabinetMedecin;user id=sa;password=P@ssw0rd";
            // cn.ConnectionString = @"data source=.\sqlexpress2008;initial catalog=cabinetMedecin;integrated security=true";

            cn.Open();

            com.Connection = cn;
            com.CommandText = "select * from medecin";
            da = new SqlDataAdapter(com);
            da.Fill(ds, "Medecin");
            bs.DataSource = ds;
            bs.DataMember = "Medecin";

            listBox1.DisplayMember = "nom";
            listBox1.ValueMember = "id";
            listBox1.DataSource = bs;



        }
    }
}
