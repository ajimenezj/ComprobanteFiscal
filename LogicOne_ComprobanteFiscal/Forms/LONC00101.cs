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
    public partial class LONC00101 : DexUIForm
    {
        public LONC00101()
        {
            InitializeComponent();
        }

        private void LONC00101_Load(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}