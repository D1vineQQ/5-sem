﻿namespace WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            SumButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(70, 111);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(203, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(70, 206);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(233, 23);
            textBox3.TabIndex = 2;
            // 
            // SumButton
            // 
            SumButton.Location = new Point(135, 62);
            SumButton.Name = "SumButton";
            SumButton.Size = new Size(100, 23);
            SumButton.TabIndex = 3;
            SumButton.Text = "Сложить";
            SumButton.UseVisualStyleBackColor = true;
            SumButton.Click += SumButton_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(102, 188);
            label1.Name = "label1";
            label1.Size = new Size(163, 15);
            label1.TabIndex = 4;
            label1.Text = "Результат запроса с сервера";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HotTrack;
            ClientSize = new Size(381, 289);
            Controls.Add(label1);
            Controls.Add(SumButton);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button SumButton;
        private Label label1;
    }
}
