﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMPMenuGen;


namespace GenerateTester
{
  public partial class GenerateTester : Form
  {



     public GenerateTester()
    {
      InitializeComponent();

      GenerateMenu generateMenu = new GenerateMenu();
      generateMenu.loadMenuSettings();

    }

    private void btGo_Click(object sender, EventArgs e)
    {

    }
  }
}
