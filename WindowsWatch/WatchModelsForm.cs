using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public partial class WatchModelsForm : Form
    {
        public WatchModelsForm()
        {
            InitializeComponent();
            this.Text = "Каталог часов";
            this.StartPosition = FormStartPosition.CenterParent;
            LoadData();

            dgvWatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWatches.MultiSelect = false;
            dgvWatches.ReadOnly = true;
            dgvWatches.AllowUserToAddRows = false;
        }

        private void LoadData()
        {
            string query = "SELECT id, model_name, type, price, description, image_path FROM WatchModels ORDER BY id";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvWatches.DataSource = dt;

            if (dgvWatches.Columns.Count > 0)
            {
                dgvWatches.Columns["id"].HeaderText = "ID";
                dgvWatches.Columns["id"].Width = 40;

                dgvWatches.Columns["model_name"].HeaderText = "Модель";
                dgvWatches.Columns["model_name"].Width = 150;

                dgvWatches.Columns["type"].HeaderText = "Тип";

                dgvWatches.Columns["price"].HeaderText = "Цена";
                dgvWatches.Columns["price"].DefaultCellStyle.Format = "C2";

                dgvWatches.Columns["description"].HeaderText = "Описание";
                dgvWatches.Columns["image_path"].HeaderText = "Фото";

                dgvWatches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WatchModelEditForm form = new WatchModelEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvWatches.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvWatches.SelectedRows[0].Cells["id"].Value);
                string name = dgvWatches.SelectedRows[0].Cells["name"].Value.ToString();
                string type = dgvWatches.SelectedRows[0].Cells["type"].Value.ToString();
                decimal price = Convert.ToDecimal(dgvWatches.SelectedRows[0].Cells["price"].Value);
                string desc = dgvWatches.SelectedRows[0].Cells["description"].Value.ToString();
                string img = dgvWatches.SelectedRows[0].Cells["image_path"].Value.ToString();

                WatchModelEditForm form = new WatchModelEditForm(id, name, type, price, desc, img);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
            else MessageBox.Show("Выберите часы!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvWatches.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Удалить модель?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvWatches.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM WatchModels WHERE id = @id";
                    NpgsqlParameter[] p = { new NpgsqlParameter("@id", id) };
                    DatabaseHelper.ExecuteNonQuery(query, p);
                    LoadData();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvWatches.DataSource as DataTable; 

            if (dt != null)
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(searchText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // Ищем только по названию модели
                    dt.DefaultView.RowFilter = $"model_name LIKE '%{searchText}%'";
                }
            }
        }
    }
}
