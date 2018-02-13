using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBController
{
    public partial class logo : Form
    {
        public logo()
        {
            InitializeComponent();
        }

        private void logo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue; 
            this.TransparencyKey = this.BackColor;
        }
    }
}
