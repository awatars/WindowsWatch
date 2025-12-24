using Npgsql;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsWatch
{
    public partial class ComponentEditForm : Form
    {
        private int? _id = null;

        public ComponentEditForm(int? id, string category = "", string name = "", decimal cost = 2, string measure = "")
        {
            InitializeComponent();
            _id = id;

            txtCategory.Text = category;
            txtName.Text = name;
            numCost.Value = cost;
            txtMeasure.Text = measure;

            this.Text = (_id == null) ? "Новый компонент" : "Редактирование компонента";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название компонента!");
                return;
            }

            string query;
            if (_id == null)
            {
                query = "INSERT INTO Components (category, name, unit_cost, unit_measure) VALUES (@cat, @name, @cost, @meas)";
            }
            else
            {
                query = "UPDATE Components SET category=@cat, name=@name, unit_cost=@cost, unit_measure=@meas WHERE id=@id";
            }

            NpgsqlParameter[] p = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cat", txtCategory.Text),
                new NpgsqlParameter("@name", txtName.Text),
                new NpgsqlParameter("@cost", numCost.Value),
                new NpgsqlParameter("@meas", txtMeasure.Text)
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
