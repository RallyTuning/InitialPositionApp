using System;
using System.Windows.Forms;

namespace InitialPositionApp
{
    public partial class Frm_Impostazioni : Form
    {
        public Frm_Impostazioni()
        {
            InitializeComponent();
        }

        private void Frm_Impostazioni_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in Screen.AllScreens)
                {
                    Cmb_Monitors.Items.Add(item.DeviceName);
                }

                Txt_Exe_Path.Text = Funzioni.AppPath;

                Nud_App_Altezza.Value = Funzioni.FormAltezza;
                Nud_App_Larghezza.Value = Funzioni.FormLarghezza;
                Nud_App_X.Value = Funzioni.FormPosX;
                Nud_App_Y.Value = Funzioni.FormPosY;

                Nud_Exe_Altezza.Value = Funzioni.ExeAltezza;
                Nud_Exe_Larghezza.Value = Funzioni.ExeLarghezza;
                Nud_Exe_X.Value = Funzioni.ExePosX;
                Nud_Exe_Y.Value = Funzioni.ExePosY;

                Chk_MostraToolbar.Checked = Funzioni.MostraToolBar;

                Cmb_Monitors.SelectedIndex = Funzioni.Monitor;

                if (Funzioni.RestartTimerMin > 0)
                {
                    Nud_RestartMin.Minimum = 1;
                    Nud_RestartMin.Value = Funzioni.RestartTimerMin;
                    Nud_RestartMin.Enabled = true;
                    Chk_Restart.Checked = true;
                }
                else
                {
                    Nud_RestartMin.Minimum = 0;
                    Nud_RestartMin.Value = 0;
                    Nud_RestartMin.Enabled = false;
                    Chk_Restart.Checked = false;
                }

                Chk_RestartSeFreezed.Checked = Funzioni.RestartSeFreezed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Apri_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog OFD = new OpenFileDialog())
                {
                    OFD.Title = "Select the file";
                    OFD.Filter = "Any|*.*";
                    OFD.InitialDirectory = @"C:\";
                    OFD.Multiselect = false;
                    if (OFD.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(OFD.FileName) && System.IO.File.Exists(OFD.FileName))
                        {
                            Txt_Exe_Path.Text = OFD.FileName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_Impostazioni_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bool DaSalvare = false;

                switch (true)
                {
                    case true when Txt_Exe_Path.Text != Funzioni.AppPath:
                    case true when Nud_App_Altezza.Value != Funzioni.FormAltezza:
                    case true when Nud_App_Larghezza.Value != Funzioni.FormLarghezza:
                    case true when Nud_App_X.Value != Funzioni.FormPosX:
                    case true when Nud_App_Y.Value != Funzioni.FormPosY:
                    case true when Nud_Exe_Altezza.Value != Funzioni.ExeAltezza:
                    case true when Nud_Exe_Larghezza.Value != Funzioni.ExeLarghezza:
                    case true when Nud_Exe_X.Value != Funzioni.ExePosX:
                    case true when Nud_Exe_Y.Value != Funzioni.ExePosY:
                    case true when Chk_MostraToolbar.Checked != Funzioni.MostraToolBar:
                    case true when Nud_RestartMin.Value != Funzioni.RestartTimerMin:
                    case true when Chk_RestartSeFreezed.Checked != Funzioni.RestartSeFreezed:
                        DaSalvare = true;
                        break;
                    default:
                        break;
                }

                if (DaSalvare && MessageBox.Show("Save changes?", "InitialPositionApp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!string.IsNullOrWhiteSpace(Txt_Exe_Path.Text) && System.IO.File.Exists(Txt_Exe_Path.Text))
                    {
                        Funzioni.AppPath = Txt_Exe_Path.Text;
                    }
                    else
                    {
                        MessageBox.Show("Invalid file!", "InitialPositionApp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                    }

                    Funzioni.FormAltezza = (int)Nud_App_Altezza.Value;
                    Funzioni.FormLarghezza = (int)Nud_App_Larghezza.Value;
                    Funzioni.FormPosX = (int)Nud_App_X.Value;
                    Funzioni.FormPosY = (int)Nud_App_Y.Value;

                    Funzioni.ExeAltezza = (int)Nud_Exe_Altezza.Value;
                    Funzioni.ExeLarghezza = (int)Nud_Exe_Larghezza.Value;
                    Funzioni.ExePosX = (int)Nud_Exe_X.Value;
                    Funzioni.ExePosY = (int)Nud_Exe_Y.Value;

                    Funzioni.RestartTimerMin = (int)Nud_RestartMin.Value;

                    Funzioni.MostraToolBar = Chk_MostraToolbar.Checked;

                    Funzioni.RestartSeFreezed = Chk_RestartSeFreezed.Checked;

                    Funzioni.LeggiImpostazioni(true);

                    if (Funzioni.HWnd != IntPtr.Zero)
                        Funzioni.MoveWindow(Funzioni.HWnd, Funzioni.ExePosX, Funzioni.ExePosY, Funzioni.ExeLarghezza, Funzioni.ExeAltezza - Funzioni.AltezzaToolBar, true);

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Chk_Restart_CheckedChanged(object sender, EventArgs e)
        {
            Nud_RestartMin.Enabled = Chk_Restart.Checked;

            if (Chk_Restart.Checked)
            {
                Nud_RestartMin.Minimum = 1;
                if (Funzioni.RestartTimerMin > 0)
                    Nud_RestartMin.Value = Funzioni.RestartTimerMin;
            }
            else
            {
                Nud_RestartMin.Minimum = 0;
                Nud_RestartMin.Value = 0;
            }
        }
    }
}
