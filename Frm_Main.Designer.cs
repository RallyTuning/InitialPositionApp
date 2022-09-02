namespace InitianPositionApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.Pnl_Centrale = new System.Windows.Forms.Panel();
            this.ToolBarSotto = new System.Windows.Forms.ToolStrip();
            this.Btn_Chiudi = new System.Windows.Forms.ToolStripButton();
            this.Btn_Riavvia = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Txt_Restart = new System.Windows.Forms.ToolStripTextBox();
            this.Lbl_Timer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ToolBarSotto.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Centrale
            // 
            this.Pnl_Centrale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Centrale.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Centrale.Name = "Pnl_Centrale";
            this.Pnl_Centrale.Size = new System.Drawing.Size(428, 234);
            this.Pnl_Centrale.TabIndex = 0;
            // 
            // ToolBarSotto
            // 
            this.ToolBarSotto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolBarSotto.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolBarSotto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Chiudi,
            this.Btn_Riavvia,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.Txt_Restart,
            this.Lbl_Timer,
            this.toolStripLabel2});
            this.ToolBarSotto.Location = new System.Drawing.Point(0, 234);
            this.ToolBarSotto.Name = "ToolBarSotto";
            this.ToolBarSotto.ShowItemToolTips = false;
            this.ToolBarSotto.Size = new System.Drawing.Size(428, 25);
            this.ToolBarSotto.TabIndex = 1;
            // 
            // Btn_Chiudi
            // 
            this.Btn_Chiudi.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Chiudi.Image")));
            this.Btn_Chiudi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Chiudi.Name = "Btn_Chiudi";
            this.Btn_Chiudi.Size = new System.Drawing.Size(62, 22);
            this.Btn_Chiudi.Text = "&Chiudi";
            this.Btn_Chiudi.Click += new System.EventHandler(this.Btn_Chiudi_Click);
            // 
            // Btn_Riavvia
            // 
            this.Btn_Riavvia.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Riavvia.Image")));
            this.Btn_Riavvia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Riavvia.Name = "Btn_Riavvia";
            this.Btn_Riavvia.Size = new System.Drawing.Size(64, 22);
            this.Btn_Riavvia.Text = "&Riavvia";
            this.Btn_Riavvia.Click += new System.EventHandler(this.Btn_Riavvia_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "Riavvia ogni";
            // 
            // Txt_Restart
            // 
            this.Txt_Restart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Txt_Restart.Name = "Txt_Restart";
            this.Txt_Restart.Size = new System.Drawing.Size(50, 25);
            this.Txt_Restart.Text = "999";
            this.Txt_Restart.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_Restart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Restart_KeyPress);
            this.Txt_Restart.TextChanged += new System.EventHandler(this.Txt_Restart_TextChanged);
            // 
            // Lbl_Timer
            // 
            this.Lbl_Timer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Lbl_Timer.Name = "Lbl_Timer";
            this.Lbl_Timer.Size = new System.Drawing.Size(49, 22);
            this.Lbl_Timer.Text = "00:00:00";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(28, 22);
            this.toolStripLabel2.Text = "min";
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(428, 259);
            this.Controls.Add(this.Pnl_Centrale);
            this.Controls.Add(this.ToolBarSotto);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Shown += new System.EventHandler(this.Frm_Main_Shown);
            this.ToolBarSotto.ResumeLayout(false);
            this.ToolBarSotto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Centrale;
        private System.Windows.Forms.ToolStrip ToolBarSotto;
        private System.Windows.Forms.ToolStripButton Btn_Chiudi;
        private System.Windows.Forms.ToolStripButton Btn_Riavvia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox Txt_Restart;
        private System.Windows.Forms.ToolStripLabel Lbl_Timer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}

