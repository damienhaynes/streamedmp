using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StreamedMPEditor
{
    public partial class SubItemProperties : Form
    {

        public string baseName = null;
        public int initialIndex = -1;
        public string currentSkinID = string.Empty;

        public SubItemProperties(bool showHyperlinkParameterDialog, string skinFileID)
        {
            InitializeComponent();
            gbHyperlinkParameter.Enabled = false;
            currentSkinID = skinFileID;

            if (showHyperlinkParameterDialog)
                gbHyperlinkParameter.Enabled = true;

            switch (skinFileID)
            {
              case formStreamedMpEditor.tvseriesSkinID:
                foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
                {
                  cboViews.Items.Add(tvv.Value);
                }
                break;
              case formStreamedMpEditor.musicSkinID:
                foreach (KeyValuePair<string, string> mvv in formStreamedMpEditor.musicViews)
                {
                  cboViews.Items.Add(mvv.Value);
                }
                break;
            }
        }

        public string BaseName
        {
            set { baseName = value; }
        }

        public int InitialIndex
        {
            set { initialIndex = value; }
        }

        public string DisplayName
        {
            get
            {
                return tbItemDisplayName.Text;
            }
            set
            {
                tbItemDisplayName.Text = value;
            }
        }

        public string HypelinkParameterName
        {
            get
            {
                return cboViews.Text;
            }
            set
            {
                cboViews.Text = value;
            }
        }

        public string tvseriesHypelinkParameter
        {
            get
            {
                if (formStreamedMpEditor.tvseriesViews.Count == 0)
                    return cboViews.Text;

                foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
                {
                    if (tvv.Value == cboViews.Text)
                        return tvv.Key;
                }
                return "false";
            }

            set
            {
                if (formStreamedMpEditor.tvseriesViews.Count == 0)
                    cboViews.Text = value;
                int i = 0;
                foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
                {
                    if (value == tvv.Key)
                    {
                        cboViews.Text = tvv.Value;
                        initialIndex = i;
                        break;
                    }
                    i++;
                }
            }
        }

        public string musicHypelinkParameter
        {
          get
          {
            if (formStreamedMpEditor.musicViews.Count == 0)
              return cboViews.Text;

            foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.musicViews)
            {
              if (tvv.Value == cboViews.Text)
                return tvv.Key;
            }
            return "false";
          }

          set
          {
            if (formStreamedMpEditor.musicViews.Count == 0)
              cboViews.Text = value;
            int i = 0;
            foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.musicViews)
            {
              if (value == tvv.Key)
              {
                cboViews.Text = tvv.Value;
                initialIndex = i;
                break;
              }
              i++;
            }
          }
        }

        private void tbItemDisplayName_TextChanged(object sender, EventArgs e)
        {
            int start = tbItemDisplayName.SelectionStart;
            if (isIlegalXML(tbItemDisplayName.Text))
            {
                tbItemDisplayName.Text = tbItemDisplayName.Text.Substring(0, tbItemDisplayName.Text.Length - 1);
                tbItemDisplayName.SelectionStart = start;
                return;
            }
            tbItemDisplayName.Text = tbItemDisplayName.Text.ToUpper();
            tbItemDisplayName.SelectionStart = start;
        }

        bool isIlegalXML(string theValue)
        {
            Match m = formStreamedMpEditor.isIleagalXML.Match(theValue);
            return m.Success;
        }

        private void tbItemDisplayName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Hide();

            if (e.KeyCode == Keys.Escape)
            {
                tbItemDisplayName.Text = string.Empty;
                this.Hide();
            }
        }

        private void btSaveAndClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btClearParameter_Click(object sender, EventArgs e)
        {
            cboViews.Text = string.Empty;
        }


        private void cboViews_SelectedIndexChanged(object sender, EventArgs e)
        {
          if (initialIndex != -1 && (tbItemDisplayName.Text == baseName || initialIndex != cboViews.SelectedIndex))
          {
            if (currentSkinID == formStreamedMpEditor.tvseriesSkinID)
              tbItemDisplayName.Text = formStreamedMpEditor.tvseriesViews[cboViews.SelectedIndex].Value;


            if (currentSkinID == formStreamedMpEditor.musicSkinID)
              tbItemDisplayName.Text = formStreamedMpEditor.musicViews[cboViews.SelectedIndex].Value;

            initialIndex = cboViews.SelectedIndex;
          }
        }
    }
}
