using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication5
{
    public partial class Default : Page
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetData();
            }
        }
        protected void btn_gravar_Click(object sender, EventArgs e)
        {
            string cep = txtCEP.Text;
            string result = string.Empty;

            if (cep.Length < 8 || cep.Length > 8)
            {
                lblMensagem.Text = "CEP Inválido";
                return;
            }

            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT Cep FROM CEP WHERE replace(Cep,'-','') = '" + txtCEP.Text + "'", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                lblMensagem.Text = "Registro ja Existe.";
                reader.Dispose();
                return; 
            }
            else
            {
                reader.Dispose();
                string viaCEPUrl = "https://viacep.com.br/ws/" + cep + "/json/";

                WebClient client = new WebClient();
                result = client.DownloadString(viaCEPUrl);

                JObject jsonRetorno = JsonConvert.DeserializeObject<JObject>(result);

                string query = "INSERT INTO [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) VALUES (";
                query = query + "'" + jsonRetorno["cep"] + "'";
                query = query + ",'" + jsonRetorno["logradouro"] + "'";
                query = query + ",'" + jsonRetorno["complemento"] + "'";
                query = query + ",'" + jsonRetorno["bairro"] + "'";
                query = query + ",'" + jsonRetorno["localidade"] + "'";
                query = query + ",'" + jsonRetorno["uf"] + "'";
                query = query + ",'" + jsonRetorno["unidade"] + "'";
                query = query + ",'" + jsonRetorno["ibge"] + "'";
                query = query + ",'" + jsonRetorno["gia"] + "'" + ")";


                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.CommandType = CommandType.Text;

                try
                {
                    // connection.Open();

                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text =ex.GetBaseException().ToString();
                }
                finally
                {
                    connection.Close();
                }

                sqlCommand.Dispose();
                connection.Close();
                connection.Dispose();
            }

            reader.Close();
            reader.Dispose();
            connection.Close();


        }
        protected void btn_pesquisar_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Select * from CEP where uf like '" + txtEstado.Text + "%'", connection);
            SqlDataAdapter sda = new SqlDataAdapter();

            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                sda.SelectCommand = sqlCommand;
                sda.Fill(dt);
                GrdCEP.DataSource = dt;
                GrdCEP.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  {0}", ex.GetBaseException().ToString());
            }
            finally
            {
                connection.Close();
            }

        }
        
        private void GetData()
        {
            SqlCommand cmd = new SqlCommand("SELECT  * FROM CEP", connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            try
            {
                connection.Open();

                DataTable dt = new DataTable();
                sda.Fill(dt);
                GrdCEP.DataSource = dt;
                GrdCEP.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  {0}", ex.GetBaseException().ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        private void VerificaCEP()
        {
            SqlCommand cmd = new SqlCommand("SELECT  * FROM CEP where cep like '" + txtEstado.Text + "%'", connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            try
            {
                connection.Open();

                DataTable dt = new DataTable();
                sda.Fill(dt);
                GrdCEP.DataSource = dt;
                GrdCEP.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  {0}", ex.GetBaseException().ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}