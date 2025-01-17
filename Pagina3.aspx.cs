﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace EjercicioVideoJuegos
{
    public partial class Pagina3 : System.Web.UI.Page
    {

        protected OdbcConnection conectarBD()
        {
            String stringConexion = "Driver={SQL Server Native Client 11.0};Server=CC102-31\\SA;Uid=sa;Pwd=adminadmin;Database=gameSpot";
            try
            {
                OdbcConnection conexion = new OdbcConnection(stringConexion);
                conexion.Open();
                lbError.Text = "conexion exitosa";
                return conexion;
            }
            catch (Exception ex)
            {
                lbError.Text = ex.StackTrace.ToString();
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OdbcConnection miConexion = conectarBD();
            if (miConexion != null)
            {
                String query = String.Format("select juegos.nombre from juegos, escriben where escriben.claveU={0} and escriben.ClaveJ=juegos.claveJ", Session["claveUnica"].ToString());
                OdbcCommand cmd = new OdbcCommand(query, miConexion);
                OdbcDataReader rd = cmd.ExecuteReader();
                ddJuegos.Items.Clear();
                while (rd.Read())
                {
                    ddJuegos.Items.Add(rd.GetString(0));
                }
                rd.Close();

            }
            }

        protected void ddJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {
            OdbcConnection miConexion = conectarBD();
            if (miConexion != null)
            {
                String query = String.Format("select claveJ from juegos where nombre='{0}'", ddJuegos.SelectedValue);
                OdbcCommand cmd = new OdbcCommand(query, miConexion);
                OdbcDataReader rd = cmd.ExecuteReader();
                rd.Read();
                int claveJuego = rd.GetInt16(0);
                    String query2 = String.Format("select critica.contenido from critica where critica.claveC=(select claveC from escriben where escriben.claveU={0} and escriben.claveJ={1})", Session["ClaveUnica"].ToString(), claveJuego);
                    OdbcCommand cmd2 = new OdbcCommand(query, miConexion);
                    OdbcDataReader rd2 = cmd.ExecuteReader();
                    rd2.Read();
                lbCritica.Text = rd2.GetString(0);
                rd.Close();
                rd2.Close();

                }
                
            }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pagina4.aspx");
        }
    }
}