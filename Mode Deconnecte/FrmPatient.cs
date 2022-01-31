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

        }
    }
}
