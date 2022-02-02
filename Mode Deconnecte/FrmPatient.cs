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
    public partial class FrmPatient : Form
    {
        BindingSource bsPatient;
        public FrmPatient()
        {
            InitializeComponent();
        }

        private void FrmPatient_Load(object sender, EventArgs e)
        {


            bsPatient = db.RemplirListe("patient");
            listBox1.DisplayMember = "nom";
            listBox1.ValueMember = "id";
            listBox1.DataSource = bsPatient;

            txtNom.DataBindings.Add("Text", bsPatient, "nom");
            txtPrenom.DataBindings.Add("Text", bsPatient, "prenom");
            txtTel.DataBindings.Add("Text", bsPatient, "telephone");
            txtEmail.DataBindings.Add("Text", bsPatient, "email");



        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("La suppression d'un patient entrainera la suppression de toutes les consultations. Etes vous certain de le supprimer?","Suppression",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                DataRelation rel = db.ds.Relations["fk_consultation_patient"];
                rel.ChildKeyConstraint.DeleteRule = Rule.Cascade;
         //       DataRelation rel2 = db.ds.Relations["fk_rendezvous_patient"];
         //       rel2.ChildKeyConstraint.DeleteRule = Rule.Cascade;

          
                bsPatient.RemoveCurrent();
                db.MiseAjour("consultation");
         //       db.MiseAjour("rendezvous");
                db.MiseAjour("patient");
                rel.ChildKeyConstraint.DeleteRule = Rule.None;
          //      rel2.ChildKeyConstraint.DeleteRule = Rule.None;
          
                rel = null;
            }
        }
    }
}
