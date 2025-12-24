using System;
using System.Windows.Forms;
using Npgsql; // Обязательно для работы с базой

namespace WindowsWatch
{
    public partial class ClientEditForm : Form
    {
        // Переменная для хранения ID. Если null — значит добавляем нового.
        private int? _id = null;

        // Конструктор формы. Принимает данные, если мы редактируем.
        public ClientEditForm(int? id, string name = "", string inn = "", string addr = "", string phone = "", string email = "")
        {
            InitializeComponent();
            _id = id;

            // Заполняем поля значениями (если они есть)
            txtName.Text = name;
            txtInn.Text = inn;
            txtAddress.Text = addr;
            txtPhone.Text = phone;
            txtEmail.Text = email;

            // Меняем заголовок окна для красоты
            this.Text = (_id == null) ? "Добавление клиента" : "Редактирование клиента";
        }

        // Кнопка СОХРАНИТЬ
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Простая проверка, чтобы имя не было пустым
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите ФИО клиента!");
                return;
            }

            string query;
            if (_id == null)
            {
                // Если ID нет — это INSERT (Вставка нового)
                query = "INSERT INTO Clients (full_name, inn, address, phone, email) VALUES (@name, @inn, @addr, @phone, @email)";
            }
            else
            {
                // Если ID есть — это UPDATE (Обновление старого)
                query = "UPDATE Clients SET full_name=@name, inn=@inn, address=@addr, phone=@phone, email=@email WHERE id=@id";
            }

            // Упаковываем данные в параметры (защита от взлома и ошибок)
            NpgsqlParameter[] p = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name", txtName.Text),
                new NpgsqlParameter("@inn", txtInn.Text),
                new NpgsqlParameter("@addr", txtAddress.Text),
                new NpgsqlParameter("@phone", txtPhone.Text),
                new NpgsqlParameter("@email", txtEmail.Text)
            };

            // Если это редактирование, добавляем еще и ID в параметры
            if (_id != null)
            {
                Array.Resize(ref p, p.Length + 1);
                p[p.Length - 1] = new NpgsqlParameter("@id", _id);
            }

            // Отправляем запрос в базу
            DatabaseHelper.ExecuteNonQuery(query, p);

            // Закрываем форму с успешным результатом
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Кнопка ОТМЕНА
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
