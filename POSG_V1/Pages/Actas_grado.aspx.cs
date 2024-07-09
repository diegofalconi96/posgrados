using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace POSG_V1.Pages
{
    public partial class Actas_grado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarDropDownPeriodosAcademicos();
                LlenarDropDownNombreMaestria();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string periodoAcademico = ddlPeriodoAcademico.SelectedValue;
            string nombreMaestria = ddlNombreMaestria.SelectedValue;
            bool? presentaDocumentacion = string.IsNullOrEmpty(ddlPresentaDocumentacion.SelectedValue) ? (bool?)null : Convert.ToBoolean(ddlPresentaDocumentacion.SelectedValue);
            if (string.IsNullOrEmpty(periodoAcademico) || string.IsNullOrEmpty(nombreMaestria))
            {
                return;
            }
            CargarDatos(periodoAcademico, nombreMaestria, presentaDocumentacion);
        }

        private void LlenarDropDownPeriodosAcademicos()
        {
            string connectionString = "data source=.; database=Titulacion; integrated security=SSPI";
            string query = "SELECT DISTINCT strPeriodoAcademico_act FROM POSG_ACTAS_GRADOS";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlPeriodoAcademico.DataSource = reader;
                    ddlPeriodoAcademico.DataTextField = "strPeriodoAcademico_act";
                    ddlPeriodoAcademico.DataValueField = "strPeriodoAcademico_act";
                    ddlPeriodoAcademico.DataBind();

                    ddlPeriodoAcademico.Items.Insert(0, new ListItem("Seleccionar Período Académico", ""));
                    reader.Close();
                }
            }
        }

        private void LlenarDropDownNombreMaestria()
        {
            string connectionString = "data source=.; database=Titulacion; integrated security=SSPI";
            string query = "SELECT DISTINCT strNombreMaestria_ins FROM POSG_INSCRIPCION";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlNombreMaestria.DataSource = reader;
                    ddlNombreMaestria.DataTextField = "strNombreMaestria_ins";
                    ddlNombreMaestria.DataValueField = "strNombreMaestria_ins";
                    ddlNombreMaestria.DataBind();

                    ddlNombreMaestria.Items.Insert(0, new ListItem("Seleccionar Nombre de Maestría", ""));
                    reader.Close();
                }
            }
        }

        private void CargarDatos(string periodoAcademico, string nombreMaestria, bool? presentaDocumentacion)
        {
            string connectionString = "data source=.; database=Titulacion; integrated security=SSPI";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("POSG_GetActas", sqlConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (string.IsNullOrEmpty(periodoAcademico) && string.IsNullOrEmpty(nombreMaestria) && !presentaDocumentacion.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Comodin", "ALL");
                        cmd.Parameters.AddWithValue("@FILTRO1", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FILTRO2", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FILTRO3", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Comodin", "byPeriodo");
                        cmd.Parameters.AddWithValue("@FILTRO1", string.IsNullOrEmpty(periodoAcademico) ? DBNull.Value : (object)periodoAcademico);
                        cmd.Parameters.AddWithValue("@FILTRO2", string.IsNullOrEmpty(nombreMaestria) ? DBNull.Value : (object)nombreMaestria);
                        cmd.Parameters.AddWithValue("@FILTRO3", !presentaDocumentacion.HasValue ? DBNull.Value : (object)presentaDocumentacion.Value);
                    }

                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader sqlReader = cmd.ExecuteReader();
                        tablaActa.DataSource = sqlReader;
                        tablaActa.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (sqlConnection.State == System.Data.ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }
    }
}