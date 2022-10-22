namespace InitialPositionApp
{
    partial class Frm_Main
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pnl_Centrale = new System.Windows.Forms.Panel();
            this.ToolBarSotto = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.Btn_Lbl_Versione = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_About = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Impostazioni = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Esci = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Riavvia = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Txt_Restart = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Lbl_Timer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.Cmb_RestartIfFreezed = new System.Windows.Forms.ToolStripComboBox();
            this.Lbl_Stato = new System.Windows.Forms.ToolStripLabel();
            this.ToolBarSotto.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Centrale
            // 
            this.Pnl_Centrale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Centrale.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Centrale.Name = "Pnl_Centrale";
            this.Pnl_Centrale.Size = new System.Drawing.Size(650, 375);
            this.Pnl_Centrale.TabIndex = 0;
            // 
            // ToolBarSotto
            // 
            this.ToolBarSotto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolBarSotto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.Btn_Riavvia,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.Txt_Restart,
            this.toolStripLabel2,
            this.Lbl_Timer,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.Cmb_RestartIfFreezed,
            this.Lbl_Stato});
            this.ToolBarSotto.Location = new System.Drawing.Point(0, 375);
            this.ToolBarSotto.Name = "ToolBarSotto";
            this.ToolBarSotto.ShowItemToolTips = false;
            this.ToolBarSotto.Size = new System.Drawing.Size(650, 25);
            this.ToolBarSotto.TabIndex = 1;
            this.ToolBarSotto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToolBarSotto_MouseMove);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Lbl_Versione,
            this.Btn_About,
            this.Btn_Impostazioni,
            this.toolStripSeparator3,
            this.Btn_Esci});
            this.toolStripDropDownButton1.Image = global::InitialPositionApp.Properties.Resources.monitor;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripDropDownButton1.Text = "&Menu";
            // 
            // Btn_Lbl_Versione
            // 
            this.Btn_Lbl_Versione.Enabled = false;
            this.Btn_Lbl_Versione.Name = "Btn_Lbl_Versione";
            this.Btn_Lbl_Versione.Size = new System.Drawing.Size(124, 22);
            this.Btn_Lbl_Versione.Text = "Version: 0";
            // 
            // Btn_About
            // 
            this.Btn_About.Image = global::InitialPositionApp.Properties.Resources.information;
            this.Btn_About.Name = "Btn_About";
            this.Btn_About.Size = new System.Drawing.Size(124, 22);
            this.Btn_About.Text = "&About";
            this.Btn_About.Click += new System.EventHandler(this.Btn_About_Click);
            // 
            // Btn_Impostazioni
            // 
            this.Btn_Impostazioni.Image = global::InitialPositionApp.Properties.Resources.setting_tools;
            this.Btn_Impostazioni.Name = "Btn_Impostazioni";
            this.Btn_Impostazioni.Size = new System.Drawing.Size(124, 22);
            this.Btn_Impostazioni.Text = "&Settings";
            this.Btn_Impostazioni.Click += new System.EventHandler(this.Btn_Impostazioni_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // Btn_Esci
            // 
            this.Btn_Esci.Image = global::InitialPositionApp.Properties.Resources.door_in;
            this.Btn_Esci.Name = "Btn_Esci";
            this.Btn_Esci.Size = new System.Drawing.Size(124, 22);
            this.Btn_Esci.Text = "&Quit";
            this.Btn_Esci.Click += new System.EventHandler(this.Btn_Esci_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Btn_Riavvia
            // 
            this.Btn_Riavvia.Image = global::InitialPositionApp.Properties.Resources.arrow_refresh;
            this.Btn_Riavvia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Riavvia.Name = "Btn_Riavvia";
            this.Btn_Riavvia.Size = new System.Drawing.Size(89, 22);
            this.Btn_Riavvia.Text = "&Restart now";
            this.Btn_Riavvia.Click += new System.EventHandler(this.Btn_Riavvia_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel1.Text = "Restart every";
            // 
            // Txt_Restart
            // 
            this.Txt_Restart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Restart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Txt_Restart.Name = "Txt_Restart";
            this.Txt_Restart.Size = new System.Drawing.Size(50, 25);
            this.Txt_Restart.Text = "999";
            this.Txt_Restart.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_Restart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Restart_KeyPress);
            this.Txt_Restart.TextChanged += new System.EventHandler(this.Txt_Restart_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabel2.Text = "minutes";
            // 
            // Lbl_Timer
            // 
            this.Lbl_Timer.Name = "Lbl_Timer";
            this.Lbl_Timer.Size = new System.Drawing.Size(49, 22);
            this.Lbl_Timer.Text = "00:00:00";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel3.Text = "Restart if freezed:";
            // 
            // Cmb_RestartIfFreezed
            // 
            this.Cmb_RestartIfFreezed.AutoSize = false;
            this.Cmb_RestartIfFreezed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_RestartIfFreezed.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.Cmb_RestartIfFreezed.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.Cmb_RestartIfFreezed.Name = "Cmb_RestartIfFreezed";
            this.Cmb_RestartIfFreezed.Size = new System.Drawing.Size(53, 23);
            this.Cmb_RestartIfFreezed.SelectedIndexChanged += new System.EventHandler(this.Cmb_RestartIfFreezed_SelectedIndexChanged);
            // 
            // Lbl_Stato
            // 
            this.Lbl_Stato.Name = "Lbl_Stato";
            this.Lbl_Stato.Size = new System.Drawing.Size(57, 22);
            this.Lbl_Stato.Text = "Waiting...";
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.Pnl_Centrale);
            this.Controls.Add(this.ToolBarSotto);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
            this.Shown += new System.EventHandler(this.Frm_Main_Shown);
            this.ToolBarSotto.ResumeLayout(false);
            this.ToolBarSotto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Centrale;
        private System.Windows.Forms.ToolStrip ToolBarSotto;
        private System.Windows.Forms.ToolStripButton Btn_Riavvia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox Txt_Restart;
        private System.Windows.Forms.ToolStripLabel Lbl_Timer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Btn_About;
        private System.Windows.Forms.ToolStripMenuItem Btn_Impostazioni;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem Btn_Esci;
        private System.Windows.Forms.ToolStripMenuItem Btn_Lbl_Versione;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox Cmb_RestartIfFreezed;
        private System.Windows.Forms.ToolStripLabel Lbl_Stato;
    }
}

