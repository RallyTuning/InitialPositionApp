namespace InitialPositionApp
{
    partial class Frm_Impostazioni
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
            this.Txt_Exe_Path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Apri = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Nud_App_Larghezza = new System.Windows.Forms.NumericUpDown();
            this.Nud_App_Altezza = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Nud_App_Y = new System.Windows.Forms.NumericUpDown();
            this.Nud_App_X = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Nud_Exe_Larghezza = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.Nud_Exe_Altezza = new System.Windows.Forms.NumericUpDown();
            this.Nud_Exe_Y = new System.Windows.Forms.NumericUpDown();
            this.Nud_Exe_X = new System.Windows.Forms.NumericUpDown();
            this.Nud_RestartMin = new System.Windows.Forms.NumericUpDown();
            this.Chk_Restart = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Chk_MostraToolbar = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Cmb_Monitors = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Larghezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Altezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Larghezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Altezza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_RestartMin)).BeginInit();
            this.SuspendLayout();
            // 
            // Txt_Exe_Path
            // 
            this.Txt_Exe_Path.Location = new System.Drawing.Point(12, 27);
            this.Txt_Exe_Path.Name = "Txt_Exe_Path";
            this.Txt_Exe_Path.Size = new System.Drawing.Size(308, 21);
            this.Txt_Exe_Path.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Exe or whatever to run:";
            // 
            // Btn_Apri
            // 
            this.Btn_Apri.Location = new System.Drawing.Point(326, 26);
            this.Btn_Apri.Name = "Btn_Apri";
            this.Btn_Apri.Size = new System.Drawing.Size(46, 23);
            this.Btn_Apri.TabIndex = 1;
            this.Btn_Apri.Text = "...";
            this.Btn_Apri.UseVisualStyleBackColor = true;
            this.Btn_Apri.Click += new System.EventHandler(this.Btn_Apri_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Nud_App_X);
            this.groupBox1.Controls.Add(this.Nud_App_Y);
            this.groupBox1.Controls.Add(this.Nud_App_Altezza);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Nud_App_Larghezza);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 140);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "App";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Nud_Exe_X);
            this.groupBox2.Controls.Add(this.Nud_Exe_Altezza);
            this.groupBox2.Controls.Add(this.Nud_Exe_Y);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.Nud_Exe_Larghezza);
            this.groupBox2.Location = new System.Drawing.Point(207, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 140);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nested exe";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Height:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Nud_App_Larghezza
            // 
            this.Nud_App_Larghezza.Location = new System.Drawing.Point(68, 49);
            this.Nud_App_Larghezza.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_App_Larghezza.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_App_Larghezza.Name = "Nud_App_Larghezza";
            this.Nud_App_Larghezza.Size = new System.Drawing.Size(91, 21);
            this.Nud_App_Larghezza.TabIndex = 1;
            this.Nud_App_Larghezza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nud_App_Altezza
            // 
            this.Nud_App_Altezza.Location = new System.Drawing.Point(68, 22);
            this.Nud_App_Altezza.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_App_Altezza.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_App_Altezza.Name = "Nud_App_Altezza";
            this.Nud_App_Altezza.Size = new System.Drawing.Size(91, 21);
            this.Nud_App_Altezza.TabIndex = 0;
            this.Nud_App_Altezza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "X:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Y:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Nud_App_Y
            // 
            this.Nud_App_Y.Location = new System.Drawing.Point(68, 103);
            this.Nud_App_Y.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_App_Y.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_App_Y.Name = "Nud_App_Y";
            this.Nud_App_Y.Size = new System.Drawing.Size(91, 21);
            this.Nud_App_Y.TabIndex = 3;
            this.Nud_App_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nud_App_X
            // 
            this.Nud_App_X.Location = new System.Drawing.Point(68, 76);
            this.Nud_App_X.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_App_X.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_App_X.Name = "Nud_App_X";
            this.Nud_App_X.Size = new System.Drawing.Size(91, 21);
            this.Nud_App_X.TabIndex = 2;
            this.Nud_App_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Height:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Width:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(7, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "X:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Nud_Exe_Larghezza
            // 
            this.Nud_Exe_Larghezza.Location = new System.Drawing.Point(68, 49);
            this.Nud_Exe_Larghezza.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_Exe_Larghezza.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_Exe_Larghezza.Name = "Nud_Exe_Larghezza";
            this.Nud_Exe_Larghezza.Size = new System.Drawing.Size(91, 21);
            this.Nud_Exe_Larghezza.TabIndex = 1;
            this.Nud_Exe_Larghezza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Y:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Nud_Exe_Altezza
            // 
            this.Nud_Exe_Altezza.Location = new System.Drawing.Point(68, 22);
            this.Nud_Exe_Altezza.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_Exe_Altezza.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_Exe_Altezza.Name = "Nud_Exe_Altezza";
            this.Nud_Exe_Altezza.Size = new System.Drawing.Size(91, 21);
            this.Nud_Exe_Altezza.TabIndex = 0;
            this.Nud_Exe_Altezza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nud_Exe_Y
            // 
            this.Nud_Exe_Y.Location = new System.Drawing.Point(68, 103);
            this.Nud_Exe_Y.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_Exe_Y.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_Exe_Y.Name = "Nud_Exe_Y";
            this.Nud_Exe_Y.Size = new System.Drawing.Size(91, 21);
            this.Nud_Exe_Y.TabIndex = 3;
            this.Nud_Exe_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nud_Exe_X
            // 
            this.Nud_Exe_X.Location = new System.Drawing.Point(68, 76);
            this.Nud_Exe_X.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Nud_Exe_X.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Nud_Exe_X.Name = "Nud_Exe_X";
            this.Nud_Exe_X.Size = new System.Drawing.Size(91, 21);
            this.Nud_Exe_X.TabIndex = 2;
            this.Nud_Exe_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nud_RestartMin
            // 
            this.Nud_RestartMin.Location = new System.Drawing.Point(158, 199);
            this.Nud_RestartMin.Maximum = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            this.Nud_RestartMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_RestartMin.Name = "Nud_RestartMin";
            this.Nud_RestartMin.Size = new System.Drawing.Size(91, 21);
            this.Nud_RestartMin.TabIndex = 0;
            this.Nud_RestartMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Nud_RestartMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Chk_Restart
            // 
            this.Chk_Restart.AutoSize = true;
            this.Chk_Restart.Location = new System.Drawing.Point(12, 200);
            this.Chk_Restart.Name = "Chk_Restart";
            this.Chk_Restart.Size = new System.Drawing.Size(140, 19);
            this.Chk_Restart.TabIndex = 4;
            this.Chk_Restart.Text = "Restart the app every";
            this.Chk_Restart.UseVisualStyleBackColor = true;
            this.Chk_Restart.CheckedChanged += new System.EventHandler(this.Chk_Restart_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(255, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "minutes";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Chk_MostraToolbar
            // 
            this.Chk_MostraToolbar.AutoSize = true;
            this.Chk_MostraToolbar.Location = new System.Drawing.Point(207, 225);
            this.Chk_MostraToolbar.Name = "Chk_MostraToolbar";
            this.Chk_MostraToolbar.Size = new System.Drawing.Size(98, 19);
            this.Chk_MostraToolbar.TabIndex = 5;
            this.Chk_MostraToolbar.Text = "Show toolbar";
            this.Chk_MostraToolbar.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 223);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Monitor:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cmb_Monitors
            // 
            this.Cmb_Monitors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Monitors.FormattingEnabled = true;
            this.Cmb_Monitors.Location = new System.Drawing.Point(73, 223);
            this.Cmb_Monitors.Name = "Cmb_Monitors";
            this.Cmb_Monitors.Size = new System.Drawing.Size(104, 23);
            this.Cmb_Monitors.TabIndex = 6;
            // 
            // Frm_Impostazioni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.Cmb_Monitors);
            this.Controls.Add(this.Chk_MostraToolbar);
            this.Controls.Add(this.Chk_Restart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Nud_RestartMin);
            this.Controls.Add(this.Btn_Apri);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_Exe_Path);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Impostazioni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Impostazioni_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Impostazioni_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Larghezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Altezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_App_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Larghezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Altezza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Exe_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_RestartMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_Exe_Path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Apri;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Nud_App_X;
        private System.Windows.Forms.NumericUpDown Nud_App_Y;
        private System.Windows.Forms.NumericUpDown Nud_App_Altezza;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Nud_App_Larghezza;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Nud_Exe_X;
        private System.Windows.Forms.NumericUpDown Nud_Exe_Altezza;
        private System.Windows.Forms.NumericUpDown Nud_Exe_Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Nud_Exe_Larghezza;
        private System.Windows.Forms.NumericUpDown Nud_RestartMin;
        private System.Windows.Forms.CheckBox Chk_Restart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox Chk_MostraToolbar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox Cmb_Monitors;
    }
}