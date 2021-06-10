
namespace BD
{
    partial class FieldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Action_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.col_cb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(168, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Удаление по полю";
            // 
            // Action_button
            // 
            this.Action_button.Enabled = false;
            this.Action_button.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.Action_button.Location = new System.Drawing.Point(360, 216);
            this.Action_button.Name = "Action_button";
            this.Action_button.Size = new System.Drawing.Size(160, 50);
            this.Action_button.TabIndex = 1;
            this.Action_button.Text = "Удалить записи";
            this.Action_button.UseVisualStyleBackColor = true;
            this.Action_button.Click += new System.EventHandler(this.Action_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.button2.Location = new System.Drawing.Point(16, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "На главную";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label2.Location = new System.Drawing.Point(80, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Поле:";
            // 
            // col_cb
            // 
            this.col_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.col_cb.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.col_cb.FormattingEnabled = true;
            this.col_cb.Location = new System.Drawing.Point(152, 88);
            this.col_cb.Name = "col_cb";
            this.col_cb.Size = new System.Drawing.Size(250, 29);
            this.col_cb.TabIndex = 3;
            this.col_cb.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label3.Location = new System.Drawing.Point(40, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Значение:";
            // 
            // value
            // 
            this.value.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.value.Location = new System.Drawing.Point(152, 144);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(250, 29);
            this.value.TabIndex = 5;
            // 
            // FieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 286);
            this.ControlBox = false;
            this.Controls.Add(this.value);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.col_cb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Action_button);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FieldForm";
            this.Text = "FieldDelete";
            this.Load += new System.EventHandler(this.FieldDelete_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Action_button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox col_cb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox value;
    }
}