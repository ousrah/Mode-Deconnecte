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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void patientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.patientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cabinetMedecinDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'cabinetMedecinDataSet.consultation'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.consultationTableAdapter.Fill(this.cabinetMedecinDataSet.consultation);
            // TODO: cette ligne de code charge les données dans la table 'cabinetMedecinDataSet.patient'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.patientTableAdapter.Fill(this.cabinetMedecinDataSet.patient);

        }
    }
}
