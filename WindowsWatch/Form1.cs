using System;
using System.Windows.Forms;

namespace WindowsWatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Магазин Часов - Главное Меню";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // 1. Кнопка КЛИЕНТЫ
        private void btnClients_Click(object sender, EventArgs e)
        {
            // Создаем форму клиентов и показываем её
            ClientsForm form = new ClientsForm();
            form.ShowDialog(); // ShowDialog блокирует главное окно, пока открыто дочернее
        }

        // 2. Кнопка МОДЕЛИ ЧАСОВ
        private void btnWatches_Click(object sender, EventArgs e)
        {
            WatchModelsForm form = new WatchModelsForm();
            form.ShowDialog();
        }

        // 3. Кнопка КОМПОНЕНТЫ
        private void btnComponents_Click(object sender, EventArgs e)
        {
            ComponentsForm form = new ComponentsForm();
            form.ShowDialog();
        }

        // 4. Кнопка ЗАКАЗЫ
        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm();
            form.ShowDialog();
        }
    }
}