using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FacadeProperties {
    public class UnfocusedAlpha {

        private UnfocusedAlphaDialog AlphaDialog = new UnfocusedAlphaDialog();

        public bool ShowUnfocusedAlpha() {
            DialogResult result = AlphaDialog.ShowDialog();
            if (result == DialogResult.OK) {
                return true;
            }
            return false;
        }

        #region Public Get UnfocusedAlpha methods

        public string GetUnfocusedAlphaListItems() {
            return AlphaDialog.AlphaListItem.ToString();
        }

        public string GetUnfocusedAlphaThumbs() {
            return AlphaDialog.AlphaThumbs.ToString();
        }

        #endregion

        #region Public Set Colour methods

        public void SetUnfocusedAlphaListItems(string alpha) {
            AlphaDialog.AlphaListItem = int.Parse(alpha);
        }

        public void SetUnfocusedAlphaThumbs(string alpha) {
            AlphaDialog.AlphaThumbs = int.Parse(alpha);
        }

        #endregion


    }
}
