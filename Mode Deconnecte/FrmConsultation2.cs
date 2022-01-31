using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mode_Deconnecte
{
    public partial class FrmConsultation2 : Form
    {
        BindingSource bsConsultation;
        BindingSource bsPatient;
        bool isSaved = true;


        public FrmConsultation2()
        {
            InitializeComponent();
        }

        private void FrmConsultation2_Load(object sender, EventArgs e)
        {

            bsPatient = db.RemplirListe("patient");
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = bsPatient;

            bsConsultation = db.RemplirListe("consultation");
            listBox1.DisplayMember = "dateConsultation";
            listBox1.ValueMember = "id";
            listBox1.DataSource = bsConsultation;

            txtDC.DataBindings.Add("Text", bsConsultation, "dateConsultation");
            txtObservation.DataBindings.Add("Text", bsConsultation, "observation");

        }

        private void txtDC_TextChanged(object sender, EventArgs e)
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

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            bsConsultation.AddNew();
            txtDC.Text = dtpConsultation.Value.ToString();
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            bsConsultation.EndEdit();
            isSaved = false;
        }



        private void FrmConsultation2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
                if (MessageBox.Show("Voulez vous enregistrer les modifications?", "enregistrement", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    db.MiseAjour("consultation");

        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            isSaved = false;
        }
    }
}
