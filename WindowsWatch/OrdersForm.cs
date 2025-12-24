using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            this.Text = "Журнал заказов";
            this.StartPosition = FormStartPosition.CenterParent;

            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;

            LoadData();
        }

        private void LoadData()
        {
            // ИЗМЕНЕНИЕ: Добавили JOIN с таблицей WatchModels (проверь название своей таблицы!)
            // И выбираем w.model_name
            string query = @"
                SELECT o.id, o.client_id, c.full_name, 
                       o.watch_model_id, w.model_name, 
                       o.order_date, o.status, o.total_amount 
                FROM Orders o
                JOIN Clients c ON o.client_id = c.id
                LEFT JOIN WatchModels w ON o.watch_model_id = w.id 
                ORDER BY o.id DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvOrders.DataSource = dt;

            if (dgvOrders.Columns.Count > 0)
            {
                dgvOrders.Columns["id"].HeaderText = "Номер";
                dgvOrders.Columns["id"].Width = 50;

                // Скрываем ID клиента и ID часов
                if (dgvOrders.Columns.Contains("client_id")) dgvOrders.Columns["client_id"].Visible = false;
                if (dgvOrders.Columns.Contains("watch_model_id")) dgvOrders.Columns["watch_model_id"].Visible = false;

                dgvOrders.Columns["full_name"].HeaderText = "Клиент";
                dgvOrders.Columns["full_name"].Width = 150;

                // ИЗМЕНЕНИЕ: Настраиваем колонку с названием часов
                if (dgvOrders.Columns.Contains("model_name"))
                {
                    dgvOrders.Columns["model_name"].HeaderText = "Модель часов";
                    dgvOrders.Columns["model_name"].Width = 150;
                }

                dgvOrders.Columns["order_date"].HeaderText = "Дата";
                dgvOrders.Columns["status"].HeaderText = "Статус";

                dgvOrders.Columns["total_amount"].HeaderText = "Сумма";
                dgvOrders.Columns["total_amount"].DefaultCellStyle.Format = "C2";

                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Передаем null, так как это новый заказ
            OrderEditForm form = new OrderEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["id"].Value);
                int clientId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["client_id"].Value);

                // ИЗМЕНЕНИЕ: Достаем ID часов. Проверка на DBNull, если вдруг часов нет
                int? watchId = null;
                if (dgvOrders.SelectedRows[0].Cells["watch_model_id"].Value != DBNull.Value)
                {
                    watchId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["watch_model_id"].Value);
                }

                DateTime date = Convert.ToDateTime(dgvOrders.SelectedRows[0].Cells["order_date"].Value);
                string status = dgvOrders.SelectedRows[0].Cells["status"].Value.ToString();
                decimal amount = Convert.ToDecimal(dgvOrders.SelectedRows[0].Cells["total_amount"].Value);

                // Передаем watchId в форму редактирования
                OrderEditForm form = new OrderEditForm(id, clientId, watchId, date, status, amount);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
            else MessageBox.Show("Выберите заказ!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Удалить заказ?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM Orders WHERE id = @id";
                    NpgsqlParameter[] p = { new NpgsqlParameter("@id", id) };

                    DatabaseHelper.ExecuteNonQuery(query, p);
                    LoadData();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvOrders.DataSource as DataTable;

            if (dt != null)
            {
                // 2. Чистим текст от кавычек (чтобы программа не вылетала, если ввести ' )
                string searchText = txtSearch.Text.Trim().Replace("'", "''");

                // 3. Если поле пустое — сбрасываем фильтр (показываем всё)
                if (string.IsNullOrEmpty(searchText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // Ищем совпадение в Имени Клиента (full_name) ИЛИ в Модели Часов (model_name)
                    dt.DefaultView.RowFilter = $"full_name LIKE '%{searchText}%' OR model_name LIKE '%{searchText}%'";
                }
            }
        }
    }
}