using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FacadeProperties {
    public class Colors {

        private ListColorDialog ColorDialog = new ListColorDialog();

        public bool ShowColors() {
            DialogResult result = ColorDialog.ShowDialog();
            if (result == DialogResult.OK) {
                return true;
            }
            return false;
        }

        #region Public Get Colour methods
        
        public string GetTextColor() {
            return ColorDialog.ColorText;
        }

        public string GetText2Color() {
            return ColorDialog.ColorText2;
        }

        public string GetText3Color() {
            return ColorDialog.ColorText3;
        }

        public string GetWatchedColor() {
            return ColorDialog.ColorWatched;
        }

        public string GetRemoteColor() {
            return ColorDialog.ColorRemote;
        }

        #endregion

        #region Public Set Colour methods

        public void SetTextColor(string col) {
            ColorDialog.ColorText = col;
        }

        public void SetText2Color(string col) {
            ColorDialog.ColorText2 = col;
        }

        public void SetText3Color(string col) {
            ColorDialog.ColorText3 = col;
        }

        public void SetWatchedColor(string col) {
            ColorDialog.ColorWatched = col;
        }

        public void SetRemoteColor(string col) {
            ColorDialog.ColorRemote = col;
        }

        #endregion

    }
}
