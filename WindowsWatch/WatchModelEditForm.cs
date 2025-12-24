using System;
using System.Windows.Forms;
using Npgsql;

namespace WindowsWatch
{
    public partial class WatchModelEditForm : Form
    {
        private int? _id = null;

        public WatchModelEditForm(int? id, string name = "", string type = "", decimal price = 2, string desc = "", string img = "")
        {
            InitializeComponent();
            _id = id;

            txtName.Text = name;
            txtType.Text = type;
            numPrice.Value = price;
            txtDesc.Text = desc;
            txtImage.Text = img;

            this.Text = (_id == null) ? "Новые часы" : "Редактирование часов";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название модели!");
                return;
            }

            string query;
            if (_id == null)
            {
                query = "INSERT INTO WatchModels (name, type, price, description, image_path) VALUES (@name, @type, @price, @desc, @img)";
            }
            else
            {
                query = "UPDATE WatchModels SET name=@name, type=@type, price=@price, description=@desc, image_path=@img WHERE id=@id";
            }

            NpgsqlParameter[] p = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name", txtName.Text),
                new NpgsqlParameter("@type", txtType.Text),
                new NpgsqlParameter("@price", numPrice.Value),
                new NpgsqlParameter("@desc", txtDesc.Text),
                new NpgsqlParameter("@img", txtImage.Text)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
