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
    public partial class FrmConsultation : Form
    {
        SqlCommand comP = new SqlCommand();
        SqlCommand comC = new SqlCommand();

        DataSet ds = new DataSet();
        SqlDataAdapter daP;
        SqlDataAdapter daC;

        BindingSource bsP = new BindingSource();
        BindingSource bsC = new BindingSource();

        SqlCommandBuilder cb;

        public FrmConsultation()
        {
            InitializeComponent();
        }

        private void FrmConsultation_Load(object sender, EventArgs e)
        {

            db.OuvrirConnection();

            //gestion des patients
            comP.Connection = db.cn;
            comP.CommandText = "select * from patient";
            daP = new SqlDataAdapter(comP);

            daP.Fill(ds, "Patient");
            bsP.DataSource = ds;
            bsP.DataMember = "Patient";

            listBox1.DisplayMember = "nom";
            listBox1.ValueMember = "id";
            listBox1.DataSource = bsP;

            //gestion des consultations
            comC.Connection = db.cn;
            comC.CommandText = "select * from consultation";
            daC = new SqlDataAdapter(comC);
            cb = new SqlCommandBuilder(daC);
            daC.Fill(ds, "Consultation");

            //ajouter la relation
            DataColumn idP_Patient = ds.Tables["patient"].Columns["id"];
            DataColumn idP_Consultation = ds.Tables["consultation"].Columns["idPatient"];
            DataRelation fk_consultation_patient = new DataRelation("fk_consultation_patient", idP_Patient, idP_Consultation);
            ds.Relations.Add(fk_consultation_patient);

            //afficher les consultations du patient selectionné
            bsC.DataSource = bsP;
            bsC.DataMember = "fk_consultation_patient";

            listBox2.DisplayMember = "dateConsultation";
            listBox2.ValueMember = "id";
            listBox2.DataSource = bsC;

            txtDC.DataBindings.Add("Text", bsC, "dateConsultation");
            txtObservation.DataBindings.Add("Text", bsC, "observation");





        }

        private void btnChercher_Click(object sender, EventArgs e)
        {
            bsP.Filter = "nom like '%" + txtRecherche.Text + "%'";
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            bsC.AddNew();
            txtDC.Text = DateTime.Now.ToString();

        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            bsC.EndEdit();
            daC.Update(ds.Tables["Consultation"]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtpConsultation.Value = Convert.ToDateTime(txtDC.Text);
            }
            catch (Exception ex)
            { }
        }

        private void dtpConsultation_ValueChanged(object sender, EventArgs e)
        {
            txtDC.Text = dtpConsultation.Value.ToString();
        }
    }
}
