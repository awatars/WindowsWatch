using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public partial class OrderEditForm : Form
    {
        private int? _id = null;

        // ИЗМЕНЕНИЕ: Добавили аргумент watchId в конструктор
        public OrderEditForm(int? id, int? clientId = null, int? watchId = null, DateTime? date = null, string status = "", decimal amount = 0)
        {
            InitializeComponent();
            _id = id;

            // Загружаем списки
            LoadClients();
            LoadWatches(); // Новый метод

            // Установка значений при редактировании
            if (date != null) dtpDate.Value = date.Value;
            if (!string.IsNullOrEmpty(status)) cmbStatus.Text = status;
            numAmount.Minimum = 0;        
            numAmount.Maximum = 10000000;

            if (clientId != null)
            {
                cmbClients.SelectedValue = clientId;
            }

            // ИЗМЕНЕНИЕ: Выбираем часы в списке, если редактируем
            if (watchId != null)
            {
                cmbWatches.SelectedValue = watchId;
            }

            this.Text = (_id == null) ? "Новый заказ" : "Редактирование заказа";
        }

        private void LoadClients()
        {
            string query = "SELECT id, full_name FROM Clients ORDER BY full_name";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            cmbClients.DataSource = dt;
            cmbClients.DisplayMember = "full_name";
            cmbClients.ValueMember = "id";
        }

        // ИЗМЕНЕНИЕ: Новый метод загрузки моделей часов
        private void LoadWatches()
        {
            // Предполагаем, что таблица называется WatchModels и там есть model_name
            string query = "SELECT id, model_name FROM WatchModels ORDER BY model_name";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            cmbWatches.DataSource = dt;
            cmbWatches.DisplayMember = "model_name"; // Показываем название
            cmbWatches.ValueMember = "id";           // Храним ID
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbClients.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }
            // ИЗМЕНЕНИЕ: Проверка на выбор часов
            if (cmbWatches.SelectedValue == null)
            {
                MessageBox.Show("Выберите модель часов!");
                return;
            }

            try
            {
                string query;
                // ИЗМЕНЕНИЕ: Добавил watch_model_id в SQL запросы
                if (_id == null)
                {
                    query = "INSERT INTO Orders (client_id, watch_model_id, order_date, status, total_amount) VALUES (@client, @watch, @date, @status, @amount)";
                }
                else
                {
                    query = "UPDATE Orders SET client_id=@client, watch_model_id=@watch, order_date=@date, status=@status, total_amount=@amount WHERE id=@id";
                }

                NpgsqlParameter[] p = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@client", (int)cmbClients.SelectedValue),
                    new NpgsqlParameter("@watch", (int)cmbWatches.SelectedValue), // Новый параметр
                    new NpgsqlParameter("@date", dtpDate.Value),
                    new NpgsqlParameter("@status", cmbStatus.Text),
                    new NpgsqlParameter("@amount", numAmount.Value)
                };

                if (_id != null)
                {
                    Array.Resize(ref p, p.Length + 1);
                    p[p.Length - 1] = new NpgsqlParameter("@id", _id);
                }

                DatabaseHelper.ExecuteNonQuery(query, p);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}