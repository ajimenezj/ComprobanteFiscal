using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.MenusForVisualStudioToolsDictionary;
using System.Windows.Forms;
using MicrosoftDynamicsGPLogicOne;
using System.Reflection;


namespace LogicOne_ComprobanteFiscal
{
    public class GPAddIn : IDexterityAddIn
    {

        // IDexterityAddIn interface
        LONCF00401 MantenimientoComprobanteFiscal;
        LONCF00402 ConfiguracionComprobanteFiscal;

        //LOUPR00101FORM 

        // Application Name
        const string APPNAME = "LogicOne_LicenciaTSS";
        // Dictionary ID Constants
        const short DYNAMICS = 0;

        // Shortcut Key Modifier Constants
        const int COMMAND_SHORTCUT_CTRL = 65536;
        const int COMMAND_SHORTCUT_CTRLSHIFT = 327680;
        const int COMMAND_SHORTCUT_CTRLALT = 196608;
        const int COMMAND_SHORTCUT_ALT = 131072;
        const int COMMAND_SHORTCUT_ALTSHIFT = 393216;
        const int COMMAND_SHORTCUT_CTRLALTSHIFT = 458752;

        // Shortcut Key Function Key Constants, can be used instead of ASCII value
        const short COMMAND_SHORTCUT_KEY_F1 = 112;
        const short COMMAND_SHORTCUT_KEY_F2 = 113;
        const short COMMAND_SHORTCUT_KEY_F3 = 114;
        const short COMMAND_SHORTCUT_KEY_F4 = 115;
        const short COMMAND_SHORTCUT_KEY_F5 = 116;
        const short COMMAND_SHORTCUT_KEY_F6 = 117;
        const short COMMAND_SHORTCUT_KEY_F7 = 118;
        const short COMMAND_SHORTCUT_KEY_F8 = 119;
        const short COMMAND_SHORTCUT_KEY_F9 = 120;
        const short COMMAND_SHORTCUT_KEY_F10 = 121;
        const short COMMAND_SHORTCUT_KEY_F11 = 122;
        const short COMMAND_SHORTCUT_KEY_F12 = 123;

        short MenuListTag1;
        short MenuTag1;
        short MenuTag2;
        short MenuTag3;
        short MenuTag4;
        short MenuTag5;
        short MenuTag6;
        short MenuTag7;


        // IDexterityAddIn interface
        public void Initialize()
        {

            // Version 10.0 Syntax still works but new syntax using function events now available
            // // Register Event to add menu entries
            // MenusForVisualStudioTools.Forms.VstmCommandForm.Api.RegisterButton.ClickAfterOriginal += new EventHandler(VSTMCommandFormRegister);
            // // Register Event to handle callbacks from menu entries
            // MenusForVisualStudioTools.Forms.VstmCommandForm.Api.CmdId.ValidateAfterOriginal += new EventHandler(VSTMCommandFormCallback);

            // Register Event to add menu entries
            MenusForVisualStudioTools.Functions.EventRegister.InvokeAfterOriginal += new EventRegisterFunction.InvokeEventHandler(VSTMCommandFormRegister);
            // Register Event to handle callbacks from menu entries
            MenusForVisualStudioTools.Functions.EventHandler.InvokeAfterOriginal += new EventHandlerFunction.InvokeEventHandler(VSTMCommandFormCallback);
        }

