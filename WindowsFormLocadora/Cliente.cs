
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormLocadora
{
    public partial class Cliente : Form
    {
        private void Excluir()
        {
            string stringConexao = "Server=localhost; Port=5432; " +
                "User Id=postgres; Password=12345678; DataBase=locadora_car;";

            // objeto de conexao
            NpgsqlConnection con = new NpgsqlConnection(stringConexao);

            DataRowView dr = (DataRowView)dtgridcliente.Rows[dtgridcliente.SelectedRows[0].Index].DataBoundItem;

            int linhaSelecionada = int.Parse(dr["codigo"].ToString());

            // instrucao sql para o banco de dados
            string instrucao = $"delete from cliente where codigo =" + linhaSelecionada;


            NpgsqlCommand cmd = new NpgsqlCommand(instrucao, con);


            con.Open();


            try
            {
                //executar comando
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente Excluido com Sucesso!");
                Close();
            }
            catch (Exception ex)
            {
                // se ocorrer erro da uma msg
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //fecha a conexao
                con.Close(); con.Dispose();
            }
        }

        public Cliente()
        {
            InitializeComponent();

            string stringConexao = "Server=localhost; Port=5432; " +
                                "User Id=postgres; Password=12345678; DataBase=locadora_car;";

            // objeto de conexao
            NpgsqlConnection con = new NpgsqlConnection(stringConexao);

            // instrucao sql para o banco de dados
            string instrucao = "SELECT * FROM Cliente " +
                "order by nome";

            DataTable dt = new DataTable(); // tabela virtual pra armazenar resultado

            NpgsqlCommand cmd = new NpgsqlCommand(instrucao, con); // passa por parametro a instrucao sql

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd); // nunca muda

            da.Fill(dt); // preenche data table com resultado

            // Fecha conexao com o banco
            con.Close();
            con.Dispose();

            dtgridcliente.DataSource = dt; // carrega na lista da tela
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
           
        }

        private void btnvoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            if (dtgridcliente.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha para Excluir.");

            }
            else
            {
                if (MessageBox.Show("Tem certeza que deseja Excluir? ", "Tela de Clientes",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    Excluir();


                    DataGridViewRow selectedRow = dtgridcliente.SelectedRows[0];
                    dtgridcliente.Rows.Remove(selectedRow);
                }
            }
        }

        private void btnalterar_Click(object sender, EventArgs e)
        {
            if (dtgridcliente.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha para Alterar.");
            }
            else
            {

                DataRowView dr = (DataRowView)dtgridcliente.Rows[dtgridcliente.SelectedRows[0].Index].DataBoundItem;

                int linhaSelecionada = int.Parse(dr["codigo"].ToString());

                MessageBox.Show("Codigo do Cliente é: " + linhaSelecionada);

                FormularioCliente form = new FormularioCliente();
                form.CodigoCliente = linhaSelecionada;
                form.ShowDialog();


            }
        }

        private void btnincluir_Click(object sender, EventArgs e)
        {
            FormularioCliente form = new FormularioCliente();
            form.ShowDialog();

        }
    }
}

