using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FacadeProperties {
    public partial class UnfocusedAlphaDialog : Form {

        #region UnfocusedAlpha Properties
        public int AlphaListItem { get; set; }
        public int AlphaThumbs { get; set; }
        #endregion

        #region Constructor
        public UnfocusedAlphaDialog() {
            InitializeComponent();
        }
        #endregion

        protected override void OnLoad(EventArgs e) {
            UpdateAlpha();
            base.OnLoad(e);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void UpdateAlpha() {
            trackBarListAlpha.Value = AlphaListItem;
            trackBarThumbAlpha.Value = AlphaThumbs;
            lblListItemAlpha.Text = AlphaListItem.ToString();
            lblThumbsAlpha.Text = AlphaThumbs.ToString();    
        }

        private void linkReset_Click(object sender, EventArgs e) {
            AlphaListItem = 100;
            AlphaThumbs = 100;
            UpdateAlpha();
        }

        private void trackBarListAlpha_Scroll(object sender, EventArgs e) {
            AlphaListItem = trackBarListAlpha.Value;
            UpdateAlpha();
        }

        private void trackBarThumbAlpha_Scroll(object sender, EventArgs e) {
            AlphaThumbs = trackBarThumbAlpha.Value;
            UpdateAlpha();
        }
    }
}
