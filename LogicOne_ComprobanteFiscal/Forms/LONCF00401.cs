using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Shell;

namespace LogicOne_ComprobanteFiscal
{
    public partial class LONCF00401 : DexUIForm
    {
        public LONCF00401()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void LONCF00401_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}