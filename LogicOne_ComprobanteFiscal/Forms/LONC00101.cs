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
                int status = 0;
                MicrosoftDynamicsGPLogicOne.Lookup look = new Lookup("Busqueda de Cliente ", string.Format("LODYNDEV.dbo.LONC00101S1 {0} ", Globales.glb_InterCompany), 0, 1, 0, 0, 0, 0, "");
                if (look.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtClientCode.Text = look.xData1;
                    txtName.Text = look.xData2;
                    Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101S2 '{0}','{1}' ", Globales.glb_InterCompany, txtClientCode.Text),0);
                    txtClase.Text = Globales.rsRecords.Fields[1].Value.ToString();
                    txtTelefono.Text = Globales.rsRecords.Fields[2].Value.ToString();
                    // Estatus del label
                    Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101S4 '{0}','{1}' ", Globales.glb_InterCompany, txtClientCode.Text));
                    status = Convert.ToInt32(Globales.rsRecords.Fields[0].Value);
                    if (status == 0)
                    {
                        labelStatus.Text = "Inactivo";
                        labelStatus.BackColor = Color.Red;
                    }
                    else if (status  > 0)
                    {
                        labelStatus.Text = "Activo";
                        labelStatus.BackColor = Color.Green;
                    }
                    // Estatus del comprobante fiscal
                    //ckActivo.Checked = Convert.ToBoolean(status);
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
                int status = 0;
                int actualizar = 0;
                if (ckActivo.Checked)
                {
                    status = 1;
                }

                Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101S3 '{0}','{1}' ", Globales.glb_InterCompany, txtClientCode.Text));
                actualizar = Convert.ToInt32(Globales.rsRecords.Fields[0].Value);
                if (actualizar > 0)
                {
                    Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101SI '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' ", Globales.glb_InterCompany, txtClientCode.Text, txtClase.Text, txtName.Text, cboTipoNcf.Text, status, Globales.glb_UserId, 1));                    
                }
                else if (actualizar == 0)
                {
                    Globales.SQLQueryExecute(Globales.xProject, string.Format("LODYNDEV.DBO.LONC00101SI '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' ", Globales.glb_InterCompany, txtClientCode.Text, txtClase.Text, txtName.Text, cboTipoNcf.Text, status, Globales.glb_UserId, 0));                    
                }
                // Limpio todos los campos
                LimpiarCampos();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // LIMPIAR CAMPOS METODO

        public void LimpiarCampos()
        {
            try
            {
                txtClientCode.Clear();
                txtName.Clear();
                txtTelefono.Clear();
                txtClase.Clear();
                //cboTipoNcf.SelectedIndex = 1;
                labelStatus.BackColor = Color.White;
                ckActivo.Checked = false;
                labelStatus.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne Número Comprobante Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}