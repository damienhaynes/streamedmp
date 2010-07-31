using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMPpatch
{
  public partial class UpdateMessage : Form
  {
    public UpdateMessage()
    {
      InitializeComponent();
    }

    public string countDown
    {
      set { lbCountDown.Text = value; }
    }

    public string statusMessage
    {
      set { lbStatusMessage.Text = value; }
    }

  }
}
