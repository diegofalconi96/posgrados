using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace POSG_V1
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Puedes añadir inicializaciones adicionales aquí si es necesario
            }
        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            string connectionString = "Data source=DESKTOP-B7G293A; Initial Catalog=Titulacion; integrated security=SSPI";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT strId_per, strNomb_per, strApellidom_per, strApellidop_per, strRol_per FROM POSG_PERSONA WHERE strEmail_per = @usuario AND strPass_per = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@usuario", txtusuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        string userId = dr["strId_per"].ToString();
                        string nombreCompleto = $"{dr["strNomb_per"]} {dr["strApellidop_per"]} {dr["strApellidom_per"]}";
                        string rol = dr["strRol_per"].ToString();

                        Session["UserId"] = userId;
                        Session["UserNombreCompleto"] = nombreCompleto;

                        if (rol == "1")
                        {
                            Response.Redirect("Pages/Notas.aspx");
                        }
                        else if (rol == "2")
                        {
                            Response.Redirect("Default.aspx");
                        }
                    }
                    else
                    {
                        lblError.Text = "Credenciales incorrectas";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}
