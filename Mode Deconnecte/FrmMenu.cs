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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AfficherFenetre(Form f)
        {

            if (panel1.Controls.Count!=0)
            {
                Form oldf = (Form)panel1.Controls[0];
                oldf.Close();

            }

            panel1.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
               f.FormBorderStyle = FormBorderStyle.None;
             this.panel1.Controls.Add(f);
  
            f.Show();
        }
   

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           AfficherFenetre(new FrmPatient());
   
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            AfficherFenetre(new FrmMedecin());
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            db.CreerTable("medecin");
            db.CreerTable("patient");
            db.CreerTable("consultation");
            db.CreerTable("medicament");
            db.CreerTable("ordonance");
            db.CreerRelation("medecin", "consultation", "id", "idMedecin");
            db.CreerRelation("patient", "consultation", "id", "idPatient");
            db.CreerRelation("medicament", "ordonance", "id", "idMedicament");
            db.CreerRelation("consultation", "ordonance", "id", "idConsultation");



        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AfficherFenetre(new FrmConsultation());
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AfficherFenetre(new FrmConsultation2());
        }
    }
}
