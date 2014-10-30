using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Dexterity.Applications;
using MicrosoftDynamicsGPLogicOne;

namespace LogicOne_ComprobanteFiscal
{

    public static class Globales
    {
        public static MicrosoftDynamicsGPLogicOne.Conexion xConexion = new MicrosoftDynamicsGPLogicOne.Conexion();
        public static Functions xFunciones = new Functions();
        public static Seguridad xSeguridad = new Seguridad();

        public static object xProject = Assembly.GetExecutingAssembly().GetName().Name;
        public static ADODB.Recordset rsRecords = new ADODB.Recordset();
        public static DataSet dsRecords = new DataSet();

        public static bool ProcessStatus = false;
        public static string glb_AplicationName = "Logicone Licencia TSS";
        public static string glb_DataSource;
        public static string glb_InterCompany;
        public static string glb_CompanyName;
        public static int glb_CompanyID = -1;
        public static string glb_SearchKey;
        public static string glb_UserId;
        public static string glb_UserName;
        public static string glb_Password;
        public static int glb_BotonMouseSelect = 1;
        public static int glb_Interfase = 2;    //-- Local (1), Dynamics GP (2), Dynamics AX (3)
        public static string glb_CrystalReports;
        public static string glb_ReportingServer;
        public static string glb_FileShared;
        public static string glb_DBDefault;
        public static object glb_Project;
        public static string glb_ValueKey;
        public static string glb_ProductKey;
        public static string glb_ProductName;
        public static string glb_ServerName;
        public static string glb_SQLUID;
        public static string glb_SQLPWD;


        public static object SQLQueryExecute(object xProject, string sQuery, int iQueryType = 0)
        {

            //-- Asignando valores predeterminados --

            int Interfase = MicrosoftDynamicsGPLogicOne.Conexion.xInterfase;

            //if (Interfase == 2)
            //{
            MicrosoftDynamicsGPLogicOne.Conexion.InterCompany = Dynamics.Globals.IntercompanyId.Value.Trim();
            MicrosoftDynamicsGPLogicOne.Conexion.CompanyName = Dynamics.Globals.CompanyName.Value.Trim();
            MicrosoftDynamicsGPLogicOne.Conexion.UserId = Dynamics.Globals.UserId.Value.Trim();
            //}


            try
            {
                if (iQueryType == 0)
                {
                    return rsRecords = (ADODB.Recordset)xConexion.SQLExecute(xProject, sQuery, iQueryType);

                }
                else if (iQueryType == 1)
                {
                    return (bool)xConexion.SQLExecute(xProject, sQuery, iQueryType);
                }
                else if (iQueryType == 2)
                {
                    return dsRecords = (DataSet)xConexion.SQLExecute(xProject, sQuery, iQueryType);
                }

                return true;
            }
            catch (Exception ex)
            {
                 //(ex.Message, MsgBoxStyle.Critical, NombreAplicacion)
                return false;
            }
        }

    }
}