        // Script to Register menu entries
        //void VSTMCommandFormRegister(object sender, EventArgs e)
        void VSTMCommandFormRegister(object sender, EventRegisterFunction.InvokeEventArgs e)
        {
            short ParentTag = 0;
            short BelowTag = 0;
            short ResID = 0;
            short Err = 0;

            try
            {
                Globales.glb_DataSource = Dynamics.Globals.SqlDataSourceName.Value;
                Globales.glb_CompanyName = Dynamics.Globals.CompanyName.Value;
                Globales.glb_InterCompany = Dynamics.Globals.IntercompanyId.Value;
                Globales.glb_UserId = Dynamics.Globals.UserId.Value;

                MicrosoftDynamicsGPLogicOne.Conexion.xFileINI = Directory.GetCurrentDirectory().Trim() + "\\LogicOne.ini";

                if (String.IsNullOrEmpty(MicrosoftDynamicsGPLogicOne.Conexion.xSqlDataSource))
                {
                    MicrosoftDynamicsGPLogicOne.Conexion.xSqlDataSource = MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 1).ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xSqlUID = "louser";// MicrosoftDynamicsGPLogicOne.xFileStream.Desencriptar128BitRijndael(MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 2), "MicrosoftDynamicsGPLogicOneUser").ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xSqlPWD = "louser";// MicrosoftDynamicsGPLogicOne.xFileStream.Desencriptar128BitRijndael(MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 3), "MicrosoftDynamicsGPLogicOnePassword").ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xCrystalReportPath = MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 4).ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xReportingServerPath = MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 5).ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xFileShared = MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 6).ToString();//.Replace(null, "");
                    MicrosoftDynamicsGPLogicOne.Conexion.xInterfase = Convert.ToInt32(MicrosoftDynamicsGPLogicOne.xFileStream.GetFileText(MicrosoftDynamicsGPLogicOne.Conexion.xFileINI, 7).ToString());//.Replace(null, ""));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Verificando registro --
            //ProdActive = Funcionesx.ValidarRegistro(ProdKey, ProdName, ServerName)
            //ProdActive = True

            try
            {

                // Get Parent Tag for command list to add menu to
                ParentTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(
                                    DYNAMICS, "Command_Payroll", "CL_Payroll_Transactions");           // Dictionary ID, Form Name, Command Name
                if (ParentTag <= 0)
                {
                    throw new Exception("Parent GetTagByName, error code: " + Convert.ToString(ParentTag));
                }

                // Get Below Tag for command/command list to add menu below
                BelowTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(
                                    DYNAMICS, "Command_Sales", "RM_Customer_Maintenance");  // Dictionary ID, Form Name, Command Name
                if (BelowTag <= 0)
                {
                    throw new Exception("Below GetTagByName, error code: " + Convert.ToString(BelowTag));
                }

                // Get Security Form Resource ID for menus to inherit security access from
                ResID = MenusForVisualStudioTools.Functions.GetFormResId.Invoke(
                                    DYNAMICS, "RM_Customer_Maintenance");                   // Get Form Resource ID for Security 
                if (ResID <= 0)
                {
                    throw new Exception("GetFormResId, error code: " + Convert.ToString(ResID));
                }

                // Add Menu entry using API Function call to create parent for sub menu
                MenuListTag1 = MenusForVisualStudioTools.Functions.Register.Invoke(
                                    ParentTag,                                              // Parent Command Tag
                                    "LogicOne - Licencia TSS Apec",                                         // Menu Caption
                                    "",                                                     // Menu Tooltip
                                    0, 0,                                                   // Menu Shortcut Key, Shortcut Modifier
                                    false, false, false,                                    // Checked, Disabled, Hidden
                                    BelowTag,                                               // Add Below Command Tag
                                    false, true);                                           // Add Separator, Add Command List
                if (MenuListTag1 <= 0)
                {
                    throw new Exception("Command List Register, error code: " + Convert.ToString(MenuListTag1));
                }

                // Add Menu entry using API Function call to create first sub menu entry with security

                MenuTag1 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                                    MenuListTag1,                                           // Parent Command Tag
                                    "Registro de licencias TSS",                                       // Menu Caption
                                    "Click para registrar Registro de licencias TSS",                // Menu Tooltip
                                    (int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                                    true, false, false,                                     // Checked, Disabled, Hidden
                                    0,                                                      // Add Below Command Tag
                                    false, false,                                           // Add Separator, Add Command List
                                    DYNAMICS, ResID);                                       // Security Dictionary and Form Resource ID

                MenuTag2 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                          MenuListTag1,                                           // Parent Command Tag
                          "LONCF00402 de licencias",                                       // Menu Caption
                          "Click para LONCF00402 de licencias",                // Menu Tooltip
                          (int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                          true, false, false,                                     // Checked, Disabled, Hidden
                          0,                                                      // Add Below Command Tag
                          false, false,                                           // Add Separator, Add Command List
                          DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID


                //  MenuTag3 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //     MenuListTag1,                                           // Parent Command Tag
                //     "Parametros Cesantia (Antes 1992) ",                                       // Menu Caption
                //     "Click para crear Parametros a la Cesantia",                // Menu Tooltip
                //     (int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                //     true, false, false,                                     // Checked, Disabled, Hidden
                //     0,                                                      // Add Below Command Tag
                //     false, false,                                           // Add Separator, Add Command List
                //     DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID


                //  MenuTag4 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //MenuListTag1,                                           // Parent Command Tag
                //"Parametros Cesantia asistencia economica",                                       // Menu Caption
                //"Click para generar Parametros Cesantia asistencia economica",                            // Menu Tooltip
                //(int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                //true, false, false,                                     // Checked, Disabled, Hidden
                //0,                                                      // Add Below Command Tag
                //false, false,                                           // Add Separator, Add Command List
                //DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID

                //  MenuTag5 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //MenuListTag1,                                           // Parent Command Tag
                //"Parametros de vacaciones",                                       // Menu Caption
                //"Click para los Parametros de vacaciones",                            // Menu Tooltip
                //(int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                //true, false, false,                                     // Checked, Disabled, Hidden
                //0,                                                      // Add Below Command Tag
                //false, false,                                           // Add Separator, Add Command List
                //DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID

                //  MenuTag6 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //      MenuListTag1,                                           // Parent Command Tag
                //      "Parametros ISR",                                       // Menu Caption
                //      "Click para ver los Parametros ISR",                // Menu Tooltip
                //      (int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                //      true, false, false,                                     // Checked, Disabled, Hidden
                //      0,                                                      // Add Below Command Tag
                //      false, false,                                           // Add Separator, Add Command List
                //      DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID


                //  MenuTag7 = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //     MenuListTag1,                                           // Parent Command Tag
                //     "Configuración Transacciones ISR proyectado",                                       // Menu Caption
                //     "Configuración Transacciones ISR proyectado",                // Menu Tooltip
                //     (int)'A', COMMAND_SHORTCUT_CTRLALT,                     // Menu Shortcut Key, Shortcut Modifier
                //     true, false, false,                                     // Checked, Disabled, Hidden
                //     0,                                                      // Add Below Command Tag
                //     false, false,                                           // Add Separator, Add Command List
                //     DYNAMICS, ResID);                                        // Security Dictionary and Form Resource ID

                if (MenuTag1 <= 0)
                {
                    throw new Exception("Command 1 RegisterWithSecurity, error code: " + Convert.ToString(MenuTag1));
                }

                // Change Tooltip on Customer Maintenance Menu Item, need to toggle Protected Mode
                MenusForVisualStudioTools.Functions.SetProtectedMode.Invoke(false);
                Err = MenusForVisualStudioTools.Functions.SetTooltip.Invoke(
                                BelowTag,                                                   // Menu Tag
                                "VS C# Test Tooltip");                                      // Tooltip
                MenusForVisualStudioTools.Functions.SetProtectedMode.Invoke(true);
                if (Err < 0)
                {
                    throw new Exception("SetTooltip, error code: " + Convert.ToString(Err));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, APPNAME);

                // Unregister first menu entry
                if (MenuTag1 > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(
                                    MenuListTag1,                                           // Parent Command Tag
                                    MenuTag1);                                              // Menu Tag
                    if (Err < 0)
                    {
                        MessageBox.Show("Command 1 Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }
                if (MenuTag2 > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(
                                    MenuListTag1,                                           // Parent Command Tag
                                    MenuTag2);                                              // Menu Tag
                    if (Err < 0)
                    {
                        MessageBox.Show("Command 1 Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

                // Unregister Menu List entry
                if (MenuListTag1 > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(
                                    ParentTag,                                              // Parent Command Tag
                                    MenuListTag1);                                          // Menu Tag
                    if (Err < 0)
                    {
                        MessageBox.Show("Command List Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

            }
        }

        // Script to handle menu entry callbacks
        //void VSTMCommandFormCallback(object sender, EventArgs e)
        void VSTMCommandFormCallback(object sender, EventHandlerFunction.InvokeEventArgs e)
        {
            short Checked = 0;
            short Tag = 0;
            string Caption = "";

            // Get Callback Tag Number for menu entry
            // Tag = MenusForVisualStudioTools.Functions.Callback.Invoke();
            Tag = e.inParam1;

            // Compare Tag Sequence Number with Menu Sequence obtained during registration
            if (Tag == MenuTag1)
            {
                // Display Menu Caption
                MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag1, out Caption);
                LONCF00401(sender, e);

                // Toggle Menu Check mark on first menu entry
                Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
                if (Checked >= 0)
                {
                    MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
                }

                // Enable second menu entry and show third menu entry
                MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
                MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            }
            if (Tag == MenuTag2)
            {
                // Display Menu Caption
                MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag2, out Caption);
                LONCF00402(sender, e);

                // Toggle Menu Check mark on first menu entry
                Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
                if (Checked >= 0)
                {
                    MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
                }

                // Enable second menu entry and show third menu entry
                MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
                MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            }
            //else if (Tag == MenuTag3)
            //{
            //    // Display Menu Caption
            //    MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag3, out Caption);
            //    LOUPR00102FORM(sender, e);

            //    // Toggle Menu Check mark on first menu entry
            //    Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
            //    if (Checked >= 0)
            //    {
            //        MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
            //    }

            //    // Enable second menu entry and show third menu entry
            //    MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
            //    MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            //}
            //else if (Tag == MenuTag4)
            //{
            //    // Display Menu Caption
            //    MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag4, out Caption);
            //    LOUPR00103FORM(sender, e);

            //    // Toggle Menu Check mark on first menu entry
            //    Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
            //    if (Checked >= 0)
            //    {
            //        MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
            //    }

            //    // Enable second menu entry and show third menu entry
            //    MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
            //    MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            //}
            //else if (Tag == MenuTag5)
            //{
            //    // Display Menu Caption
            //    MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag5, out Caption);
            //    LOUPR00120FORM(sender, e);

            //    // Toggle Menu Check mark on first menu entry
            //    Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
            //    if (Checked >= 0)
            //    {
            //        MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
            //    }

            //    // Enable second menu entry and show third menu entry
            //    MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
            //    MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            //}
            //else if (Tag == MenuTag6)
            //{
            //    // Display Menu Caption
            //    MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag6, out Caption);
            //    LOUPR00170(sender, e);

            //    // Toggle Menu Check mark on first menu entry
            //    Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
            //    if (Checked >= 0)
            //    {
            //        MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
            //    }

            //    // Enable second menu entry and show third menu entry
            //    MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
            //    MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            //}
            //else if (Tag == MenuTag7)
            //{
            //    // Display Menu Caption
            //    MenusForVisualStudioTools.Functions.GetCaption.Invoke(MenuTag7, out Caption);
            //    LOUPR00180FORM(sender, e);

            //    // Toggle Menu Check mark on first menu entry
            //    Checked = MenusForVisualStudioTools.Functions.Checked.Invoke(MenuTag1);
            //    if (Checked >= 0)
            //    {
            //        MenusForVisualStudioTools.Functions.Check.Invoke(MenuTag1, !(Checked == 1));
            //    }

            //    // Enable second menu entry and show third menu entry
            //    MenusForVisualStudioTools.Functions.Disable.Invoke(MenuTag2, false);
            //    MenusForVisualStudioTools.Functions.Hide.Invoke(MenuTag3, false);
            //}
        }

        public void LONCF00401(object sender, EventArgs e)
        {
            try
            {
                if (MantenimientoComprobanteFiscal == null)
                {
                    MantenimientoComprobanteFiscal = new LONCF00401();
                }
                else if (!MantenimientoComprobanteFiscal.Created)
                {
                    MantenimientoComprobanteFiscal = new LONCF00401();
                }
                MantenimientoComprobanteFiscal.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne - Licencia TSS Apec", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LONCF00402(object sender, EventArgs e)
        {
            try
            {
                if (ConfiguracionComprobanteFiscal == null)
                {
                    ConfiguracionComprobanteFiscal = new LONCF00402();
                }
                else if (!MantenimientoComprobanteFiscal.Created)
                {
                    ConfiguracionComprobanteFiscal = new LONCF00402();
                }
                ConfiguracionComprobanteFiscal.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LogicOne - Licencia TSS Apec", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
