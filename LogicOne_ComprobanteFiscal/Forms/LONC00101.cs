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
using MicrosoftDynamicsGPLogicOne;
using ADODB;

namespace LogicOne_ComprobanteFiscal
{
    public partial class LONC00101 : DexUIForm
    {
        Conexion conexion = new Conexion();
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
                MicrosoftDynamicsGPLogicOne.Lookup look = new Lookup("Busqueda de Preaviso", string.Format("LODYNDEV.dbo.LONC00101S1 {0} ", Globales.glb_InterCompany), 0, 1, 0, 0, 0, 0, "");
                if (look.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtClientCode.Text = look.xData1;
                    txtName.Text = look.xData2;
                    Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101S2 '{0}','{1}' ", Globales.glb_InterCompany, txtClientCode.Text),0);
                    txtClase.Text = Globales.rsRecords.Fields[1].Value.ToString();
                    txtTelefono.Text = Globales.rsRecords.Fields[2].Value.ToString();
                    if (!ckActivo.Checked)
                    {
                        labelStatus.Text = "Inactivo";
                        labelStatus.BackColor = Color.Red;
                    }
                    else if (ckActivo.Checked)
                    {
                        labelStatus.Text = "Activo";
                        labelStatus.BackColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Comentario
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}