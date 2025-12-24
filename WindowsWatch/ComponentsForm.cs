using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public partial class ComponentsForm : Form
    {
        public ComponentsForm()
        {
            InitializeComponent();
            this.Text = "Склад компонентов";
            this.StartPosition = FormStartPosition.CenterParent;
            LoadData();

            dgvComponents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComponents.MultiSelect = false;
            dgvComponents.ReadOnly = true;
            dgvComponents.AllowUserToAddRows = false;
        }

        private void LoadData()
        {
            string query = "SELECT id, category, name, unit_cost, unit_measure FROM Components ORDER BY id";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvComponents.DataSource = dt;

            // Настройка колонок
            if (dgvComponents.Columns.Count > 0)
            {
                dgvComponents.Columns["id"].HeaderText = "ID";
                dgvComponents.Columns["id"].Width = 40;

                dgvComponents.Columns["category"].HeaderText = "Категория";
                dgvComponents.Columns["name"].HeaderText = "Название";
                dgvComponents.Columns["name"].Width = 150;

                dgvComponents.Columns["unit_cost"].HeaderText = "Цена за ед.";
                dgvComponents.Columns["unit_cost"].DefaultCellStyle.Format = "C2"; // Валюта

                dgvComponents.Columns["unit_measure"].HeaderText = "Ед. изм.";

                dgvComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ComponentEditForm form = new ComponentEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvComponents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvComponents.SelectedRows[0].Cells["id"].Value);
                string cat = dgvComponents.SelectedRows[0].Cells["category"].Value.ToString();
                string name = dgvComponents.SelectedRows[0].Cells["name"].Value.ToString();
                decimal cost = Convert.ToDecimal(dgvComponents.SelectedRows[0].Cells["unit_cost"].Value);
                string meas = dgvComponents.SelectedRows[0].Cells["unit_measure"].Value.ToString();

                ComponentEditForm form = new ComponentEditForm(id, cat, name, cost, meas);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
            else MessageBox.Show("Выберите компонент!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvComponents.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Удалить компонент?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvComponents.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM Components WHERE id = @id";
                    NpgsqlParameter[] p = { new NpgsqlParameter("@id", id) };
                    DatabaseHelper.ExecuteNonQuery(query, p);
                    LoadData();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvComponents.DataSource as DataTable;

            if (dt != null)
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(searchText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // Фильтруем по Названию (name) ИЛИ Категории (category) 
                    dt.DefaultView.RowFilter = $"name LIKE '%{searchText}%' OR category LIKE '%{searchText}%'";
                }
            }
        }
    }
}
