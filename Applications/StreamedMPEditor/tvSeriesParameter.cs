using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StreamedMPEditor
{
    public partial class tvSeriesParameter : Form
    {
        public tvSeriesParameter()
        {
            InitializeComponent();

            foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
            {
                cboTVSViews.Items.Add(tvv.Key);
            }
        }


        public string SelectedView
        {
            get { return cboTVSViews.Text; }
            set { cboTVSViews.Text = value; }
        }

        public string HyperlinkParameter
        {
            get { return formStreamedMpEditor.tvseriesViews[cboTVSViews.SelectedIndex].Key; }
        }

        public string DisplayName
        {
            get 
            {
                if (cboTVSViews.SelectedIndex != -1)
                    return formStreamedMpEditor.tvseriesViews[cboTVSViews.SelectedIndex].Value;
                else
                    return "false";
            }
        }

        private void SaveAndClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
