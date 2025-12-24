namespace WindowsWatch
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClients = new System.Windows.Forms.Button();
            this.btnWatches = new System.Windows.Forms.Button();
            this.btnComponents = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.btnClients.Location = new System.Drawing.Point(12, 12);
            this.btnClients.Name = "button1";
            this.btnClients.Size = new System.Drawing.Size(168, 101);
            this.btnClients.TabIndex = 0;
            this.btnClients.Text = "Клиенты";
            this.btnClients.UseVisualStyleBackColor = true;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // button2
            // 
            this.btnWatches.Location = new System.Drawing.Point(186, 12);
            this.btnWatches.Name = "button2";
            this.btnWatches.Size = new System.Drawing.Size(168, 101);
            this.btnWatches.TabIndex = 1;
            this.btnWatches.Text = "Модели часов";
            this.btnWatches.UseVisualStyleBackColor = true;
            this.btnWatches.Click += new System.EventHandler(this.btnWatches_Click);
            // 
            // button3
            // 
            this.btnComponents.Location = new System.Drawing.Point(12, 141);
            this.btnComponents.Name = "button3";
            this.btnComponents.Size = new System.Drawing.Size(168, 101);
            this.btnComponents.TabIndex = 2;
            this.btnComponents.Text = "Компоненты";
            this.btnComponents.UseVisualStyleBackColor = true;
            this.btnComponents.Click += new System.EventHandler(this.btnComponents_Click);
            // 
            // button4
            // 
            this.btnOrders.Location = new System.Drawing.Point(186, 141);
            this.btnOrders.Name = "button4";
            this.btnOrders.Size = new System.Drawing.Size(168, 101);
            this.btnOrders.TabIndex = 3;
            this.btnOrders.Text = "Заказы";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(378, 255);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnComponents);
            this.Controls.Add(this.btnWatches);
            this.Controls.Add(this.btnClients);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnWatches;
        private System.Windows.Forms.Button btnComponents;
        private System.Windows.Forms.Button btnOrders;
    }
}

