using System;
using System.Data;
using System.Windows.Forms;
using Npgsql; // Подключаем Postgres

namespace WindowsWatch
{
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();
            this.Text = "База клиентов";
            this.StartPosition = FormStartPosition.CenterParent;
            LoadData();

            // Настройка таблицы
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.MultiSelect = false;
            dgvClients.ReadOnly = true;
            dgvClients.AllowUserToAddRows = false;
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Загрузка данных
        private void LoadData()
        {
            string query = "SELECT id, full_name, inn, address, phone, email FROM Clients ORDER BY id";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvClients.DataSource = dt;

            // Настройка колонок кодом
            if (dgvClients.Columns.Count > 0)
            {
                dgvClients.Columns["id"].HeaderText = "ID";
                dgvClients.Columns["id"].Width = 40;

                dgvClients.Columns["full_name"].HeaderText = "ФИО Клиента";
                dgvClients.Columns["full_name"].Width = 200;

                dgvClients.Columns["inn"].HeaderText = "ИНН";
                dgvClients.Columns["address"].HeaderText = "Адрес";
                dgvClients.Columns["phone"].HeaderText = "Телефон";
                dgvClients.Columns["email"].HeaderText = "Email";

                dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // 1. ДОБАВИТЬ
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClientEditForm editForm = new ClientEditForm(null);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        // 2. РЕДАКТИРОВАТЬ
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                // Получаем данные из выделенной строки
                int id = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["id"].Value);
                string name = dgvClients.SelectedRows[0].Cells["full_name"].Value.ToString();
                string inn = dgvClients.SelectedRows[0].Cells["inn"].Value.ToString();
                string addr = dgvClients.SelectedRows[0].Cells["address"].Value.ToString();
                string phone = dgvClients.SelectedRows[0].Cells["phone"].Value.ToString();
                string email = dgvClients.SelectedRows[0].Cells["email"].Value.ToString();

                // Передаем их в форму редактирования
                ClientEditForm editForm = new ClientEditForm(id, name, inn, addr, phone, email);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента!");
            }
        }

        // 3. УДАЛИТЬ
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Удалить клиента?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["id"].Value);

                    string query = "DELETE FROM Clients WHERE id = @id";
                    NpgsqlParameter[] p = { new NpgsqlParameter("@id", id) };

                    DatabaseHelper.ExecuteNonQuery(query, p);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку!");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvClients.DataSource as DataTable;

            if (dt != null)
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(searchText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // Ищем по Имени ИЛИ по Телефону
                    dt.DefaultView.RowFilter = $"full_name LIKE '%{searchText}%' OR phone LIKE '%{searchText}%'";
                }
            }
        }
    }
}
