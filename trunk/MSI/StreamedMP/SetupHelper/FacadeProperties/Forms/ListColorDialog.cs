using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FacadeProperties {
    public partial class ListColorDialog : Form {

        #region Color Properties
        public string ColorText { get; set; }
        public string ColorWatched { get; set; }
        public string ColorRemote { get; set; }
        public string ColorText2 { get; set; }
        public string ColorText3 { get; set; }
        #endregion

        #region Constructor
        public ListColorDialog() {
            InitializeComponent();            
        }
        #endregion        

        #region Colour Dialog Invoke
        private void btnText_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {              
                txtText.ForeColor = colorDialog.Color;
                txtText.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                ColorText = txtText.Text;
            }
        }

        private void btnWatched_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {                
                txtWatched.ForeColor = colorDialog.Color;
                txtWatched.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                ColorWatched = txtWatched.Text;
            }
        }

        private void btnRemote_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {                
                txtRemote.ForeColor = colorDialog.Color;
                txtRemote.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                ColorRemote = txtRemote.Text;
            }
        }

        private void btnText2_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {                
                txtText2.ForeColor = colorDialog.Color;
                txtText2.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                ColorText2 = txtText2.Text;
            }
        }

        private void btnText3_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {               
                txtText3.ForeColor = colorDialog.Color;
                txtText3.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                ColorText3 = txtText3.Text;
            }
        }                
        #endregion

        protected override void OnLoad(EventArgs e) {            
            UpdateColors();
            base.OnLoad(e);
        }

        private Color ColorInvert(Color colorIn) {
            return Color.FromArgb(colorIn.A, Color.White.R - colorIn.R,
                   Color.White.G - colorIn.G, Color.White.B - colorIn.B);
        }

        private Color ColorFromRGB(string RGB)
        {
            if (RGB.Length != 6)
                return System.Drawing.Color.FromArgb(255, 255, 255);

            byte R = ColorTranslator.FromHtml("#" + RGB).R;
            byte G = ColorTranslator.FromHtml("#" + RGB).G;
            byte B = ColorTranslator.FromHtml("#" + RGB).B;

            return System.Drawing.Color.FromArgb(int.Parse(R.ToString()),
                                                 int.Parse(G.ToString()),
                                                 int.Parse(B.ToString()));
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void UpdateColors() {
            // Reflect current colours in controls
            txtText.Text = ColorText;            
            txtText.ForeColor = ColorFromRGB(ColorText);
            
            txtText2.Text = ColorText2;
            txtText2.ForeColor = ColorFromRGB(ColorText2);

            txtText3.Text = ColorText3;
            txtText3.ForeColor = ColorFromRGB(ColorText3);

            txtWatched.Text = ColorWatched;
            txtWatched.ForeColor = ColorFromRGB(ColorWatched);

            txtRemote.Text = ColorRemote;
            txtRemote.ForeColor = ColorFromRGB(ColorRemote);
        }

        private void linkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // reset colours to defaults
            ColorText = "FFFFFF";
            ColorText2 = "FFFFFF";
            ColorText3 = "FFFFFF";
            ColorWatched = "808080";
            ColorRemote = "FFA075";
            UpdateColors();
        }
    }
}
